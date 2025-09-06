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
    public class AddUserViewModel : ViewModelBase
    {
        // Champs
        private string _newUserName;
        private string _newUserEmail;
        private int _selectedNewUserLevelId;
        private string _newUserPassword;

        private IUserRepository userRepository;
        private IUserLevelRepository userLevelRepository;

        // Commandes
        public ICommand CreateNewUserCommand { get; }
        public ICommand CloseThisWindowCommand { get; }

        // Attributs
        public string NewUserName
        {
            get
            {
                return _newUserName;
            }
            set
            {
                _newUserName = value;
                OnPropertyChanged(nameof(NewUserName));
            }
        }
        public string NewUserEmail
        {
            get
            {
                return _newUserEmail;
            }
            set
            {
                _newUserEmail = value;
                OnPropertyChanged(nameof(NewUserEmail));
            }
        }
        public int SelectedNewUserLevelId
        {
            get
            {
                return _selectedNewUserLevelId;
            }
            set
            {
                _selectedNewUserLevelId = value;
                OnPropertyChanged(nameof(SelectedNewUserLevelId));
            }
        }
        public string NewUserPassword
        {
            get
            {
                return _newUserPassword;
            }
            set
            {
                _newUserPassword = value;
                OnPropertyChanged(nameof(NewUserPassword));
            }
        }

        public ObservableCollection<UserLevel> UserLevels { get; set; } = new ObservableCollection<UserLevel>();

        public AddUserViewModel()
        {
            // Initialisations de repo
            userLevelRepository = new UserLevelRepository();
            userRepository = new UserRepository();

            // Chargement données
            LoadData();

            // Initialisation des commandes
            CreateNewUserCommand = new ViewModelCommand(ExecuteCreateNewUserCommand);
            CloseThisWindowCommand = new ViewModelCommand(ExecuteCloseThisWindowCommand);
        }
        private void LoadData()
        {
            var dbUserLevelList = new List<UserLevel>();

            dbUserLevelList = userLevelRepository.GetAllUserLevel().ToList();

            UserLevels.Clear();

            foreach (var level in dbUserLevelList)
            {
                UserLevels.Add(level);
            }
        }
        private void ExecuteCreateNewUserCommand(object obj)
        {
            if (SelectedNewUserLevelId == null)
            {
                MessageBox.Show("Aucun niveau sélectionné !");
                return;
            } 
            else
            {
                var newUser = new User()
                {
                    NameUser = NewUserName,
                    PasswordUser = NewUserPassword,
                    EmailUser = NewUserEmail,
                    UserIdLevel = this.SelectedNewUserLevelId
                };

                try
                {
                    userRepository.Add(newUser);
                    MessageBox.Show("L'utilisateur a bien été créé.", "Succès", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Erreur lors de la création de l'utilisateur : {ex.Message}", "Erreur creation utilisateur", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
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
