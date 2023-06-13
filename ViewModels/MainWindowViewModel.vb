Imports System.ComponentModel
Imports System.Net.Http
Imports Newtonsoft.Json
Imports Application.Models

Namespace ViewModels

    Public Class MainWindowViewModel
        Implements INotifyPropertyChanged

        Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged
        Private _currentWeather As CurrentWeather
        Private ReadOnly _httpClient As HttpClient

        Public Sub New(httpClient As HttpClient)
            _httpClient = httpClient
            _currentWeather = New CurrentWeather
            GetCurrentWeather()
        End Sub

        Public Property CurrentWeather As CurrentWeather
            Get
                Return _currentWeather
            End Get
            Set(value As CurrentWeather)
                _currentWeather = value
                OnPropertyChanged(NameOf(CurrentWeather))
            End Set
        End Property

        Protected Sub OnPropertyChanged(propertyName As String)
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(propertyName))
        End Sub

        Public Async Sub GetCurrentWeather()

            Dim apiUrl As String = "https://weather-service-vbnet.azurewebsites.net/api/currentweather"

            Dim currentWeather As CurrentWeather
            Try
                Dim response = Await _httpClient.GetAsync(apiUrl)

                response.EnsureSuccessStatusCode()

                Dim content = Await response.Content.ReadAsStringAsync()
                Dim data = JsonConvert.DeserializeObject(Of CurrentWeather)(content)

                currentWeather = data

            Catch ex As HttpRequestException
                Console.WriteLine("An error occurred during the HTTP request: " & ex.Message)

            Catch ex As JsonException
                Console.WriteLine("An error occurred while deserializing the JSON response: " & ex.Message)

            Catch ex As Exception
                Console.WriteLine("An error occurred: " & ex.Message)

            End Try

            _currentWeather = currentWeather
            OnPropertyChanged(NameOf(currentWeather))

        End Sub

    End Class

End Namespace