using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using NewStarTicket.Models;
using NewStarTicket.Repositories;

namespace NewStarTicket.ViewModels
{
    public class MainViewModel: ViewModelBase
    {
        // Champs
        private UserAccountModel _currentUserAccount;
        private ViewModelBase _currentChildView;

        private IUserRepository userRepository;

        // Propriétés

        public UserAccountModel CurrentUserAccount 
        { 
            get
            {
                return _currentUserAccount;
            }
            set
            {
                _currentUserAccount = value;
                OnPropertyChanged(nameof(CurrentUserAccount));
            }
        }

        public ViewModelBase CurrentChildView
        {
            get 
            {
                return _currentChildView; 
            }
            set
            {

                _currentChildView = value;
                OnPropertyChanged(nameof(CurrentChildView));
            }
        }

        // Commandes

        public ICommand ShowTicketListCommand { get; }

        public MainViewModel()
        {
            userRepository = new UserRepository();
            CurrentUserAccount = new UserAccountModel();

            // Initialisation des commandes

            ShowTicketListCommand = new ViewModelCommand(ExecuteShowTicketListCommand);

            // View par defaut

            ExecuteShowTicketListCommand(null);

            LoadCurrentUserData();
        }

        private void ExecuteShowTicketListCommand(object obj)
        {
            CurrentChildView = new TicketListViewModel();
        }

        private void LoadCurrentUserData()
        {
            var user = userRepository.GetUserByUsername(Thread.CurrentPrincipal.Identity.Name);
            if(user != null)
            {
                CurrentUserAccount.Username = user.NameUser;
                CurrentUserAccount.DisplayName = $"Bonjour, {user.NameUser}, {user.EmailUser}";
                CurrentUserAccount.ProfilePicture = null;
            } else
            {
                CurrentUserAccount.DisplayName="Utilisateur inconnu";
                //Application.Current.Shutdown();
            }
        }
    }
}
