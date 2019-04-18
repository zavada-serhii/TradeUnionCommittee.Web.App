using Microsoft.EntityFrameworkCore;
using TradeUnionCommittee.DAL.CloudStorage.Entities;

namespace TradeUnionCommittee.DAL.CloudStorage.EF
{
    public sealed class TradeUnionCommitteeCloudStorageContext : DbContext
    {
        public TradeUnionCommitteeCloudStorageContext(DbContextOptions<TradeUnionCommitteeCloudStorageContext> options): base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<ReportPdfBucket> ReportPdfBucket { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.0-rtm-35687");

            modelBuilder.Entity<ReportPdfBucket>(entity =>
            {
                entity.Property(e => e.DateFrom).HasColumnType("date");

                entity.Property(e => e.DateTo).HasColumnType("date");

                entity.Property(e => e.EmailUser)
                    .IsRequired()
                    .HasColumnType("character varying");

                entity.Property(e => e.FileName)
                    .IsRequired()
                    .HasColumnType("character varying");

                entity.Property(e => e.IpUser)
                    .IsRequired()
                    .HasColumnType("character varying");
            });
        }
    }
}