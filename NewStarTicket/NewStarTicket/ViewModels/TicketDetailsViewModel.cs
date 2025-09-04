using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using Microsoft.Win32;
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
        public Guid UserBroadcastedIdTicket => Ticket?.UserBroadcastedIdTicket ?? Guid.Empty;
        public Guid UserResolvedIdTicket => Ticket?.UserResolvedIdTicket ?? Guid.Empty;
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
        public bool HasResolver => Ticket.UserResolvedIdTicket != null;

        // Commandes
        public ICommand CloseCommand { get; }
        public ICommand ViewEmitterProfileCommand { get; }
        public ICommand SendEmailToResolverCommand { get; }
        public ICommand ExportToPdfCommand { get; }

        public TicketDetailsViewModel(Ticket ticket) 
        {
            // Commandes
            CloseCommand = new ViewModelCommand(ExecuteCloseCommand);
            ViewEmitterProfileCommand = new ViewModelCommand(ExecuteViewEmitterProfileCommand);
            SendEmailToResolverCommand = new ViewModelCommand(ExecuteSendEmailToResolverCommand);
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
        private void ExecuteSendEmailToResolverCommand(object obj)
        {
            if (!HasResolver)
            {
                MessageBox.Show("Aucun résolveur assigné à ce ticket.", "Information",
                    MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            } /*else
            {
                var result = MessageBox.Show(
        "Comment souhaitez-vous envoyer l'email ?\n\nOui = Client mail par défaut\nNon = Gmail (navigateur)\nAnnuler = Copier dans le presse-papiers",
        "Choix du client mail",
        MessageBoxButton.YesNoCancel,
        MessageBoxImage.Question);
                try
                {
                    string subject = $"Ticket #{Ticket.IdTicket} - {Ticket.TitleTicket}";
                    string body = $"Bonjour {UserResolvedName},\n\n" +
                         $"Je vous contacte concernant le ticket suivant :\n\n" +
                         $"ID: {Ticket.IdTicket}\n" +
                         $"Titre: {Ticket.TitleTicket}\n" +
                         $"Description: {Ticket.DescriptionTicket}\n" +
                         $"Lieu: {Ticket.LocationIdTicket}\n" +
                         $"Équipement: {Ticket.EquipmentIdTicket}\n" +
                         $"Créé le: {BroadcastDateTicket:yyyy/MM/dd HH:mm}\n\n" +
                         $"Cordialement,\n{UserBroadcastedName}";
                    switch (result)
                    {
                        case MessageBoxResult.Yes: // Client par défaut
                            string mailto = $"mailto:?subject={Uri.EscapeDataString(subject)}&body={Uri.EscapeDataString(body)}";
                            System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
                            {
                                FileName = mailto,
                                UseShellExecute = true
                            });
                            break;

                        case MessageBoxResult.No: // Gmail via navigateur
                            string gmailUrl = $"https://mail.google.com/mail/?view=cm&fs=1&tf=1&su={Uri.EscapeDataString(subject)}&body={Uri.EscapeDataString(body)}";
                            System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
                            {
                                FileName = gmailUrl,
                                UseShellExecute = true
                            });
                            break;

                        case MessageBoxResult.Cancel: // Copier dans le presse-papiers
                            string emailContent = $"Sujet: {subject}\n\n{body}";
                            Clipboard.SetText(emailContent);
                            MessageBox.Show("Contenu de l'email copié dans le presse-papiers.", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                            break;
                    }
                } catch (Exception ex)
                {
                    MessageBox.Show($"Erreur lors de l'ouverture du client mail : {ex.Message}",
                "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }*/
        }
        private void ExecuteExportToPdfCommand(object obj)
        {
            try
            {
                var saveFileDialog = new SaveFileDialog
                {
                    Filter = "PDF files (*.pdf)|*.pdf",
                    Title = "Enregistrer le ticket en PDF",
                    FileName = $"Ticket_{Ticket.IdTicket}_{DateTime.Now:yyyyMMdd_HHmmss}.pdf"
                };
                if (saveFileDialog.ShowDialog() != true) return;
                QuestPDF.Settings.License = LicenseType.Community;

                Document.Create(container =>
                {
                    container.Page(page =>
                    {
                        page.Size(PageSizes.A4);
                        page.Margin(2, Unit.Centimetre);

                        page.Content()
                            .PaddingVertical(1, Unit.Centimetre)
                            .Column(x =>
                            {
                                x.Item().Text("TEST PDF").FontSize(20).Bold();
                                x.Item().Text($"ID: {Ticket?.IdTicket}").FontSize(12);
                                x.Item().Text($"Titre: {Ticket?.TitleTicket}").FontSize(12);
                                x.Item().Text($"Description: {Ticket?.DescriptionTicket}").FontSize(12);
                            });
                    });
                }).GeneratePdf(saveFileDialog.FileName);

                MessageBox.Show("PDF créé avec succès !", "Succès", MessageBoxButton.OK, MessageBoxImage.Information);

            } catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors de l'exportation du pdf : {ex.Message}",
                "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
