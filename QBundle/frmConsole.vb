﻿Public Class frmConsole
    Private Delegate Sub DUpdate([AppId] As Integer, [Operation] As Integer, [data] As String)

    Private Sub frmConsole_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            cmbLog.SelectedIndex = 0
            txtLog.Text = ""
            txtLog.AppendText(String.Join(vbCrLf, frmMain.Console(0)))
            AddHandler Q.ProcHandler.Update, AddressOf ProcEvents
        Catch ex As Exception
            Generic.WriteDebug(ex)
        End Try
    End Sub

    Private Sub frmMain_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        Try
            RemoveHandler Q.ProcHandler.Update, AddressOf ProcEvents
        Catch ex As Exception
            Generic.WriteDebug(ex)
        End Try
    End Sub

    Private Sub cmbLog_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbLog.SelectedIndexChanged
        Try
            txtLog.Text = String.Join(vbCrLf, frmMain.Console(cmbLog.SelectedIndex))

        Catch ex As Exception
            Generic.WriteDebug(ex)
        End Try
    End Sub

    Private Sub ProcEvents(AppId As Integer, Operation As Integer, data As String)
        If Me.InvokeRequired Then
            Dim d As New DUpdate(AddressOf ProcEvents)
            Me.Invoke(d, New Object() {AppId, Operation, data})
            Return
        End If
        'threadsafe here
        Try
            Select Case Operation
                Case QGlobal.ProcOp.ConsoleOut And QGlobal.ProcOp.ConsoleErr
                    If Generic.DebugMe = True Then
                        txtLog.AppendText(data & vbCrLf)
                    Else
                        If AppId = QGlobal.AppNames.MariaPortable And cmbLog.SelectedIndex = 1 Then
                            txtLog.AppendText(data & vbCrLf)
                        End If
                        If AppId = QGlobal.AppNames.BRS And cmbLog.SelectedIndex = 0 Then
                            txtLog.AppendText(data & vbCrLf)
                        End If
                    End If
            End Select
        Catch ex As Exception
            Generic.WriteDebug(ex)
        End Try
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        frmMain.Console(cmbLog.SelectedIndex).Clear()
        txtLog.Text = ""
    End Sub
End Class