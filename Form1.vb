Imports System.Text
Imports System.Net
Imports System.IO


Public Class Form1

    Private Sub btn_check_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_check.Click
        CheckUpdate.ShowDialog()
    End Sub

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Label1.Text = My.Application.Info.Version.ToString()
        If IO.File.Exists("temp_old") Then
            My.Computer.FileSystem.DeleteFile("temp_old")
        End If
    End Sub
End Class
