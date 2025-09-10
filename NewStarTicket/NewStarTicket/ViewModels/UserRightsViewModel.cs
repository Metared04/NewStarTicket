using NewStarTicket.Models;
using NewStarTicket.Repositories;
using NewStarTicket.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace NewStarTicket.ViewModels
{
    public class UserRightsViewModel : ViewModelBase
    {
        private ObservableCollection<User> _currentUserList;
        private UserAccountModel _currentUser;

        private bool _isCurrentUserAdmin;

        private ViewModelBase _userInfosView;
        private ViewModelBase _editInfosView;

        private IUserRepository userRepository;

        public ObservableCollection<User> CurrentUserList
        {
            get
            {
                return _currentUserList;
            }
            set
            {
                _currentUserList = value;
                OnPropertyChanged(nameof(CurrentUserList));
            }
        }
        public UserAccountModel CurrentUser
        {
            get
            {
                return _currentUser;
            }
            set
            {
                _currentUser = value;
                OnPropertyChanged(nameof(CurrentUser));
            }
        }
        public bool IsCurrentUserAdmin
        {
            get
            {
                return _isCurrentUserAdmin;
            }
            set
            {
                _isCurrentUserAdmin = value;
                OnPropertyChanged(nameof(IsCurrentUserAdmin));
            }
        }
        public ViewModelBase UserInfosView
        {
            get
            {
                return _userInfosView;
            }
            set
            {
                _userInfosView = value;
                OnPropertyChanged(nameof(UserInfosView));
            }
        }
        public ViewModelBase EditInfosView
        {
            get
            {
                return _editInfosView;
            }
            set
            {
                _editInfosView = value;
                OnPropertyChanged(nameof(EditInfosView));
            }
        }

        // Commandes
        public ICommand ShowUserInfosCommand { get; }
        public ICommand EditUserInfosCommand { get; }
        public ICommand DeleteUserCommand { get; }

        public UserRightsViewModel(UserAccountModel currentUser)
        {
            CurrentUser = currentUser;
            userRepository = new UserRepository();

            // Initialisation des commandes

            ShowUserInfosCommand = new ViewModelCommand(ExecuteShowUserInfosCommand);
            EditUserInfosCommand = new ViewModelCommand(ExecuteEditUserInfosCommand, CanExecuteEditUserInfosCommand);
            DeleteUserCommand = new ViewModelCommand(ExecuteDeleteUserCommand, CanExecuteDeleteUserCommand);

            // Chargement donnees

            LoadUserList();
        }
        private void LoadUserList()
        {
            var userList = new List<User>();
            userList = userRepository.GetByAll().ToList();
            CurrentUserList = new ObservableCollection<User>(userList);
        }
        private void ExecuteShowUserInfosCommand(object obj)
        {
            MessageBox.Show("Je dois montrer.");
            /*
            var ticket = obj as Ticket;
            UserInfosView = new UserDetailsViewModel(user);
            var window = new UserInfosView
            {
                DataContext = UserInfosView,
                Owner = Application.Current.MainWindow
            };
            window.ShowDialog();
            */
        }
        private void ExecuteEditUserInfosCommand(object obj)
        {
            var user = obj as User;
            if (user == null) return;

            var EditInfosView = new EditUserInfosViewModel(user);
            var window = new EditUserInfosView
            {
                DataContext = EditInfosView,
                Owner = Application.Current.MainWindow
            };
            window.ShowDialog();
            LoadUserList();
        }
        private bool CanExecuteEditUserInfosCommand(object obj)
        {
            return (CurrentUser.AdminLevel >= 7);
        }
        private void ExecuteDeleteUserCommand(object obj)
        {
            var user = obj as User;
            if (user == null) return;
            var result = MessageBox.Show($"Supprimer \"{user.NameUser}\" ?", "Confirmation suppression", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result != MessageBoxResult.Yes) return;
            try
            {
                userRepository.Remove(user.IdUser);
                CurrentUserList.Remove(user);
                MessageBox.Show("Utilisateur supprimé avec succès.", "Succès", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors de la suppression : {ex}", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private bool CanExecuteDeleteUserCommand(object obj)
        {
            return (CurrentUser.AdminLevel >= 7);
        }
    }
}
