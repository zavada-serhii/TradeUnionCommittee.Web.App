using Microsoft.EntityFrameworkCore;
using TradeUnionCommittee.DAL.Entities;

namespace TradeUnionCommittee.DAL.EF
{
    public class TradeUnionCommitteeEmployeesCoreContext : DbContext
    {
        private readonly string _connectionString;

        public TradeUnionCommitteeEmployeesCoreContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        public TradeUnionCommitteeEmployeesCoreContext(DbContextOptions<TradeUnionCommitteeEmployeesCoreContext> options) : base(options)
        {
        }

        public virtual DbSet<Activities> Activities { get; set; }
        public virtual DbSet<ActivityChildrens> ActivityChildrens { get; set; }
        public virtual DbSet<ActivityEmployees> ActivityEmployees { get; set; }
        public virtual DbSet<ActivityFamily> ActivityFamily { get; set; }
        public virtual DbSet<ActivityGrandChildrens> ActivityGrandChildrens { get; set; }
        public virtual DbSet<AddressPublicHouse> AddressPublicHouse { get; set; }
        public virtual DbSet<ApartmentAccountingEmployees> ApartmentAccountingEmployees { get; set; }
        public virtual DbSet<Award> Award { get; set; }
        public virtual DbSet<AwardEmployees> AwardEmployees { get; set; }
        public virtual DbSet<Children> Children { get; set; }
        public virtual DbSet<Cultural> Cultural { get; set; }
        public virtual DbSet<CulturalChildrens> CulturalChildrens { get; set; }
        public virtual DbSet<CulturalEmployees> CulturalEmployees { get; set; }
        public virtual DbSet<CulturalFamily> CulturalFamily { get; set; }
        public virtual DbSet<CulturalGrandChildrens> CulturalGrandChildrens { get; set; }
        public virtual DbSet<Education> Education { get; set; }
        public virtual DbSet<Employee> Employee { get; set; }
        public virtual DbSet<Event> Event { get; set; }
        public virtual DbSet<EventChildrens> EventChildrens { get; set; }
        public virtual DbSet<EventEmployees> EventEmployees { get; set; }
        public virtual DbSet<EventFamily> EventFamily { get; set; }
        public virtual DbSet<EventGrandChildrens> EventGrandChildrens { get; set; }
        public virtual DbSet<Family> Family { get; set; }
        public virtual DbSet<FluorographyEmployees> FluorographyEmployees { get; set; }
        public virtual DbSet<GiftChildrens> GiftChildrens { get; set; }
        public virtual DbSet<GiftEmployees> GiftEmployees { get; set; }
        public virtual DbSet<GiftGrandChildrens> GiftGrandChildrens { get; set; }
        public virtual DbSet<GrandChildren> GrandChildren { get; set; }
        public virtual DbSet<Hobby> Hobby { get; set; }
        public virtual DbSet<HobbyChildrens> HobbyChildrens { get; set; }
        public virtual DbSet<HobbyEmployees> HobbyEmployees { get; set; }
        public virtual DbSet<HobbyGrandChildrens> HobbyGrandChildrens { get; set; }
        public virtual DbSet<MaterialAid> MaterialAid { get; set; }
        public virtual DbSet<MaterialAidEmployees> MaterialAidEmployees { get; set; }
        public virtual DbSet<Position> Position { get; set; }
        public virtual DbSet<PositionEmployees> PositionEmployees { get; set; }
        public virtual DbSet<PrivateHouseEmployees> PrivateHouseEmployees { get; set; }
        public virtual DbSet<PrivilegeEmployees> PrivilegeEmployees { get; set; }
        public virtual DbSet<Privileges> Privileges { get; set; }
        public virtual DbSet<PublicHouseEmployees> PublicHouseEmployees { get; set; }
        public virtual DbSet<Scientific> Scientific { get; set; }
        public virtual DbSet<SocialActivity> SocialActivity { get; set; }
        public virtual DbSet<SocialActivityEmployees> SocialActivityEmployees { get; set; }
        public virtual DbSet<Subdivisions> Subdivisions { get; set; }
        public virtual DbSet<TypeEvent> TypeEvent { get; set; }
        public virtual DbSet<TypeHouse> TypeHouse { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql(_connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Activities>(entity =>
            {
                entity.HasIndex(e => e.Name)
                    .HasName("Activities_Name_key")
                    .IsUnique();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("character varying");
            });

            modelBuilder.Entity<ActivityChildrens>(entity =>
            {
                entity.HasIndex(e => new { e.IdChildren, e.IdActivities, e.DateEvent })
                    .HasName("ActivityChildrens_IdChildren_IdActivities_DateEvent_key")
                    .IsUnique();

                entity.Property(e => e.DateEvent).HasColumnType("date");

                entity.HasOne(d => d.IdActivitiesNavigation)
                    .WithMany(p => p.ActivityChildrens)
                    .HasForeignKey(d => d.IdActivities)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("ActivityChildrens_IdActivities_fkey");

                entity.HasOne(d => d.IdChildrenNavigation)
                    .WithMany(p => p.ActivityChildrens)
                    .HasForeignKey(d => d.IdChildren)
                    .HasConstraintName("ActivityChildrens_IdChildren_fkey");
            });

            modelBuilder.Entity<ActivityEmployees>(entity =>
            {
                entity.HasIndex(e => new { e.IdEmployee, e.IdActivities, e.DateEvent })
                    .HasName("ActivityEmployees_IdEmployee_IdActivities_DateEvent_key")
                    .IsUnique();

                entity.Property(e => e.DateEvent).HasColumnType("date");

                entity.HasOne(d => d.IdActivitiesNavigation)
                    .WithMany(p => p.ActivityEmployees)
                    .HasForeignKey(d => d.IdActivities)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("ActivityEmployees_IdActivities_fkey");

                entity.HasOne(d => d.IdEmployeeNavigation)
                    .WithMany(p => p.ActivityEmployees)
                    .HasForeignKey(d => d.IdEmployee)
                    .HasConstraintName("ActivityEmployees_IdEmployee_fkey");
            });

            modelBuilder.Entity<ActivityFamily>(entity =>
            {
                entity.HasIndex(e => new { e.IdFamily, e.IdActivities, e.DateEvent })
                    .HasName("ActivityFamily_IdFamily_IdActivities_DateEvent_key")
                    .IsUnique();

                entity.Property(e => e.DateEvent).HasColumnType("date");

                entity.HasOne(d => d.IdActivitiesNavigation)
                    .WithMany(p => p.ActivityFamily)
                    .HasForeignKey(d => d.IdActivities)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("ActivityFamily_IdActivities_fkey");

                entity.HasOne(d => d.IdFamilyNavigation)
                    .WithMany(p => p.ActivityFamily)
                    .HasForeignKey(d => d.IdFamily)
                    .HasConstraintName("ActivityFamily_IdFamily_fkey");
            });

            modelBuilder.Entity<ActivityGrandChildrens>(entity =>
            {
                entity.HasIndex(e => new { e.IdGrandChildren, e.IdActivities, e.DateEvent })
                    .HasName("ActivityGrandChildrens_IdGrandChildren_IdActivities_DateEve_key")
                    .IsUnique();

                entity.Property(e => e.DateEvent).HasColumnType("date");

                entity.HasOne(d => d.IdActivitiesNavigation)
                    .WithMany(p => p.ActivityGrandChildrens)
                    .HasForeignKey(d => d.IdActivities)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("ActivityGrandChildrens_IdActivities_fkey");

                entity.HasOne(d => d.IdGrandChildrenNavigation)
                    .WithMany(p => p.ActivityGrandChildrens)
                    .HasForeignKey(d => d.IdGrandChildren)
                    .HasConstraintName("ActivityGrandChildrens_IdGrandChildren_fkey");
            });

            modelBuilder.Entity<AddressPublicHouse>(entity =>
            {
                entity.HasIndex(e => new { e.City, e.Street, e.NumberHouse, e.Type })
                    .HasName("AddressPublicHouse_City_Street_NumberHouse_Type_key")
                    .IsUnique();

                entity.Property(e => e.City)
                    .IsRequired()
                    .HasColumnType("character varying");

                entity.Property(e => e.NumberDormitory).HasColumnType("character varying");

                entity.Property(e => e.NumberHouse)
                    .IsRequired()
                    .HasColumnType("character varying");

                entity.Property(e => e.Street)
                    .IsRequired()
                    .HasColumnType("character varying");

                entity.HasOne(d => d.TypeNavigation)
                    .WithMany(p => p.AddressPublicHouse)
                    .HasForeignKey(d => d.Type)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("AddressPublicHouse_Type_fkey");
            });

            modelBuilder.Entity<ApartmentAccountingEmployees>(entity =>
            {
                entity.HasIndex(e => new { e.IdEmployee, e.FamilyComposition, e.NameAdministration, e.PriorityType, e.DateAdoption, e.Position, e.StartYearWork })
                    .HasName("ApartmentAccountingEmployees_IdEmployee_FamilyComposition_N_key")
                    .IsUnique();

                entity.Property(e => e.DateAdoption).HasColumnType("date");

                entity.Property(e => e.DateInclusion).HasColumnType("date");

                entity.Property(e => e.NameAdministration)
                    .IsRequired()
                    .HasColumnType("character varying");

                entity.Property(e => e.Position)
                    .IsRequired()
                    .HasColumnType("character varying");

                entity.Property(e => e.PriorityType)
                    .IsRequired()
                    .HasColumnType("character varying");

                entity.HasOne(d => d.IdEmployeeNavigation)
                    .WithMany(p => p.ApartmentAccountingEmployees)
                    .HasForeignKey(d => d.IdEmployee)
                    .HasConstraintName("ApartmentAccountingEmployees_IdEmployee_fkey");
            });

            modelBuilder.Entity<Award>(entity =>
            {
                entity.HasIndex(e => e.Name)
                    .HasName("Award_Name_key")
                    .IsUnique();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("character varying");
            });

            modelBuilder.Entity<AwardEmployees>(entity =>
            {
                entity.HasIndex(e => new { e.IdEmployee, e.IdAward, e.DateIssue })
                    .HasName("AwardEmployees_IdEmployee_IdAward_DateIssue_key")
                    .IsUnique();

                entity.Property(e => e.Amount).HasColumnType("money");

                entity.Property(e => e.DateIssue).HasColumnType("date");

                entity.HasOne(d => d.IdAwardNavigation)
                    .WithMany(p => p.AwardEmployees)
                    .HasForeignKey(d => d.IdAward)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("AwardEmployees_IdAward_fkey");

                entity.HasOne(d => d.IdEmployeeNavigation)
                    .WithMany(p => p.AwardEmployees)
                    .HasForeignKey(d => d.IdEmployee)
                    .HasConstraintName("AwardEmployees_IdEmployee_fkey");
            });

            modelBuilder.Entity<Children>(entity =>
            {
                entity.Property(e => e.BirthDate).HasColumnType("date");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasColumnType("character varying");

                entity.Property(e => e.Patronymic).HasColumnType("character varying");

                entity.Property(e => e.SecondName)
                    .IsRequired()
                    .HasColumnType("character varying");

                entity.HasOne(d => d.IdEmployeeNavigation)
                    .WithMany(p => p.Children)
                    .HasForeignKey(d => d.IdEmployee)
                    .HasConstraintName("Children_IdEmployee_fkey");
            });

            modelBuilder.Entity<Cultural>(entity =>
            {
                entity.HasIndex(e => e.Name)
                    .HasName("Cultural_Name_key")
                    .IsUnique();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("character varying");
            });

            modelBuilder.Entity<CulturalChildrens>(entity =>
            {
                entity.HasIndex(e => new { e.IdChildren, e.IdCultural, e.DateVisit })
                    .HasName("CulturalChildrens_IdChildren_IdCultural_DateVisit_key")
                    .IsUnique();

                entity.Property(e => e.Amount).HasColumnType("money");

                entity.Property(e => e.DateVisit).HasColumnType("date");

                entity.Property(e => e.Discount).HasColumnType("money");

                entity.HasOne(d => d.IdChildrenNavigation)
                    .WithMany(p => p.CulturalChildrens)
                    .HasForeignKey(d => d.IdChildren)
                    .HasConstraintName("CulturalChildrens_IdChildren_fkey");

                entity.HasOne(d => d.IdCulturalNavigation)
                    .WithMany(p => p.CulturalChildrens)
                    .HasForeignKey(d => d.IdCultural)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("CulturalChildrens_IdCultural_fkey");
            });

            modelBuilder.Entity<CulturalEmployees>(entity =>
            {
                entity.HasIndex(e => new { e.IdEmployee, e.IdCultural, e.DateVisit })
                    .HasName("CulturalEmployees_IdEmployee_IdCultural_DateVisit_key")
                    .IsUnique();

                entity.Property(e => e.Amount).HasColumnType("money");

                entity.Property(e => e.DateVisit).HasColumnType("date");

                entity.Property(e => e.Discount).HasColumnType("money");

                entity.HasOne(d => d.IdCulturalNavigation)
                    .WithMany(p => p.CulturalEmployees)
                    .HasForeignKey(d => d.IdCultural)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("CulturalEmployees_IdCultural_fkey");

                entity.HasOne(d => d.IdEmployeeNavigation)
                    .WithMany(p => p.CulturalEmployees)
                    .HasForeignKey(d => d.IdEmployee)
                    .HasConstraintName("CulturalEmployees_IdEmployee_fkey");
            });

            modelBuilder.Entity<CulturalFamily>(entity =>
            {
                entity.HasIndex(e => new { e.IdFamily, e.IdCultural, e.DateVisit })
                    .HasName("CulturalFamily_IdFamily_IdCultural_DateVisit_key")
                    .IsUnique();

                entity.Property(e => e.Amount).HasColumnType("money");

                entity.Property(e => e.DateVisit).HasColumnType("date");

                entity.Property(e => e.Discount).HasColumnType("money");

                entity.HasOne(d => d.IdCulturalNavigation)
                    .WithMany(p => p.CulturalFamily)
                    .HasForeignKey(d => d.IdCultural)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("CulturalFamily_IdCultural_fkey");

                entity.HasOne(d => d.IdFamilyNavigation)
                    .WithMany(p => p.CulturalFamily)
                    .HasForeignKey(d => d.IdFamily)
                    .HasConstraintName("CulturalFamily_IdFamily_fkey");
            });

            modelBuilder.Entity<CulturalGrandChildrens>(entity =>
            {
                entity.HasIndex(e => new { e.IdGrandChildren, e.IdCultural, e.DateVisit })
                    .HasName("CulturalGrandChildrens_IdGrandChildren_IdCultural_DateVisit_key")
                    .IsUnique();

                entity.Property(e => e.Amount).HasColumnType("money");

                entity.Property(e => e.DateVisit).HasColumnType("date");

                entity.Property(e => e.Discount).HasColumnType("money");

                entity.HasOne(d => d.IdCulturalNavigation)
                    .WithMany(p => p.CulturalGrandChildrens)
                    .HasForeignKey(d => d.IdCultural)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("CulturalGrandChildrens_IdCultural_fkey");

                entity.HasOne(d => d.IdGrandChildrenNavigation)
                    .WithMany(p => p.CulturalGrandChildrens)
                    .HasForeignKey(d => d.IdGrandChildren)
                    .HasConstraintName("CulturalGrandChildrens_IdGrandChildren_fkey");
            });

            modelBuilder.Entity<Education>(entity =>
            {
                entity.HasIndex(e => e.IdEmployee)
                    .HasName("Education_IdEmployee_key")
                    .IsUnique();

                entity.Property(e => e.LevelEducation)
                    .IsRequired()
                    .HasColumnType("character varying");

                entity.Property(e => e.NameInstitution)
                    .IsRequired()
                    .HasColumnType("character varying");

                entity.HasOne(d => d.IdEmployeeNavigation)
                    .WithOne(p => p.Education)
                    .HasForeignKey<Education>(d => d.IdEmployee)
                    .HasConstraintName("Education_IdEmployee_fkey");
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.HasIndex(e => e.IdentificationСode)
                    .HasName("Employee_IdentificationСode_key")
                    .IsUnique();

                entity.HasIndex(e => e.MechnikovCard)
                    .HasName("Employee_MechnikovCard_key")
                    .IsUnique();

                entity.Property(e => e.BasicProfession)
                    .IsRequired()
                    .HasColumnType("character varying");

                entity.Property(e => e.BirthDate).HasColumnType("date");

                entity.Property(e => e.CityPhone).HasColumnType("character varying");

                entity.Property(e => e.DateAdded)
                    .HasColumnType("date")
                    .HasDefaultValueSql("CURRENT_DATE");

                entity.Property(e => e.EndDateTradeUnion).HasColumnType("date");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasColumnType("character varying");

                entity.Property(e => e.IdentificationСode).HasColumnType("character varying");

                entity.Property(e => e.MechnikovCard).HasColumnType("character varying");

                entity.Property(e => e.MobilePhone).HasColumnType("character varying");

                entity.Property(e => e.Patronymic).HasColumnType("character varying");

                entity.Property(e => e.SecondName)
                    .IsRequired()
                    .HasColumnType("character varying");

                entity.Property(e => e.Sex)
                    .IsRequired()
                    .HasColumnType("character varying");

                entity.Property(e => e.StartDateTradeUnion).HasColumnType("date");
            });

            modelBuilder.Entity<Event>(entity =>
            {
                entity.HasIndex(e => e.Name)
                    .HasName("Event_Name_key")
                    .IsUnique();

                entity.HasIndex(e => e.TypeId)
                    .HasName("Event_TypeId_key")
                    .IsUnique();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("character varying");

                entity.HasOne(d => d.Type)
                    .WithOne(p => p.Event)
                    .HasForeignKey<Event>(d => d.TypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Event_TypeId_fkey");
            });

            modelBuilder.Entity<EventChildrens>(entity =>
            {
                entity.HasIndex(e => new { e.IdChildren, e.IdEvent, e.StartDate })
                    .HasName("EventChildrens_IdChildren_IdEvent_StartDate_key")
                    .IsUnique();

                entity.Property(e => e.Amount).HasColumnType("money");

                entity.Property(e => e.Discount).HasColumnType("money");

                entity.Property(e => e.EndDate).HasColumnType("date");

                entity.Property(e => e.StartDate).HasColumnType("date");

                entity.HasOne(d => d.IdChildrenNavigation)
                    .WithMany(p => p.EventChildrens)
                    .HasForeignKey(d => d.IdChildren)
                    .HasConstraintName("EventChildrens_IdChildren_fkey");

                entity.HasOne(d => d.IdEventNavigation)
                    .WithMany(p => p.EventChildrens)
                    .HasForeignKey(d => d.IdEvent)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("EventChildrens_IdEvent_fkey");
            });

            modelBuilder.Entity<EventEmployees>(entity =>
            {
                entity.HasIndex(e => new { e.IdEmployee, e.IdEvent, e.StartDate })
                    .HasName("EventEmployees_IdEmployee_IdEvent_StartDate_key")
                    .IsUnique();

                entity.Property(e => e.Amount).HasColumnType("money");

                entity.Property(e => e.Discount).HasColumnType("money");

                entity.Property(e => e.EndDate).HasColumnType("date");

                entity.Property(e => e.StartDate).HasColumnType("date");

                entity.HasOne(d => d.IdEmployeeNavigation)
                    .WithMany(p => p.EventEmployees)
                    .HasForeignKey(d => d.IdEmployee)
                    .HasConstraintName("EventEmployees_IdEmployee_fkey");

                entity.HasOne(d => d.IdEventNavigation)
                    .WithMany(p => p.EventEmployees)
                    .HasForeignKey(d => d.IdEvent)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("EventEmployees_IdEvent_fkey");
            });

            modelBuilder.Entity<EventFamily>(entity =>
            {
                entity.HasIndex(e => new { e.IdFamily, e.IdEvent, e.StartDate })
                    .HasName("EventFamily_IdFamily_IdEvent_StartDate_key")
                    .IsUnique();

                entity.Property(e => e.Amount).HasColumnType("money");

                entity.Property(e => e.Discount).HasColumnType("money");

                entity.Property(e => e.EndDate).HasColumnType("date");

                entity.Property(e => e.StartDate).HasColumnType("date");

                entity.HasOne(d => d.IdEventNavigation)
                    .WithMany(p => p.EventFamily)
                    .HasForeignKey(d => d.IdEvent)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("EventFamily_IdEvent_fkey");

                entity.HasOne(d => d.IdFamilyNavigation)
                    .WithMany(p => p.EventFamily)
                    .HasForeignKey(d => d.IdFamily)
                    .HasConstraintName("EventFamily_IdFamily_fkey");
            });

            modelBuilder.Entity<EventGrandChildrens>(entity =>
            {
                entity.HasIndex(e => new { e.IdGrandChildren, e.IdEvent, e.StartDate })
                    .HasName("EventGrandChildrens_IdGrandChildren_IdEvent_StartDate_key")
                    .IsUnique();

                entity.Property(e => e.Amount).HasColumnType("money");

                entity.Property(e => e.Discount).HasColumnType("money");

                entity.Property(e => e.EndDate).HasColumnType("date");

                entity.Property(e => e.StartDate).HasColumnType("date");

                entity.HasOne(d => d.IdEventNavigation)
                    .WithMany(p => p.EventGrandChildrens)
                    .HasForeignKey(d => d.IdEvent)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("EventGrandChildrens_IdEvent_fkey");

                entity.HasOne(d => d.IdGrandChildrenNavigation)
                    .WithMany(p => p.EventGrandChildrens)
                    .HasForeignKey(d => d.IdGrandChildren)
                    .HasConstraintName("EventGrandChildrens_IdGrandChildren_fkey");
            });

            modelBuilder.Entity<Family>(entity =>
            {
                entity.Property(e => e.BirthDate).HasColumnType("date");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasColumnType("character varying");

                entity.Property(e => e.Patronymic).HasColumnType("character varying");

                entity.Property(e => e.SecondName)
                    .IsRequired()
                    .HasColumnType("character varying");

                entity.HasOne(d => d.IdEmployeeNavigation)
                    .WithMany(p => p.Family)
                    .HasForeignKey(d => d.IdEmployee)
                    .HasConstraintName("Family_IdEmployee_fkey");
            });

            modelBuilder.Entity<FluorographyEmployees>(entity =>
            {
                entity.HasIndex(e => new { e.IdEmployee, e.Result, e.DatePassage })
                    .HasName("FluorographyEmployees_IdEmployee_Result_DatePassage_key")
                    .IsUnique();

                entity.Property(e => e.DatePassage).HasColumnType("date");

                entity.Property(e => e.PlacePassing)
                    .IsRequired()
                    .HasColumnType("character varying");

                entity.Property(e => e.Result)
                    .IsRequired()
                    .HasColumnType("character varying");

                entity.HasOne(d => d.IdEmployeeNavigation)
                    .WithMany(p => p.FluorographyEmployees)
                    .HasForeignKey(d => d.IdEmployee)
                    .HasConstraintName("FluorographyEmployees_IdEmployee_fkey");
            });

            modelBuilder.Entity<GiftChildrens>(entity =>
            {
                entity.HasIndex(e => new { e.IdChildren, e.NameEvent, e.NameGift, e.DateGift })
                    .HasName("GiftChildrens_IdChildren_NameEvent_NameGift_DateGift_key")
                    .IsUnique();

                entity.Property(e => e.DateGift).HasColumnType("date");

                entity.Property(e => e.Discount).HasColumnType("money");

                entity.Property(e => e.NameEvent)
                    .IsRequired()
                    .HasColumnType("character varying");

                entity.Property(e => e.NameGift)
                    .IsRequired()
                    .HasColumnType("character varying");

                entity.Property(e => e.Price).HasColumnType("money");

                entity.HasOne(d => d.IdChildrenNavigation)
                    .WithMany(p => p.GiftChildrens)
                    .HasForeignKey(d => d.IdChildren)
                    .HasConstraintName("GiftChildrens_IdChildren_fkey");
            });

            modelBuilder.Entity<GiftEmployees>(entity =>
            {
                entity.HasIndex(e => new { e.IdEmployee, e.NameEvent, e.NameGift, e.DateGift })
                    .HasName("GiftEmployees_IdEmployee_NameEvent_NameGift_DateGift_key")
                    .IsUnique();

                entity.Property(e => e.DateGift).HasColumnType("date");

                entity.Property(e => e.Discount).HasColumnType("money");

                entity.Property(e => e.NameEvent)
                    .IsRequired()
                    .HasColumnType("character varying");

                entity.Property(e => e.NameGift)
                    .IsRequired()
                    .HasColumnType("character varying");

                entity.Property(e => e.Price).HasColumnType("money");

                entity.HasOne(d => d.IdEmployeeNavigation)
                    .WithMany(p => p.GiftEmployees)
                    .HasForeignKey(d => d.IdEmployee)
                    .HasConstraintName("GiftEmployees_IdEmployee_fkey");
            });

            modelBuilder.Entity<GiftGrandChildrens>(entity =>
            {
                entity.HasIndex(e => new { e.IdGrandChildren, e.NameEvent, e.NameGifts, e.DateGift })
                    .HasName("GiftGrandChildrens_IdGrandChildren_NameEvent_NameGifts_Date_key")
                    .IsUnique();

                entity.Property(e => e.DateGift).HasColumnType("date");

                entity.Property(e => e.Discount).HasColumnType("money");

                entity.Property(e => e.NameEvent)
                    .IsRequired()
                    .HasColumnType("character varying");

                entity.Property(e => e.NameGifts)
                    .IsRequired()
                    .HasColumnType("character varying");

                entity.Property(e => e.Price).HasColumnType("money");

                entity.HasOne(d => d.IdGrandChildrenNavigation)
                    .WithMany(p => p.GiftGrandChildrens)
                    .HasForeignKey(d => d.IdGrandChildren)
                    .HasConstraintName("GiftGrandChildrens_IdGrandChildren_fkey");
            });

            modelBuilder.Entity<GrandChildren>(entity =>
            {
                entity.Property(e => e.BirthDate).HasColumnType("date");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasColumnType("character varying");

                entity.Property(e => e.Patronymic).HasColumnType("character varying");

                entity.Property(e => e.SecondName)
                    .IsRequired()
                    .HasColumnType("character varying");

                entity.HasOne(d => d.IdEmployeeNavigation)
                    .WithMany(p => p.GrandChildren)
                    .HasForeignKey(d => d.IdEmployee)
                    .HasConstraintName("GrandChildren_IdEmployee_fkey");
            });

            modelBuilder.Entity<Hobby>(entity =>
            {
                entity.HasIndex(e => e.Name)
                    .HasName("Hobby_Name_key")
                    .IsUnique();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("character varying");
            });

            modelBuilder.Entity<HobbyChildrens>(entity =>
            {
                entity.HasIndex(e => new { e.IdChildren, e.IdHobby })
                    .HasName("HobbyChildrens_IdChildren_IdHobby_key")
                    .IsUnique();

                entity.HasOne(d => d.IdChildrenNavigation)
                    .WithMany(p => p.HobbyChildrens)
                    .HasForeignKey(d => d.IdChildren)
                    .HasConstraintName("HobbyChildrens_IdChildren_fkey");

                entity.HasOne(d => d.IdHobbyNavigation)
                    .WithMany(p => p.HobbyChildrens)
                    .HasForeignKey(d => d.IdHobby)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("HobbyChildrens_IdHobby_fkey");
            });

            modelBuilder.Entity<HobbyEmployees>(entity =>
            {
                entity.HasIndex(e => new { e.IdEmployee, e.IdHobby })
                    .HasName("HobbyEmployees_IdEmployee_IdHobby_key")
                    .IsUnique();

                entity.HasOne(d => d.IdEmployeeNavigation)
                    .WithMany(p => p.HobbyEmployees)
                    .HasForeignKey(d => d.IdEmployee)
                    .HasConstraintName("HobbyEmployees_IdEmployee_fkey");

                entity.HasOne(d => d.IdHobbyNavigation)
                    .WithMany(p => p.HobbyEmployees)
                    .HasForeignKey(d => d.IdHobby)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("HobbyEmployees_IdHobby_fkey");
            });

            modelBuilder.Entity<HobbyGrandChildrens>(entity =>
            {
                entity.HasIndex(e => new { e.IdGrandChildren, e.IdHobby })
                    .HasName("HobbyGrandChildrens_IdGrandChildren_IdHobby_key")
                    .IsUnique();

                entity.HasOne(d => d.IdGrandChildrenNavigation)
                    .WithMany(p => p.HobbyGrandChildrens)
                    .HasForeignKey(d => d.IdGrandChildren)
                    .HasConstraintName("HobbyGrandChildrens_IdGrandChildren_fkey");

                entity.HasOne(d => d.IdHobbyNavigation)
                    .WithMany(p => p.HobbyGrandChildrens)
                    .HasForeignKey(d => d.IdHobby)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("HobbyGrandChildrens_IdHobby_fkey");
            });

            modelBuilder.Entity<MaterialAid>(entity =>
            {
                entity.HasIndex(e => e.Name)
                    .HasName("MaterialAid_Name_key")
                    .IsUnique();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("character varying");
            });

            modelBuilder.Entity<MaterialAidEmployees>(entity =>
            {
                entity.HasIndex(e => new { e.IdEmployee, e.IdMaterialAid, e.DateIssue })
                    .HasName("MaterialAidEmployees_IdEmployee_IdMaterialAid_DateIssue_key")
                    .IsUnique();

                entity.Property(e => e.Amount).HasColumnType("money");

                entity.Property(e => e.DateIssue).HasColumnType("date");

                entity.HasOne(d => d.IdEmployeeNavigation)
                    .WithMany(p => p.MaterialAidEmployees)
                    .HasForeignKey(d => d.IdEmployee)
                    .HasConstraintName("MaterialAidEmployees_IdEmployee_fkey");

                entity.HasOne(d => d.IdMaterialAidNavigation)
                    .WithMany(p => p.MaterialAidEmployees)
                    .HasForeignKey(d => d.IdMaterialAid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("MaterialAidEmployees_IdMaterialAid_fkey");
            });

            modelBuilder.Entity<Position>(entity =>
            {
                entity.HasIndex(e => e.Name)
                    .HasName("Position_Name_key")
                    .IsUnique();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("character varying");
            });

            modelBuilder.Entity<PositionEmployees>(entity =>
            {
                entity.HasIndex(e => e.IdEmployee)
                    .HasName("PositionEmployees_IdEmployee_key")
                    .IsUnique();

                entity.Property(e => e.EndDate).HasColumnType("date");

                entity.Property(e => e.StartDate).HasColumnType("date");

                entity.HasOne(d => d.IdEmployeeNavigation)
                    .WithOne(p => p.PositionEmployees)
                    .HasForeignKey<PositionEmployees>(d => d.IdEmployee)
                    .HasConstraintName("PositionEmployees_IdEmployee_fkey");

                entity.HasOne(d => d.IdPositionNavigation)
                    .WithMany(p => p.PositionEmployees)
                    .HasForeignKey(d => d.IdPosition)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("PositionEmployees_IdPosition_fkey");

                entity.HasOne(d => d.IdSubdivisionNavigation)
                    .WithMany(p => p.PositionEmployees)
                    .HasForeignKey(d => d.IdSubdivision)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("PositionEmployees_IdSubdivision_fkey");
            });

            modelBuilder.Entity<PrivateHouseEmployees>(entity =>
            {
                entity.Property(e => e.City)
                    .IsRequired()
                    .HasColumnType("character varying");

                entity.Property(e => e.DateReceiving).HasColumnType("date");

                entity.Property(e => e.NumberApartment).HasColumnType("character varying");

                entity.Property(e => e.NumberHouse).HasColumnType("character varying");

                entity.Property(e => e.Street)
                    .IsRequired()
                    .HasColumnType("character varying");

                entity.HasOne(d => d.IdEmployeeNavigation)
                    .WithMany(p => p.PrivateHouseEmployees)
                    .HasForeignKey(d => d.IdEmployee)
                    .HasConstraintName("PrivateHouseEmployees_IdEmployee_fkey");
            });

            modelBuilder.Entity<PrivilegeEmployees>(entity =>
            {
                entity.HasIndex(e => e.IdEmployee)
                    .HasName("PrivilegeEmployees_IdEmployee_key")
                    .IsUnique();

                entity.HasOne(d => d.IdEmployeeNavigation)
                    .WithOne(p => p.PrivilegeEmployees)
                    .HasForeignKey<PrivilegeEmployees>(d => d.IdEmployee)
                    .HasConstraintName("PrivilegeEmployees_IdEmployee_fkey");

                entity.HasOne(d => d.IdPrivilegesNavigation)
                    .WithMany(p => p.PrivilegeEmployees)
                    .HasForeignKey(d => d.IdPrivileges)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("PrivilegeEmployees_IdPrivileges_fkey");
            });

            modelBuilder.Entity<Privileges>(entity =>
            {
                entity.HasIndex(e => e.Name)
                    .HasName("Privileges_Name_key")
                    .IsUnique();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("character varying");
            });

            modelBuilder.Entity<PublicHouseEmployees>(entity =>
            {
                entity.HasKey(e => new { e.IdAddressPublicHouse, e.IdEmployee });

                entity.Property(e => e.NumberRoom).HasColumnType("character varying");

                entity.HasOne(d => d.IdAddressPublicHouseNavigation)
                    .WithMany(p => p.PublicHouseEmployees)
                    .HasForeignKey(d => d.IdAddressPublicHouse)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("PublicHouseEmployees_IdAddressPublicHouse_fkey");

                entity.HasOne(d => d.IdEmployeeNavigation)
                    .WithMany(p => p.PublicHouseEmployees)
                    .HasForeignKey(d => d.IdEmployee)
                    .HasConstraintName("PublicHouseEmployees_IdEmployee_fkey");
            });

            modelBuilder.Entity<Scientific>(entity =>
            {
                entity.HasIndex(e => e.IdEmployee)
                    .HasName("Scientific_IdEmployee_key")
                    .IsUnique();

                entity.Property(e => e.ScientificDegree)
                    .IsRequired()
                    .HasColumnType("character varying");

                entity.Property(e => e.ScientificTitle)
                    .IsRequired()
                    .HasColumnType("character varying");

                entity.HasOne(d => d.IdEmployeeNavigation)
                    .WithOne(p => p.Scientific)
                    .HasForeignKey<Scientific>(d => d.IdEmployee)
                    .HasConstraintName("Scientific_IdEmployee_fkey");
            });

            modelBuilder.Entity<SocialActivity>(entity =>
            {
                entity.HasIndex(e => e.Name)
                    .HasName("SocialActivity_Name_key")
                    .IsUnique();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("character varying");
            });

            modelBuilder.Entity<SocialActivityEmployees>(entity =>
            {
                entity.HasIndex(e => e.IdEmployee)
                    .HasName("SocialActivityEmployees_IdEmployee_key")
                    .IsUnique();

                entity.HasOne(d => d.IdEmployeeNavigation)
                    .WithOne(p => p.SocialActivityEmployees)
                    .HasForeignKey<SocialActivityEmployees>(d => d.IdEmployee)
                    .HasConstraintName("SocialActivityEmployees_IdEmployee_fkey");

                entity.HasOne(d => d.IdSocialActivityNavigation)
                    .WithMany(p => p.SocialActivityEmployees)
                    .HasForeignKey(d => d.IdSocialActivity)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("SocialActivityEmployees_IdSocialActivity_fkey");
            });

            modelBuilder.Entity<Subdivisions>(entity =>
            {
                entity.HasIndex(e => e.DeptName)
                    .HasName("Subdivisions_DeptName_key")
                    .IsUnique();

                entity.Property(e => e.DeptName)
                    .IsRequired()
                    .HasColumnType("character varying");

                entity.HasOne(d => d.IdSubordinateNavigation)
                    .WithMany(p => p.InverseIdSubordinateNavigation)
                    .HasForeignKey(d => d.IdSubordinate)
                    .HasConstraintName("Subdivisions_IdSubordinate_fkey");
            });

            modelBuilder.Entity<TypeEvent>(entity =>
            {
                entity.HasIndex(e => e.Name)
                    .HasName("TypeEvent_Name_key")
                    .IsUnique();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("character varying");
            });

            modelBuilder.Entity<TypeHouse>(entity =>
            {
                entity.HasIndex(e => e.Name)
                    .HasName("TypeHouse_Name_key")
                    .IsUnique();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("character varying");
            });
        }
    }
}
