﻿Public Class frmChangeDatabase
    Private Running As Boolean
    Private WithEvents WaitTimer As Timer

    Private Delegate Sub DProcEvents([AppId] As Integer, [Operation] As Integer, [data] As String)

    Private Delegate Sub DStarting([AppId] As Integer)

    Private Delegate Sub DStoped([AppId] As Integer)

    Private Delegate Sub DAborted([AppId] As Integer, [data] As String)

    Private StartTime As Date
    Private SelDB As Integer
    Private OldDB As Integer
    Private OP As Integer '0=copy 1=Fresh

#Region " Setup and form Events "

    Private Sub frmChangeDatabase_Closing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        Try
            If Running Then
                MsgBox("You must wait until the convertion is compleated.", MsgBoxStyle.OkOnly, "Close")
                e.Cancel = True
                Exit Sub
            End If
        Catch ex As Exception
            Generic.WriteDebug(ex)
        End Try
    End Sub

    Private Sub frmChangeDatabase_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        SelDB = Q.settings.DbType

        'check if mariadbp is installed
        Select Case SelDB
            Case QGlobal.DbType.H2
                rDB1.Enabled = False
                rDB1.Text = rDB1.Text & " (Currently used)"
            Case QGlobal.DbType.FireBird
                '    rDB2.Enabled = False
                '   rDB2.Text = rDB2.Text & " (Currently used)"
            Case QGlobal.DbType.pMariaDB
                rDB3.Enabled = False
                rDB3.Text = rDB3.Text & " (Currently used)"
            Case QGlobal.DbType.MariaDB
                rDB4.Enabled = False
                rDB4.Text = rDB4.Text & " (Currently used)"
                pnlMariaSettings.Enabled = True
        End Select
        lblCurDB.Text = Generic.GetDbNameFromType(Q.settings.DbType)
        If Not Q.settings.DbType = QGlobal.DbType.H2 Then
            setdb(QGlobal.DbType.H2)
        Else
            setdb(QGlobal.DbType.pMariaDB)
        End If

        OP = 1
        '/disable firebird
        ' rDB2.Enabled = False
        '    rDB2.Text = rDB2.Text & " (Currently Disabled)"

        'Width = 501
        'Height = 436
        ' MsgBox(pnlWiz1.Top + pnlWiz1.Height + 40)
        Width = pnlWiz1.Left + pnlWiz1.Width + 13
        Height = pnlWiz1.Top + pnlWiz1.Height + 40
    End Sub

    Private Sub setdb(id As Integer)
        rDB1.Checked = False
        '   rDB2.Checked = False
        rDB3.Checked = False
        rDB4.Checked = False
        pnlMariaSettings.Enabled = False
        SelDB = id
        Select Case SelDB
            Case QGlobal.DbType.H2
                rDB1.Checked = True
                lblFromTo.Text = Generic.GetDbNameFromType(Q.settings.DbType) & " to " &
                                 Generic.GetDbNameFromType(QGlobal.DbType.H2)
            Case QGlobal.DbType.pMariaDB
                rDB3.Checked = True
                lblFromTo.Text = Generic.GetDbNameFromType(Q.settings.DbType) & " to " &
                                 Generic.GetDbNameFromType(QGlobal.DbType.pMariaDB)
            Case QGlobal.DbType.MariaDB
                rDB4.Checked = True
                lblFromTo.Text = Generic.GetDbNameFromType(Q.settings.DbType) & " to " &
                                 Generic.GetDbNameFromType(QGlobal.DbType.MariaDB)
                pnlMariaSettings.Enabled = True
        End Select
    End Sub

    Private Sub rDB1_Click(sender As Object, e As EventArgs) Handles rDB1.Click
        setdb(QGlobal.DbType.H2)
    End Sub

    Private Sub rDB2_Click(sender As Object, e As EventArgs)
        setdb(QGlobal.DbType.FireBird)
    End Sub

    Private Sub rDB3_Click(sender As Object, e As EventArgs) Handles rDB3.Click
        setdb(QGlobal.DbType.pMariaDB)
    End Sub

    Private Sub rDB4_Click(sender As Object, e As EventArgs) Handles rDB4.Click
        setdb(QGlobal.DbType.MariaDB)
    End Sub

    Private Sub btnNext_Click(sender As Object, e As EventArgs) Handles btnNext.Click

        'download and install mariadb if that is checked and not installed
        If SelDB = QGlobal.DbType.pMariaDB Then
            If Not Q.AppManager.IsAppInstalled("MariaPortable") Then
                If _
                    MsgBox("MariaDB Portable is not installed. Would you like to download and install it now?",
                           MsgBoxStyle.Information Or MsgBoxStyle.YesNo, "Download Maria DB?") = MsgBoxResult.Yes Then
                    Hide()
                    Dim res As Boolean = Q.AppManager.InstallApp("MariaPortable")
                    Show()
                    If res = False Then Exit Sub
                Else
                    Exit Sub
                End If
            End If
        End If
        pnlWiz1.Hide()
        pnlWiz2.Show()
        pnlWiz2.Top = pnlWiz1.Top
        pnlWiz2.Left = pnlWiz1.Left
    End Sub

    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        pnlWiz2.Hide()
        pnlWiz1.Show()
    End Sub

    Private Sub rOP1_CheckedChanged(sender As Object, e As EventArgs) Handles rOP1.Click
        '   OP = 0
        '   btnStart.Text = "Start Copy"
    End Sub

    Private Sub rOP2_CheckedChanged(sender As Object, e As EventArgs) Handles rOP2.CheckedChanged
        OP = 1
        btnStart.Text = "Save and close"
    End Sub

    Private Sub btnStart_Click(sender As Object, e As EventArgs) Handles btnStart.Click

        If btnStart.Text = "Close" Then
            Close()
            Exit Sub
        End If

        If OP = 0 Then
            '      OldDB = Q.settings.DbType
            '      If frmMain.Running Then
            '      If MsgBox("The wallet must be stopped to export the database." & vbCrLf & " Would you like to stop it now?", MsgBoxStyle.Exclamation Or MsgBoxStyle.YesNo, "Stop wallet?") = MsgBoxResult.Yes Then
            '      lblStatus.Text = "Waiting for wallet to stop."
            '      frmMain.StopWallet()
            '      WaitTimer = New Timer
            '      WaitTimer.Interval = 500
            '      WaitTimer.Enabled = True
            '      WaitTimer.Start()
            '      Exit Sub
            '  End If
            '  End If


            ' AddHandler Q.ProcHandler.Aborting, AddressOf Aborted
            ' AddHandler Q.ProcHandler.Started, AddressOf Starting
            ' AddHandler Q.ProcHandler.Stopped, AddressOf Stopped
            ' AddHandler Q.ProcHandler.Update, AddressOf ProcEvents
            ' If SelDB = QGlobal.DbType.pMariaDB Or OldDB = QGlobal.DbType.pMariaDB Then
            ' StartMaria()
            ' Else
            '     StartExport()
            ' End If
        Else
            Q.settings.DbType = SelDB
            If SelDB = QGlobal.DbType.MariaDB Then
                Q.settings.DbName = QGlobal.Dbinfo(SelDB).Name
                Q.settings.DbPass = txtDbPass.Text
                Q.settings.DbUser = txtDbUser.Text
                Q.settings.DbServer = txtDbConnection.Text
            Else
                Q.settings.DbName = QGlobal.Dbinfo(SelDB).Name
                Q.settings.DbPass = QGlobal.Dbinfo(SelDB).Pass
                Q.settings.DbUser = QGlobal.Dbinfo(SelDB).Username
                Q.settings.DbServer = QGlobal.Dbinfo(SelDB).ConnString
            End If

            Q.settings.SaveSettings()
            Generic.WriteWalletConfig()
            frmMain.SetDbInfo()
            Close()
        End If
    End Sub

