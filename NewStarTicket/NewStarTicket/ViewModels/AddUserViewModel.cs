using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace NewStarTicket.ViewModels
{
    public class AddUserViewModel : ViewModelBase
    {
        // Commandes
        public ICommand CreateUserCommand { get; }
        public ICommand CloseThisWindowCommand { get; }

        public AddUserViewModel()
        {
            CreateUserCommand = new ViewModelCommand(ExecuteCreateUserCommand, CanExecuteCreateUserCommand);
            CloseThisWindowCommand = new ViewModelCommand(ExecuteCloseThisWindowCommand);
        }
        private void ExecuteCreateUserCommand(object obj)
        {

        }
        private bool CanExecuteCreateUserCommand(object obj)
        {
            return false;
        }
        private void ExecuteCloseThisWindowCommand(object obj)
        {
            if (obj is Window w)
            {
                w.Close();
                return;
            }

            var window = Application.Current.Windows.OfType<Window>().FirstOrDefault(x => x.DataContext == this);
            window?.Close();
        }
    }
}
