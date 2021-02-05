using HotelManager.DAL.Entities;
using HotelManager.DAL.Interfaces;
using System;
using System.Data;
using System.Data.Entity;
using System.Linq;

namespace HotelManager.DAL.EF
{
    public class HotelDbContext : DbContext, IDatabaseContext
    {
        public HotelDbContext()
            : base("name=HotelDbContext")
        {
            Database.SetInitializer(new HotelDbInitializer());
        }

        public virtual DbSet<Person> Persons { get; set; }
        public virtual DbSet<Client> Clients { get; set; }
        public virtual DbSet<Worker> Workers { get; set; }
        public virtual DbSet<Residence> Residences { get; set; }
        public virtual DbSet<WeeklySchedule> Schedules { get; set; }
        public virtual DbSet<HotelRoom> HotelRooms { get; set; }
        public virtual DbSet<Hotel> Hotels { get; set; }



        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // По умолчанию для иерархии объектов используется подход TPH(Table Per Hierarchy).
            // Я использую подход (Table Per Type - https://metanit.com/sharp/entityframework/7.2.php),
            // Свойства производных классов сохраняются в отдельных таблицах.
            modelBuilder.Entity<Person>().ToTable("Persons");
            modelBuilder.Entity<Client>().ToTable("Clients");
            modelBuilder.Entity<Worker>().ToTable("Workers");


            #region Persons

            modelBuilder.Entity<Person>()
                .Property(p => p.Surname)
                .IsRequired()
                .IsUnicode()
                .HasMaxLength(200);

            modelBuilder.Entity<Person>()
                .Property(p => p.Name)
                .IsRequired()
                .IsUnicode()
                .HasMaxLength(100);

            modelBuilder.Entity<Person>()
                .Property(p => p.Patronymic)
                .IsRequired()
                .IsUnicode()
                .HasMaxLength(200);

            modelBuilder.Entity<Person>()
                .Property(p => p.Email)
                .IsRequired()
                .IsUnicode()
                .HasMaxLength(200);

            modelBuilder.Entity<Person>()
                .Property(p => p.PhoneNumber)
                .IsRequired()
                .IsUnicode()
                .HasMaxLength(50);

            #endregion

            #region Clients

            modelBuilder.Entity<Client>()
                .Property(c => c.City)
                .IsRequired()
                .IsUnicode()
                .HasMaxLength(200);

            modelBuilder.Entity<Client>()
                .HasMany(c => c.Residences)
                .WithRequired(r => r.Client);

            #endregion

            #region Workers

            // связь "один-к-одному" между работником и его расписанием
            modelBuilder
                .Entity<Worker>()
                .HasRequired(w => w.CleaningSchedule)
                .WithRequiredPrincipal(s => s.Worker);

            #endregion

            #region Residences


            #endregion

            #region Schedules

            // не требуется

            #endregion

            #region HotelRooms

            modelBuilder.Entity<HotelRoom>()
                .HasMany(r => r.Residences)
                .WithRequired(r => r.HotelRoom);

            modelBuilder.Entity<HotelRoom>()
                .Property(r => r.PhoneNumber)
                .IsRequired()
                .IsUnicode()
                .HasMaxLength(50);

            #endregion

            #region Hotels

            modelBuilder.Entity<Hotel>()
                .HasMany(h => h.HotelRooms)
                .WithRequired(r => r.Hotel);

            modelBuilder.Entity<Hotel>()
                .HasMany(h => h.Workers)
                .WithRequired(r => r.Hotel);

            modelBuilder.Entity<Hotel>()
                .Property(h => h.Name)
                .IsRequired()
                .IsUnicode()
                .HasMaxLength(200);

            modelBuilder.Entity<Hotel>()
                .Property(h => h.Address)
                .IsRequired()
                .IsUnicode()
                .HasMaxLength(200);

            #endregion

            base.OnModelCreating(modelBuilder);
        }

        IDbSet<TEntity> IDatabaseContext.Set<TEntity>() => Set<TEntity>();

        private void FixEfProviderServicesProblem()
        {
            // EntityFramework.SqlServer.dll
            var instance = System.Data.Entity.SqlServer.SqlProviderServices.Instance;
        }

        public int Initialize()
        {
            try
            {
                Database.Initialize(false);
            }
            catch (DataException)
            {
                Database.Delete();
                Database.Initialize(true);
            }
            return 1;
        }
    }
}