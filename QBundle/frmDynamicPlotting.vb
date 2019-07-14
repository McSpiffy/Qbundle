﻿Imports System.IO

Public Class frmDynamicPlotting
    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click

        'we do not care about settings if disabled
        If rDisable.Checked = True Then
            Q.settings.DynPlotEnabled = False
            Q.settings.SaveSettings()
            Me.Close()
        End If

        'we are enabled. lets make checks
        If Not Q.AppManager.IsAppInstalled("Xplotter") Then
            If _
                MsgBox("Xplotter is not installed yet. Do you want to download and install it now?",
                       MsgBoxStyle.Information Or MsgBoxStyle.YesNo, "Download Xplotter") = MsgBoxResult.Yes Then
                Me.Hide()
                Dim res As Boolean = Q.AppManager.InstallApp("Xplotter")
                Me.Show()
                If res = False Then Exit Sub
            Else
                Exit Sub
            End If
        End If

        Q.settings.DynPlotEnabled = True

        If rEnable.Checked = True Then

        Else
            Q.settings.DynPlotEnabled = False
        End If
        If radPoC2.Checked = True Then
            Q.settings.DynPlotType = 2
        Else
            Q.settings.DynPlotType = 1
        End If
        Q.settings.DynPlotPath = txtPath.Text
        Q.settings.DynPlotAcc = txtAccount.Text
        Q.settings.DynPlotSize = HSSize.Value
        Q.settings.DynPlotFree = trFreeSpace.Value
        Q.settings.DynPlotHide = chkHide.Checked
        Q.settings.DynThreads = nrThreads.Value
        Q.settings.DynRam = nrRam.Value
        Q.settings.SaveSettings()

        Me.Close()
    End Sub

    Private Sub frmDynamicPlotting_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If Q.settings.DynPlotEnabled = True Then
            rEnable.Checked = True
            rDisable.Checked = False
        End If

        Try
            nrThreads.Maximum = Environment.ProcessorCount
            nrRam.Maximum = CDec(Math.Round((My.Computer.Info.TotalPhysicalMemory/1024/1024/1024)))
        Catch ex As Exception

        End Try


        txtPath.Text = Q.settings.DynPlotPath
        txtAccount.Text = Q.settings.DynPlotAcc
        HSSize.Value = Q.settings.DynPlotSize
        trFreeSpace.Value = Q.settings.DynPlotFree
        chkHide.Checked = Q.settings.DynPlotHide
        nrThreads.Value = Q.settings.DynThreads
        nrRam.Value = Q.settings.DynRam

        Dim TotalSpace As Long
        If Q.settings.DynPlotEnabled Then
            Try
                TotalSpace = Generic.GetDiskspace(Q.settings.DynPlotPath) _
                ' My.Computer.FileSystem.GetDriveInfo(Q.settings.DynPlotPath).TotalSize  'bytes
                TotalSpace = CLng(Math.Floor(TotalSpace/1024/1024/1024)) 'GiB
                trFreeSpace.Minimum = 1
                trFreeSpace.Maximum = CInt(TotalSpace)
            Catch ex As Exception

            End Try
        End If
        txtPath.Text = Q.settings.DynPlotPath
        txtAccount.Text = Q.settings.DynPlotAcc
        HSSize.Value = Q.settings.DynPlotSize
        trFreeSpace.Value = Q.settings.DynPlotFree
        chkHide.Checked = Q.settings.DynPlotHide
        lblPlotSize.Text = HSSize.Value.ToString & "GiB"
        lblFreeSpace.Text = CStr(trFreeSpace.Value) & "GiB (" & Math.Floor((trFreeSpace.Value/TotalSpace)*100).ToString &
                            "%)"
        lstPlots.Items.Clear()
        If Q.settings.Plots <> "" Then
            Dim buffer() As String = Split(Q.settings.Plots, "|")
            For Each plot As String In buffer
                If plot.Length > 1 Then
                    lstPlots.Items.Add(plot)
                End If
            Next
        End If
        cmlAccounts.Items.Clear()
        Dim mnuitm As ToolStripMenuItem
        For Each account As clsAccounts.Account In Q.Accounts.AccArray
            mnuitm = New ToolStripMenuItem
            mnuitm.Name = account.AccountName
            mnuitm.Text = account.AccountName
            AddHandler (mnuitm.Click), AddressOf SelectAccountID
            cmlAccounts.Items.Add(mnuitm)
        Next
    End Sub

    Private Sub SelectAccountID(sender As Object, e As EventArgs)
        Dim mnuitm As ToolStripMenuItem = Nothing
        Try
            mnuitm = DirectCast(sender, ToolStripMenuItem)
        Catch ex As Exception
            Generic.WriteDebug(ex)
            Exit Sub
        End Try
        txtAccount.Text = Q.Accounts.GetAccountID(mnuitm.Text)
    End Sub

    Private Sub rDisable_CheckedChanged(sender As Object, e As EventArgs) Handles rDisable.Click
        pnlOnOff.Enabled = False
    End Sub

    Private Sub rEnable_CheckedChanged(sender As Object, e As EventArgs) Handles rEnable.CheckedChanged
        pnlOnOff.Enabled = True
    End Sub

    Private Sub btnPath_Click(sender As Object, e As EventArgs) Handles btnPath.Click
        Dim FD As New FolderBrowserDialog
        If FD.ShowDialog() = DialogResult.OK Then
            txtPath.Text = FD.SelectedPath
            If Path.GetPathRoot(FD.SelectedPath) = FD.SelectedPath Then
                MsgBox(
                    "Xplotter does not allow to plot directly to root path of a drive. Create a directory and put your plots in there.",
                    MsgBoxStyle.Exclamation Or MsgBoxStyle.OkOnly, "Wrong path")
            End If
            If Generic.DriveCompressed(FD.SelectedPath) Then
                Dim Msg = "The selected path is on a NTFS compressed drive or folder."
                Msg &= " This is not supported by Xplotter." & vbCrLf & vbCrLf
                MsgBox(Msg, MsgBoxStyle.Critical Or MsgBoxStyle.OkOnly, "Compressed drive")
            End If

            Dim TotalSpace As Long = Generic.GetDiskspace(txtPath.Text) _
            ' My.Computer.FileSystem.GetDriveInfo(txtPath.Text).TotalSize  'bytes
            TotalSpace = CLng(Math.Floor(TotalSpace/1024/1024/1024)) 'GiB
            trFreeSpace.Minimum = 1
            trFreeSpace.Maximum = CInt(TotalSpace)
            trFreeSpace.Value = CInt(TotalSpace/10) 'set 10% as start value
            lblFreeSpace.Text = CStr(TotalSpace/10) & "GiB (10%)"

        End If
    End Sub

    Private Sub HSSize_Scroll(sender As Object, e As EventArgs) Handles HSSize.ValueChanged
        lblPlotSize.Text = HSSize.Value.ToString & "GiB"
    End Sub

    Private Sub trFreeSpace_Scroll(sender As Object, e As EventArgs) Handles trFreeSpace.ValueChanged
        Try
            Dim TotalSpace As Long = Generic.GetDiskspace(txtPath.Text) _
            ' My.Computer.FileSystem.GetDriveInfo(txtPath.Text).TotalSize  'bytes
            TotalSpace = CLng(Math.Floor(TotalSpace/1024/1024/1024)) 'GiB
            lblFreeSpace.Text = CStr(trFreeSpace.Value) & "GiB (" &
                                Math.Floor((trFreeSpace.Value/TotalSpace)*100).ToString & "%)"
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnAccounts_Click(sender As Object, e As EventArgs) Handles btnAccounts.Click
        Try
            Me.cmlAccounts.Show(Me.btnAccounts, Me.btnAccounts.PointToClient(Cursor.Position))
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnImport_Click(sender As Object, e As EventArgs) Handles btnImport.Click
        Me.cmImport.Show(Me.btnImport, Me.btnImport.PointToClient(Cursor.Position))
    End Sub

    Private Sub btnRemove_Click(sender As Object, e As EventArgs) Handles btnRemove.Click
        If lstPlots.SelectedIndex = - 1 Then
            MsgBox("You need to select a plot to remove.", MsgBoxStyle.Information Or MsgBoxStyle.OkOnly,
                   "Nothing to remove")
            Exit Sub
        End If

        If _
            MsgBox("Are you sure you want to remove selected plot(s)?" & vbCrLf & "It will not be deleted from disk.",
                   MsgBoxStyle.Exclamation Or MsgBoxStyle.YesNo, "Remove plotfile") = MsgBoxResult.Yes Then
            Q.settings.Plots = ""
            For i = 0 To lstPlots.Items.Count - 1
                If Not lstPlots.GetSelected(i) = True Then
                    Q.settings.Plots &= lstPlots.Items.Item(i).ToString & "|"
                End If
            Next
            Q.settings.SaveSettings()
            UpdatePlotList()
        End If
    End Sub

    Private Sub ImportFileToolStripMenuItem_Click(sender As Object, e As EventArgs) _
        Handles ImportFileToolStripMenuItem.Click
        Dim ofd As New OpenFileDialog
        If ofd.ShowDialog = DialogResult.OK Then
            If File.Exists(ofd.FileName) Then
                lstPlots.Items.Add(ofd.FileName)
                Q.settings.Plots &= ofd.FileName & "|"
                Q.settings.SaveSettings()
            End If
        End If
    End Sub

    Private Sub ImportFolderToolStripMenuItem_Click(sender As Object, e As EventArgs) _
        Handles ImportFolderToolStripMenuItem.Click
        Dim ofd As New FolderBrowserDialog
        If ofd.ShowDialog = DialogResult.OK Then
            If Directory.Exists(ofd.SelectedPath) Then
                Dim fileEntries As String() = Directory.GetFiles(ofd.SelectedPath)
                For Each file As String In fileEntries
                    lstPlots.Items.Add(file)
                    Q.settings.Plots &= file & "|"
                Next

                Q.settings.SaveSettings()
            End If
        End If
    End Sub

    Private Sub lblSelectAll_Click(sender As Object, e As EventArgs) Handles lblSelectAll.Click
        If lstPlots.Items.Count <= 0 Then Exit Sub
        For i = 0 To lstPlots.Items.Count - 1
            Me.lstPlots.SetSelected(i, True)
        Next
    End Sub

    Private Sub lblDeselectAll_Click(sender As Object, e As EventArgs) Handles lblDeselectAll.Click
        If lstPlots.Items.Count <= 0 Then Exit Sub
        For i = 0 To lstPlots.Items.Count - 1
            Me.lstPlots.SetSelected(i, False)
        Next
    End Sub

    Private Sub UpdatePlotList()
        lstPlots.Items.Clear()
        If Q.settings.Plots <> "" Then
            Dim buffer() As String = Split(Q.settings.Plots, "|")
            For Each plot As String In buffer
                If plot.Length > 1 Then
                    lstPlots.Items.Add(plot)
                End If
            Next
        End If
    End Sub
End Class