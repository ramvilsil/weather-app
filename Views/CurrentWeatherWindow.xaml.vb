Imports Application.ViewModels

Namespace Views
    Partial Class CurrentWeatherWindow

        Public Sub New()
            InitializeComponent()

            DataContext = CType(Application.Current, Application).GetService(Of CurrentWeatherViewModel)()
        End Sub

        Private Sub Button_Click(sender As Object, e As RoutedEventArgs)
            Debug.WriteLine("Button clicked!")
        End Sub

    End Class
End Namespace