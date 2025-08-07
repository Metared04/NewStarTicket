using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NewStarTicket.Models;

namespace NewStarTicket.Datacontext
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<UserLevel> UserLevels { get; set; }
        public DbSet<EmergencyLevel> EmergencyLevels { get; set; }
        public DbSet<Equipment> Equipments { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<TicketStatus> TicketStatuses { get; set; }
        public DbSet<Ticket> Tickets { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuration UserLevel
            modelBuilder.Entity<UserLevel>(entity =>
            {
                entity.HasKey(e => e.IdUserLevel);
                entity.Property(e => e.NameUserLevel).IsRequired().HasMaxLength(100);
            });

            // Configuration User
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.IdUser);
                entity.Property(e => e.NameUser).IsRequired().HasMaxLength(100);
                entity.Property(e => e.PasswordUser).IsRequired().HasMaxLength(255);
                entity.Property(e => e.EmailUser).IsRequired().HasMaxLength(255);

                // Relation avec UserLevel (clé étrangère)
                entity.HasOne(u => u.UserLevel)
                      .WithMany()
                      .HasForeignKey(u => u.UserIdLevel)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            // Configuration EmergencyLevel
            modelBuilder.Entity<EmergencyLevel>(entity =>
            {
                entity.HasKey(e => e.IdEmergencyLevel);
                entity.Property(e => e.NameEmergencyLevel).IsRequired().HasMaxLength(100);
            });

            // Configuration Equipment
            modelBuilder.Entity<Equipment>(entity =>
            {
                entity.HasKey(e => e.IdEquipment);
                entity.Property(e => e.NameEquipment).IsRequired().HasMaxLength(100);
            });

            // Configuration Location
            modelBuilder.Entity<Location>(entity =>
            {
                entity.HasKey(e => e.IdLocation);
                entity.Property(e => e.NameLocation).IsRequired().HasMaxLength(100);
            });

            // Configuration TicketStatus
            modelBuilder.Entity<TicketStatus>(entity =>
            {
                entity.HasKey(e => e.IdTicketStatus);
                entity.Property(e => e.NameTicketStatus).IsRequired().HasMaxLength(100);
            });

            // Configuration Ticket (la plus complexe avec toutes les relations)
            modelBuilder.Entity<Ticket>(entity =>
            {
                entity.HasKey(e => e.IdTicket);
                entity.Property(e => e.TitleTicket).IsRequired().HasMaxLength(200);
                entity.Property(e => e.DescriptionTicket).IsRequired().HasMaxLength(2000);
                entity.Property(e => e.BroadcastDateTicket).IsRequired();
                entity.Property(e => e.ResolvedDateTicket).IsRequired(false); // Nullable

                // Relations avec les autres entités
                entity.HasOne(t => t.TicketStatus)
                      .WithMany()
                      .HasForeignKey(t => t.StatusIdTicket)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(t => t.EmergencyLevel)
                      .WithMany()
                      .HasForeignKey(t => t.EmergencyLevelIdTicket)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(t => t.Equipment)
                      .WithMany()
                      .HasForeignKey(t => t.EquipmentIdTicket)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(t => t.Location)
                      .WithMany()
                      .HasForeignKey(t => t.LocationIdTicket)
                      .OnDelete(DeleteBehavior.Restrict);

                // Relations avec User (broadcast et resolved)
                entity.HasOne<User>()
                      .WithMany()
                      .HasForeignKey(t => t.UserBroadcastedIdTicket)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne<User>()
                      .WithMany()
                      .HasForeignKey(t => t.UserResolvedIdTicket)
                      .OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}
