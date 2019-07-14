﻿Imports System.IO
Imports System.Management
Imports System.Net
Imports System.Net.NetworkInformation
Imports System.Net.Sockets
Imports System.Reflection
Imports System.Text.RegularExpressions
Imports System.Threading
Imports Microsoft.VisualBasic.ApplicationServices
Imports Microsoft.Win32


Friend Class Generic
    Private Declare Function GetDiskFreeSpaceEx Lib "kernel32" Alias "GetDiskFreeSpaceExA"(lpDirectoryName As String,
                                                                                           ByRef _
                                                                                              lpFreeBytesAvailableToCaller _
                                                                                              As Long,
                                                                                           ByRef lpTotalNumberOfBytes As _
                                                                                              Long,
                                                                                           ByRef _
                                                                                              lpTotalNumberOfFreeBytes _
                                                                                              As Long) As Long
    Public Shared DebugMe As Boolean

    Friend Shared Sub CheckUpgrade()
        Dim CurVer As Integer = Assembly.GetExecutingAssembly.GetName.Version.Major*100
        CurVer += Assembly.GetExecutingAssembly.GetName.Version.Minor*10
        CurVer += Assembly.GetExecutingAssembly.GetName.Version.Revision
        Dim OldVer As Integer = Q.settings.Upgradev
        If CurVer <= OldVer Then Exit Sub
        Do
            Select Case OldVer
                Case 11 'upgrade from 11 to 12
                    'check for old settings file. if there is one make the settings.
                    'if there is we use maria and local java
                    Try
                        'only execute this if there is a settings.ini.
                        If File.Exists(QGlobal.BaseDir & "\Settings.ini") Then
                            Dim lines() As String = File.ReadAllLines(QGlobal.BaseDir & "\Settings.ini")
                            Q.settings.FirstRun = False
                            'CheckForUpdates, True, 3
                            Dim cell() As String
                            For Each line As String In lines
                                If line <> "" Then
                                    cell = Split(line, ",")
                                    Select Case cell(0)
                                        Case "CheckForUpdates"
                                            If cell(1) = "True" Then
                                                Q.settings.CheckForUpdates = True
                                            Else
                                                Q.settings.CheckForUpdates = False
                                            End If
                                    End Select
                                    Q.settings.DbName = "burstwallet"
                                    Q.settings.DbUser = "burstwallet"
                                    Q.settings.DbPass = "burstwallet"
                                    Q.settings.DbServer = "localhost:3306"
                                    Q.settings.DbType = QGlobal.DbType.pMariaDB
                                    Q.settings.JavaType = QGlobal.AppNames.JavaPortable
                                End If
                            Next
                            File.Delete(QGlobal.BaseDir & "\Settings.ini")
                            Q.settings.SaveSettings()
                        End If


                    Catch ex As Exception
                        WriteDebug(ex)
                    End Try

                Case 12 'from 12-13
                    Q.settings.UseOnlineWallet = Q.settings.CheckForUpdates
                Case 13 ' from 13-14
                    Q.settings.GetCoinMarket = Q.settings.CheckForUpdates
                    Q.settings.NTPCheck = Q.settings.CheckForUpdates
                Case 14 ' from 14-15
                Case 15 ' from 15-16
                Case 16 ' from 16-17
                    Try
                        If File.Exists(QGlobal.BaseDir & "\Acconts.xml") Then
                            File.Move(QGlobal.BaseDir & "\Acconts.xml", QGlobal.BaseDir & "\Accounts.xml")
                            Q.Accounts.LoadAccounts()
                        End If
                    Catch ex As Exception
                        WriteDebug(ex)
                    End Try

                    Select Case Q.settings.DbType
                        Case QGlobal.DbType.FireBird
                            Q.settings.DbServer = "jdbc:firebirdsql:embedded:./burst_db/burst.firebird.db"
                            Q.settings.DbUser = "sa"
                            Q.settings.DbPass = "sa"
                        Case QGlobal.DbType.pMariaDB
                            Q.settings.DbServer = "jdbc:mariadb://localhost:3306/burstwallet"
                            Q.settings.DbUser = "burstwallet"
                            Q.settings.DbPass = "burstwallet"
                        Case QGlobal.DbType.MariaDB
                            Q.settings.DbServer = "jdbc:mariadb://" & Q.settings.DbServer & "/" & Q.settings.DbName
                        Case QGlobal.DbType.H2
                            Q.settings.DbServer = "jdbc:h2:./burst_db/burst;DB_CLOSE_ON_EXIT=False"
                            Q.settings.DbUser = "sa"
                            Q.settings.DbPass = "sa"
                    End Select
                    Q.settings.SaveSettings()
                Case 17 '  from 17-18
                    'must check settings
                    Select Case Q.settings.DbType
                        Case QGlobal.DbType.FireBird
                            Q.settings.DbServer = "jdbc:firebirdsql:embedded:./burst_db/burst.firebird.db"
                            Q.settings.DbUser = "sa"
                            Q.settings.DbPass = "sa"
                        Case QGlobal.DbType.pMariaDB
                            Q.settings.DbServer = "jdbc:mariadb://localhost:3306/burstwallet"
                            Q.settings.DbUser = "burstwallet"
                            Q.settings.DbPass = "burstwallet"
                        Case QGlobal.DbType.H2
                            Q.settings.DbServer = "jdbc:h2:./burst_db/burst;DB_CLOSE_ON_EXIT=False"
                            Q.settings.DbUser = "sa"
                            Q.settings.DbPass = "sa"
                    End Select
                    Q.settings.SaveSettings()
                Case 18 '18 -19
                    Q.settings.Currency = "USD"
                    Q.settings.SaveSettings()
                Case 190 '1.9.0-2.0.0
                Case 200 '2.0.0-2.0.1
                    Try
                        If File.Exists(QGlobal.BaseDir & "restarter.exe") Then
                            File.Delete(QGlobal.BaseDir & "restarter.exe")
                        End If
                    Catch ex As Exception

                    End Try
                Case 201 '2.0.1 - 2.0.2
                Case 211 '2.1.1 - 2.1.2
                Case 213 '2.5.1
                Case 214 '2.5.2
            End Select
            OldVer += 1
            If CurVer = OldVer Then Exit Do
        Loop

        Q.settings.Upgradev = CurVer
        Q.settings.SaveSettings()
    End Sub

    Friend Shared Sub WriteNewBRSConfig(Optional ByVal WriteDebug As Boolean = False)
        Dim Buffer() As String = Nothing
        Dim Props As New clsProperties
        Props.Load(QGlobal.AppDir & "conf\brs.properties")

        'Peer settings
        Buffer = Split(Q.settings.ListenPeer, ";")
        Props.Add("P2P.Listen", Buffer(0))
        Props.Add("P2P.Port", Buffer(1))


        'API settings
        Buffer = Split(Q.settings.ListenIf, ";")
        Props.Add("API.Port", Buffer(1))
        Props.Add("API.Listen", Buffer(0))
        If Q.settings.ConnectFrom.Contains("0.0.0.0") Then
            Props.Add("API.allowed", "*")
        Else
            Props.Add("API.allowed", Q.settings.ConnectFrom)
        End If

        'autoip
        If Q.settings.AutoIp Then
            Dim ip As String = GetMyIp()
            Dim address As IPAddress = Nothing
            If ip <> "" And IPAddress.TryParse(ip, address) Then
                Props.Add("P2P.myAddress", ip)
            End If
        Else
            If Q.settings.Broadcast.Length > 0 Then
                Props.Add("P2P.myAddress", Q.settings.Broadcast)
            End If
        End If

        'platformname
        If Q.settings.DynPlatform Then
            Props.Add("P2P.myPlatform", "Q-" & GetDbNameFromType(Q.settings.DbType))
        End If

        'database
        Props.Add("DB.Url", Q.settings.DbServer)
        Props.Add("DB.Username", Q.settings.DbUser)
        Props.Add("DB.Password", Q.settings.DbPass)

        'OpenCL
        If Q.settings.useOpenCL Then
            Props.Add("GPU.AutoDetect", "on")
            Props.Add("GPU.HashesPerBatch", "100")
            Props.Add("GPU.Acceleration", "on")
        Else
            Props.Add("GPU.Acceleration", "off")
        End If

        If WriteDebug Then
            Props.Add("brs.disablePeerConnectingThread", "true")
            Props.Add("brs.enableTransactionRebroadcasting", "false")
            Props.Add("brs.getMorePeers", "false")
            Props.Add("brs.disableProcessTransactionsThread", "true")
            Props.Add("brs.disableRemoveUnconfirmedTransactionsThread", "true")
            Props.Add("brs.disableRebroadcastTransactionsThread", "true")
            Props.Add("API.ServerEnforcePOST", "false")
            Props.Add("API.Debug", "true")
        Else
            Props.Delete("brs.disablePeerConnectingThread")
            Props.Delete("brs.enableTransactionRebroadcasting")
            Props.Delete("brs.getMorePeers")
            Props.Delete("brs.disableProcessTransactionsThread")
            Props.Delete("brs.disableRemoveUnconfirmedTransactionsThread")
            Props.Delete("brs.disableRebroadcastTransactionsThread")
            Props.Delete("API.ServerEnforcePOST")
            Props.Delete("API.Debug")
        End If

        Props.Save(QGlobal.AppDir & "conf\brs.properties")
    End Sub


    Friend Shared Sub WriteBRSConfig(Optional ByVal WriteDebug As Boolean = False)
        Dim Buffer() As String = Nothing
        Try
            If File.Exists(QGlobal.AppDir & "conf\brs.properties") Then
                Buffer = File.ReadAllLines(QGlobal.AppDir & "conf\brs.properties")
            End If
        Catch ex As Exception
            Generic.WriteDebug(ex)
        End Try

        Dim Data = ""

        'writing brs.properties

        'Peer settings
        Data &= "#Peer network" & vbCrLf
        Buffer = Split(Q.settings.ListenPeer, ";")
        Data &= "P2P.Port = " & Buffer(1) & vbCrLf
        Data &= "P2P.Listen = " & Buffer(0) & vbCrLf & vbCrLf

        'API settings
        Data &= "#API network" & vbCrLf
        Buffer = Split(Q.settings.ListenIf, ";")
        Data &= "API.Port = " & Buffer(1) & vbCrLf
        Data &= "API.Listen = " & Buffer(0) & vbCrLf
        If Q.settings.ConnectFrom.Contains("0.0.0.0") Then
            Data &= "API.allowed = *" & vbCrLf & vbCrLf
        Else
            Data &= "API.allowed = " & Q.settings.ConnectFrom & vbCrLf & vbCrLf
        End If


        'autoip
        If Q.settings.AutoIp Then
            Dim ip As String = GetMyIp()
            If ip <> "" Then
                Data &= "#Auto IP set" & vbCrLf
                Data &= "P2P.myAddress = " & ip & vbCrLf & vbCrLf
            End If
        Else
            If Q.settings.Broadcast.Length > 0 Then
                Data &= "#Manual broadcast" & vbCrLf
                Data &= "P2P.myAddress = " & Q.settings.Broadcast & vbCrLf & vbCrLf
            End If
        End If

        'Dyn platform
        If Q.settings.DynPlatform Then
            Data &= "#Dynamic platform" & vbCrLf
            Data &= "P2P.myPlatform = Q-" & GetDbNameFromType(Q.settings.DbType) & vbCrLf & vbCrLf
        End If

        Select Case Q.settings.DbType
            Case QGlobal.DbType.pMariaDB
                Data &= "#Using MariaDb Portable" & vbCrLf
            Case QGlobal.DbType.MariaDB
                Data &= "#Using installed MariaDb" & vbCrLf
            Case QGlobal.DbType.H2
                Data &= "#Using H2" & vbCrLf
        End Select

        Data &= "DB.Url = " & Q.settings.DbServer & vbCrLf
        Data &= "DB.Username = " & Q.settings.DbUser & vbCrLf
        Data &= "DB.Password = " & Q.settings.DbPass & vbCrLf & vbCrLf


        If Q.settings.useOpenCL Then
            Data &= "#CPU Offload" & vbCrLf
            Data &= "GPU.AutoDetect = on" & vbCrLf
            Data &= "GPU.HashesPerBatch = 100" & vbCrLf
            Data &= "GPU.Acceleration = on" & vbCrLf & vbCrLf
        End If
        If WriteDebug Then
            Data &= "#Debug mode" & vbCrLf
            Data &= "brs.disablePeerConnectingThread = true" & vbCrLf
            Data &= "brs.enableTransactionRebroadcasting=false" & vbCrLf
            Data &= "brs.getMorePeers = false " & vbCrLf
            Data &= "brs.disableProcessTransactionsThread = true" & vbCrLf
            Data &= "brs.disableRemoveUnconfirmedTransactionsThread = true" & vbCrLf
            Data &= "brs.disableRebroadcastTransactionsThread = true" & vbCrLf
            Data &= "API.ServerEnforcePOST = false" & vbCrLf
            Data &= "API.Debug = true" & vbCrLf & vbCrLf
        End If
        Try
            File.WriteAllText(QGlobal.AppDir & "conf\brs.properties", Data)
        Catch ex As Exception
            Generic.WriteDebug(ex)
        End Try
    End Sub

    Friend Shared Sub WriteWalletConfig(Optional ByVal WriteDebug As Boolean = False)
        WriteNewBRSConfig(WriteDebug)
    End Sub

    Friend Shared Function CalculateBytes(value As Long, decimals As Integer, Optional ByVal startat As Integer = 0) _
        As String
        Dim t As Integer
        Dim res = CDbl(value)
        For t = startat To 10
            If res > 1024 Then
                res /= 1024
            Else
                Exit For
            End If
        Next
        If startat = 11 Then
            Return Math.Round(res, decimals).ToString("0.00")
        End If
        If t = 0 Then
            Return Math.Round(res, decimals).ToString("0.00") & "bytes"
        End If
        If t = 1 Then
            Return Math.Round(res, decimals).ToString("0.00") & "KiB"
        End If
        If t = 2 Then
            Return Math.Round(res, decimals).ToString("0.00") & "MiB"
        End If
        If t = 3 Then
            Return Math.Round(res, decimals).ToString("0.00") & "GiB"
        End If
        If t = 4 Then
            Return Math.Round(res, decimals).ToString("0.00") & "TiB"
        End If
        Return "??"
    End Function

    Friend Shared Function IsAdmin() As Boolean
        Try
            If My.User.IsInRole(BuiltInRole.Administrator) Then
            Else
                Return False
            End If
        Catch ex As Exception
            Return False
        End Try
        Return True
    End Function

    Friend Shared Sub SetFirewallFromSettings()

        Dim s() As String
        Dim buffer As String
        If IsAdmin() Then
            s = Split(Q.settings.ListenPeer, ";")
            If s(0) = "0.0.0.0" Then s(0) = "*"
            If Not SetFirewall("Burst Peers", s(1), s(0), "") Then
                MsgBox("Failed to apply firewall rules.", MsgBoxStyle.Exclamation Or MsgBoxStyle.OkOnly, "Firewall")
                End
            End If
            s = Split(Q.settings.ListenIf, ";")
            If s(0) = "0.0.0.0" Then s(0) = "*"
            buffer = Trim(Q.settings.ConnectFrom)
            If buffer <> "" Then
                buffer = buffer.Replace(";", ",")
                buffer = buffer.Replace(" ", "")
                If buffer.EndsWith(",") Then buffer = buffer.Remove(buffer.Length - 1)
            End If
            If Not SetFirewall("Burst Api", s(1), s(0), buffer) Then
                MsgBox("Failed to apply firewall rules.", MsgBoxStyle.Exclamation Or MsgBoxStyle.OkOnly, "Firewall")
                End
            End If
            MsgBox("Windows firewall rules sucessfully applied.", MsgBoxStyle.Information Or MsgBoxStyle.OkOnly,
                   "Firewall")
        Else
            'start it as admin
            Try
                Dim p = New Process
                p.StartInfo.WorkingDirectory = QGlobal.BaseDir
                p.StartInfo.Arguments = "ADDFW"
                p.StartInfo.UseShellExecute = True
                p.StartInfo.FileName = Application.ExecutablePath
                p.StartInfo.Verb = "runas"
                p.Start()
            Catch ex As Exception
                MsgBox("Failed to apply firewall rules.", MsgBoxStyle.Exclamation Or MsgBoxStyle.OkOnly, "Firewall")
            End Try
        End If
    End Sub

    Private Shared Function SetFirewall(fwName As String, ports As String, LocalNet As String, RemoteNet As String) _
        As Boolean
        Try
            'first we try to remove old rule if any
            Const NET_FW_IP_PROTOCOL_TCP = 6
            Const NET_FW_RULE_DIR_IN = 1
            Const NET_FW_ACTION_ALLOW = 1
            Dim fwPolicy2 As Object = CreateObject("HNetCfg.FwPolicy2")
            Dim RulesObject As Object = fwPolicy2.Rules
            'remove old if exists
            RulesObject.Remove(fwName)
            'add new settings
            Dim CurrentProfiles As Object = fwPolicy2.CurrentProfileTypes
            Dim NewRule As Object = CreateObject("HNetCfg.FWRule")
            NewRule.Name = fwName
            NewRule.Description = "Allows incoming traffic to " & fwName
            NewRule.Protocol = NET_FW_IP_PROTOCOL_TCP
            NewRule.LocalPorts = ports
            NewRule.Direction = NET_FW_RULE_DIR_IN
            NewRule.Enabled = True
            NewRule.LocalAddresses = LocalNet
            NewRule.RemoteAddresses = RemoteNet _
            '"127.0.0.1/255.255.255.255,192.168.0.0/255.255.255.0,192.168.1.0/255.255.0.0"
            NewRule.Profiles = 7 'CurrentProfiles
            NewRule.Action = NET_FW_ACTION_ALLOW
            RulesObject.Add(NewRule)
        Catch ex As Exception
            Return False
        End Try

        Return True
    End Function

    Friend Shared Sub CheckCommandArgs()
        '0 = appname
        '1 = Type to do

        Dim clArgs() As String = Environment.GetCommandLineArgs()
        If UBound(clArgs) > 0 Then
            Select Case clArgs(1)
                Case "ADDFW"
                    Try
                        SetFirewallFromSettings()
                    Catch ex As Exception
                        MsgBox("Failed to apply firewall rules. Maybe you run another firewall on your computer?",
                               MsgBoxStyle.Exclamation Or MsgBoxStyle.OkOnly, "Firewall")
                    End Try
                    End
                Case "Debug"
                    DebugMe = True
                Case "BetaUpdate"
                    Q.AppManager.AppXML = "BetaUpdate.xml"
            End Select
        End If
    End Sub

    Friend Shared Function SanityCheck() As Boolean

        Dim Ok = True

        Dim cmdline = ""
        Dim Msg = ""
        Dim res As MsgBoxResult = Nothing
        'Check if Java is running another burst.jar
        Try
            Dim searcher As ManagementObjectSearcher
            searcher = New ManagementObjectSearcher("root\CIMV2", "SELECT * FROM Win32_Process WHERE Name='java.exe'")
            For Each p As ManagementObject In searcher.[Get]()
                cmdline = p("CommandLine").ToString
                If cmdline.ToLower.Contains("burst.jar") Then
                    Msg = "Qbundle has detected that another burst wallet is running." & vbCrLf
                    Msg &= "If the other wallet use the same setting as this one. it will not work." & vbCrLf
                    Msg &= "Would you like to stop the other wallet?" & vbCrLf & vbCrLf
                    Msg &= "Yes = Stop the other wallet and start this one." & vbCrLf
                    Msg &= "No = Start this wallet despite the other wallet." & vbCrLf
                    Msg &= "Cancel = Do not start this one." & vbCrLf
                    res = MsgBox(Msg, MsgBoxStyle.Information Or MsgBoxStyle.YesNoCancel, "Another wallet is running")
                    If res = MsgBoxResult.Yes Then
                        Dim proc As Process = Process.GetProcessById(Integer.Parse(p("ProcessId").ToString))
                        proc.Kill()
                        Thread.Sleep(1000)
                    ElseIf res = MsgBoxResult.No Then
                        'do nothing 
                    Else
                        Ok = False
                    End If
                End If
            Next

        Catch ex As Exception
            WriteDebug(ex)
        End Try
        Try
            If Q.settings.DbType = QGlobal.DbType.pMariaDB And Ok = True Then
                cmdline = ""
                Msg = ""
                Dim searcher As ManagementObjectSearcher
                searcher = New ManagementObjectSearcher("root\CIMV2",
                                                        "SELECT * FROM Win32_Process WHERE Name='mysqld.exe'")
                For Each p As ManagementObject In searcher.[Get]()
                    ' cmdline = p("CommandLine")
                    Msg = "Qbundle has detected that another Mysql/MariaDB is running." & vbCrLf
                    Msg &= "If the other database use the same setting as this one. it will not work." & vbCrLf
                    Msg &= "Would you like to stop the other database?" & vbCrLf & vbCrLf
                    Msg &= "Yes = Stop the other database and start this one." & vbCrLf
                    Msg &= "No = Start this database despite the other database." & vbCrLf
                    Msg &= "Cancel = Do not start this one." & vbCrLf
                    res = MsgBox(Msg, MsgBoxStyle.Information Or MsgBoxStyle.YesNoCancel, "Another database is running")
                    If res = MsgBoxResult.Yes Then
                        Dim proc As Process = Process.GetProcessById(Integer.Parse(p("ProcessId").ToString))
                        proc.Kill()
                        Thread.Sleep(1000)
                    ElseIf res = MsgBoxResult.No Then
                        'do nothing 
                    Else
                        Ok = False
                    End If
                Next
            End If
        Catch ex As Exception
            WriteDebug(ex)
        End Try
        If Q.settings.NTPCheck Then
            Try
                Dim ntpTime As Date = GetNTPTime("time.windows.com")
                Dim OffSeconds = 0
                Dim localTimezoneNTPTime As Date = TimeZoneInfo.ConvertTime(ntpTime, TimeZoneInfo.Utc,
                                                                            TimeZoneInfo.Local)
                If Now > localTimezoneNTPTime Then
                    OffSeconds = CInt((Now - localTimezoneNTPTime).TotalSeconds)
                ElseIf Now < localTimezoneNTPTime Then
                    OffSeconds = CInt((localTimezoneNTPTime - Now).TotalSeconds)
                End If

                If OffSeconds > 15 Then
                    Msg = "Your computer clock is drifting." & vbCrLf
                    Msg &= "World time (UTC): " & ntpTime.ToString & vbCrLf
                    Msg &= "Your computer time: " & Now.ToString & vbCrLf & vbCrLf
                    Msg &= "Your computer time with current timezone setting " & vbCrLf
                    Msg &= "should be: " & localTimezoneNTPTime.ToString & vbCrLf & vbCrLf
                    Msg &= "Your time is currently off with " & OffSeconds.ToString & " Seconds" & vbCrLf
                    Msg &= "Burstwallet allows max 15 seconds drifting." & vbCrLf
                    MsgBox(Msg, MsgBoxStyle.Critical Or MsgBoxStyle.OkOnly, "Change computer time")
                    Ok = False
                End If
            Catch ex As Exception
                WriteDebug(ex)
            End Try
        End If
        Return Ok
    End Function

    Friend Shared Function IsProcessRunning(Name As String) As Boolean
        Dim Ok = True
        Dim searcher As ManagementObjectSearcher
        Dim RetVal = False
        Dim prc = ""
        searcher = New ManagementObjectSearcher("root\CIMV2", "SELECT * FROM Win32_Process") _
        ' WHERE Name='" & exeName & "'
        For Each p As ManagementObject In searcher.[Get]()
            prc = LCase(p("Name").ToString)
            If prc.Contains(LCase(Name)) Then RetVal = True
        Next
        Return RetVal
    End Function

    Friend Shared Sub KillAllProcessesWithName(Name As String)
        Dim Ok = True
        Dim searcher As ManagementObjectSearcher
        Dim prc = ""
        searcher = New ManagementObjectSearcher("root\CIMV2", "SELECT * FROM Win32_Process") _
        ' WHERE Name='" & exeName & "'
        For Each p As ManagementObject In searcher.[Get]()
            prc = LCase(p("Name").ToString)
            If prc.Contains(LCase(Name)) Then
                Dim proc As Process = Process.GetProcessById(Integer.Parse(p("ProcessId").ToString))
                proc.Kill()
                Thread.Sleep(100)
            End If


        Next
    End Sub

    Friend Shared Function GetMyIp() As String
        Try
            Dim WC = New WebClient()
            Return WC.DownloadString("http://whatismyip.akamai.com/")
        Catch ex As Exception
            WriteDebug(ex)
        End Try
        Return ""
    End Function

    Friend Shared Function CheckWritePermission() As Boolean
        Try
            File.WriteAllText(QGlobal.AppDir & "testfile", "test")
            File.Delete(QGlobal.AppDir & "testfile")
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Friend Shared Sub WriteDebug(exc As Exception)

        Try
            If DebugMe Then
                Dim strErr As String = "------------------------- " & Now.ToString & " --------------------------" &
                                       vbCrLf
                strErr &= "Message: " & exc.Message & vbCrLf
                strErr &= "StackTrace:" & exc.StackTrace & vbCrLf

                File.AppendAllText(QGlobal.LogDir & "\bwl_debug.txt", strErr)


            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Friend Shared Sub RestartBundle()
        Try
            Dim p = New Process
            p.StartInfo.WorkingDirectory = QGlobal.BaseDir
            p.StartInfo.UseShellExecute = True
            If DebugMe Then p.StartInfo.Arguments = "Debug"
            p.StartInfo.FileName = Application.ExecutablePath
            p.Start()
        Catch ex As Exception
            MsgBox("Failed to start Qbundle", MsgBoxStyle.Exclamation Or MsgBoxStyle.OkOnly)
        End Try
    End Sub

    Friend Shared Sub RestartAsAdmin()

        Try
            Dim p = New Process
            p.StartInfo.WorkingDirectory = QGlobal.BaseDir
            p.StartInfo.UseShellExecute = True
            If DebugMe Then p.StartInfo.Arguments = "Debug"
            p.StartInfo.FileName = Application.ExecutablePath
            p.StartInfo.Verb = "runas"
            p.Start()
        Catch ex As Exception
            MsgBox("Failed to start Qbundle as administrator.", MsgBoxStyle.Exclamation Or MsgBoxStyle.OkOnly, "Admin")
        End Try
    End Sub

    Public Shared Function GetLatencyMs(ByRef hostNameOrAddress As String) As Long
        Dim ping As New Ping
        Return ping.Send(hostNameOrAddress, 300).RoundtripTime
    End Function

    Public Shared Sub UpdateLocalWallet()
        Dim s() As String = Split(Q.settings.ListenIf, ";")
        Dim url As String = Nothing
        If s(0) = "0.0.0.0" Then
            url = "http://127.0.0.1:" & s(1)
        Else
            url = "http://" & s(0) & ":" & s(1)
        End If
        Q.AppManager.AppStore.Wallets(0).Address = url
    End Sub

    Friend Shared Function GetStartNonce(AccountID As String, Length As Double) As Double

        Dim Plotfiles() As String

        Dim StartNonce As Double = 0
        Dim EndNonce As Double = StartNonce + Length
        Dim HighestEndNonce As Double = 0
        Dim PEndNonce As Double = 0
        Try
            If Q.settings.Plots.Length > 0 Then
                Plotfiles = Split(Q.settings.Plots, "|")
                For Each Plot As String In Plotfiles
                    If Plot.Length > 1 Then
                        Dim N() As String = Split(Path.GetFileName(Plot), "_")
                        If UBound(N) > 1 Then
                            If N(0) = Trim(AccountID) Then
                                PEndNonce = CDbl(N(1)) + CDbl(N(2))
                                If PEndNonce > HighestEndNonce Then HighestEndNonce = PEndNonce
                            End If
                        End If
                    End If
                Next
            End If
        Catch ex As Exception
            Return 0
        End Try

        Return HighestEndNonce
    End Function

    Public Shared Function GetNTPTime(ntpServer As String) As Date

        Dim socket = New Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp)
        Try

            Dim ntpData = New Byte(47) {}
            ntpData(0) = &H1B
            Dim addresses = Dns.GetHostEntry(ntpServer).AddressList
            Dim ipEndPoint = New IPEndPoint(addresses(0), 123)

            socket.SendTimeout = 5000
            socket.ReceiveTimeout = 5000

            socket.Connect(ipEndPoint)
            socket.Send(ntpData)
            socket.Receive(ntpData)
            socket.Close()

            Dim intPart As ULong = CULng(ntpData(40)) << 24 Or CULng(ntpData(41)) << 16 Or CULng(ntpData(42)) << 8 Or
                                   CULng(ntpData(43))
            Dim fractPart As ULong = CULng(ntpData(44)) << 24 Or CULng(ntpData(45)) << 16 Or CULng(ntpData(46)) << 8 Or
                                     CULng(ntpData(47))

            Dim milliseconds = (intPart*1000) + ((fractPart*1000)/&H100000000L)
            Dim networkDateTime = (New DateTime(1900, 1, 1)).AddMilliseconds(CLng(milliseconds))

            Return networkDateTime

        Catch ex As Exception
            WriteDebug(ex)
        Finally
            socket.Dispose()
        End Try
        Return Now
    End Function

    Friend Shared Function PlotDriveTypeOk(path As String) As Boolean

        Dim TheDrive = New DriveInfo(path)

        If TheDrive.DriveFormat = "NTFS" Then
            Return True
        End If


        Return False
    End Function

    Friend Shared Function DriveCompressed(path As String) As Boolean
        Dim dirInfo = New DirectoryInfo(path)
        If (dirInfo.Attributes And FileAttributes.Compressed) = FileAttributes.Compressed Then
            Return True
        End If
        Return False
    End Function

    Friend Shared Function CheckDotNet() As Boolean
        Const subkey = "SOFTWARE\Microsoft\NET Framework Setup\NDP\v4\Full\"
        Using _
            ndpKey As RegistryKey =
                RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry32).OpenSubKey(subkey)
            If ndpKey IsNot Nothing AndAlso ndpKey.GetValue("Release") IsNot Nothing Then
                If ((CInt(ndpKey.GetValue("Release")) >= 379893)) Then 'ver 4.5.2
                    Return True
                End If
            End If
        End Using
        Return False
    End Function

    Friend Shared Function IsValidPlottFilename(filename As String) As Boolean
        If IsNothing(filename) Then Return False
        If Regex.IsMatch(filename, "\d+_\d+_\d+_\d+") Then 'PoC1
            Return True
        ElseIf Regex.IsMatch(filename, "\d+_\d+_\d+") Then 'PoC2
            Return True
        End If

        Return False
    End Function

    Friend Shared Function IsValidPlottFilePath(filepath As String) As Boolean
        If IsNothing(filepath) Then Return False
        Return IsValidPlottFilename(Path.GetFileName(filepath))
    End Function

    Friend Shared Function GetDiskspace(path As String) As Long

        Try
            Dim FreeSpace As Long
            GetDiskFreeSpaceEx(path, vbNull, vbNull, FreeSpace)
            Return FreeSpace
        Catch ex As Exception
            WriteDebug(ex)
        End Try
        Return 0
    End Function

    Friend Shared Function GetDbNameFromType(Dtype As Integer) As String
        Select Case Dtype
            Case QGlobal.DbType.H2
                Return "H2"
            Case QGlobal.DbType.FireBird
                Return "FireBird"
            Case QGlobal.DbType.MariaDB
                Return "MariaDB"
            Case QGlobal.DbType.pMariaDB
                Return "MariaDB"
        End Select
        Return ""
    End Function

    Public Shared Function CheckOpenCL() As Boolean
        Try
            If File.Exists(Environment.SystemDirectory & "\OpenCL.dll") Then
                Return True
            End If
        Catch ex As Exception
            WriteDebug(ex)
        End Try
        Return False
    End Function
End Class
