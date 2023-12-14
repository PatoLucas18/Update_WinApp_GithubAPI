Imports System.Text
Imports System.Net
Imports System.IO
Imports Newtonsoft.Json
Imports System.Text.RegularExpressions
Imports Newtonsoft.Json.Linq

Public Class CheckUpdate

    Dim savePath As String

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        ' URL de la API de GitHub
        Dim url As String = "https://api.github.com/repos/PatoLucas18/Fix-Face-HD-Poly-Conv-Low-Poly-to-WE9/releases/latest"
        ServicePointManager.Expect100Continue = True
        ServicePointManager.SecurityProtocol = 3072

        Dim webRequest As WebRequest = webRequest.Create(url)
        Dim request As HttpWebRequest = CType(webRequest, HttpWebRequest)
        request.Method = "GET"
        request.ContentType = "application/json"
        request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/58.0.3029.110 Safari/537.3"

        Dim response As HttpWebResponse = CType(request.GetResponse(), HttpWebResponse)

        Using reader As StreamReader = New StreamReader(response.GetResponseStream())
            Dim apiResp As String = reader.ReadToEnd()
            'Console.WriteLine(apiResp)
            Dim githubAPI = JsonConvert.DeserializeObject(Of GithubAPI)(apiResp)
            Debug.WriteLine(githubAPI.url)
            Debug.WriteLine(githubAPI.tag_name)
            For Each asset As Asset In githubAPI.assets
                Debug.WriteLine(asset.browser_download_url)
                ' Descargar el archivo
                Dim downloadUrl As String = asset.browser_download_url
                savePath = Path.GetFileName(downloadUrl)
                'Dim savePath As String = "Update_WinApp.exe"

                My.Computer.FileSystem.RenameFile("Update_WinApp.exe", "Update_WinApp_old.exe")
                IO.File.Copy("Fix.Face.HD.Poly.Conv.Low.Poly.to.WE9.exe", "Update_WinApp.exe", True)

                'Using webClient As New WebClient()
                '    webClient.DownloadFile(downloadUrl, savePath)
                '    Debug.WriteLine("Archivo descargado en: " & savePath)

                '    '' Ruta del archivo ZIP a descomprimir
                '    ''Dim zipFilePath As String = "C:\Ruta\del\archivo.zip"
                '    '' Carpeta donde deseas extraer los archivos
                '    'Dim extractFolder As String = savePath & "_temp"

                '    '' Descomprimir el archivo ZIP
                '    'ZipFile.ExtractToDirectory(savePath, extractFolder)

                '    'Console.WriteLine("Archivo ZIP descomprimido con éxito en la carpeta: " & extractFolder)

                'End Using


                Using webClient As New WebClient()
                    ' Agregar el evento DownloadProgressChanged
                    AddHandler webClient.DownloadProgressChanged, AddressOf WebClientDownloadProgressChanged
                    AddHandler webClient.DownloadFileCompleted, AddressOf WebClientDownloadCompleted


                    ' Descargar el archivo
                    webClient.DownloadFileAsync(New Uri(downloadUrl), savePath)

                    ' Puedes bloquear el hilo actual hasta que se complete la descarga si es necesario
                    ' webClient.DownloadFile(New Uri(downloadUrl), savePath)
                End Using
            Next
        End Using

        ' Esperar a que el usuario presione una tecla antes de cerrar la aplicación
        Console.ReadLine()
    End Sub


    ' Evento para manejar el progreso de la descarga
    Private Sub WebClientDownloadProgressChanged(ByVal sender As Object, ByVal e As DownloadProgressChangedEventArgs)
        ' e.BytesReceived contiene la cantidad de bytes descargados hasta ahora
        ' e.TotalBytesToReceive contiene la cantidad total de bytes que se esperan
        ' Puedes utilizar estos valores para calcular el porcentaje de progreso

        ' Por ejemplo, calcular el progreso y actualizar una barra de progreso:
        Dim progressPercentage As Integer = CInt((e.BytesReceived / e.TotalBytesToReceive) * 100)

        ' Actualizar la barra de progreso en tu interfaz de usuario
        ' (Asegúrate de invocar la actualización en el hilo de la interfaz de usuario si es necesario)
        UpdateProgressBar(progressPercentage)
    End Sub

    ' Método para actualizar la barra de progreso en la interfaz de usuario
    Private Sub UpdateProgressBar(ByVal progressPercentage As Integer)
        ' Aquí debes agregar el código para actualizar tu barra de progreso
        ' Por ejemplo, si tienes una ProgressBar llamada progressBar1, puedes hacer algo como:
        ProgressBar1.Value = progressPercentage
    End Sub


    ' Evento para manejar la finalización de la descarga
    Private Sub WebClientDownloadCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.AsyncCompletedEventArgs)
        ' Verificar si la descarga se completó sin errores
        If e.Error Is Nothing Then
            ' Mostrar un mensaje de éxito
            MessageBox.Show("Descarga completada exitosamente.", "Descarga completada se reiniciara el programa", MessageBoxButtons.OK, MessageBoxIcon.Information)

            ' Aquí puedes agregar código adicional después de la descarga exitosa
            ' Por ejemplo, descomprimir el archivo o realizar otras operaciones

            If IO.File.Exists(savePath) Then

                applyUpdate(savePath)
            End If

        Else
            ' Mostrar un mensaje de error en caso de fallo
            MessageBox.Show("Error durante la descarga: " & e.Error.Message, "Error de descarga", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If

        ' Puedes agregar más código aquí según sea necesario

        ' Por ejemplo, cerrar la aplicación después de la descarga
        ' Application.Exit()
    End Sub
    Private Sub CheckUpdate_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load


        lb_current.Text = My.Application.Info.Version.ToString()
        'MsgBox("Versión del programa: " & version)

        ' URL de la API de GitHub
        Dim url As String = "https://api.github.com/repos/PatoLucas18/Fix-Face-HD-Poly-Conv-Low-Poly-to-WE9/releases/latest"
        ServicePointManager.Expect100Continue = True
        ServicePointManager.SecurityProtocol = 3072

        Dim webRequest As WebRequest = webRequest.Create(url)

        Dim request As HttpWebRequest = CType(webRequest, HttpWebRequest)
        request.Method = "GET"
        request.ContentType = "application/json"
        request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/58.0.3029.110 Safari/537.3"

        Dim response As HttpWebResponse = CType(request.GetResponse(), HttpWebResponse)

        Using reader As StreamReader = New StreamReader(response.GetResponseStream())
            Dim apiResp As String = reader.ReadToEnd()
            'Console.WriteLine(apiResp)
            Dim githubAPI = JsonConvert.DeserializeObject(Of GithubAPI)(apiResp)
            Debug.WriteLine(githubAPI.url)
            Debug.WriteLine(githubAPI.tag_name)
            Debug.WriteLine(githubAPI.body)

            Label3.Text = "Notes:" & vbCrLf & githubAPI.body

            Dim versionString As String = githubAPI.tag_name
            lb_last.Text = versionString.Replace("v", "")

            'Dim version As String = My.Application.Info.Version.ToString()
            'MsgBox("Versión del programa: " & version)

            Dim githubVersion As New Version(versionString)
            ' Obtener la versión de la aplicación
            Dim appVersion As Version = My.Application.Info.Version

            ' Comparar las versiones
            Dim comparisonResult As Integer = appVersion.CompareTo(githubVersion)

            If comparisonResult < 0 Then
                lb_update.Text = ("Hay una actualizacion Diponible. ¿Quiere Descargarla?.")
            ElseIf comparisonResult > 0 Then
                lb_update.Text = ("La versión de la aplicación es más reciente que la versión de GitHub.")
            Else
                lb_update.Text = ("La versión de la aplicación está actualizada.")
            End If

        End Using



    End Sub

    Public Sub applyUpdate(ByVal rutaArchivoZip As String)
        Dim executablePath As String = Application.ExecutablePath
        Dim executableName As String = Path.GetFileName(executablePath) & "_old"

        Console.WriteLine("Nombre del ejecutable actual: " & executableName)

        If IO.File.Exists("temp_old") Then
            My.Computer.FileSystem.DeleteFile("temp_old")
        End If

        ' Ruta al archivo ZIP
        ' Ruta de destino para la extracción
        My.Computer.FileSystem.RenameFile(executablePath, "temp_old")

        ' Construir el comando PowerShell
        Dim comandoPowerShell As String = String.Format("Expand-Archive -Path '{0}' -DestinationPath '{1}'", rutaArchivoZip, Application.StartupPath)

        ' Configurar el proceso para ejecutar PowerShell
        Dim proceso As New Process()
        Dim inicioInfo As New ProcessStartInfo("powershell.exe")

        ' Configurar la redirección de la entrada y salida estándar
        inicioInfo.RedirectStandardInput = True
        inicioInfo.RedirectStandardOutput = True
        inicioInfo.UseShellExecute = False
        inicioInfo.CreateNoWindow = True

        proceso.StartInfo = inicioInfo

        ' Iniciar el proceso
        proceso.Start()

        ' Ejecutar el comando PowerShell
        Dim entradaStreamWriter As StreamWriter = proceso.StandardInput
        Dim salidaStreamReader As StreamReader = proceso.StandardOutput

        entradaStreamWriter.WriteLine(comandoPowerShell)
        entradaStreamWriter.Close()

        ' Esperar a que el proceso de PowerShell termine
        proceso.WaitForExit()

        ' Cerrar el proceso de PowerShell
        proceso.Close()

        ' Mostrar un mensaje (opcional)
        MessageBox.Show("Actualizacion completada. Se Reiniciará el programa")

        ' Reiniciar la aplicación
        Application.Restart()
    End Sub


    Public Sub applyUpdate2(ByVal rutaArchivoZip As String)
        Dim executablePath As String = Application.ExecutablePath
        Dim executableName As String = Path.GetFileName(executablePath) & "_old"

        Console.WriteLine("Nombre del ejecutable actual: " & executableName)


        If IO.File.Exists("temp_old") Then
            My.Computer.FileSystem.DeleteFile("temp_old")
        End If

        ' Ruta al archivo ZIP
        ' Ruta de destino para la extracción
        My.Computer.FileSystem.RenameFile(executablePath, "temp_old")

        ' Construir el comando PowerShell
        Dim comandoPowerShell As String = String.Format("Expand-Archive -Path '{0}' -DestinationPath '{1}'", rutaArchivoZip, Application.StartupPath)

        ' Configurar el proceso para ejecutar PowerShell
        Dim proceso As New Process()
        Dim inicioInfo As New ProcessStartInfo("powershell.exe")

        ' Configurar la redirección de la entrada y salida estándar
        inicioInfo.RedirectStandardInput = True
        inicioInfo.RedirectStandardOutput = True
        inicioInfo.UseShellExecute = False
        inicioInfo.CreateNoWindow = True

        proceso.StartInfo = inicioInfo

        ' Iniciar el proceso
        proceso.Start()

        ' Ejecutar el comando PowerShell
        Dim entradaStreamWriter As StreamWriter = proceso.StandardInput
        Dim salidaStreamReader As StreamReader = proceso.StandardOutput

        entradaStreamWriter.WriteLine(comandoPowerShell)
        entradaStreamWriter.Close()

        ' Esperar a que el proceso termine
        proceso.WaitForExit()

        ' Cerrar el proceso
        proceso.Close()

        ' Mostrar un mensaje (opcional)
        MessageBox.Show("Actualizacion completada. Se Reiniciará el programa")

        Application.Restart()
    End Sub
End Class
