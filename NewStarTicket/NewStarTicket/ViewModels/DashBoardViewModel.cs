using NewStarTicket.Models;
using NewStarTicket.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OxyPlot;
using OxyPlot.Series;

namespace NewStarTicket.ViewModels
{
    public class DashBoardViewModel : ViewModelBase
    {
        // Champs

        private int _resolvedTicketNumber;
        private int _waitingTicketCount;
        private int _createdTicketToday;
        private Dictionary<int, int> _ticketsCountedByStatus;
        private string _cardTitle1;
        private string _cardTitle2;
        private string _cardTitle3;
        private bool _isCurrentUserAdmin;
        private Guid _currentUserId;
        private PlotModel _pieModel;

        private ITicketRepository ticketRepository;
        public int ResolvedTicketNumber 
        {
            get
            {
                return _resolvedTicketNumber;
            }
            set
            {
                _resolvedTicketNumber = value;
                OnPropertyChanged(nameof(ResolvedTicketNumber));
            }
        }
        public int WaitingTicketCount
        {
            get
            {
                return _waitingTicketCount;
            }
            set
            {
                _waitingTicketCount = value;
                OnPropertyChanged(nameof(WaitingTicketCount));
            }
        }
        public int CreatedTicketToday 
        { 
            get
            {
                return _createdTicketToday;
            }
            set
            {
                _createdTicketToday = value;
                OnPropertyChanged(nameof(CreatedTicketToday));
            }
        }
        public Dictionary<int, int> TicketsCountedByStatus
        {
            get
            {
                return _ticketsCountedByStatus;
            }
            set
            {
                _ticketsCountedByStatus = value;
                OnPropertyChanged(nameof(TicketsCountedByStatus));
            }
        }
        public string CardTitle1
        {
            get
            {
                return _cardTitle1;
            }
            set
            { 
                _cardTitle1 = value;
                OnPropertyChanged(nameof(CardTitle1));
            }
        }
        public string CardTitle2
        {
            get
            {
                return _cardTitle2;
            }
            set
            {
                _cardTitle2 = value;
                OnPropertyChanged(nameof(CardTitle2));
            }
        }
        public string CardTitle3
        {
            get
            {
                return _cardTitle3;
            }
            set
            {
                _cardTitle3 = value;
                OnPropertyChanged(nameof(CardTitle3));
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
        public Guid CurrentUserId
        {
            get
            {
                return _currentUserId;
            }
            set
            {
                _currentUserId = value;
                OnPropertyChanged(nameof(CurrentUserId));
            }
        }
        public PlotModel PieModel
        {
            get => _pieModel;
            set
            {
                _pieModel = value;
                OnPropertyChanged(nameof(PieModel));
            }
        }

        public DashBoardViewModel(UserAccountModel currentUserAccount)
        {
            LoadDashboardData(currentUserAccount);
        }
        private void LoadDashboardData(UserAccountModel currentUser)
        {
            ticketRepository = new TicketRepository();
            IsCurrentUserAdmin = currentUser.IsAdmin;
            CurrentUserId = currentUser.Id;

            GetResolvedTicketNumber();
            GetWaitingTicketCount();
            GetCreatedTicketToday();
            GetTicketsCountedByStatus();
        }
        private void GetResolvedTicketNumber()
        {
            if (IsCurrentUserAdmin)
            {
                ResolvedTicketNumber = ticketRepository.GetTicketCountResolvedByAdmin(CurrentUserId);
                CardTitle1 = $"Tickets résolue(s)";
            }
            else
            {
                ResolvedTicketNumber = ticketRepository.GetEmittedTicketCountByUser(CurrentUserId);
                CardTitle1 = $"Tickets émit";
            }

        }
        private void GetWaitingTicketCount()
        {
            if (IsCurrentUserAdmin)
            {
                WaitingTicketCount = ticketRepository.GetTicketsWaitingCount();
            }
            else
            {
                WaitingTicketCount = ticketRepository.GetEmittedTicketCountWaiting(CurrentUserId);
            }
            CardTitle2 = $"Ticket(s) en attente";
        }
        private void GetCreatedTicketToday()
        {
            if (IsCurrentUserAdmin)
            {
                CreatedTicketToday = ticketRepository.GetTicketsCreatedToday();
            }
            else
            {
                CreatedTicketToday = ticketRepository.GetTicketsCreatedTodayByUser(CurrentUserId);
            }
            CardTitle3 = $"Ticket(s) créer aujourd'hui";
        }
        private void GetTicketsCountedByStatus()
        {
            if (IsCurrentUserAdmin)
            {
                TicketsCountedByStatus = ticketRepository.GetTicketsByStatus();
            }
            else
            {
                TicketsCountedByStatus = ticketRepository.GetUsersTicketsByStatus(CurrentUserId);
            }
            LoadPieChart();
        }
        private void LoadPieChart()
        {
            var model = new PlotModel();

            var pieSeries = new PieSeries
            {
                StrokeThickness = 0.25,
                InsideLabelPosition = 0.8,
                AngleSpan = 360,
                StartAngle = 0
            };

            foreach (var kvp in TicketsCountedByStatus)
            {
                string label = GetStatusLabel(kvp.Key);
                pieSeries.Slices.Add(new PieSlice(label, kvp.Value));
            }

            model.Series.Add(pieSeries);
            PieModel = model;
        }
        private string GetStatusLabel(int statusId)
        {
            return statusId switch
            {
                1 => "En attente",
                2 => "En cours",
                3 => "Annulé",
                4 => "Achevé",
                5 => "Echoué",
                _ => "Inconnu"
            };
        }
    }
}
