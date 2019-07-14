Imports System.IO
Imports System.IO.Compression
Imports System.Net
Imports System.Threading

Public Class frmDownloadManager
    Private _Aborted As Boolean
    Friend DownloadName As String = ""
    Friend Url As String = ""
    Friend Unzip As Boolean = False

    Private Delegate Sub DProgress _
        ([Job] As Integer, [AppId] As Integer, [percent] As Integer, [Speed] As Integer, [lRead] As Long,
         [lLength] As Long)

    Private Delegate Sub DDone()

    Private Delegate Sub DAborting()

    Private Result As DialogResult = Nothing
    Private ReadOnly TimeElapsed As New Stopwatch

    Private Sub frmDownloadManager_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        lblSpeed.Text = "0 KB/sec"
        lblRead.Text = "0 / 0 bytes"
        lblProgress.Text = "0%"
        TimeElapsed.Start()
        Pb1.Value = 0
        DownloadFile()
    End Sub

    Public Sub Done()
        If InvokeRequired Then
            Dim d As New DDone(AddressOf Done)
            Invoke(d, New Object() {})
            Return
        End If

        lblProgress.Text = "100%"
        Pb1.Value = 100
        Try

        Catch ex As Exception
            Generic.WriteDebug(ex)
        End Try

        'we are done so close
        If Result = Nothing Then
            DialogResult = DialogResult.OK
        Else
            DialogResult = Result
        End If

        Close()
    End Sub

    Private Sub Aborting()
        If InvokeRequired Then
            Dim d As New DAborting(AddressOf Aborting)
            Invoke(d, New Object() {})
            Return
        End If
        If Result = Nothing Then Result = DialogResult.Abort
        DialogResult = Result
        Close()
    End Sub

    Private Sub Progress(Job As Integer, AppId As Integer, percent As Integer, Speed As Integer, lRead As Long,
                         lLength As Long)
        If InvokeRequired Then
            Dim d As New DProgress(AddressOf Progress)
            Invoke(d, New Object() {Job, AppId, percent, Speed, lRead, lLength})
            Return
        End If
        Dim TimeLeft As TimeSpan
        Select Case Job
            Case 0
                Try
                    TimeLeft = TimeSpan.FromMilliseconds((lLength - lRead)*TimeElapsed.ElapsedMilliseconds/lRead)
                Catch ex As Exception
                End Try

                lblStatus.Text = "Downloading " & DownloadName
                If Speed > 1024 Then
                    lblSpeed.Text = CStr(Math.Round(Speed/1024, 2)) & " MiB / sec"
                Else
                    lblSpeed.Text = CStr(Speed) & " KiB / sec"
                End If

                lblRead.Text = CStr(lRead) & " / " & CStr(lLength) & " bytes"
                Try
                    lblTime.Text = TimeLeft.Hours & "h " & TimeLeft.Minutes & "m " & TimeLeft.Seconds & "s"
                Catch ex As Exception
                End Try
            Case 1
                lblStatus.Text = "Extracting: " & DownloadName
                lblSpeed.Visible = False
                lblRead.Visible = False
        End Select
        lblProgress.Text = CStr(percent) & "%"
        Pb1.Value = percent
    End Sub

    Private Sub btnAbort_Click(sender As Object, e As EventArgs) Handles btnAbort.Click
        _Aborted = True

        DialogResult = DialogResult.Cancel
    End Sub

    Public Sub DownloadFile()
        _Aborted = False
        Dim trda As Thread
        trda = New Thread(AddressOf Download)
        trda.IsBackground = True
        trda.Start()
    End Sub

    Private Function Download() As Boolean
        Dim DLOk = False
        Dim filename As String = QGlobal.AppDir & Path.GetFileName(Url)
        Dim File As FileStream = Nothing

        Try
            Dim bBuffer(262143) As Byte '256k chunks download buffer
            Dim TotalRead As Long
            Dim iBytesRead
            Dim ContentLength As Long
            Dim percent

            ServicePointManager.Expect100Continue = true
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12
            Dim http As WebRequest = WebRequest.Create(Url)
            Dim WebResponse As WebResponse = http.GetResponse
            ContentLength = WebResponse.ContentLength
            Dim sChunks As Stream = WebResponse.GetResponseStream
            File = New FileStream(filename, FileMode.Create, FileAccess.Write)
            TotalRead = 0
            Dim SW As Stopwatch = Stopwatch.StartNew
            Dim speed = 0
            Do
                If _Aborted Then Exit Do 'we will return false 
                iBytesRead = sChunks.Read(bBuffer, 0, 262144)
                If iBytesRead = 0 Then Exit Do
                TotalRead += iBytesRead
                File.Write(bBuffer, 0, iBytesRead)
                If SW.ElapsedMilliseconds > 0 Then speed = CInt(TotalRead/SW.ElapsedMilliseconds)
                percent = CInt(Math.Round((TotalRead/ContentLength)*100, 0))
                Progress(0, 0, percent, speed, TotalRead, ContentLength)
            Loop
            File.Flush()
            sChunks.Close()
            DLOk = True
        Catch ex As Exception
            Generic.WriteDebug(ex)
        End Try
        Try
            File.Close()
        Catch ex As Exception
            Generic.WriteDebug(ex)
        End Try
        If Unzip Then
            Extract()
            DeleteFile()
        End If
        Done()
        Return True
    End Function

    Private Function Extract() As Boolean
        Dim AllOk = False
        Try
            Dim filename As String = QGlobal.AppDir & Path.GetFileName(Url)
            Dim target As String = QGlobal.AppDir
            ZipFile.ExtractToDirectory(filename, target)
            AllOk = True
        Catch ex As Exception
            Generic.WriteDebug(ex)
        End Try

        Return AllOk
    End Function

    Private Sub DeleteFile()
        Try
            Dim filename As String = QGlobal.AppDir & Path.GetFileName(Url)
            If File.Exists(filename) Then
                File.Delete(filename)
            End If
        Catch ex As Exception
            Generic.WriteDebug(ex)
        End Try
    End Sub
End Class