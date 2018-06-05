using Microsoft.EntityFrameworkCore;
using TradeUnionCommittee.Common.Entities;

namespace TradeUnionCommittee.Common.EF
{
    public class TradeUnionCommitteeEmployeesCoreContext : DbContext
    {
        public TradeUnionCommitteeEmployeesCoreContext()
        {
        }

        public TradeUnionCommitteeEmployeesCoreContext(DbContextOptions<TradeUnionCommitteeEmployeesCoreContext> options)
            : base(options)
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
        public virtual DbSet<HobbyChildrens> Hobbychildrens { get; set; }
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
                optionsBuilder.UseNpgsql("Host=127.0.0.1;Port=5432;Database=TradeUnionCommitteeEmployeesCore;Username=postgres;Password=postgres");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Activities>(entity =>
            {
                entity.ToTable("activities", "directories");

                entity.HasIndex(e => e.Name)
                    .HasName("activities_name_key")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("nextval('directories.activities_id_seq'::regclass)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasColumnType("character varying");
            });

            modelBuilder.Entity<ActivityChildrens>(entity =>
            {
                entity.ToTable("activitychildrens", "lists");

                entity.HasIndex(e => new { e.IdChildren, e.IdActivities, e.DateEvent })
                    .HasName("activitychildrens_idchildren_idactivities_dateevent_key")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("nextval('lists.activitychildrens_id_seq'::regclass)");

                entity.Property(e => e.DateEvent)
                    .HasColumnName("dateevent")
                    .HasColumnType("date");

                entity.Property(e => e.IdActivities).HasColumnName("idactivities");

                entity.Property(e => e.IdChildren).HasColumnName("idchildren");

                entity.HasOne(d => d.IdActivitiesNavigation)
                    .WithMany(p => p.ActivityChildrens)
                    .HasForeignKey(d => d.IdActivities)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("activitychildrens_idactivities_fkey");

                entity.HasOne(d => d.IdChildrenNavigation)
                    .WithMany(p => p.ActivityChildrens)
                    .HasForeignKey(d => d.IdChildren)
                    .HasConstraintName("activitychildrens_idchildren_fkey");
            });

            modelBuilder.Entity<ActivityEmployees>(entity =>
            {
                entity.ToTable("activityemployees", "lists");

                entity.HasIndex(e => new { e.IdEmployee, e.IdActivities, e.DateEvent })
                    .HasName("activityemployees_idemployee_idactivities_dateevent_key")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("nextval('lists.activityemployees_id_seq'::regclass)");

                entity.Property(e => e.DateEvent)
                    .HasColumnName("dateevent")
                    .HasColumnType("date");

                entity.Property(e => e.IdActivities).HasColumnName("idactivities");

                entity.Property(e => e.IdEmployee).HasColumnName("idemployee");

                entity.HasOne(d => d.IdActivitiesNavigation)
                    .WithMany(p => p.ActivityEmployees)
                    .HasForeignKey(d => d.IdActivities)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("activityemployees_idactivities_fkey");

                entity.HasOne(d => d.IdEmployeeNavigation)
                    .WithMany(p => p.ActivityEmployees)
                    .HasForeignKey(d => d.IdEmployee)
                    .HasConstraintName("activityemployees_idemployee_fkey");
            });

            modelBuilder.Entity<ActivityFamily>(entity =>
            {
                entity.ToTable("activityfamily", "lists");

                entity.HasIndex(e => new { e.IdFamily, e.IdActivities, e.DateEvent })
                    .HasName("activityfamily_idfamily_idactivities_dateevent_key")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("nextval('lists.activityfamily_id_seq'::regclass)");

                entity.Property(e => e.DateEvent)
                    .HasColumnName("dateevent")
                    .HasColumnType("date");

                entity.Property(e => e.IdActivities).HasColumnName("idactivities");

                entity.Property(e => e.IdFamily).HasColumnName("idfamily");

                entity.HasOne(d => d.IdActivitiesNavigation)
                    .WithMany(p => p.ActivityFamily)
                    .HasForeignKey(d => d.IdActivities)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("activityfamily_idactivities_fkey");

                entity.HasOne(d => d.IdFamilyNavigation)
                    .WithMany(p => p.ActivityFamily)
                    .HasForeignKey(d => d.IdFamily)
                    .HasConstraintName("activityfamily_idfamily_fkey");
            });

            modelBuilder.Entity<ActivityGrandChildrens>(entity =>
            {
                entity.ToTable("activitygrandchildrens", "lists");

                entity.HasIndex(e => new { e.IdGrandChildren, e.IdActivities, e.DateEvent })
                    .HasName("activitygrandchildrens_idgrandchildren_idactivities_dateeve_key")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("nextval('lists.activitygrandchildrens_id_seq'::regclass)");

                entity.Property(e => e.DateEvent)
                    .HasColumnName("dateevent")
                    .HasColumnType("date");

                entity.Property(e => e.IdActivities).HasColumnName("idactivities");

                entity.Property(e => e.IdGrandChildren).HasColumnName("idgrandchildren");

                entity.HasOne(d => d.IdActivitiesNavigation)
                    .WithMany(p => p.ActivityGrandChildrens)
                    .HasForeignKey(d => d.IdActivities)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("activitygrandchildrens_idactivities_fkey");

                entity.HasOne(d => d.IdGrandchildrenNavigation)
                    .WithMany(p => p.ActivityGrandChildrens)
                    .HasForeignKey(d => d.IdGrandChildren)
                    .HasConstraintName("activitygrandchildrens_idgrandchildren_fkey");
            });

            modelBuilder.Entity<AddressPublicHouse>(entity =>
            {
                entity.ToTable("addresspublichouse", "directories");

                entity.HasIndex(e => new { e.City, e.Street, e.NumberHouse, e.Type })
                    .HasName("addresspublichouse_city_street_numberhouse_type_key")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("nextval('directories.addresspublichouse_id_seq'::regclass)");

                entity.Property(e => e.City)
                    .IsRequired()
                    .HasColumnName("city")
                    .HasColumnType("character varying");

                entity.Property(e => e.NumberDormitory)
                    .HasColumnName("numberdormitory")
                    .HasColumnType("character varying");

                entity.Property(e => e.NumberHouse)
                    .IsRequired()
                    .HasColumnName("numberhouse")
                    .HasColumnType("character varying");

                entity.Property(e => e.Street)
                    .IsRequired()
                    .HasColumnName("street")
                    .HasColumnType("character varying");

                entity.Property(e => e.Type).HasColumnName("type");

                entity.HasOne(d => d.TypeNavigation)
                    .WithMany(p => p.AddressPublicHouse)
                    .HasForeignKey(d => d.Type)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("addresspublichouse_type_fkey");
            });

            modelBuilder.Entity<ApartmentAccountingEmployees>(entity =>
            {
                entity.ToTable("apartmentaccountingemployees", "lists");

                entity.HasIndex(e => new { e.IdEmployee, e.FamilyComposition, e.NameAdministration, e.PriorityType, e.DateAdoption, e.Position, e.StartYearWork })
                    .HasName("apartmentaccountingemployees_idemployee_familycomposition_n_key")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("nextval('lists.apartmentaccountingemployees_id_seq'::regclass)");

                entity.Property(e => e.DateAdoption)
                    .HasColumnName("dateadoption")
                    .HasColumnType("date");

                entity.Property(e => e.DateInclusion)
                    .HasColumnName("dateinclusion")
                    .HasColumnType("date");

                entity.Property(e => e.FamilyComposition).HasColumnName("familycomposition");

                entity.Property(e => e.IdEmployee).HasColumnName("idemployee");

                entity.Property(e => e.NameAdministration)
                    .IsRequired()
                    .HasColumnName("nameadministration")
                    .HasColumnType("character varying");

                entity.Property(e => e.Position)
                    .IsRequired()
                    .HasColumnName("position")
                    .HasColumnType("character varying");

                entity.Property(e => e.PriorityType)
                    .IsRequired()
                    .HasColumnName("prioritytype")
                    .HasColumnType("character varying");

                entity.Property(e => e.StartYearWork).HasColumnName("startyearwork");

                entity.HasOne(d => d.IdEmployeeNavigation)
                    .WithMany(p => p.ApartmentAccountingEmployees)
                    .HasForeignKey(d => d.IdEmployee)
                    .HasConstraintName("apartmentaccountingemployees_idemployee_fkey");
            });

            modelBuilder.Entity<Award>(entity =>
            {
                entity.ToTable("award", "directories");

                entity.HasIndex(e => e.Name)
                    .HasName("award_name_key")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("nextval('directories.award_id_seq'::regclass)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasColumnType("character varying");
            });

            modelBuilder.Entity<AwardEmployees>(entity =>
            {
                entity.ToTable("awardemployees", "lists");

                entity.HasIndex(e => new { e.IdEmployee, e.IdAward, e.DateIssue })
                    .HasName("awardemployees_idemployee_idaward_dateissue_key")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("nextval('lists.awardemployees_id_seq'::regclass)");

                entity.Property(e => e.Amount)
                    .HasColumnName("amount")
                    .HasColumnType("money");

                entity.Property(e => e.DateIssue)
                    .HasColumnName("dateissue")
                    .HasColumnType("date");

                entity.Property(e => e.IdAward).HasColumnName("idaward");

                entity.Property(e => e.IdEmployee).HasColumnName("idemployee");

                entity.HasOne(d => d.IdAwardNavigation)
                    .WithMany(p => p.AwardEmployees)
                    .HasForeignKey(d => d.IdAward)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("awardemployees_idaward_fkey");

                entity.HasOne(d => d.IdEmployeeNavigation)
                    .WithMany(p => p.AwardEmployees)
                    .HasForeignKey(d => d.IdEmployee)
                    .HasConstraintName("awardemployees_idemployee_fkey");
            });

            modelBuilder.Entity<Children>(entity =>
            {
                entity.ToTable("children", "main");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("nextval('main.children_id_seq'::regclass)");

                entity.Property(e => e.BirthDate)
                    .HasColumnName("birthdate")
                    .HasColumnType("date");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasColumnName("firstname")
                    .HasColumnType("character varying");

                entity.Property(e => e.IdEmployee).HasColumnName("idemployee");

                entity.Property(e => e.Patronymic)
                    .HasColumnName("patronymic")
                    .HasColumnType("character varying");

                entity.Property(e => e.SecondName)
                    .IsRequired()
                    .HasColumnName("secondname")
                    .HasColumnType("character varying");

                entity.HasOne(d => d.IdEmployeeNavigation)
                    .WithMany(p => p.Children)
                    .HasForeignKey(d => d.IdEmployee)
                    .HasConstraintName("children_idemployee_fkey");
            });

            modelBuilder.Entity<Cultural>(entity =>
            {
                entity.ToTable("cultural", "directories");

                entity.HasIndex(e => e.Name)
                    .HasName("cultural_name_key")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("nextval('directories.cultural_id_seq'::regclass)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasColumnType("character varying");
            });

            modelBuilder.Entity<CulturalChildrens>(entity =>
            {
                entity.ToTable("culturalchildrens", "lists");

                entity.HasIndex(e => new { e.IdChildren, e.IdCultural, e.DateVisit })
                    .HasName("culturalchildrens_idchildren_idcultural_datevisit_key")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("nextval('lists.culturalchildrens_id_seq'::regclass)");

                entity.Property(e => e.Amount)
                    .HasColumnName("amount")
                    .HasColumnType("money");

                entity.Property(e => e.DateVisit)
                    .HasColumnName("datevisit")
                    .HasColumnType("date");

                entity.Property(e => e.Discount)
                    .HasColumnName("discount")
                    .HasColumnType("money");

                entity.Property(e => e.IdChildren).HasColumnName("idchildren");

                entity.Property(e => e.IdCultural).HasColumnName("idcultural");

                entity.HasOne(d => d.IdChildrenNavigation)
                    .WithMany(p => p.CulturalChildrens)
                    .HasForeignKey(d => d.IdChildren)
                    .HasConstraintName("culturalchildrens_idchildren_fkey");

                entity.HasOne(d => d.IdCulturalNavigation)
                    .WithMany(p => p.CulturalChildrens)
                    .HasForeignKey(d => d.IdCultural)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("culturalchildrens_idcultural_fkey");
            });

            modelBuilder.Entity<CulturalEmployees>(entity =>
            {
                entity.ToTable("culturalemployees", "lists");

                entity.HasIndex(e => new { e.IdEmployee, e.IdCultural, e.DateVisit })
                    .HasName("culturalemployees_idemployee_idcultural_datevisit_key")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("nextval('lists.culturalemployees_id_seq'::regclass)");

                entity.Property(e => e.Amount)
                    .HasColumnName("amount")
                    .HasColumnType("money");

                entity.Property(e => e.DateVisit)
                    .HasColumnName("datevisit")
                    .HasColumnType("date");

                entity.Property(e => e.Discount)
                    .HasColumnName("discount")
                    .HasColumnType("money");

                entity.Property(e => e.IdCultural).HasColumnName("idcultural");

                entity.Property(e => e.IdEmployee).HasColumnName("idemployee");

                entity.HasOne(d => d.IdCulturalNavigation)
                    .WithMany(p => p.CulturalEmployees)
                    .HasForeignKey(d => d.IdCultural)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("culturalemployees_idcultural_fkey");

                entity.HasOne(d => d.IdEmployeeNavigation)
                    .WithMany(p => p.CulturalEmployees)
                    .HasForeignKey(d => d.IdEmployee)
                    .HasConstraintName("culturalemployees_idemployee_fkey");
            });

            modelBuilder.Entity<CulturalFamily>(entity =>
            {
                entity.ToTable("culturalfamily", "lists");

                entity.HasIndex(e => new { e.IdFamily, e.IdCultural, e.DateVisit })
                    .HasName("culturalfamily_idfamily_idcultural_datevisit_key")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("nextval('lists.culturalfamily_id_seq'::regclass)");

                entity.Property(e => e.Amount)
                    .HasColumnName("amount")
                    .HasColumnType("money");

                entity.Property(e => e.DateVisit)
                    .HasColumnName("datevisit")
                    .HasColumnType("date");

                entity.Property(e => e.Discount)
                    .HasColumnName("discount")
                    .HasColumnType("money");

                entity.Property(e => e.IdCultural).HasColumnName("idcultural");

                entity.Property(e => e.IdFamily).HasColumnName("idfamily");

                entity.HasOne(d => d.IdCulturalNavigation)
                    .WithMany(p => p.CulturalFamily)
                    .HasForeignKey(d => d.IdCultural)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("culturalfamily_idcultural_fkey");

                entity.HasOne(d => d.IdFamilyNavigation)
                    .WithMany(p => p.CulturalFamily)
                    .HasForeignKey(d => d.IdFamily)
                    .HasConstraintName("culturalfamily_idfamily_fkey");
            });

            modelBuilder.Entity<CulturalGrandChildrens>(entity =>
            {
                entity.ToTable("culturalgrandchildrens", "lists");

                entity.HasIndex(e => new { e.IdGrandChildren, e.IdCultural, e.DateVisit })
                    .HasName("culturalgrandchildrens_idgrandchildren_idcultural_datevisit_key")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("nextval('lists.culturalgrandchildrens_id_seq'::regclass)");

                entity.Property(e => e.Amount)
                    .HasColumnName("amount")
                    .HasColumnType("money");

                entity.Property(e => e.DateVisit)
                    .HasColumnName("datevisit")
                    .HasColumnType("date");

                entity.Property(e => e.Discount)
                    .HasColumnName("discount")
                    .HasColumnType("money");

                entity.Property(e => e.IdCultural).HasColumnName("idcultural");

                entity.Property(e => e.IdGrandChildren).HasColumnName("idgrandchildren");

                entity.HasOne(d => d.IdCulturalNavigation)
                    .WithMany(p => p.CulturalGrandChildrens)
                    .HasForeignKey(d => d.IdCultural)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("culturalgrandchildrens_idcultural_fkey");

                entity.HasOne(d => d.IdGrandchildrenNavigation)
                    .WithMany(p => p.CulturalGrandChildrens)
                    .HasForeignKey(d => d.IdGrandChildren)
                    .HasConstraintName("culturalgrandchildrens_idgrandchildren_fkey");
            });

            modelBuilder.Entity<Education>(entity =>
            {
                entity.ToTable("education", "main");

                entity.HasIndex(e => e.IdEmployee)
                    .HasName("education_idemployee_key")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("nextval('main.education_id_seq'::regclass)");

                entity.Property(e => e.DateReceiving).HasColumnName("datereceiving");

                entity.Property(e => e.IdEmployee).HasColumnName("idemployee");

                entity.Property(e => e.LevelEducation)
                    .IsRequired()
                    .HasColumnName("leveleducation")
                    .HasColumnType("character varying");

                entity.Property(e => e.NameInstitution)
                    .IsRequired()
                    .HasColumnName("nameinstitution")
                    .HasColumnType("character varying");

                entity.HasOne(d => d.IdEmployeeNavigation)
                    .WithOne(p => p.Education)
                    .HasForeignKey<Education>(d => d.IdEmployee)
                    .HasConstraintName("education_idemployee_fkey");
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.ToTable("employee", "main");

                entity.HasIndex(e => e.IdentificationСode)
                    .HasName("employee_identificationСode_key")
                    .IsUnique();

                entity.HasIndex(e => e.MechnikovCard)
                    .HasName("employee_mechnikovcard_key")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("nextval('main.employee_id_seq'::regclass)");

                entity.Property(e => e.BasicProfession)
                    .IsRequired()
                    .HasColumnName("basicprofession")
                    .HasColumnType("character varying");

                entity.Property(e => e.BirthDate)
                    .HasColumnName("birthdate")
                    .HasColumnType("date");

                entity.Property(e => e.CityPhone)
                    .HasColumnName("cityphone")
                    .HasColumnType("character varying");

                entity.Property(e => e.DateAdded)
                    .HasColumnName("dateadded")
                    .HasColumnType("date")
                    .HasDefaultValueSql("CURRENT_DATE");

                entity.Property(e => e.EndDateTradeUnion)
                    .HasColumnName("enddatetradeunion")
                    .HasColumnType("date");

                entity.Property(e => e.EndYearWork).HasColumnName("endyearwork");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasColumnName("firstname")
                    .HasColumnType("character varying");

                entity.Property(e => e.IdentificationСode)
                    .HasColumnName("identificationСode")
                    .HasColumnType("character varying");

                entity.Property(e => e.MechnikovCard)
                    .HasColumnName("mechnikovcard")
                    .HasColumnType("character varying");

                entity.Property(e => e.MobilePhone)
                    .HasColumnName("mobilephone")
                    .HasColumnType("character varying");

                entity.Property(e => e.Note).HasColumnName("note");

                entity.Property(e => e.Patronymic)
                    .HasColumnName("patronymic")
                    .HasColumnType("character varying");

                entity.Property(e => e.SecondName)
                    .IsRequired()
                    .HasColumnName("secondname")
                    .HasColumnType("character varying");

                entity.Property(e => e.Sex)
                    .IsRequired()
                    .HasColumnName("sex")
                    .HasColumnType("character varying");

                entity.Property(e => e.StartDateTradeUnion)
                    .HasColumnName("startdatetradeunion")
                    .HasColumnType("date");

                entity.Property(e => e.StartYearWork).HasColumnName("startyearwork");
            });

            modelBuilder.Entity<Event>(entity =>
            {
                entity.ToTable("event", "directories");

                entity.HasIndex(e => e.Name)
                    .HasName("event_name_key")
                    .IsUnique();

                entity.HasIndex(e => e.TypeId)
                    .HasName("event_typeid_key")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("nextval('directories.event_id_seq'::regclass)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasColumnType("character varying");

                entity.Property(e => e.TypeId).HasColumnName("typeid");

                entity.HasOne(d => d.Type)
                    .WithOne(p => p.Event)
                    .HasForeignKey<Event>(d => d.TypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("event_typeid_fkey");
            });

            modelBuilder.Entity<EventChildrens>(entity =>
            {
                entity.ToTable("eventchildrens", "lists");

                entity.HasIndex(e => new { e.IdChildren, e.IdEvent, e.StartDate })
                    .HasName("eventchildrens_idchildren_idevent_startdate_key")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("nextval('lists.eventchildrens_id_seq'::regclass)");

                entity.Property(e => e.Amount)
                    .HasColumnName("amount")
                    .HasColumnType("money");

                entity.Property(e => e.Discount)
                    .HasColumnName("discount")
                    .HasColumnType("money");

                entity.Property(e => e.EndDate)
                    .HasColumnName("enddate")
                    .HasColumnType("date");

                entity.Property(e => e.IdChildren).HasColumnName("idchildren");

                entity.Property(e => e.IdEvent).HasColumnName("idevent");

                entity.Property(e => e.StartDate)
                    .HasColumnName("startdate")
                    .HasColumnType("date");

                entity.HasOne(d => d.IdChildrenNavigation)
                    .WithMany(p => p.EventChildrens)
                    .HasForeignKey(d => d.IdChildren)
                    .HasConstraintName("eventchildrens_idchildren_fkey");

                entity.HasOne(d => d.IdEventNavigation)
                    .WithMany(p => p.EventChildrens)
                    .HasForeignKey(d => d.IdEvent)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("eventchildrens_idevent_fkey");
            });

            modelBuilder.Entity<EventEmployees>(entity =>
            {
                entity.ToTable("eventemployees", "lists");

                entity.HasIndex(e => new { e.IdEmployee, e.IdEvent, e.StartDate })
                    .HasName("eventemployees_idemployee_idevent_startdate_key")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("nextval('lists.eventemployees_id_seq'::regclass)");

                entity.Property(e => e.Amount)
                    .HasColumnName("amount")
                    .HasColumnType("money");

                entity.Property(e => e.Discount)
                    .HasColumnName("discount")
                    .HasColumnType("money");

                entity.Property(e => e.EndDate)
                    .HasColumnName("enddate")
                    .HasColumnType("date");

                entity.Property(e => e.IdEmployee).HasColumnName("idemployee");

                entity.Property(e => e.IdEvent).HasColumnName("idevent");

                entity.Property(e => e.StartDate)
                    .HasColumnName("startdate")
                    .HasColumnType("date");

                entity.HasOne(d => d.IdEmployeeNavigation)
                    .WithMany(p => p.EventEmployees)
                    .HasForeignKey(d => d.IdEmployee)
                    .HasConstraintName("eventemployees_idemployee_fkey");

                entity.HasOne(d => d.IdEventNavigation)
                    .WithMany(p => p.EventEmployees)
                    .HasForeignKey(d => d.IdEvent)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("eventemployees_idevent_fkey");
            });

            modelBuilder.Entity<EventFamily>(entity =>
            {
                entity.ToTable("eventfamily", "lists");

                entity.HasIndex(e => new { e.IdFamily, e.IdEvent, e.StartDate })
                    .HasName("eventfamily_idfamily_idevent_startdate_key")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("nextval('lists.eventfamily_id_seq'::regclass)");

                entity.Property(e => e.Amount)
                    .HasColumnName("amount")
                    .HasColumnType("money");

                entity.Property(e => e.Discount)
                    .HasColumnName("discount")
                    .HasColumnType("money");

                entity.Property(e => e.EndDate)
                    .HasColumnName("enddate")
                    .HasColumnType("date");

                entity.Property(e => e.IdEvent).HasColumnName("idevent");

                entity.Property(e => e.IdFamily).HasColumnName("idfamily");

                entity.Property(e => e.StartDate)
                    .HasColumnName("startdate")
                    .HasColumnType("date");

                entity.HasOne(d => d.IdEventNavigation)
                    .WithMany(p => p.EventFamily)
                    .HasForeignKey(d => d.IdEvent)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("eventfamily_idevent_fkey");

                entity.HasOne(d => d.IdFamilyNavigation)
                    .WithMany(p => p.EventFamily)
                    .HasForeignKey(d => d.IdFamily)
                    .HasConstraintName("eventfamily_idfamily_fkey");
            });

            modelBuilder.Entity<EventGrandChildrens>(entity =>
            {
                entity.ToTable("eventgrandchildrens", "lists");

                entity.HasIndex(e => new { e.IdGrandChildren, e.IdEvent, e.StartDate })
                    .HasName("eventgrandchildrens_idgrandchildren_idevent_startdate_key")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("nextval('lists.eventgrandchildrens_id_seq'::regclass)");

                entity.Property(e => e.Amount)
                    .HasColumnName("amount")
                    .HasColumnType("money");

                entity.Property(e => e.Discount)
                    .HasColumnName("discount")
                    .HasColumnType("money");

                entity.Property(e => e.EndDate)
                    .HasColumnName("enddate")
                    .HasColumnType("date");

                entity.Property(e => e.IdEvent).HasColumnName("idevent");

                entity.Property(e => e.IdGrandChildren).HasColumnName("idgrandchildren");

                entity.Property(e => e.StartDate)
                    .HasColumnName("startdate")
                    .HasColumnType("date");

                entity.HasOne(d => d.IdEventNavigation)
                    .WithMany(p => p.EventGrandChildrens)
                    .HasForeignKey(d => d.IdEvent)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("eventgrandchildrens_idevent_fkey");

                entity.HasOne(d => d.IdGrandChildrenNavigation)
                    .WithMany(p => p.EventGrandChildrens)
                    .HasForeignKey(d => d.IdGrandChildren)
                    .HasConstraintName("eventgrandchildrens_idgrandchildren_fkey");
            });

            modelBuilder.Entity<Family>(entity =>
            {
                entity.ToTable("family", "main");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("nextval('main.family_id_seq'::regclass)");

                entity.Property(e => e.BirthDate)
                    .HasColumnName("birthdate")
                    .HasColumnType("date");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasColumnName("firstname")
                    .HasColumnType("character varying");

                entity.Property(e => e.IdEmployee).HasColumnName("idemployee");

                entity.Property(e => e.Patronymic)
                    .HasColumnName("patronymic")
                    .HasColumnType("character varying");

                entity.Property(e => e.SecondName)
                    .IsRequired()
                    .HasColumnName("secondname")
                    .HasColumnType("character varying");

                entity.HasOne(d => d.IdEmployeeNavigation)
                    .WithMany(p => p.Family)
                    .HasForeignKey(d => d.IdEmployee)
                    .HasConstraintName("family_idemployee_fkey");
            });

            modelBuilder.Entity<FluorographyEmployees>(entity =>
            {
                entity.ToTable("fluorographyemployees", "lists");

                entity.HasIndex(e => new { e.IdEmployee, e.Result, e.DatePassage })
                    .HasName("fluorographyemployees_idemployee_result_datepassage_key")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("nextval('lists.fluorographyemployees_id_seq'::regclass)");

                entity.Property(e => e.DatePassage)
                    .HasColumnName("datepassage")
                    .HasColumnType("date");

                entity.Property(e => e.IdEmployee).HasColumnName("idemployee");

                entity.Property(e => e.PlacePassing)
                    .IsRequired()
                    .HasColumnName("placepassing")
                    .HasColumnType("character varying");

                entity.Property(e => e.Result)
                    .IsRequired()
                    .HasColumnName("result")
                    .HasColumnType("character varying");

                entity.HasOne(d => d.IdEmployeeNavigation)
                    .WithMany(p => p.FluorographyEmployees)
                    .HasForeignKey(d => d.IdEmployee)
                    .HasConstraintName("fluorographyemployees_idemployee_fkey");
            });

            modelBuilder.Entity<GiftChildrens>(entity =>
            {
                entity.ToTable("giftchildrens", "lists");

                entity.HasIndex(e => new { e.IdChildren, e.NameEvent, e.NameGift, e.DateGift })
                    .HasName("giftchildrens_idchildren_nameevent_namegift_dategift_key")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("nextval('lists.giftchildrens_id_seq'::regclass)");

                entity.Property(e => e.DateGift)
                    .HasColumnName("dategift")
                    .HasColumnType("date");

                entity.Property(e => e.Discount)
                    .HasColumnName("discount")
                    .HasColumnType("money");

                entity.Property(e => e.IdChildren).HasColumnName("idchildren");

                entity.Property(e => e.NameEvent)
                    .IsRequired()
                    .HasColumnName("nameevent")
                    .HasColumnType("character varying");

                entity.Property(e => e.NameGift)
                    .IsRequired()
                    .HasColumnName("namegift")
                    .HasColumnType("character varying");

                entity.Property(e => e.Price)
                    .HasColumnName("price")
                    .HasColumnType("money");

                entity.HasOne(d => d.IdChildrenNavigation)
                    .WithMany(p => p.GiftChildrens)
                    .HasForeignKey(d => d.IdChildren)
                    .HasConstraintName("giftchildrens_idchildren_fkey");
            });

            modelBuilder.Entity<GiftEmployees>(entity =>
            {
                entity.ToTable("giftemployees", "lists");

                entity.HasIndex(e => new { e.IdEmployee, e.NameEvent, e.NameGift, e.DateGift })
                    .HasName("giftemployees_idemployee_nameevent_namegift_dategift_key")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("nextval('lists.giftemployees_id_seq'::regclass)");

                entity.Property(e => e.DateGift)
                    .HasColumnName("dategift")
                    .HasColumnType("date");

                entity.Property(e => e.Discount)
                    .HasColumnName("discount")
                    .HasColumnType("money");

                entity.Property(e => e.IdEmployee).HasColumnName("idemployee");

                entity.Property(e => e.NameEvent)
                    .IsRequired()
                    .HasColumnName("nameevent")
                    .HasColumnType("character varying");

                entity.Property(e => e.NameGift)
                    .IsRequired()
                    .HasColumnName("namegift")
                    .HasColumnType("character varying");

                entity.Property(e => e.Price)
                    .HasColumnName("price")
                    .HasColumnType("money");

                entity.HasOne(d => d.IdEmployeeNavigation)
                    .WithMany(p => p.GiftEmployees)
                    .HasForeignKey(d => d.IdEmployee)
                    .HasConstraintName("giftemployees_idemployee_fkey");
            });

            modelBuilder.Entity<GiftGrandChildrens>(entity =>
            {
                entity.ToTable("giftgrandchildrens", "lists");

                entity.HasIndex(e => new { e.IdGrandChildren, e.NameEvent, e.NameGifts, e.DateGift })
                    .HasName("giftgrandchildrens_idgrandchildren_nameevent_namegifts_date_key")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("nextval('lists.giftgrandchildrens_id_seq'::regclass)");

                entity.Property(e => e.DateGift)
                    .HasColumnName("dategift")
                    .HasColumnType("date");

                entity.Property(e => e.Discount)
                    .HasColumnName("discount")
                    .HasColumnType("money");

                entity.Property(e => e.IdGrandChildren).HasColumnName("idgrandchildren");

                entity.Property(e => e.NameEvent)
                    .IsRequired()
                    .HasColumnName("nameevent")
                    .HasColumnType("character varying");

                entity.Property(e => e.NameGifts)
                    .IsRequired()
                    .HasColumnName("namegifts")
                    .HasColumnType("character varying");

                entity.Property(e => e.Price)
                    .HasColumnName("price")
                    .HasColumnType("money");

                entity.HasOne(d => d.IdGrandChildrenNavigation)
                    .WithMany(p => p.GiftGrandChildrens)
                    .HasForeignKey(d => d.IdGrandChildren)
                    .HasConstraintName("giftgrandchildrens_idgrandchildren_fkey");
            });

            modelBuilder.Entity<GrandChildren>(entity =>
            {
                entity.ToTable("grandchildren", "main");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("nextval('main.grandchildren_id_seq'::regclass)");

                entity.Property(e => e.BirthDate)
                    .HasColumnName("birthdate")
                    .HasColumnType("date");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasColumnName("firstname")
                    .HasColumnType("character varying");

                entity.Property(e => e.IdEmployee).HasColumnName("idemployee");

                entity.Property(e => e.Patronymic)
                    .HasColumnName("patronymic")
                    .HasColumnType("character varying");

                entity.Property(e => e.SecondName)
                    .IsRequired()
                    .HasColumnName("secondname")
                    .HasColumnType("character varying");

                entity.HasOne(d => d.IdEmployeeNavigation)
                    .WithMany(p => p.GrandChildren)
                    .HasForeignKey(d => d.IdEmployee)
                    .HasConstraintName("grandchildren_idemployee_fkey");
            });

            modelBuilder.Entity<Hobby>(entity =>
            {
                entity.ToTable("hobby", "directories");

                entity.HasIndex(e => e.Name)
                    .HasName("hobby_name_key")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("nextval('directories.hobby_id_seq'::regclass)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasColumnType("character varying");
            });

            modelBuilder.Entity<HobbyChildrens>(entity =>
            {
                entity.ToTable("hobbychildrens", "lists");

                entity.HasIndex(e => new { e.IdChildren, e.IdHobby })
                    .HasName("hobbychildrens_idchildren_idhobby_key")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("nextval('lists.hobbychildrens_id_seq'::regclass)");

                entity.Property(e => e.IdChildren).HasColumnName("idchildren");

                entity.Property(e => e.IdHobby).HasColumnName("idhobby");

                entity.HasOne(d => d.IdChildrenNavigation)
                    .WithMany(p => p.HobbyChildrens)
                    .HasForeignKey(d => d.IdChildren)
                    .HasConstraintName("hobbychildrens_idchildren_fkey");

                entity.HasOne(d => d.IdHobbyNavigation)
                    .WithMany(p => p.HobbyChildrens)
                    .HasForeignKey(d => d.IdHobby)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("hobbychildrens_idhobby_fkey");
            });

            modelBuilder.Entity<HobbyEmployees>(entity =>
            {
                entity.ToTable("hobbyemployees", "lists");

                entity.HasIndex(e => new { e.IdEmployee, e.IdHobby })
                    .HasName("hobbyemployees_idemployee_idhobby_key")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("nextval('lists.hobbyemployees_id_seq'::regclass)");

                entity.Property(e => e.IdEmployee).HasColumnName("idemployee");

                entity.Property(e => e.IdHobby).HasColumnName("idhobby");

                entity.HasOne(d => d.IdEmployeeNavigation)
                    .WithMany(p => p.HobbyEmployees)
                    .HasForeignKey(d => d.IdEmployee)
                    .HasConstraintName("hobbyemployees_idemployee_fkey");

                entity.HasOne(d => d.IdHobbyNavigation)
                    .WithMany(p => p.HobbyEmployees)
                    .HasForeignKey(d => d.IdHobby)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("hobbyemployees_idhobby_fkey");
            });

            modelBuilder.Entity<HobbyGrandChildrens>(entity =>
            {
                entity.ToTable("hobbygrandchildrens", "lists");

                entity.HasIndex(e => new { e.IdGrandChildren, e.IdHobby })
                    .HasName("hobbygrandchildrens_idgrandchildren_idhobby_key")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("nextval('lists.hobbygrandchildrens_id_seq'::regclass)");

                entity.Property(e => e.IdGrandChildren).HasColumnName("idgrandchildren");

                entity.Property(e => e.IdHobby).HasColumnName("idhobby");

                entity.HasOne(d => d.IdGrandChildrenNavigation)
                    .WithMany(p => p.HobbyGrandChildrens)
                    .HasForeignKey(d => d.IdGrandChildren)
                    .HasConstraintName("hobbygrandchildrens_idgrandchildren_fkey");

                entity.HasOne(d => d.IdHobbyNavigation)
                    .WithMany(p => p.HobbyGrandChildrens)
                    .HasForeignKey(d => d.IdHobby)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("hobbygrandchildrens_idhobby_fkey");
            });

            modelBuilder.Entity<MaterialAid>(entity =>
            {
                entity.ToTable("materialaid", "directories");

                entity.HasIndex(e => e.Name)
                    .HasName("materialaid_name_key")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("nextval('directories.materialaid_id_seq'::regclass)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasColumnType("character varying");
            });

            modelBuilder.Entity<MaterialAidEmployees>(entity =>
            {
                entity.ToTable("materialaidemployees", "lists");

                entity.HasIndex(e => new { e.IdEmployee, e.IdMaterialAid, e.DateIssue })
                    .HasName("materialaidemployees_idemployee_idmaterialaid_dateissue_key")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("nextval('lists.materialaidemployees_id_seq'::regclass)");

                entity.Property(e => e.Amount)
                    .HasColumnName("amount")
                    .HasColumnType("money");

                entity.Property(e => e.DateIssue)
                    .HasColumnName("dateissue")
                    .HasColumnType("date");

                entity.Property(e => e.IdEmployee).HasColumnName("idemployee");

                entity.Property(e => e.IdMaterialAid).HasColumnName("idmaterialaid");

                entity.HasOne(d => d.IdEmployeeNavigation)
                    .WithMany(p => p.MaterialAidEmployees)
                    .HasForeignKey(d => d.IdEmployee)
                    .HasConstraintName("materialaidemployees_idemployee_fkey");

                entity.HasOne(d => d.IdMaterialAidNavigation)
                    .WithMany(p => p.MaterialAidEmployees)
                    .HasForeignKey(d => d.IdMaterialAid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("materialaidemployees_idmaterialaid_fkey");
            });

            modelBuilder.Entity<Position>(entity =>
            {
                entity.ToTable("position", "directories");

                entity.HasIndex(e => e.Name)
                    .HasName("position_name_key")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("nextval('directories.position_id_seq'::regclass)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasColumnType("character varying");
            });

            modelBuilder.Entity<PositionEmployees>(entity =>
            {
                entity.ToTable("positionemployees", "lists");

                entity.HasIndex(e => e.IdEmployee)
                    .HasName("positionemployees_idemployee_key")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("nextval('lists.positionemployees_id_seq'::regclass)");

                entity.Property(e => e.CheckPosition).HasColumnName("checkposition");

                entity.Property(e => e.EndDate)
                    .HasColumnName("enddate")
                    .HasColumnType("date");

                entity.Property(e => e.IdEmployee).HasColumnName("idemployee");

                entity.Property(e => e.IdPosition).HasColumnName("idposition");

                entity.Property(e => e.IdSubdivision).HasColumnName("idsubdivision");

                entity.Property(e => e.StartDate)
                    .HasColumnName("startdate")
                    .HasColumnType("date");

                entity.HasOne(d => d.IdEmployeeNavigation)
                    .WithOne(p => p.PositionEmployees)
                    .HasForeignKey<PositionEmployees>(d => d.IdEmployee)
                    .HasConstraintName("positionemployees_idemployee_fkey");

                entity.HasOne(d => d.IdPositionNavigation)
                    .WithMany(p => p.PositionEmployees)
                    .HasForeignKey(d => d.IdPosition)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("positionemployees_idposition_fkey");

                entity.HasOne(d => d.IdSubdivisionNavigation)
                    .WithMany(p => p.PositionEmployees)
                    .HasForeignKey(d => d.IdSubdivision)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("positionemployees_idsubdivision_fkey");
            });

            modelBuilder.Entity<PrivateHouseEmployees>(entity =>
            {
                entity.ToTable("privatehouseemployees", "lists");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("nextval('lists.privatehouseemployees_id_seq'::regclass)");

                entity.Property(e => e.City)
                    .IsRequired()
                    .HasColumnName("city")
                    .HasColumnType("character varying");

                entity.Property(e => e.DateReceiving)
                    .HasColumnName("datereceiving")
                    .HasColumnType("date");

                entity.Property(e => e.IdEmployee).HasColumnName("idemployee");

                entity.Property(e => e.NumberApartment)
                    .HasColumnName("numberapartment")
                    .HasColumnType("character varying");

                entity.Property(e => e.NumberHouse)
                    .HasColumnName("numberhouse")
                    .HasColumnType("character varying");

                entity.Property(e => e.Street)
                    .IsRequired()
                    .HasColumnName("street")
                    .HasColumnType("character varying");

                entity.HasOne(d => d.IdEmployeeNavigation)
                    .WithMany(p => p.PrivateHouseEmployees)
                    .HasForeignKey(d => d.IdEmployee)
                    .HasConstraintName("privatehouseemployees_idemployee_fkey");
            });

            modelBuilder.Entity<PrivilegeEmployees>(entity =>
            {
                entity.ToTable("privilegeemployees", "lists");

                entity.HasIndex(e => e.IdEmployee)
                    .HasName("privilegeemployees_idemployee_key")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("nextval('lists.privilegeemployees_id_seq'::regclass)");

                entity.Property(e => e.CheckPrivileges).HasColumnName("checkprivileges");

                entity.Property(e => e.IdEmployee).HasColumnName("idemployee");

                entity.Property(e => e.IdPrivileges).HasColumnName("idprivileges");

                entity.Property(e => e.Note).HasColumnName("note");

                entity.HasOne(d => d.IdEmployeeNavigation)
                    .WithOne(p => p.PrivilegeEmployees)
                    .HasForeignKey<PrivilegeEmployees>(d => d.IdEmployee)
                    .HasConstraintName("privilegeemployees_idemployee_fkey");

                entity.HasOne(d => d.IdPrivilegesNavigation)
                    .WithMany(p => p.PrivilegeEmployees)
                    .HasForeignKey(d => d.IdPrivileges)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("privilegeemployees_idprivileges_fkey");
            });

            modelBuilder.Entity<Privileges>(entity =>
            {
                entity.ToTable("privileges", "directories");

                entity.HasIndex(e => e.Name)
                    .HasName("privileges_name_key")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("nextval('directories.privileges_id_seq'::regclass)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasColumnType("character varying");
            });

            modelBuilder.Entity<PublicHouseEmployees>(entity =>
            {
                entity.HasKey(e => new { e.IdAddressPublicHouse, e.IdEmployee });

                entity.ToTable("publichouseemployees", "lists");

                entity.Property(e => e.IdAddressPublicHouse).HasColumnName("idaddresspublichouse");

                entity.Property(e => e.IdEmployee).HasColumnName("idemployee");

                entity.Property(e => e.NumberRoom)
                    .HasColumnName("numberroom")
                    .HasColumnType("character varying");

                entity.HasOne(d => d.IdAddressPublicHouseNavigation)
                    .WithMany(p => p.PublicHouseEmployees)
                    .HasForeignKey(d => d.IdAddressPublicHouse)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("publichouseemployees_idaddresspublichouse_fkey");

                entity.HasOne(d => d.IdEmployeeNavigation)
                    .WithMany(p => p.PublicHouseEmployees)
                    .HasForeignKey(d => d.IdEmployee)
                    .HasConstraintName("publichouseemployees_idemployee_fkey");
            });

            modelBuilder.Entity<Scientific>(entity =>
            {
                entity.ToTable("scientific", "main");

                entity.HasIndex(e => e.IdEmployee)
                    .HasName("scientific_idemployee_key")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("nextval('main.scientific_id_seq'::regclass)");

                entity.Property(e => e.IdEmployee).HasColumnName("idemployee");

                entity.Property(e => e.ScientificDegree)
                    .IsRequired()
                    .HasColumnName("scientificdegree")
                    .HasColumnType("character varying");

                entity.Property(e => e.ScientificTitle)
                    .IsRequired()
                    .HasColumnName("scientifictitle")
                    .HasColumnType("character varying");

                entity.HasOne(d => d.IdEmployeeNavigation)
                    .WithOne(p => p.Scientific)
                    .HasForeignKey<Scientific>(d => d.IdEmployee)
                    .HasConstraintName("scientific_idemployee_fkey");
            });

            modelBuilder.Entity<SocialActivity>(entity =>
            {
                entity.ToTable("socialactivity", "directories");

                entity.HasIndex(e => e.Name)
                    .HasName("socialactivity_name_key")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("nextval('directories.socialactivity_id_seq'::regclass)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasColumnType("character varying");
            });

            modelBuilder.Entity<SocialActivityEmployees>(entity =>
            {
                entity.ToTable("socialactivityemployees", "lists");

                entity.HasIndex(e => e.IdEmployee)
                    .HasName("socialactivityemployees_idemployee_key")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("nextval('lists.socialactivityemployees_id_seq'::regclass)");

                entity.Property(e => e.CheckSocialActivity).HasColumnName("checksocialactivity");

                entity.Property(e => e.IdEmployee).HasColumnName("idemployee");

                entity.Property(e => e.IdSocialActivity).HasColumnName("idsocialactivity");

                entity.Property(e => e.Note).HasColumnName("note");

                entity.HasOne(d => d.IdEmployeeNavigation)
                    .WithOne(p => p.SocialActivityEmployees)
                    .HasForeignKey<SocialActivityEmployees>(d => d.IdEmployee)
                    .HasConstraintName("socialactivityemployees_idemployee_fkey");

                entity.HasOne(d => d.IdSocialActivityNavigation)
                    .WithMany(p => p.SocialActivityEmployees)
                    .HasForeignKey(d => d.IdSocialActivity)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("socialactivityemployees_idsocialactivity_fkey");
            });

            modelBuilder.Entity<Subdivisions>(entity =>
            {
                entity.ToTable("subdivisions", "directories");

                entity.HasIndex(e => e.DeptName)
                    .HasName("subdivisions_deptname_key")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("nextval('directories.subdivisions_id_seq'::regclass)");

                entity.Property(e => e.DeptName)
                    .IsRequired()
                    .HasColumnName("deptname")
                    .HasColumnType("character varying");

                entity.Property(e => e.IdSubordinate).HasColumnName("idsubordinate");

                entity.HasOne(d => d.IdSubordinateNavigation)
                    .WithMany(p => p.InverseIdSubordinateNavigation)
                    .HasForeignKey(d => d.IdSubordinate)
                    .HasConstraintName("subdivisions_idsubordinate_fkey");
            });

            modelBuilder.Entity<TypeEvent>(entity =>
            {
                entity.ToTable("typeevent", "directories");

                entity.HasIndex(e => e.Name)
                    .HasName("typeevent_name_key")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("nextval('directories.typeevent_id_seq'::regclass)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasColumnType("character varying");
            });

            modelBuilder.Entity<TypeHouse>(entity =>
            {
                entity.ToTable("typehouse", "directories");

                entity.HasIndex(e => e.Name)
                    .HasName("typehouse_name_key")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("nextval('directories.typehouse_id_seq'::regclass)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasColumnType("character varying");
            });

            modelBuilder.HasSequence("activities_id_seq");

            modelBuilder.HasSequence("addresspublichouse_id_seq");

            modelBuilder.HasSequence("award_id_seq");

            modelBuilder.HasSequence("cultural_id_seq");

            modelBuilder.HasSequence("event_id_seq");

            modelBuilder.HasSequence("hobby_id_seq");

            modelBuilder.HasSequence("materialaid_id_seq");

            modelBuilder.HasSequence("position_id_seq");

            modelBuilder.HasSequence("privileges_id_seq");

            modelBuilder.HasSequence("socialactivity_id_seq");

            modelBuilder.HasSequence("subdivisions_id_seq");

            modelBuilder.HasSequence("typeevent_id_seq");

            modelBuilder.HasSequence("typehouse_id_seq");

            modelBuilder.HasSequence("activitychildrens_id_seq");

            modelBuilder.HasSequence("activityemployees_id_seq");

            modelBuilder.HasSequence("activityfamily_id_seq");

            modelBuilder.HasSequence("activitygrandchildrens_id_seq");

            modelBuilder.HasSequence("apartmentaccountingemployees_id_seq");

            modelBuilder.HasSequence("awardemployees_id_seq");

            modelBuilder.HasSequence("culturalchildrens_id_seq");

            modelBuilder.HasSequence("culturalemployees_id_seq");

            modelBuilder.HasSequence("culturalfamily_id_seq");

            modelBuilder.HasSequence("culturalgrandchildrens_id_seq");

            modelBuilder.HasSequence("eventchildrens_id_seq");

            modelBuilder.HasSequence("eventemployees_id_seq");

            modelBuilder.HasSequence("eventfamily_id_seq");

            modelBuilder.HasSequence("eventgrandchildrens_id_seq");

            modelBuilder.HasSequence("fluorographyemployees_id_seq");

            modelBuilder.HasSequence("giftchildrens_id_seq");

            modelBuilder.HasSequence("giftemployees_id_seq");

            modelBuilder.HasSequence("giftgrandchildrens_id_seq");

            modelBuilder.HasSequence("hobbychildrens_id_seq");

            modelBuilder.HasSequence("hobbyemployees_id_seq");

            modelBuilder.HasSequence("hobbygrandchildrens_id_seq");

            modelBuilder.HasSequence("materialaidemployees_id_seq");

            modelBuilder.HasSequence("positionemployees_id_seq");

            modelBuilder.HasSequence("privatehouseemployees_id_seq");

            modelBuilder.HasSequence("privilegeemployees_id_seq");

            modelBuilder.HasSequence("socialactivityemployees_id_seq");

            modelBuilder.HasSequence("children_id_seq");

            modelBuilder.HasSequence("education_id_seq");

            modelBuilder.HasSequence("employee_id_seq");

            modelBuilder.HasSequence("family_id_seq");

            modelBuilder.HasSequence("grandchildren_id_seq");

            modelBuilder.HasSequence("scientific_id_seq");
        }
    }
}
