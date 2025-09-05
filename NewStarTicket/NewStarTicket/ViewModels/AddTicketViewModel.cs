using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace NewStarTicket.ViewModels
{
    public class AddTicketViewModel : ViewModelBase
    {
        // Commandes
        public ICommand CreateTicketCommand { get; }
        public ICommand CloseWindowCommand { get; }


        public AddTicketViewModel()
        {
            // Initialisation des commandes 
            CreateTicketCommand = new ViewModelCommand(ExecuteCreateTicketCommand, CanExecuteCreateTicketCommand);
            CloseWindowCommand = new ViewModelCommand(ExecuteCloseWindowCommand);
        }

        private void ExecuteCreateTicketCommand(object obj)
        {

        }

        private bool CanExecuteCreateTicketCommand(object obj)
        {
            return false;
        }

        private void ExecuteCloseWindowCommand(object obj)
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
