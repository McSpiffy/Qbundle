﻿Imports System.IO.Pipes
Imports System.ServiceProcess
Imports System.Text
Imports System.Threading

' TODO remove this, it is deprecated
Public Class clsServiceHandler
    Public Event MonitorEvents(Operation As Integer, Data As String) '0 service information '1 pipe information
    Public Event Update(Pid As Integer, Status As Integer, Data As String) 'used to signal wallet status
    Public Event Stopped(Pid As Integer)

    Private pipeClient As NamedPipeClientStream
    Private PipeBuffer() As Byte
    Private ShouldPipeRun As Boolean

#Region " Public Service Subs / Functions  "

    Public Sub StartService(Optional ByVal Monitor As Boolean = True)
        If Not IsServiceRunning() And IsInstalled() Then
            Dim service = New ServiceController("Burst Service")
            If _
                service.Status.Equals(ServiceControllerStatus.Stopped) Or
                service.Status.Equals(ServiceControllerStatus.StopPending) Then
                If Generic.IsAdmin Then
                    service.Start()
                Else
                    Dim p = New Process
                    p.StartInfo.WorkingDirectory = QGlobal.BaseDir
                    p.StartInfo.Arguments = "StartService"
                    p.StartInfo.UseShellExecute = True
                    p.StartInfo.CreateNoWindow = True
                    p.StartInfo.FileName = Application.ExecutablePath
                    p.StartInfo.Verb = "runas"
                    p.Start()
                End If

                If Monitor Then
                    Dim trda As Thread
                    trda = New Thread(AddressOf WaitForStart)
                    trda.IsBackground = True
                    trda.Start()
                    trda = Nothing
                End If

            End If
        End If
    End Sub

    Public Sub StopService(Optional ByVal Monitor As Boolean = True)
        If IsServiceRunning() And IsInstalled() Then
            Dim service = New ServiceController("Burst Service")
            If _
                service.Status.Equals(ServiceControllerStatus.Running) Or
                service.Status.Equals(ServiceControllerStatus.StartPending) Then

                If Generic.IsAdmin Then
                    service.Stop()
                Else
                    Dim p = New Process
                    p.StartInfo.WorkingDirectory = QGlobal.BaseDir
                    p.StartInfo.Arguments = "StopService"
                    p.StartInfo.UseShellExecute = True
                    p.StartInfo.CreateNoWindow = True
                    p.StartInfo.FileName = Application.ExecutablePath
                    p.StartInfo.Verb = "runas"
                    p.Start()
                End If

                If Monitor Then
                    Dim trda As Thread
                    trda = New Thread(AddressOf WaitForStop)
                    trda.IsBackground = True
                    trda.Start()
                    trda = Nothing
                End If
            End If
        End If
    End Sub

    Public Function IsInstalled() As Boolean
        Try
            Dim service = New ServiceController("Burst Service")
            If service.Status = Nothing Then
                Return False
            End If
            Return True
        Catch ex As Exception

        End Try
        Return False
    End Function

    Public Function IsServiceRunning() As Boolean
        Try
            Dim service = New ServiceController("Burst Service")
            If service.Status.Equals(ServiceControllerStatus.Running) Then _
'Or service.Status.Equals(ServiceControllerStatus.StartPending)
                Return True
            End If
        Catch ex As Exception
        End Try
        Return False
    End Function

    Public Function IsServiceStopping() As Boolean
        Try
            Dim service = New ServiceController("Burst Service")
            If _
                service.Status.Equals(ServiceControllerStatus.Running) Or
                service.Status.Equals(ServiceControllerStatus.StartPending) Or
                service.Status.Equals(ServiceControllerStatus.StopPending) Then
                Return True
            End If
        Catch ex As Exception
        End Try
        Return False
    End Function

    Public Sub WaitForStart()

        Do
            Thread.Sleep(1000)
            If IsServiceRunning() Then
                Exit Do
            End If
        Loop
        RaiseEvent Update(QGlobal.AppNames.BRS, QGlobal.ProcOp.FoundSignal, "")
    End Sub

    Public Sub WaitForStop()
        RaiseEvent Update(QGlobal.AppNames.BRS, QGlobal.ProcOp.Stopping, "")
        Do
            Thread.Sleep(1000)
            If Not IsServiceStopping() Then
                Exit Do
            End If
        Loop
        RaiseEvent Stopped(QGlobal.AppNames.BRS)
    End Sub

#End Region

#Region " Public Wallet Sub / Functions "


    Public Function IsConnected() As Boolean
        Return True
    End Function

    Public Sub StartWallet()
    End Sub

    Public Sub StopWallet()
    End Sub

    Public Sub GetConsoleLogs()
    End Sub

    Public Sub SendCommands(data As String)
        Dim sendbuffer() As Byte = Encoding.UTF8.GetBytes(data)
        pipeClient.Write(sendbuffer, 0, sendbuffer.Length)
    End Sub

#End Region

#Region " Pipe Client "

    Public Sub RunPipeClient()
        ShouldPipeRun = True
        Dim trda As Thread
        trda = New Thread(AddressOf StopPipeClient)
        trda.IsBackground = True
        trda.Start()
        trda = Nothing
    End Sub

    Private Sub StopPipeClient()
        ShouldPipeRun = False
        If pipeClient.IsConnected Then
            pipeClient.Close()
            pipeClient.Dispose()
        End If
    End Sub

    Private Sub PipeMonitor()
        Do
            If Not pipeClient.IsConnected Then
                StartPipeClient()
            End If
            Thread.Sleep(1000)
            If ShouldPipeRun = False Then Exit Do
        Loop
    End Sub

    Private Sub StartPipeClient()
        Try
            If ShouldPipeRun = True Then
                pipeClient = New NamedPipeClientStream(".", "BurstPipe", PipeDirection.InOut, PipeOptions.Asynchronous)
                pipeClient.Connect(5000) '5000ms is huge!
                pipeClient.BeginRead(PipeBuffer, 0, PipeBuffer.Length, AddressOf IncPipeMessage, pipeClient)
            End If
        Catch ex As Exception
        End Try
    End Sub


    Private Sub IncPipeMessage(iar As IAsyncResult)
        Try
            Dim totalbytes As Integer = pipeClient.EndRead(iar) 'stop this asyncread and start new later
            Dim stringData As String = Encoding.UTF8.GetString(PipeBuffer, 0, totalbytes)
            RaiseEvent MonitorEvents(1, stringData)
            pipeClient.BeginRead(PipeBuffer, 0, PipeBuffer.Length, AddressOf IncPipeMessage, iar)
        Catch ex As Exception

        End Try
    End Sub

#End Region
End Class
