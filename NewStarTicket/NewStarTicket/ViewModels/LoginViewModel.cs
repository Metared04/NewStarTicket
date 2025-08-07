using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace NewStarTicket.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        // Champs
        private string _userName;
        private string _password;
        private string _errorMessage;
        private bool _isViewVisible = true;

        public string UserName 
        {
            get 
            {
                return _userName;
            }
            set 
            { 
                _userName = value;
                OnPropertyChanged(nameof(UserName));
            } 
        }
        public string Password 
        { 
            get 
            { 
                return _password; 
            } 
            set 
            { 
                _password = value;
                OnPropertyChanged(nameof(Password));
            } 
        }
        public string ErrorMessage 
        { 
            get 
            { 
                return _errorMessage; 
            } 
            set 
            { 
                _errorMessage = value; 
                OnPropertyChanged(nameof(ErrorMessage));
            }
        }
        public bool IsViewVisible 
        { 
            get 
            { 
                return _isViewVisible; 
            }
            set 
            { 
                _isViewVisible = value;
                OnPropertyChanged(nameof(IsViewVisible));
            }
        }

        // Commandes
        public ICommand LoginCommand { get; }// Pour se loger
        public ICommand ShowRegisterScreenCommand { get; }// Pour passer sur l'ecran d'inscription
        public ICommand RecoverPasswordCommand { get; }// Pour le mdp oublier

        // Constructeur
        public LoginViewModel()
        {
            LoginCommand = new ViewModelCommand(ExecuteLoginCommand, CanExecuteLoginCommand);
            ShowRegisterScreenCommand = new ViewModelCommand(ExecuteShowRegisterScreenCommand);
        }
        private bool CanExecuteLoginCommand(object obj)
        {
            bool validData;
            if(string.IsNullOrWhiteSpace(UserName) || UserName.Length < 3 || Password == null || Password.Length < 3)
            {
                validData = false;
            } else
            {
                validData = true;
            }
                return validData;
        }

        private void ExecuteLoginCommand(object obj)
        {
            throw new NotImplementedException();
        }
        private void ExecuteShowRegisterScreenCommand(object obj)
        {
            throw new NotImplementedException();
        }
    }
}
