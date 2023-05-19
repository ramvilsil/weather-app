Imports Application.ViewModels
Imports Application.Models

Namespace Views

    Class MainWindow
        Private viewModel As New MainWindowViewModel()

        Public Sub New()
            InitializeComponent()
            DataContext = viewModel
        End Sub

        Private Async Sub GetWeatherData(sender As Object, e As RoutedEventArgs)
            Dim ipAddress as String = viewModel.GetIpAddress()
            Dim currentWeatherData As CurrentWeather = Await viewModel.GetWeatherDataAsync(ipAddress)
            viewModel.Geolocation = currentWeatherData.Geolocation
        End Sub
    End Class

End Namespace