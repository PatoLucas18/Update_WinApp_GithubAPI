Imports System.Text
Imports System.IO
Imports System.Threading.Tasks

Public Class Form1


    Private Sub btn_check_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_check.Click
        CheckUpdate.ShowDialog()
    End Sub

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        FindUpdatesToolStripMenuItem.Checked = My.Settings.updates
        Label1.Text = My.Application.Info.Version.ToString()
        ' Delete the old executable 
        If IO.File.Exists("temp_old") Then
            Try
                My.Computer.FileSystem.DeleteFile("temp_old")
            Catch ex As Exception

            End Try
        End If

        If FindUpdatesToolStripMenuItem.Checked = True Then
            YourAsyncEvent()
        End If
    End Sub

    Private Sub YourAsyncEvent()
        ' Synchronous code before the asynchronous operation
        Dim asynchronousTask As Task = YourAsyncOperation()
        asynchronousTask.ContinueWith(Sub(t)
                                          ' Code to execute after the asynchronous operation has finished
                                      End Sub, TaskScheduler.FromCurrentSynchronizationContext())
    End Sub
    Private Function YourAsyncOperation() As Task
        ' Asynchronous operation code
        Return Task.Factory.StartNew(Sub()
                                         ' More asynchronous code if necessary
                                         If FindUpdatesToolStripMenuItem.Checked = True Then
                                             Try
                                                 Dim githubAPI = CheckUpdate.get_githubAPI()
                                                 If CheckUpdate.compare(githubAPI) = True Then
                                                     Dim result As DialogResult = MessageBox.Show("An update is available. Do you want to see the details?", "Update", MessageBoxButtons.YesNo, MessageBoxIcon.Question)

                                                     If result = DialogResult.Yes Then
                                                         CheckUpdate.ShowDialog()
                                                     End If

                                                 End If
                                             Catch ex As Exception

                                             End Try
                                         End If
                                     End Sub)
    End Function


    Private Sub FindUpdatesToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FindUpdatesToolStripMenuItem.Click
        If FindUpdatesToolStripMenuItem.Checked = True Then
            FindUpdatesToolStripMenuItem.Checked = False
        Else
            FindUpdatesToolStripMenuItem.Checked = True
        End If
        My.Settings.updates = FindUpdatesToolStripMenuItem.Checked
        My.Settings.Save()
    End Sub

    Private Sub CheckUpdatesToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckUpdatesToolStripMenuItem.Click
        CheckUpdate.ShowDialog()
    End Sub

End Class
