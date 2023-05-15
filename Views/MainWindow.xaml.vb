Namespace Views

    Class MainWindow

        Private Sub Submit(sender As Object, e As RoutedEventArgs)
            Dim name As String = txtName.Text
            Dim email As String = txtEmail.Text

            MessageBox.Show($"Name: {name}{Environment.NewLine}Email: {email}", "Form Data")
        End Sub

    End Class

End Namespace