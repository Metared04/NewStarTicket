using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace NewStarTicket.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        // Champs
        private string _userName;
        private SecureString _password;
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
        public SecureString Password 
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
        public ICommand DisplayForgetPasswordScreen { get; }// Pour le mdp oublier

        // Constructeur
        public LoginViewModel()
        {
            LoginCommand = new ViewModelCommand(ExecuteLoginCommand, CanExecuteLoginCommand);
            ShowRegisterScreenCommand = new ViewModelCommand(ExecuteShowRegisterScreenCommand);
            DisplayForgetPasswordScreen = new ViewModelCommand(ExecuteDisplayForgetPasswordScreen);
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
            // TODO: Implémenter la logique de connexion
            try
            {
                // Ici vous ajouterez la logique pour vérifier les identifiants
                // avec votre service d'authentification

                // Exemple de structure :
                // var authService = new AuthenticationService();
                // var user = authService.Login(UserName, Password);
                // if (user != null)
                // {
                //     // Connexion réussie - naviguer vers l'écran principal
                //     IsViewVisible = false;
                // }
                // else
                // {
                //     ErrorMessage = "Identifiants incorrects";
                // }

                ErrorMessage = "Fonctionnalité à implémenter";
            }
            catch (Exception ex)
            {
                ErrorMessage = $"Erreur lors de la connexion : {ex.Message}";
            }
        }
        private void ExecuteShowRegisterScreenCommand(object obj)
        {
            // TODO: Navigation vers l'écran d'inscription
            // Exemple : 
            // var registerView = new RegisterView();
            // registerView.Show();
            // IsViewVisible = false;
            throw new NotImplementedException("Navigation vers RegisterView à implémenter");
        }

        private void ExecuteDisplayForgetPasswordScreen(object obj)
        {
            // TODO: Implémenter la récupération de mot de passe
            throw new NotImplementedException("Récupération de mot de passe à implémenter");
        }
    }
}
