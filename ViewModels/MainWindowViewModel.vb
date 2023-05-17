Imports System.Net.Http
Imports System.Text

Namespace ViewModels

    Public Class MainWindowViewModel

        Public Async Function PostDataAsync() As Task(Of String)
            Dim client As New HttpClient()
            Dim apiUrl As String = "http://127.0.0.1:8000/api/geolocation"

            Dim requestBody As String = "{ ""ip_address"": ""73.157.229.121"" }"
            Dim content As New StringContent(requestBody, Encoding.UTF8, "application/json")

            Dim response As HttpResponseMessage = Await client.PostAsync(apiUrl, content)

            If response.IsSuccessStatusCode Then
                Dim responseContent As String = Await response.Content.ReadAsStringAsync()
                Return responseContent
            Else
                Console.WriteLine("Error: " & response.StatusCode)
                Return String.Empty
            End If
        End Function

    End Class

End Namespace