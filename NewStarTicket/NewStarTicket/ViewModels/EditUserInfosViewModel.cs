using NewStarTicket.Models;
using NewStarTicket.Repositories;
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
    public class EditUserInfosViewModel : ViewModelBase
    {
        private Guid _idUser;
        private string _nameUser;
        private string _passwordUser;
        private string _emailUser;
        private int _userIdLevel;

        private IUserRepository userRepository;
        private IUserLevelRepository userLevelRepository;

        public Guid IdUser
        {
            get 
            { 
                return _idUser; 
            }
            set
            {
                _idUser = value;
                OnPropertyChanged(nameof(IdUser));
            }
        }
        public string NameUser
        {
            get
            {
                return _nameUser;
            }
            set
            {
                _nameUser = value;
                OnPropertyChanged(nameof(NameUser));
            }
        }
        public string PasswordUser
        {
            get
            {
                return _passwordUser;
            }
            set
            {
                _passwordUser = value;
                OnPropertyChanged(nameof(PasswordUser));
            }
        }
        public string EmailUser
        {
            get
            {
                return _emailUser;
            }
            set
            {
                _emailUser = value;
                OnPropertyChanged(nameof(EmailUser));
            }
        }
        public int UserIdLevel
        {
            get
            {
                return _userIdLevel;
            }
            set
            {
                _userIdLevel = value;
                OnPropertyChanged(nameof(UserIdLevel));
            }
        }

        public ObservableCollection<UserLevel> UserLevels { get; set; } = new ObservableCollection<UserLevel>();

        public ICommand SaveCommand { get; }
        public ICommand CancelCommand { get; }

        public EditUserInfosViewModel(User userToEdit)
        {
            userRepository = new UserRepository();
            userLevelRepository = new UserLevelRepository();

            IdUser = userToEdit.IdUser;
            NameUser = userToEdit.NameUser;
            PasswordUser = userToEdit.PasswordUser;
            EmailUser = userToEdit.EmailUser;
            UserIdLevel = userToEdit.UserIdLevel;

            LoadUserLevels();

            SaveCommand = new ViewModelCommand(ExecuteSaveCommand);
            CancelCommand = new ViewModelCommand(ExecuteCancelCommand);
        }

        private void LoadUserLevels()
        {
            var levels = userLevelRepository.GetAllUserLevel().ToList();
            UserLevels.Clear();
            foreach (var level in levels)
            {
                UserLevels.Add(level);
            }
        }

        private void ExecuteSaveCommand(object obj)
        {
            try
            {
                var updatedUser = new User
                {
                    IdUser = this.IdUser,
                    NameUser = this.NameUser,
                    PasswordUser = this.PasswordUser,
                    EmailUser = this.EmailUser,
                    UserIdLevel = this.UserIdLevel
                };

                // On appelle le repo pour update
                userRepository.Edit(new User { IdUser = this.IdUser }, updatedUser);

                MessageBox.Show("Utilisateur modifié avec succès.", "Succès", MessageBoxButton.OK, MessageBoxImage.Information);

                if (obj is Window w)
                    w.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors de la modification : {ex.Message}", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ExecuteCancelCommand(object obj)
        {
            if (obj is Window w)
                w.Close();
        }
    }
}
