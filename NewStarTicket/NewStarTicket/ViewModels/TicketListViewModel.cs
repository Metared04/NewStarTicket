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
    public class TicketListViewModel : ViewModelBase
    {
        private ObservableCollection<Ticket> _currentTicketList;
        private bool _isCurrentUserAdmin;
        private Guid _currentUserGuid;
        private ViewModelBase _ticketInfosView;

        private ITicketRepository ticketRepository;

        public ObservableCollection<Ticket> CurrentTicketList 
        { 
            get
            {
                return _currentTicketList;
            }
            set
            {
                _currentTicketList = value;
                OnPropertyChanged(nameof(CurrentTicketList));
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
                //OnPropertyChanged(nameof(AdminColumnVisibility));
            } 
        }

        public Guid CurrentUserGuid
        {
            get
            {
                return _currentUserGuid;
            }
            set
            {
                _currentUserGuid = value;
                OnPropertyChanged(nameof(CurrentUserGuid));
            }
        }

        public ViewModelBase TicketInfosView
        {
            get
            {
                return _ticketInfosView;
            }
            set
            {

                _ticketInfosView = value;
                OnPropertyChanged(nameof(TicketInfosView));
            }
        }

        public Visibility AdminColumnVisibility => IsCurrentUserAdmin ? Visibility.Visible : Visibility.Collapsed;
        public string FinishedTicketButtonText => IsCurrentUserAdmin ? "Terminer !" : "Annuler.";

        // Commandes
        public ICommand ShowTicketInfosCommand { get; }
        public ICommand DeleteTicketCommand { get; }
        public ICommand TakeTicketCommand { get; }
        public ICommand FinishingTicketCommand { get; }

        public TicketListViewModel(UserAccountModel currentUser)
        {
            ticketRepository = new TicketRepository();
            IsCurrentUserAdmin = currentUser.IsAdmin;
            CurrentUserGuid = currentUser.Id;

            // Initialisation des commandes

            ShowTicketInfosCommand = new ViewModelCommand(ExecuteShowTicketInfosCommand);
            DeleteTicketCommand = new ViewModelCommand(ExecuteDeleteTicketCommand, CanExecuteDeleteTicketCommand);
            TakeTicketCommand = new ViewModelCommand(ExecuteTakeTicketCommand, CanExecuteTakeTicketCommand);
            FinishingTicketCommand = new ViewModelCommand(ExecuteFinishingTicketCommand, CanExecuteFinishingTicketCommand);

            // View par defaut

            LoadTicketDataList(currentUser);
        }

        private void LoadTicketDataList(UserAccountModel currentUser)
        {
            var ticketsList = new List<Ticket>();

            if (currentUser.IsAdmin)
            {
                ticketsList = ticketRepository.GetAll().ToList();
            }
            else
            {
                ticketsList = ticketRepository.GetAllByUserId(currentUser.Id).ToList();
            }

            CurrentTicketList = new ObservableCollection<Ticket>(ticketsList);
        }

        private void ExecuteShowTicketInfosCommand(object obj)
        {
            // Ouvrir un fenetre a part avec les infos du ticket
            var ticket = obj as Ticket;
            TicketInfosView = new TicketDetailsViewModel(ticket);
            var window = new TicketDetailsView
            {
                DataContext = TicketInfosView,
                Owner = Application.Current.MainWindow
            };
            window.ShowDialog();
        }

        private void ExecuteDeleteTicketCommand(object obj)
        {
            var ticket = obj as Ticket;
            if (ticket == null) return;
            var result = MessageBox.Show($"Supprimer le ticket \"{ticket.TitleTicket}\" ?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result != MessageBoxResult.Yes) return;
            try
            {
                ticketRepository.Remove(ticket);
                CurrentTicketList.Remove(ticket);
                MessageBox.Show("Ticket supprimé avec succès.", "Succès", MessageBoxButton.OK, MessageBoxImage.Information);
            } catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors de la suppression : {ex}", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private bool CanExecuteDeleteTicketCommand(object obj)
        {
            return IsCurrentUserAdmin;
        }
        private void ExecuteTakeTicketCommand(object obj)
        {
            var ticket = obj as Ticket;
            if (ticket == null) return;
            // Le status passe de 1 a 2 et on met l'id du mec
            try
            {
                ticketRepository.TakingTicket(ticket, CurrentUserGuid);
                MessageBox.Show("Ticket recuperer avec succès(?)", "Succès", MessageBoxButton.OK, MessageBoxImage.Information);
                ticket.StatusIdTicket = 2;
                ticket.UserResolvedIdTicket = CurrentUserGuid;
                CommandManager.InvalidateRequerySuggested();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors de la recuperation : {ex}", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private bool CanExecuteTakeTicketCommand(object obj)
        {
            if (!IsCurrentUserAdmin)
                return false;
            if (obj is not Ticket ticket)
                return false;
            return ticket.StatusIdTicket == 1;
        }
        private void ExecuteFinishingTicketCommand(object obj)
        {
            var ticket = obj as Ticket;
            if (ticket == null) return;
            // Si l'utilisateur est un admin, c'est le fonctionnement normal.
            // Sinon le bouton devient le bouton "annule" et le met le status sur annulé
            if (IsCurrentUserAdmin)
            {
                var result = MessageBox.Show($"Valider le ticket \"{ticket.TitleTicket}\" ?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (result != MessageBoxResult.Yes) return;
                try
                {
                    ticketRepository.EditStatus(ticket, 4);
                    MessageBox.Show("Ticket validé avec succès.", "Succès", MessageBoxButton.OK, MessageBoxImage.Information);
                    ticket.StatusIdTicket = 4;
                    CommandManager.InvalidateRequerySuggested();
                } catch (Exception ex)
                {
                    MessageBox.Show($"Erreur lors de la validation : {ex}", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            } else
            {
                var result = MessageBox.Show($"ANNULER le ticket \"{ticket.TitleTicket}\" ?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (result != MessageBoxResult.Yes) return;
                try
                {
                    ticketRepository.EditStatus(ticket, 3);
                    MessageBox.Show("Ticket echoué avec succès(?)", "Succès", MessageBoxButton.OK, MessageBoxImage.Information);
                    ticket.StatusIdTicket = 3;
                    CommandManager.InvalidateRequerySuggested();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Erreur lors de l'echec : {ex}", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private bool CanExecuteFinishingTicketCommand(object obj)
        {
            if (obj is not Ticket ticket)
                return false;
            return ticket.StatusIdTicket == 1 || ticket.StatusIdTicket == 2;
        }
    }
}
