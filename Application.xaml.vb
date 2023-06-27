Imports System.Net.Http
Imports System.Configuration
Imports Microsoft.Extensions.DependencyInjection
Imports Application.Services
Imports Application.ViewModels

Class Application

    Private _serviceProvider As IServiceProvider

    Protected Overrides Sub OnStartup(e As StartupEventArgs)
        Dim services = New ServiceCollection()

        'Register configuration variables
        Dim weatherServiceApiUrl As String = ConfigurationManager.AppSettings("WeatherServiceApi.Url")
        services.AddSingleton(Of String)(weatherServiceApiUrl)

        'Register services
        services.AddSingleton(Of HttpClient)(New HttpClient())
        services.AddTransient(Of CurrentWeatherService)

        services.AddTransient(Of CurrentWeatherViewModel)

        _serviceProvider = services.BuildServiceProvider()

        MyBase.OnStartup(e)
    End Sub

    Public Function GetService(Of T)() As T
        Return _serviceProvider.GetRequiredService(Of T)()
    End Function

End Class