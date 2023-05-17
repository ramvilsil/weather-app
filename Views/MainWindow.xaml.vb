Imports Application.ViewModels

Namespace Views

    Class MainWindow

        Private Sub Submit(sender As Object, e As RoutedEventArgs)
            Dim name As String = txtName.Text
            Dim email As String = txtEmail.Text

            MessageBox.Show($"Name: {name}{Environment.NewLine}Email: {email}", "Form Data")
        End Sub

        Private Async Sub Button_Click(sender As Object, e As RoutedEventArgs)
            Dim viewModel As MainWindowViewModel = TryCast(DataContext, MainWindowViewModel)

            Dim response As String = Await viewModel.PostDataAsync()

            MessageBox.Show($"{response}")
        End Sub

    End Class

End Namespace