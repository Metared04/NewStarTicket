using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using NewStarTicket.Models;
using NewStarTicket.Repositories;

namespace NewStarTicket.ViewModels
{
    public class MainViewModel: ViewModelBase
    {
        // Champs
        private UserAccountModel _currentUserAccount;
        private IUserRepository userRepository;

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
        public MainViewModel()
        {
            userRepository = new UserRepository();
            CurrentUserAccount = new UserAccountModel();
            LoadCurrentUserData();
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
