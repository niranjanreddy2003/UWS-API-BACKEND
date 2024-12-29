using Microsoft.EntityFrameworkCore;
using UWS_BACK.Models;

namespace UWS_BACK.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<AuthenticationModel> Authentications { get; set; }
        public DbSet<UsersModel> Users { get; set; }

        public DbSet<SpecialPickupModel> SpecialPickups { get; set; }

        public DbSet<PublicReportModel> PublicReports { get; set; }

        public DbSet<FeedbackModel>Feedbacks { get; set; }

        public DbSet<RouteModel> Routes { get; set; }
        public DbSet<LocationModel> Locations { get; set; }
        public DbSet<DriverModel> Drivers { get; set; }
        public DbSet<TruckModel> Trucks { get; set; }

        public DbSet<ScheduleModel> Schedules { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Route Relationships
            modelBuilder.Entity<RouteModel>()
                .HasMany(r => r.Locations)
                .WithOne(l => l.Route)
                .HasForeignKey(l => l.routeId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<RouteModel>()
                .HasMany(r => r.Users)
                .WithOne(u => u.Route)
                .HasForeignKey(u => u.routeId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<AuthenticationModel>()
               .HasOne(a => a.User)
               .WithOne(p => p.Authentication)
               .HasForeignKey<UsersModel>(p => p.UserId)
               .IsRequired(false);  // Make the relationship optional

            modelBuilder.Entity<UsersModel>()
                .HasMany(u => u.PublicReports)
                .WithOne(r => r.Profile)
                .HasForeignKey(r => r.userId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<UsersModel>()
                .HasMany(u => u.SpecialPickups)
                .WithOne(s => s.Profile)
                .HasForeignKey(s => s.userId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<UsersModel>()
                .HasMany(u => u.Feedbacks)
                .WithOne(f => f.Profile)
                .HasForeignKey(f => f.userId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Restrict);

            // Truck Relationships
            modelBuilder.Entity<TruckModel>()
                .HasOne(t => t.Route)
                .WithMany()
                .HasForeignKey(t => t.RouteId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<TruckModel>()
                .HasOne(t => t.Driver)
                .WithOne(d => d.Truck)
                .HasForeignKey<DriverModel>(d => d.TruckId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Cascade);

            // Driver Relationships
            modelBuilder.Entity<DriverModel>()
                .HasOne(d => d.Route)
                .WithMany()
                .HasForeignKey(d => d.RouteId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Restrict);

            // Schedule Relationships
            modelBuilder.Entity<ScheduleModel>()
                .HasOne(s => s.Driver)
                .WithMany()
                .HasForeignKey(s => s.driverId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ScheduleModel>()
                .HasOne(s => s.Route)
                .WithMany()
                .HasForeignKey(s => s.routeId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ScheduleModel>()
                .HasOne(s => s.Truck)
                .WithMany()
                .HasForeignKey(s => s.truckId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
