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
        private string _caption;

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
                OnPropertyChanged(nameof(IsAdmin));
            }
        }

        public bool IsAdmin => CurrentUserAccount?.IsAdmin ?? false;

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

        public string Caption
        {
            get
            {
                return _caption;
            }
            set
            {
                _caption = value;
                OnPropertyChanged(nameof(Caption));
            }
        }

        // Commandes

        public ICommand ShowTicketListCommand { get; }
        public ICommand ShowDashboardCommand { get; }
        public ICommand ShowStatisticCommand { get; }

        public MainViewModel()
        {
            userRepository = new UserRepository();
            CurrentUserAccount = new UserAccountModel();

            // Initialisation des commandes

            ShowTicketListCommand = new ViewModelCommand(ExecuteShowTicketListCommand);
            ShowDashboardCommand = new ViewModelCommand(ExecuteShowDashboardCommand);
            ShowStatisticCommand = new ViewModelCommand(ExecuteShowStatisticCommand);

            // View par defaut

            ExecuteShowDashboardCommand(null);

            LoadCurrentUserData();
        }

        private void ExecuteShowStatisticCommand(object obj)
        {
            CurrentChildView = new StatisticViewModel();
            Caption = "Statistiques";
        }

        private void ExecuteShowDashboardCommand(object obj)
        {
            CurrentChildView = new DashBoardViewModel();
            Caption = "Dashboard";
        }

        private void ExecuteShowTicketListCommand(object obj)
        {
            CurrentChildView = new TicketListViewModel(CurrentUserAccount);
            Caption = "Liste tickets";
        }
        /*
        private bool CanExecuteShowTicketListCommand(object obj)
        {
            return (IsAdmin);
        }
        */

        private void LoadCurrentUserData()
        {
            var user = userRepository.GetUserByUsername(Thread.CurrentPrincipal.Identity.Name);
            if(user != null)
            {
                CurrentUserAccount.Id = user.IdUser;
                CurrentUserAccount.Username = user.NameUser;
                CurrentUserAccount.DisplayName = $"Bonjour, {user.NameUser}, {user.EmailUser}, {user.IsAdmin}";
                CurrentUserAccount.ProfilePicture = null;
                CurrentUserAccount.IsAdmin = user.IsAdmin;
            } else
            {
                CurrentUserAccount.DisplayName="Utilisateur inconnu";
                //Application.Current.Shutdown();
            }
        }
    }
}
