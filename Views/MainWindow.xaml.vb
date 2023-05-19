Imports Application.ViewModels
Imports Application.Models
Imports System.Net
Imports System.Net.Http

Namespace Views

    Class MainWindow

        Private viewModel As MainWindowViewModel

        Public Sub New()
            InitializeComponent()
            Dim webClient As New WebClient()
            Dim httpClient As New HttpClient()
            viewModel = New MainWindowViewModel(webClient, httpClient)
            DataContext = viewModel
        End Sub

        Private Async Sub GetWeatherData(sender As Object, e As RoutedEventArgs)
            Dim ipAddress as String = viewModel.GetIpAddress()
            Dim currentWeatherData As CurrentWeather = Await viewModel.GetWeatherDataAsync(ipAddress)
            viewModel.Geolocation = currentWeatherData.Geolocation
        End Sub
    End Class

End Namespace