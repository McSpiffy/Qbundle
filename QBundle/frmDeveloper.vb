﻿Public Class frmDeveloper
    Private Sub LinkLabel1_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) _
        Handles LinkLabel1.LinkClicked
        Process.Start("https://forums.getburst.net/u/Quibus")
    End Sub

    Private Sub LinkLabel2_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) _
        Handles LinkLabel2.LinkClicked
        Process.Start("https://discord.gg/NnVBPJX")
    End Sub

    Private Sub LinkLabel3_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) _
        Handles LinkLabel3.LinkClicked
        Process.Start("https://github.com/Quibus-burst")
    End Sub

    Private Sub frmDeveloper_Load(sender As Object, e As EventArgs) Handles MyBase.Load
    End Sub
End Class