#End Region


    Private Sub StartExport()
        btnBack.Enabled = False
        btnStart.Enabled = False
        rOP1.Enabled = False
        rOP2.Enabled = False


        StartTime = Now
        Dim Pset As New clsProcessHandler.pSettings
        Pset.AppId = QGlobal.AppNames.Export
        If Q.settings.JavaType = QGlobal.AppNames.JavaInstalled Then
            Pset.AppPath = "java"
        Else
            Pset.AppPath = QGlobal.AppDir & "Java\bin\java.exe"
        End If
        Pset.Cores = Q.settings.Cpulimit
        '  Pset.Params = QGlobal.WalletLaunchString.Export & QGlobal.AppDir & "Convertion.bbd"
        Pset.StartSignal = ""
        Pset.StartsignalMaxTime = 1
        Pset.WorkingDirectory = QGlobal.AppDir
        Q.ProcHandler.StartProcess(Pset)

        Running = True
    End Sub

    Private Sub WaitTimer_tick() Handles WaitTimer.Tick
        If frmMain.Running = False Then
            WaitTimer.Stop()
            WaitTimer.Enabled = False
            AddHandler Q.ProcHandler.Aborting, AddressOf Aborted
            AddHandler Q.ProcHandler.Started, AddressOf Starting
            AddHandler Q.ProcHandler.Stopped, AddressOf Stopped
            AddHandler Q.ProcHandler.Update, AddressOf ProcEvents
            If SelDB = QGlobal.DbType.pMariaDB Or OldDB = QGlobal.DbType.pMariaDB Then
                StartMaria()
            Else
                StartExport()
            End If
        End If
    End Sub

    Private Sub Starting(AppId As Integer)
        If InvokeRequired Then
            Dim d As New DStarting(AddressOf Starting)
            Invoke(d, New Object() {AppId})
            Return
        End If

        Select Case AppId
            Case QGlobal.AppNames.Export
                lblStatus.Text = "Starting to export"
                pb1.Value = 0
            Case QGlobal.AppNames.Import
                lblStatus.Text = "Starting to Import"
                pb1.Value = 0
        End Select
    End Sub

    Private Sub Stopped(AppId As Integer)
        If InvokeRequired Then
            Dim d As New DStoped(AddressOf Stopped)
            Invoke(d, New Object() {AppId})
            Return
        End If
        If AppId = QGlobal.AppNames.Export Then
            Q.settings.DbType = SelDB
            If SelDB = QGlobal.DbType.MariaDB Then
                Q.settings.DbName = QGlobal.Dbinfo(SelDB).Name
                Q.settings.DbPass = txtDbPass.Text
                Q.settings.DbUser = txtDbUser.Text
                Q.settings.DbServer = txtDbConnection.Text
            Else
                Q.settings.DbName = QGlobal.Dbinfo(SelDB).Name
                Q.settings.DbPass = QGlobal.Dbinfo(SelDB).Pass
                Q.settings.DbUser = QGlobal.Dbinfo(SelDB).Username
                Q.settings.DbServer = QGlobal.Dbinfo(SelDB).ConnString
            End If
            Q.settings.SaveSettings()
            Generic.WriteWalletConfig()
            StartImport()
        End If
        If AppId = QGlobal.AppNames.Import Then
            If SelDB = QGlobal.DbType.pMariaDB Or OldDB = QGlobal.DbType.pMariaDB Then
                StopMaria() 'even if we dont use it we call it
            Else
                Complete()
            End If
        End If
        If AppId = QGlobal.AppNames.MariaPortable Then
            Complete()
        End If
    End Sub

    Private Sub Complete()
        frmMain.SetDbInfo() 'update frmmain as well
        Dim ElapsedTime As TimeSpan = Now.Subtract(StartTime)
        lblStatus.Text = "Done, Conversion completed in " & ElapsedTime.Hours & "h " & ElapsedTime.Minutes & "m " &
                         ElapsedTime.Seconds & "s"
        btnStart.Text = "Close"
        btnStart.Enabled = True
        pb1.Value = 100
        Running = False
        RemoveHandler Q.ProcHandler.Aborting, AddressOf Aborted
        RemoveHandler Q.ProcHandler.Started, AddressOf Starting
        RemoveHandler Q.ProcHandler.Stopped, AddressOf Stopped
        RemoveHandler Q.ProcHandler.Update, AddressOf ProcEvents
    End Sub

    Private Sub ProcEvents(AppId As Integer, Operation As Integer, data As String)
        If InvokeRequired Then
            Dim d As New DProcEvents(AddressOf ProcEvents)
            Invoke(d, New Object() {AppId, Operation, data})
            Return
        End If
        Dim darray() As String = Nothing
        Dim percent = 0
        If AppId = QGlobal.AppNames.Export Then
            Select Case Operation

                Case QGlobal.ProcOp.ConsoleOut And QGlobal.ProcOp.ConsoleErr
                    Try
                        darray = Split(data, " ")
                        If UBound(darray) = 8 Then
                            If darray(3) = "CreateBinDump" And darray(7) = "/" Then
                                If Not darray(6) = "0" And Not darray(8) = "0" Then
                                    percent = CInt(Math.Round(Val(darray(6))/Val(darray(8))*100, 0))
                                Else
                                    percent = 100
                                End If
                                lblStatus.Text = "Exporting " & darray(5).Replace(":", "") & " " & darray(6) & " of " &
                                                 darray(8)
                                pb1.Value = percent
                            End If
                        End If
                    Catch ex As Exception
                        lblStatus.Text = "Error parsing data. Job still continues."
                    End Try
                Case QGlobal.ProcOp.Err
                    Running = False
            End Select
        End If
        If AppId = QGlobal.AppNames.Import Then 'we need to filter messages
            Select Case Operation
                Case QGlobal.ProcOp.ConsoleOut And QGlobal.ProcOp.ConsoleErr
                    Try
                        darray = Split(data, " ")
                        If UBound(darray) = 8 Then
                            If darray(3) = "LoadBinDump" And darray(7) = "/" Then
                                If Not darray(6) = "0" And Not darray(8) = "0" Then
                                    percent = CInt(Math.Round(Val(darray(6))/Val(darray(8))*100, 0))
                                Else
                                    percent = 100
                                End If
                                lblStatus.Text = "Importing " & darray(5).Replace(":", "") & " " & darray(6) & " of " &
                                                 darray(8)
                                pb1.Value = percent
                            End If
                        End If
                        If UBound(darray) > 5 Then
                            If darray(5) = "Compacting" And darray(6) = "database" Then
                                lblStatus.Text = "Compacting database. Please wait."
                                pb1.Value = 0
                            End If
                        End If
                    Catch ex As Exception
                        lblStatus.Text = "Error parsing data. Job still continues."
                    End Try
                Case QGlobal.ProcOp.Err
                    Running = False
            End Select
        End If
        If AppId = QGlobal.AppNames.MariaPortable Then
            Select Case Operation
                Case QGlobal.ProcOp.FoundSignal
                    StartExport()
            End Select
        End If
    End Sub

    Private Sub Aborted(AppId As Integer, Data As String)
        If InvokeRequired Then
            Dim d As New DAborted(AddressOf Aborted)
            Invoke(d, New Object() {AppId, Data})
            Return
        End If

        If AppId = QGlobal.AppNames.Export Or AppId = QGlobal.AppNames.Import Then
            MsgBox(Data)
        End If
    End Sub

    Private Sub StartImport()
        Dim Pset As New clsProcessHandler.pSettings
        Pset.AppId = QGlobal.AppNames.Import
        If Q.settings.JavaType = QGlobal.AppNames.JavaInstalled Then
            Pset.AppPath = "java"
        Else
            Pset.AppPath = QGlobal.AppDir & "Java\bin\java.exe"
        End If
        Pset.Cores = Q.settings.Cpulimit
        '   Pset.Params = QGlobal.WalletLaunchString.Import & QGlobal.AppDir & "Convertion.bbd -y"
        Pset.StartSignal = ""
        Pset.StartsignalMaxTime = 1
        Pset.WorkingDirectory = QGlobal.AppDir
        Q.ProcHandler.StartProcess(Pset)
    End Sub

    Private Sub StartMaria()
        Try
            'we already have a handler
            If Generic.SanityCheck Then
                lblStatus.Text = "Starting MariaDB"
                Dim pr As New clsProcessHandler.pSettings
                pr.AppId = QGlobal.AppNames.MariaPortable
                pr.AppPath = QGlobal.AppDir & "MariaDb\bin\mysqld.exe"
                pr.Cores = 0
                pr.Params = "--console"
                pr.WorkingDirectory = QGlobal.AppDir & "MariaDb\bin\"
                pr.StartSignal = "ready for connections"
                pr.StartsignalMaxTime = 60
                Q.ProcHandler.StartProcess(pr)
            End If
        Catch ex As Exception
            MsgBox("Unable to start Maria Portable.")
        End Try
    End Sub

    Private Sub StopMaria()
        lblStatus.Text = "Stopping MariaDB"
        Q.ProcHandler.StopProcess(QGlobal.AppNames.MariaPortable)
    End Sub
End Class