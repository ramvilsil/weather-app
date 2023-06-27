Imports System.Net.Http
Imports System.Text
Imports Newtonsoft.Json
Imports Application.Models

Namespace Services

    Public Class CurrentWeatherService

        Private ReadOnly _weatherServiceApiUrl As String
        Private ReadOnly _httpClient As HttpClient

        Public Sub New(
            weatherServiceApiUrl As String,
            HttpClient As HttpClient
        )
            _weatherServiceApiUrl = weatherServiceApiUrl
            _httpClient = HttpClient
        End Sub

        Public Async Function GetAsync() As Task(Of CurrentWeather)
            Dim currentWeather As CurrentWeather = Nothing

            Try
                Dim response = Await _httpClient.GetAsync(_weatherServiceApiUrl)
                Debug.WriteLine($"----------{Environment.NewLine}API Response:{Environment.NewLine}{response.StatusCode}{Environment.NewLine}{Await response.Content.ReadAsStringAsync()}{Environment.NewLine}----------")
                response.EnsureSuccessStatusCode()

                Dim responseContent = Await response.Content.ReadAsStringAsync()

                currentWeather = JsonConvert.DeserializeObject(Of CurrentWeather)(responseContent)

            Catch ex As Exception
                Debug.WriteLine("An error occurred: " & ex.Message)
            End Try

            Return currentWeather
        End Function

        Public Async Function GetAsync(geolocation As String) As Task(Of CurrentWeather)
            Dim currentWeather As CurrentWeather = Nothing

            Dim currentWeatherRequestObj As String = JsonConvert.SerializeObject(New With {
                .Geolocation = geolocation
            })

            Dim requestContent = New StringContent(currentWeatherRequestObj, Encoding.UTF8, "application/json")

            Try
                Dim response = Await _httpClient.PostAsync(_weatherServiceApiUrl, requestContent)
                Debug.WriteLine($"----------{Environment.NewLine}API Response:{Environment.NewLine}{response.StatusCode}{Environment.NewLine}{Await response.Content.ReadAsStringAsync()}{Environment.NewLine}----------")
                response.EnsureSuccessStatusCode()

                Dim responseContent = Await response.Content.ReadAsStringAsync()

                currentWeather = JsonConvert.DeserializeObject(Of CurrentWeather)(responseContent)

            Catch ex As Exception
                Debug.WriteLine("An error occurred: " & ex.Message)
            End Try

            If currentWeather Is Nothing Then
                currentWeather = Await GetAsync()
            End If

            Return currentWeather
        End Function

    End Class

End Namespace