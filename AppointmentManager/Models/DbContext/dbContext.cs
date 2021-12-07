namespace AppointmentManager
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class dbContext : DbContext
    {
        public dbContext()
            : base("name=DefaultConnection")
        {
        }

        public virtual DbSet<AspNetRole> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUser> AspNetUsers { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<CalendarAppointment> CalendarAppointments { get; set; }
        public virtual DbSet<Booking_Room> Booking_Rooms { get; set; }

        public virtual DbSet<Booking_Event> Booking_Events { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AspNetRole>()
                .HasMany(e => e.AspNetUsers)
                .WithMany(e => e.AspNetRoles)
                .Map(m => m.ToTable("AspNetUserRoles").MapLeftKey("RoleId").MapRightKey("UserId"));

            modelBuilder.Entity<AspNetUser>()
                .HasMany(e => e.AspNetUserClaims)
                .WithRequired(e => e.AspNetUser)
                .HasForeignKey(e => e.UserId);

            modelBuilder.Entity<AspNetUser>()
                .HasMany(e => e.AspNetUserLogins)
                .WithRequired(e => e.AspNetUser)
                .HasForeignKey(e => e.UserId);

            modelBuilder.Entity<AspNetUser>()
                .HasMany(e => e.Appointments)
                .WithRequired(e => e.User)
                .HasForeignKey(e => e.Id_User);

            modelBuilder.Entity<Customer>()
                .HasMany(e => e.Appointments)
                .WithRequired(e => e.Customer)
                .HasForeignKey(e => e.Id_Customer);

            modelBuilder.Entity<Booking_Room>()
           .HasMany(e => e.Booking_Events)
           .WithRequired(e => e.Booking_Resource)
           .HasForeignKey(e => e.resourceId);

            modelBuilder.Entity<Customer>()
             .HasMany(e => e.Booking_Events)
             .WithRequired(e => e.Customer)
             .HasForeignKey(e => e.customerId);
        }
    }
}
