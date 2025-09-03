using NewStarTicket.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace NewStarTicket.ViewModels
{
    public class TicketDetailsViewModel : ViewModelBase
    {
        // Champs
        public Ticket _ticket;
        private Ticket _currentTicket;

        // Proprietes
        public Ticket Ticket 
        {
            get
            {
                return _ticket;
            }
            set 
            { 
                _ticket = value;
                OnPropertyChanged(nameof(Ticket));
                OnPropertyChanged(nameof(TitleTicket));
                OnPropertyChanged(nameof(IdTicket));
                OnPropertyChanged(nameof(DescriptionTicket));
                OnPropertyChanged(nameof(LocationIdTicket));
                OnPropertyChanged(nameof(EquipmentIdTicket));
                OnPropertyChanged(nameof(EmergencyLevelIdTicket));
                OnPropertyChanged(nameof(StatusIdTicket));
                OnPropertyChanged(nameof(BroadcastDateTicket));
                OnPropertyChanged(nameof(ResolvedDateTicket));
            }
        }

        public string TitleTicket => Ticket?.TitleTicket ?? string.Empty;
        public Guid IdTicket => Ticket?.IdTicket ?? Guid.Empty;
        public string DescriptionTicket => Ticket?.DescriptionTicket ?? "Pas de description";

        public int LocationIdTicket => Ticket?.LocationIdTicket ?? 0;
        public int EquipmentIdTicket => Ticket?.EquipmentIdTicket ?? 0;
        public int EmergencyLevelIdTicket => Ticket?.EmergencyLevelIdTicket ?? 0;
        public int StatusIdTicket => Ticket?.StatusIdTicket ?? 0;

        /*
        public string LocationName => Ticket?.Location?.Name ?? "Non défini";
        public string EquipmentName => Ticket?.Equipment?.Name ?? "Non défini";
        public string EmergencyLevelName => Ticket?.EmergencyLevel?.Name ?? "Non défini";
        public string StatusName => Ticket?.TicketStatus?.Name ?? "Non défini";
        */
        public DateTime? BroadcastDateTicket => Ticket?.BroadcastDateTicket;
        public DateTime? ResolvedDateTicket => Ticket?.ResolvedDateTicket;
        public string UserBroadcastedName => Ticket?.UserBroadcastedTicketName ?? "Inconnu";
        public string UserResolvedName => Ticket?.UserResovelvedTicketName ?? "Personne.";
        public Ticket CurrentTicket 
        {
            get 
            {
                return _currentTicket;
            } 
            set
            {
                _currentTicket = value;
                OnPropertyChanged(nameof(CurrentTicket));
            }
        }

        // Commandes
        public ICommand CloseCommand { get; }
        public ICommand ViewEmitterProfileCommand { get; }
        public ICommand ViewResolverProfileCommand { get; }
        public ICommand ExportToPdfCommand { get; }

        public TicketDetailsViewModel(Ticket ticket) 
        {
            // Commandes
            CloseCommand = new ViewModelCommand(ExecuteCloseCommand);
            ViewEmitterProfileCommand = new ViewModelCommand(ExecuteViewEmitterProfileCommand);
            ViewResolverProfileCommand = new ViewModelCommand(ExecuteViewResolverProfileCommand);
            ExportToPdfCommand = new ViewModelCommand(ExecuteExportToPdfCommand);

            // Chargement du ticket
            Ticket = ticket;
        }

        private void LoadCurrentTicketData(Ticket ticket)
        {

        }

        private void ExecuteCloseCommand(object obj)
        {
            if (obj is Window w)
            {
                w.Close();
                return;
            }

            var window = Application.Current.Windows.OfType<Window>().FirstOrDefault(x => x.DataContext == this);
            window?.Close();
        }
        private void ExecuteViewEmitterProfileCommand(object obj)
        {
            MessageBox.Show("Profile.");
            //MessageBox.Show($"(Non implémenté) Voir profil de : {EmitterName}", "Profil émetteur", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        private void ExecuteViewResolverProfileCommand(object obj)
        {
            MessageBox.Show("Profile 2.");
        }
        private void ExecuteExportToPdfCommand(object obj)
        {
            MessageBox.Show("Export pdf");
        }
    }
}
