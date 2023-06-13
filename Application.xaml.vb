Imports System.Net.Http
Imports Microsoft.Extensions.DependencyInjection
Imports Application.ViewModels

Class Application

    Private _serviceProvider As IServiceProvider
    Private _httpClient As HttpClient

    Protected Overrides Sub OnStartup(e As StartupEventArgs)
        _httpClient = New HttpClient()

        Dim services = New ServiceCollection()
        services.AddSingleton(Of HttpClient)(_httpClient)
        services.AddTransient(Of MainWindowViewModel)()

        _serviceProvider = services.BuildServiceProvider()

        MyBase.OnStartup(e)
    End Sub

    Public Function GetService(Of T)() As T
        Return _serviceProvider.GetRequiredService(Of T)()
    End Function

End Class