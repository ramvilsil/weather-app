Imports System.ComponentModel
Imports System.Net.Http
Imports Newtonsoft.Json
Imports Application.Models

Namespace ViewModels

    Public Class MainWindowViewModel
        Implements INotifyPropertyChanged

        Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged
        Private ReadOnly _httpClient As HttpClient

        Private Const _weatherServiceApiUrl As String = "https://weather-service-vbnet.azurewebsites.net/api/currentweather"

        Private _currentWeather As CurrentWeather
        Private _backgroundImage As String

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

        Public Property BackgroundImage As String
            Get
                Return _backgroundImage
            End Get
            Set(value As String)
                _backgroundImage = value
                OnPropertyChanged(NameOf(BackgroundImage))
            End Set
        End Property

        Protected Sub OnPropertyChanged(propertyName As String)
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(propertyName))
        End Sub

        Public Async Sub GetCurrentWeather()

            Try
                Dim response = Await _httpClient.GetAsync(_weatherServiceApiUrl)

                response.EnsureSuccessStatusCode()

                Dim content = Await response.Content.ReadAsStringAsync()

                _currentWeather = JsonConvert.DeserializeObject(Of CurrentWeather)(content)

            Catch ex As HttpRequestException
                Debug.WriteLine("An error occurred during the HTTP request: " & ex.Message)

            Catch ex As JsonException
                Debug.WriteLine("An error occurred while deserializing the JSON response: " & ex.Message)

            Catch ex As Exception
                Debug.WriteLine("An error occurred: " & ex.Message)

            End Try

            SetBackgroundImage(_currentWeather.Condition)

            OnPropertyChanged(NameOf(CurrentWeather))
            OnPropertyChanged(NameOf(BackgroundImage))
        End Sub

        Private Sub SetBackgroundImage(weatherCondition As String)
            Select Case weatherCondition
                Case "Cloudy"
                    _backgroundImage = "https://images.pexels.com/photos/1254736/pexels-photo-1254736.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1"
                Case "Sunny"
                    _backgroundImage = "https://images.pexels.com/photos/912364/pexels-photo-912364.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1"
                Case "Rainy"
                    _backgroundImage = "https://images.pexels.com/photos/166360/pexels-photo-166360.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1"
                Case "Snowy"
                    _backgroundImage = "https://images.pexels.com/photos/688660/pexels-photo-688660.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1"
            End Select
        End Sub

    End Class

End Namespace