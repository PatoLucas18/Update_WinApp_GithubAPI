
Imports System.Text
Imports System.Net
Imports System.IO
Imports Newtonsoft.Json
Imports System.Text.RegularExpressions
Imports Newtonsoft.Json.Linq

Public Class CheckUpdate
    Dim savePath As String

    ' GitHub API URL
    Public Shared url As String = "https://api.github.com/repos/PatoLucas18/Update_WinApp_GithubAPI/releases/latest"
    'Public Shared url As String = "http://localhost/latest.json"
    Dim githubAPI

    Public Shared Function get_githubAPI()
        ServicePointManager.Expect100Continue = True
        ServicePointManager.SecurityProtocol = 3072

        Dim webRequest As WebRequest = WebRequest.Create(url)

        Dim request As HttpWebRequest = CType(webRequest, HttpWebRequest)
        request.Method = "GET"
        request.ContentType = "application/json"
        request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/58.0.3029.110 Safari/537.3"

        Dim response As HttpWebResponse = CType(request.GetResponse(), HttpWebResponse)

        Using reader As StreamReader = New StreamReader(response.GetResponseStream())
            Dim apiResp As String = reader.ReadToEnd()
            'Console.WriteLine(apiResp)
            Dim githubAPI = JsonConvert.DeserializeObject(Of GithubAPI)(apiResp)
            Return githubAPI
        End Using
    End Function

    Public Function get_url(ByVal githubAPI) As String
        Debug.WriteLine(githubAPI.url)
        Return githubAPI.url
    End Function

    Public Function get_tag_name(ByVal githubAPI) As String
        Debug.WriteLine(githubAPI.tag_name)
        Return githubAPI.tag_name.ToString.Replace("v", "")
    End Function

    Public Function get_body(ByVal githubAPI) As String
        Debug.WriteLine(githubAPI.body)
        Return githubAPI.body
    End Function

    Public Function compare(ByVal githubAPI) As Boolean
        Dim versionString As String = githubAPI.tag_name.ToString.Replace("v", "")
        Dim githubVersion As New Version(versionString)

        ' Get the application version
        Dim appVersion As Version = My.Application.Info.Version

        ' Compare the versions
        Dim comparisonResult As Integer = appVersion.CompareTo(githubVersion)

        If comparisonResult < 0 Then
            'lb_update.Text = ("An update is available. Do you want to download it?")
            Return True
        Else
            'lb_update.Text = ("The application version is up-to-date.")
            Return False
        End If
    End Function

    Public Sub get_download_url(ByVal githubAPI)
        For Each asset As Asset In githubAPI.assets
            Debug.WriteLine(asset.browser_download_url)
            ' Download the file
            Dim downloadUrl As String = asset.browser_download_url
            savePath = Path.GetFileName(downloadUrl)

            Using webClient As New WebClient()
                ' Add the DownloadProgressChanged event
                AddHandler webClient.DownloadProgressChanged, AddressOf WebClientDownloadProgressChanged
                AddHandler webClient.DownloadFileCompleted, AddressOf WebClientDownloadCompleted

                ' Download the file
                webClient.DownloadFileAsync(New Uri(downloadUrl), savePath)

                ' You can block the current thread until the download is complete if necessary
                ' webClient.DownloadFile(New Uri(downloadUrl), savePath)
            End Using
        Next
    End Sub

    ' Download button click event
    Private Sub btn_download_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_download.Click
        Dim result As DialogResult = MessageBox.Show("Save your work, the program will restart, Do you want to continue?", "Update", MessageBoxButtons.YesNo, MessageBoxIcon.Question)

        If result = DialogResult.Yes Then

            get_download_url(githubAPI)
        End If

    End Sub

    ' Event to handle download progress
    Private Sub WebClientDownloadProgressChanged(ByVal sender As Object, ByVal e As DownloadProgressChangedEventArgs)
        ' e.BytesReceived contains the amount of bytes downloaded so far
        ' e.TotalBytesToReceive contains the total amount of bytes expected
        ' You can use these values to calculate the progress percentage

        ' For example, calculate the progress and update a progress bar:
        Dim progressPercentage As Integer = CInt((e.BytesReceived / e.TotalBytesToReceive) * 100)

        ' Update the progress bar in your user interface
        ' (Make sure to invoke the update on the user interface thread if necessary)
        UpdateProgressBar(progressPercentage)
    End Sub

    ' Method to update the progress bar in the user interface
    Private Sub UpdateProgressBar(ByVal progressPercentage As Integer)
        ' Add your code here to update your progress bar
        ' For example, if you have a ProgressBar named progressBar1, you can do something like:
        ProgressBar1.Value = progressPercentage
    End Sub

    ' Event to handle download completion
    Private Sub WebClientDownloadCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.AsyncCompletedEventArgs)
        ' Check if the download completed without errors
        If e.Error Is Nothing Then
            ' Show a success message
            MessageBox.Show("Download completed successfully. The program will restart.", "Download completed", MessageBoxButtons.OK, MessageBoxIcon.Information)

            ' You can add additional code here after successful download
            ' For example, unzip the file or perform other operations

            If IO.File.Exists(savePath) Then
                applyUpdate(savePath)
            End If
        Else
            ' Show an error message in case of failure
            MessageBox.Show("Error during download: " & e.Error.Message, "Download error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If

        ' You can add more code here as needed

        ' For example, close the application after download
        ' Application.Exit()
    End Sub

    ' Form loading with update check
    Private Sub CheckUpdate_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        githubAPI = get_githubAPI()
        lb_current.Text = My.Application.Info.Version.ToString()
        lb_last.Text = get_tag_name(githubAPI)

        If compare(githubAPI) = False Then
            lb_update.Text = ("The application version is up-to-date.")
            btn_download.Enabled = False
        Else
            btn_download.Enabled = True
            lb_update.Text = ("An update is available." & vbCrLf & "Do you want to download it?")
            RichTextBox1.Text = get_body(githubAPI)
        End If
    End Sub

    Public Sub applyUpdate(ByVal zipFilePath As String)
        Dim executablePath As String = Application.ExecutablePath
        Dim executableName As String = Path.GetFileName(executablePath) & "_old"

        Console.WriteLine("Current executable name: " & executableName)

        ' Path to the ZIP file
        ' Destination path for extraction
        My.Computer.FileSystem.RenameFile(executablePath, "temp_old")

        ' Build the PowerShell command
        Dim powershellCommand As String = String.Format("Expand-Archive -Path '{0}' -DestinationPath '{1}'", zipFilePath, Application.StartupPath)

        ' Configure the process to run PowerShell
        Dim process As New Process()
        Dim startInfo As New ProcessStartInfo("powershell.exe")

        ' Configure redirection of standard input and output
        startInfo.RedirectStandardInput = True
        startInfo.RedirectStandardOutput = True
        startInfo.UseShellExecute = False
        startInfo.CreateNoWindow = True

        process.StartInfo = startInfo

        ' Start the process
        process.Start()

        ' Run the PowerShell command
        Dim inputStreamWriter As StreamWriter = process.StandardInput
        Dim outputStreamReader As StreamReader = process.StandardOutput

        inputStreamWriter.WriteLine(powershellCommand)
        inputStreamWriter.Close()

        ' Wait for the PowerShell process to exit
        process.WaitForExit()

        ' Close the PowerShell process
        process.Close()

        ' Show a message (optional)
        MessageBox.Show("Update completed. The program will restart.")
        My.Computer.FileSystem.DeleteFile(zipFilePath)

        ' Restart the application
        Application.Restart()
    End Sub
End Class
