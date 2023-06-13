Imports Application.ViewModels

Namespace Views
    Partial Class MainWindow
        Private viewModel As MainWindowViewModel

        Public Sub New()
            InitializeComponent()

            viewModel = CType(Application.Current, Application).GetService(Of MainWindowViewModel)()
            DataContext = viewModel
        End Sub

    End Class
End Namespace