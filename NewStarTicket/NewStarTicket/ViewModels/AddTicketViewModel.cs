using Microsoft.Identity.Client;
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
    public class AddTicketViewModel : ViewModelBase
    {
        // Champs
        private string _ticketTitle;
        private string _ticketDescription;
        private int _selectedEmergencyLevelId;
        private int _selectedLocationId;
        private int _selectedEquipmentId;

        private Guid _currentUserId;
        private string _currentUserName;

        private ITicketRepository ticketRepository;
        private IEmergencyLevelRepository emergencyLevelRepository;
        private IEquipmentRepository equipmentRepository;
        private ILocationRepository locationRepository;
        private ITicketStatusRepository ticketStatusRepository;
        private IUserLevelRepository userLevelRepository;

        // Commandes
        public ICommand CreateTicketCommand { get; }
        public ICommand CloseWindowCommand { get; }

        public string TicketTitle
        {
            get
            {
                return _ticketTitle;
            }
            set
            {
                _ticketTitle = value;
                OnPropertyChanged(nameof(TicketTitle));
            }
        }
        public string TicketDescription
        {
            get
            {
                return _ticketDescription;
            }
            set
            {
                _ticketDescription = value;
                OnPropertyChanged(nameof(TicketDescription));
            }
        }
        public int SelectedEmergencyLevelId
        {
            get
            {
                return _selectedEmergencyLevelId;
            }
            set
            {
                _selectedEmergencyLevelId = value;
                OnPropertyChanged(nameof(_selectedEmergencyLevelId));
            }
        }
        public int SelectedLocationId
        {
            get
            {
                return _selectedLocationId;
            }
            set
            {
                _selectedLocationId = value;
                OnPropertyChanged(nameof(_selectedLocationId));
            }
        }
        public int SelectedEquipmentId
        {
            get
            {
                return _selectedEquipmentId;
            }
            set
            {
                _selectedEquipmentId = value;
                OnPropertyChanged(nameof(_selectedEquipmentId));
            }
        }
        public Guid CurrentUserId
        {
            get
            {
                return _currentUserId;
            }
            set
            {
                _currentUserId = value;
                OnPropertyChanged(nameof(_currentUserId));
            }
        }
        public ObservableCollection<EmergencyLevel> EmergencyLevels { get; set; } = new ObservableCollection<EmergencyLevel>();
        public ObservableCollection<Location> Locations { get; set; } = new ObservableCollection<Location>();
        public ObservableCollection<Equipment> Equipments { get; set; } = new ObservableCollection<Equipment>();
        public AddTicketViewModel(Guid currentUserId)
        {
            CurrentUserId = currentUserId;

            // Initialisation des repositories
            emergencyLevelRepository = new EmergencyLevelRepository();
            equipmentRepository = new EquipmentRepository();
            locationRepository = new LocationRepository();
            ticketRepository = new TicketRepository();

            // Chargement des donnees
            LoadAllData();

            // Initialisation des commandes 
            CreateTicketCommand = new ViewModelCommand(ExecuteCreateTicketCommand, CanExecuteCreateTicketCommand);
            CloseWindowCommand = new ViewModelCommand(ExecuteCloseWindowCommand);
        }

        private void LoadAllData()
        {
            var dbEmergencyLevelsList = new List<EmergencyLevel>();
            var dbEquipmentList = new List<Equipment>();
            var dbLocationList = new List<Location>();

            dbEmergencyLevelsList = emergencyLevelRepository.GetAllEmergencyLevel().ToList();
            dbEquipmentList = equipmentRepository.GetAllEquipment().ToList();
            dbLocationList = locationRepository.GetAllLocations().ToList();

            EmergencyLevels.Clear();
            Locations.Clear();
            Equipments.Clear();

            foreach (var level in dbEmergencyLevelsList)
            {
                EmergencyLevels.Add(level);
            }
            foreach (var equipment in dbEquipmentList)
            {
                Equipments.Add(equipment);
            }
            foreach (var location in dbLocationList)
            {
                Locations.Add(location);
            }
        }

        private void ExecuteCreateTicketCommand(object obj)
        {
            /*
            MessageBox.Show($"Types des valeurs : {TicketTitle.GetType}, {TicketDescription.GetType}, {this.SelectedEmergencyLevelId.GetType}");
            MessageBox.Show($"Types des valeurs : {this.SelectedLocationId.GetType}, {this.SelectedEquipmentId.GetType}, {this.SelectedEmergencyLevelId.GetType}");
            MessageBox.Show($"Types des valeurs : {CurrentUserId.GetType}");
            */
            if (SelectedEmergencyLevelId == null)
            {
                MessageBox.Show("Aucun niveau sélectionné !");
                return;
            } else
            {
                var newTicket = new Ticket
                {
                    TitleTicket = TicketTitle,
                    DescriptionTicket = TicketDescription,
                    BroadcastDateTicket = (DateTime.Now),
                    ResolvedDateTicket = null,
                    EmergencyLevelIdTicket = this.SelectedEmergencyLevelId,
                    LocationIdTicket = this.SelectedLocationId,
                    EquipmentIdTicket = this.SelectedEquipmentId,
                    StatusIdTicket = 1,
                    UserBroadcastedIdTicket = CurrentUserId
                };

                try
                {
                    ticketRepository.Add(newTicket);
                    MessageBox.Show("Le ticket a bien été créé.", "Succès", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Erreur lors de la création du ticket : {ex.Message}", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private bool CanExecuteCreateTicketCommand(object obj)
        {
            return true;
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
