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
    public class TicketListViewModel : ViewModelBase
    {
        private ObservableCollection<Ticket> _currentTicketList;
        private bool _isCurrentUserAdmin;

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
                OnPropertyChanged(nameof(AdminColumnVisibility));
            } 
        }

        public Visibility AdminColumnVisibility => IsCurrentUserAdmin ? Visibility.Visible : Visibility.Collapsed;

        // Commandes
        public ICommand ShowTicketInfosCommand { get; }
        public ICommand DeleteTicketCommand { get; }
        public ICommand TakeTicketCommand { get; }
        public ICommand FinishingTicketCommand { get; }

        public TicketListViewModel(UserAccountModel currentUser)
        {
            ticketRepository = new TicketRepository();
            IsCurrentUserAdmin = currentUser.IsAdmin;

            // Initialisation des commandes

            ShowTicketInfosCommand = new ViewModelCommand(ExecuteShowTicketInfosCommand);
            DeleteTicketCommand = new ViewModelCommand(ExecuteDeleteTicketCommand, CanExecuteDeleteTicketCommand);

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
            return;
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
    }
}
