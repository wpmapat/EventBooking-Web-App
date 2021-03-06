﻿using EventCatalogAPI.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventCatalogAPI.Data
{
    public class EventCatalogContext : DbContext
    {
        public EventCatalogContext(DbContextOptions options) :
            base(options)
        { }

        public DbSet<Place> Places { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<EventCatalog> Eventcatalogs { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Place>(ConfigurePlace);
            builder.Entity<Ticket>(ConfigureTicket);
            builder.Entity<User>(ConfigureUser);
            builder.Entity<EventCatalog>(ConfigureEventcatalog);
        }

        private void ConfigureEventcatalog(EntityTypeBuilder<EventCatalog> builder)
        {
            builder.ToTable("Catalog");
            builder.Property(c => c.Id)
                .ForSqlServerUseSequenceHiLo("catalog_hilo3")
                .IsRequired();
            builder.Property(c => c.Name)
                .IsRequired();
            builder.Property(c => c.Description)
                .IsRequired();
            builder.Property(c => c.PlaceId)
                .IsRequired();
            builder.Property(c => c.StartDate)
                .IsRequired();
            builder.Property(c => c.EndDate)
               .IsRequired();
            builder.Property(c => c.Price)
                .IsRequired();
            builder.Property(c => c.ImageURL)
                .IsRequired();
            builder.Property(c => c.PriceType)
                .IsRequired();
            builder.Property(c => c.Category)
                .IsRequired();
            builder.Property(c => c.Type)
                .IsRequired();
        }

        private void ConfigureUser(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("User");
            builder.Property(c => c.UserId)
                .ForSqlServerUseSequenceHiLo("User_hilo3");
            builder.Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(50);
            builder.Property(c => c.Email)
              .IsRequired()
              .HasMaxLength(50);
            builder.Property(c => c.Password)
              .IsRequired()
              .HasMaxLength(50);
            builder.Property(c => c.BillingAddress)
              .IsRequired()
              .HasMaxLength(100);

            builder.HasOne(c => c.Eventcatalog)
             .WithMany()
             .HasForeignKey(c => c.EventId);

            builder.Property(c => c.CreditCardNo)
              .IsRequired();
        }

        private void ConfigureTicket(EntityTypeBuilder<Ticket> builder)
        {
            builder.ToTable("Ticket");
            builder.Property(c => c.TicketId)
                .ForSqlServerUseSequenceHiLo("Ticket_hilo1")
              .IsRequired();
            builder.HasOne(c => c.Eventcatalog)
             .WithMany()
             .HasForeignKey(c => c.EventId);

        }

        private void ConfigurePlace(EntityTypeBuilder<Place> builder)
        {
            builder.ToTable("Place");
            builder.Property(c => c.PlaceId)
              .ForSqlServerUseSequenceHiLo("Place_hilo1")
               .IsRequired();
            builder.Property(c => c.PlaceName)
                .IsRequired()
                .HasMaxLength(50);
            builder.Property(c => c.Address)
                .IsRequired()
                .HasMaxLength(50);
            builder.Property(c => c.City)
                .IsRequired()
                .HasMaxLength(50);
            builder.Property(c => c.State)
                .IsRequired()
                .HasMaxLength(50);
            builder.Property(c => c.ZipCode)
                .IsRequired()
                .HasMaxLength(50);
            builder.Property(c => c.PlacePrice)
              .IsRequired();
        }
    }
}
