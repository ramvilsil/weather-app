Imports System.Net.Http
Imports System.Text
Imports System.Net
Imports System.ComponentModel
Imports Newtonsoft.Json
Imports Application.Models

Namespace ViewModels

    Public Class MainWindowViewModel
        Implements INotifyPropertyChanged

        Private ReadOnly _webClient As WebClient
        Private ReadOnly _httpClient As HttpClient

        Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged

        Private _geolocation As String

        Public Sub New(webClient As WebClient, httpClient As HttpClient)
            _webClient = webClient
            _httpClient = httpClient
        End Sub

        Public Property Geolocation As String
            Get
                Return _geolocation
            End Get
            Set(value As String)
                _geolocation = value
                OnPropertyChanged("Geolocation")
            End Set
        End Property

        Protected Sub OnPropertyChanged(propertyName As String)
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(propertyName))
        End Sub

        Public Function GetIpAddress() As String
            Try
                Dim result As String = _webClient.DownloadString("https://api.ipify.org")
                Return result
            Catch ex As Exception
                Console.WriteLine(ex.Message)
            End Try
            Return Nothing
        End Function

        Public Async Function GetWeatherDataAsync(ipAddress As String) As Task(Of CurrentWeather)
            Dim apiUrl As String = "http://127.0.0.1:8000/api/weather"

            Dim requestBodyJson As String = JsonConvert.SerializeObject(
                New With
                {
                    .IpAddress = ipAddress
                }
            )

            Dim content As New StringContent(requestBodyJson, Encoding.UTF8, "application/json")

            Dim response As HttpResponseMessage = Await _httpClient.PostAsync(apiUrl, content)

            If response.IsSuccessStatusCode Then

                Dim responseContent As String = response.Content.ReadAsStringAsync().Result
                Dim currentWeatherData As CurrentWeather = JsonConvert.DeserializeObject(Of CurrentWeather)(responseContent)
                Return currentWeatherData

            Else
                Console.WriteLine("Error: " & response.StatusCode)
                Return Nothing
            End If
        End Function

    End Class

End Namespace