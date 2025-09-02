using NewStarTicket.Models;
using NewStarTicket.Repositories;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewStarTicket.ViewModels
{
    public class TicketListViewModel : ViewModelBase
    {
        private ITicketRepository ticketRepository;

        private ObservableCollection<Ticket> _currentTicketList;

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

        public TicketListViewModel(UserAccountModel currentUser)
        {
            ticketRepository = new TicketRepository();
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
