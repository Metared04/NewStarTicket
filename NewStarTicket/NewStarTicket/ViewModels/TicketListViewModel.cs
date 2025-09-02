using NewStarTicket.Models;
using NewStarTicket.Repositories;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

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

        public TicketListViewModel(UserAccountModel currentUser)
        {
            ticketRepository = new TicketRepository();
            IsCurrentUserAdmin = currentUser.IsAdmin;
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
    }
}
