Imports System.ComponentModel
Imports Application.Commands
Imports Application.Models
Imports Application.Services

Namespace ViewModels

    Public Class CurrentWeatherViewModel
        Implements INotifyPropertyChanged

        Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged
        Protected Sub OnPropertyChanged(propertyName As String)
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(propertyName))
        End Sub

        Private _submitGeolocation As ICommand
        Private _inputGeolocation As String

        Private _currentWeather As CurrentWeather
        Private _currentWeatherService As CurrentWeatherService

        Public Sub New(
            currentWeatherService As CurrentWeatherService
        )
            _currentWeatherService = currentWeatherService
            _submitGeolocation = New RelayCommand(AddressOf OnSubmitGeolocation, AddressOf CanSubmitGeolocation)
            InitializeAsync()
        End Sub

        Private Async Sub InitializeAsync()
            _currentWeather = Await _currentWeatherService.GetAsync()
            UpdateProperties()
        End Sub

        Private Sub UpdateProperties()
            OnPropertyChanged(NameOf(CurrentWeather))
            OnPropertyChanged(NameOf(Geolocation))
            OnPropertyChanged(NameOf(BackgroundImage))
        End Sub

        Private Async Sub OnSubmitGeolocation()
            _currentWeather = Await _currentWeatherService.GetAsync(_inputGeolocation)
            UpdateProperties()
        End Sub

        Private Function CanSubmitGeolocation() As Boolean
            Return Not String.IsNullOrWhiteSpace(_inputGeolocation)
        End Function

        Public ReadOnly Property SubmitGeolocation As ICommand
            Get
                If _submitGeolocation Is Nothing Then
                    _submitGeolocation = New RelayCommand(AddressOf OnSubmitGeolocation, AddressOf CanSubmitGeolocation)
                End If
                Return _submitGeolocation
            End Get
        End Property

        Public Property InputGeolocation As String
            Get
                Return _inputGeolocation
            End Get
            Set(value As String)
                If value <> _inputGeolocation Then
                    _inputGeolocation = value
                    OnPropertyChanged(NameOf(InputGeolocation))
                    CType(SubmitGeolocation, RelayCommand).RaiseCanExecuteChanged()
                End If
            End Set
        End Property

        Public Property CurrentWeather As CurrentWeather
            Get
                Return _currentWeather
            End Get
            Set(value As CurrentWeather)
                _currentWeather = value
            End Set
        End Property

        Public ReadOnly Property Geolocation As String
            Get
                If _currentWeather Is Nothing Then
                    Return Nothing
                End If

                Dim geolocationProps As New List(Of String)

                If Not String.IsNullOrEmpty(_currentWeather.Geolocation.City) Then geolocationProps.Add(_currentWeather.Geolocation.City)
                If Not String.IsNullOrEmpty(_currentWeather.Geolocation.Region) Then geolocationProps.Add(_currentWeather.Geolocation.Region)
                If Not String.IsNullOrEmpty(_currentWeather.Geolocation.Country) Then geolocationProps.Add(_currentWeather.Geolocation.Country)

                Dim geolocationString = String.Join(", ", geolocationProps)

                If String.IsNullOrEmpty(geolocationString) Then
                    Return Nothing
                End If

                Return geolocationString
            End Get
        End Property

        Public ReadOnly Property BackgroundImage As String
            Get
                If Not _currentWeather Is Nothing Then
                    Select Case _currentWeather.Condition
                        Case "Sunny"
                            Return "https://images.pexels.com/photos/912364/pexels-photo-912364.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1"
                        Case "Rainy"
                            Return "https://images.pexels.com/photos/166360/pexels-photo-166360.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1"
                        Case "Snowy"
                            Return "https://images.pexels.com/photos/688660/pexels-photo-688660.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1"
                    End Select
                End If

                'Default image
                Return "https://images.pexels.com/photos/1254736/pexels-photo-1254736.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1"
            End Get
        End Property

    End Class

End Namespace