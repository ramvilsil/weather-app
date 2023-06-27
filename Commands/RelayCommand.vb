Namespace Commands
    Public Class RelayCommand
        Implements ICommand

        Private _execute As Action
        Private _canExecute As Func(Of Boolean)

        Public Sub New(execute As Action, canExecute As Func(Of Boolean))
            _execute = execute
            _canExecute = canExecute
        End Sub

        Public Event CanExecuteChanged As EventHandler Implements ICommand.CanExecuteChanged

        Public Sub Execute(parameter As Object) Implements ICommand.Execute
            _execute()
        End Sub

        Public Function CanExecute(parameter As Object) As Boolean Implements ICommand.CanExecute
            Return _canExecute.Invoke()
        End Function

        Public Sub RaiseCanExecuteChanged()
            RaiseEvent CanExecuteChanged(Me, EventArgs.Empty)
        End Sub
    End Class
End Namespace