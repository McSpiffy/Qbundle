﻿Imports System.Threading

Public Class clsProcessHandler
    Public Event Update(Pid As Integer, Status As Integer, Data As String)
    Public Event Stopped(Pid As Integer)
    Public Event Started(Pid As Integer)
    Public Event Aborting(Pid As Integer, Data As String)

    Private ReadOnly P() As clsProcessWorker

    Sub New()
        ReDim P(UBound([Enum].GetNames(GetType(QGlobal.AppNames))))
    End Sub

    Public Sub StartProcess(Pcls As pSettings)
        P(Pcls.AppId) = New clsProcessWorker
        P(Pcls.AppId).Appid = Pcls.AppId
        P(Pcls.AppId).AppToStart = Pcls.AppPath
        P(Pcls.AppId).Arguments = Pcls.Params
        P(Pcls.AppId).WorkingDirectory = Pcls.WorkingDirectory
        P(Pcls.AppId).StartSignal = Pcls.StartSignal
        P(Pcls.AppId).Cores = CInt(2^Pcls.Cores) - 1 'processaffinity is set bitwise 
        P(Pcls.AppId).SSMTEnd = Pcls.StartsignalMaxTime

        P(Pcls.AppId).UpgradeSignal = Pcls.UpgradeSignal
        P(Pcls.AppId).UpgradeCmd = Pcls.UpgradeCmd

        AddHandler P(Pcls.AppId).UpdateConsole, AddressOf ProcUpdate

        Dim trda As Thread
        trda = New Thread(AddressOf P(Pcls.AppId).Work)
        trda.IsBackground = True
        trda.Start()
        trda = Nothing
    End Sub

    Friend Sub ReStartProcess(AppId As Object)
        Dim trda As Thread
        trda = New Thread(AddressOf RestartWorker)
        trda.IsBackground = True
        trda.Start(AppId)
        trda = Nothing
    End Sub

    Private Sub RestartWorker(AppId As Object)
        If Not IsNothing(P(CInt(AppId))) Then
            If P(CInt(AppId)).IsRunning Then
                P(CInt(AppId)).StopProc()
                Do
                    If P(CInt(AppId)).State = QGlobal.ProcOp.Stopped Then Exit Do
                    Thread.Sleep(500)
                Loop
            End If
            'we have stopped
            Dim trda As Thread
            trda = New Thread(AddressOf P(CInt(AppId)).Work)
            trda.IsBackground = True
            trda.Start()
            trda = Nothing
        End If
    End Sub


    Public Sub StartProcessSquence(Pcls() As pSettings)
        'this is needed if working with mariadb portable. maybe for otherthings later aswell.
        Dim trda As Thread
        '  trda = New Thread(AddressOf StartPS)
        trda = New Thread(AddressOf StartPS)
        trda.IsBackground = True
        trda.Start(Pcls)
        trda = Nothing
    End Sub

    Private Sub StartPS(pcls() As Object)
        Dim ps = CType(pcls, pSettings())

        For Each Proc As pSettings In ps
            P(Proc.AppId) = New clsProcessWorker
            P(Proc.AppId).Appid = Proc.AppId
            P(Proc.AppId).AppToStart = Proc.AppPath
            P(Proc.AppId).Arguments = Proc.Params
            P(Proc.AppId).WorkingDirectory = Proc.WorkingDirectory
            P(Proc.AppId).StartSignal = Proc.StartSignal
            P(Proc.AppId).Cores = CInt(2^Proc.Cores) - 1 'processaffinity is set bitwise 
            P(Proc.AppId).SSMTEnd = Proc.StartsignalMaxTime

            P(Proc.AppId).UpgradeSignal = Proc.UpgradeSignal
            P(Proc.AppId).UpgradeCmd = Proc.UpgradeCmd

            AddHandler P(Proc.AppId).UpdateConsole, AddressOf ProcUpdate
            Dim trda As Thread
            trda = New Thread(AddressOf P(Proc.AppId).Work)
            trda.IsBackground = True
            trda.Start()
            trda = Nothing
            Do
                Thread.Sleep(1000)
                If P(Proc.AppId).IsRunning Then Exit Do
                If P(Proc.AppId).State <> QGlobal.ProcOp.Running Then Exit Do
            Loop
            If P(Proc.AppId).State <> QGlobal.ProcOp.Running Then Exit For 'abort
        Next
    End Sub


    Public Sub StopProcess(Appid As Integer)
        Try
            If Not IsNothing(P(Appid)) Then
                P(Appid).StopProc()
            End If
        Catch ex As Exception
            Generic.WriteDebug(ex)
        End Try
    End Sub

    Public Sub StopProcessSquence(Appid() As Object)
        Dim trda As Thread
        trda = New Thread(AddressOf StopPS)
        trda.IsBackground = True
        trda.Start(Appid)
        trda = Nothing
    End Sub

    Private Sub StopPS(pId() As Object)

        For Each Appid As Integer In pId
            P(Appid).StopProc()
            Do
                Thread.Sleep(1000)
                If P(Appid).IsRunning = False Then Exit Do
            Loop
        Next
    End Sub

    Private Sub ProcUpdate(AppId As Integer, Operation As Integer, Data As String)
        'Streamlineing threads output
        Select Case Operation
            Case QGlobal.ProcOp.Running
                RaiseEvent Started(AppId)
            Case QGlobal.ProcOp.FoundSignal
                RaiseEvent Update(AppId, Operation, Data)
            Case QGlobal.ProcOp.Stopping
                RaiseEvent Update(AppId, Operation, Data)
            Case QGlobal.ProcOp.Stopped
                RaiseEvent Stopped(AppId)
            Case QGlobal.ProcOp.Err
                RaiseEvent Aborting(AppId, Data)
            Case QGlobal.ProcOp.ConsoleOut
                RaiseEvent Update(AppId, Operation, Data)
            Case QGlobal.ProcOp.ConsoleErr
                RaiseEvent Update(AppId, Operation, Data)
        End Select
    End Sub


    Public Class pSettings
        Public AppId As Integer = 0
        Public AppPath As String = ""
        Public Params As String = ""
        Public Cores As Integer = 0
        Public StartSignal As String = ""
        Public WorkingDirectory As String = ""
        Public StartsignalMaxTime As Integer = 300 '5 minutes
        Public UpgradeSignal As String = ""
        Public UpgradeCmd As String = ""
    End Class

    Private Class clsProcessWorker
        Public Event UpdateConsole(AppId As Integer, Operation As Integer, Data As String)

        Private p As Process
        Private OutputBuffer As String
        Private ErrorBuffer As String
        Private _state As Integer
        Public AppToStart As String
        Public WorkingDirectory As String
        Public Arguments As String
        Public Appid As Integer
        Public StartSignal As String
        Public FoundUpgradeSignal As Boolean
        Public Cores As Integer
        Public SSMTEnd As Integer
        Private Abort As Boolean

        Public FoundStartSignal As Boolean
        Public UpgradeSignal As String
        Public UpgradeCmd As String

        Private Enum CtrlTypes As UInteger
            CTRL_C_EVENT = 0
            CTRL_BREAK_EVENT
            CTRL_CLOSE_EVENT
            CTRL_LOGOFF_EVENT = 5
            CTRL_SHUTDOWN_EVENT
        End Enum

        Private Delegate Function ConsoleCtrlDelegate(CtrlType As CtrlTypes) As Boolean

        Private Declare Function AttachConsole Lib "kernel32"(dwProcessId As UInteger) As Boolean
        Private Declare Function GenerateConsoleCtrlEvent Lib "kernel32" Alias "GenerateConsoleCtrlEvent"(
                                                                                                          dwCtrlEvent As _
                                                                                                             Long,
                                                                                                          dwProcessGroupId _
                                                                                                             As Long) _
            As Long
        Private Declare Function SetConsoleCtrlHandler Lib "kernel32"(Handler As ConsoleCtrlDelegate, Add As Boolean) _
            As Boolean
        Private Declare Function FreeConsole Lib "kernel32"() As Boolean

        Private Sub ShutDown(SigIntSleep As Integer, SigKillSleep As Integer)
            Try
                FreeConsole()
            Catch ex As Exception
                Generic.WriteDebug(ex)
            End Try
            Try
                AttachConsole(p.Id)
                SetConsoleCtrlHandler(AddressOf OnExit, True)
                GenerateConsoleCtrlEvent(CtrlTypes.CTRL_C_EVENT, 0)
                p.WaitForExit(SigIntSleep) 'wait for exit before we release. if not we might get ourself terminated.
                If Not p.HasExited Then
                    p.Kill()
                    p.WaitForExit(SigKillSleep)
                End If
                FreeConsole()
                SetConsoleCtrlHandler(AddressOf OnExit, False)
            Catch ex As Exception
                Generic.WriteDebug(ex)
            End Try
            Try
                p.Kill() 'fixing no java cleanup.
            Catch ex As Exception
                Generic.WriteDebug(ex)
            End Try
        End Sub

        Private Function OnExit(CtrlType As CtrlTypes) As Boolean
            Return True
        End Function

        Public Sub Work()
            _state = QGlobal.ProcOp.Running
            FoundStartSignal = False
            FoundUpgradeSignal = False
            Abort = False
            p = New Process
            p.StartInfo.WorkingDirectory = WorkingDirectory
            p.StartInfo.Arguments = Arguments
            p.StartInfo.UseShellExecute = False

            p.StartInfo.RedirectStandardError = True
            p.StartInfo.CreateNoWindow = True

            AddHandler p.ErrorDataReceived, AddressOf ErroutHandler
            p.StartInfo.FileName = AppToStart

            Try

                p.Start()
                If Cores <> 0 Then p.ProcessorAffinity = CType(Cores, IntPtr)
                p.BeginErrorReadLine()


            Catch ex As Exception

                RaiseEvent UpdateConsole(Appid, QGlobal.ProcOp.Err, ex.Message)
                RaiseEvent UpdateConsole(Appid, QGlobal.ProcOp.Stopped, "")
                Abort = True
                Exit Sub
            End Try

            RaiseEvent UpdateConsole(Appid, QGlobal.ProcOp.Running, "")


            If StartSignal = "" Then FoundStartSignal = True
            Dim SSMTEndTime As Date = Now.AddSeconds(SSMTEnd)

            Do 'just wait and see if we have exit.
                If FoundStartSignal = False And Now > SSMTEndTime Then
                    RaiseEvent _
                        UpdateConsole(Appid, QGlobal.ProcOp.Err,
                                      "Process did not completely start in reasonable time. Shutting down.")
                    Exit Do
                End If
                Thread.Sleep(500)
                If p.HasExited Then Exit Do
                If Abort Then Exit Do
            Loop

            If Not p.HasExited Then
                RaiseEvent UpdateConsole(Appid, QGlobal.ProcOp.Stopping, "")
                ShutDown(300000, 30000) '5min and 30 sec
            End If
            _state = QGlobal.ProcOp.Stopped
            RaiseEvent UpdateConsole(Appid, QGlobal.ProcOp.Stopped, "")
        End Sub

        Public Function IsRunning() As Boolean
            Try
                If p.HasExited Then

                    Return False
                End If
            Catch ex As Exception
                Generic.WriteDebug(ex)
            End Try
            If FoundStartSignal Then Return True
            Return False
        End Function

        Public Function State() As Integer
            Return _state
        End Function

        Public Sub StopProc()
            Abort = True
        End Sub

        Public Function KillMe() As Boolean
            Try
                p.Kill()
                Return True
            Catch ex As Exception
                Generic.WriteDebug(ex)
            End Try
            Return False
        End Function

        Public Sub Cleanup()
            Try
                p = Nothing
                AppToStart = Nothing
                WorkingDirectory = Nothing
                Arguments = Nothing
                OutputBuffer = Nothing
                ErrorBuffer = Nothing

            Catch ex As Exception

            End Try
        End Sub

        Sub OutputHandler(sender As Object, e As DataReceivedEventArgs)
            If Not String.IsNullOrEmpty(e.Data) Then
                If UpgradeSignal.Length > 0 And e.Data.Contains(UpgradeSignal) Then FoundUpgradeSignal = True

                If FoundStartSignal = False Then
                    If StartSignal.Length > 0 And e.Data.Contains(StartSignal) Then

                        If FoundUpgradeSignal Then
                            RaiseEvent _
                                UpdateConsole(Appid, QGlobal.ProcOp.ConsoleOut,
                                              "Qbundle: Executing second stage upgrade. Please wait up to 5 minutes for completion.")
                            ExecuteUpgradeCmd()
                        End If
                        FoundStartSignal = True 'we have this before upgrade so we do not kill proces during upgrade
                        RaiseEvent UpdateConsole(Appid, QGlobal.ProcOp.FoundSignal, "")
                    End If
                End If

                RaiseEvent UpdateConsole(Appid, QGlobal.ProcOp.ConsoleOut, e.Data)
            End If
        End Sub

        Sub ErroutHandler(sender As Object, e As DataReceivedEventArgs)

            If Not String.IsNullOrEmpty(e.Data) Then

                If UpgradeSignal.Length > 0 And e.Data.Contains(UpgradeSignal) Then FoundUpgradeSignal = True

                If FoundStartSignal = False Then
                    If StartSignal.Length > 0 And e.Data.Contains(StartSignal) Then
                        If FoundUpgradeSignal Then
                            RaiseEvent _
                                UpdateConsole(Appid, QGlobal.ProcOp.ConsoleErr,
                                              "Qbundle: Executing second stage upgrade. Please wait up to 5 minutes for completion.")
                            ExecuteUpgradeCmd()
                        End If
                        FoundStartSignal = True 'we have this before upgrade so we do not kill proces during upgrade
                        RaiseEvent UpdateConsole(Appid, QGlobal.ProcOp.FoundSignal, "")
                    End If
                End If

                RaiseEvent UpdateConsole(Appid, QGlobal.ProcOp.ConsoleErr, e.Data)
            End If
        End Sub


        Private Sub ExecuteUpgradeCmd()
            Try
                Dim pr = New Process
                pr.StartInfo.WorkingDirectory = WorkingDirectory
                pr.StartInfo.Arguments = ""
                pr.StartInfo.UseShellExecute = False
                pr.StartInfo.CreateNoWindow = True
                pr.StartInfo.FileName = UpgradeCmd
                pr.Start()
                pr.WaitForExit(300000)
                pr = Nothing
            Catch ex As Exception
                Generic.WriteDebug(ex)
            End Try
        End Sub
    End Class
End Class
