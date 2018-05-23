using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace TravelAppCore.Models
{
    public partial class travelappdbContext : DbContext
    {
        public virtual DbSet<Books> Books { get; set; }
        public virtual DbSet<EfmigrationsHistory> EfmigrationsHistory { get; set; }
        public virtual DbSet<Trips> Trips { get; set; }
        public virtual DbSet<Userapikey> Userapikey { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseMySql("Server=35.226.153.115;database=travelappdb;uid=root;pwd=vwkCtEmpk#1;");
                //optionsBuilder.UseMySql("Server=travelappdbinstance.cnsie3kyxsst.us-west-2.rds.amazonaws.com;database=travelappdb;uid=admin;pwd=y5kMcPtM;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Books>(entity =>
            {
                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.Author).HasColumnType("text");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.Name).HasColumnType("text");

                entity.Property(e => e.Price).HasColumnType("decimal(18,2)");
            });

            modelBuilder.Entity<EfmigrationsHistory>(entity =>
            {
                entity.HasKey(e => e.MigrationId);

                entity.ToTable("__EFMigrationsHistory");

                entity.Property(e => e.MigrationId).HasMaxLength(150);

                entity.Property(e => e.ProductVersion)
                    .IsRequired()
                    .HasMaxLength(32);
            });

            modelBuilder.Entity<Trips>(entity =>
            {
                entity.ToTable("trips");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Lat)
                    .HasColumnName("lat")
                    .HasColumnType("decimal(12,10)");

                entity.Property(e => e.Lon)
                    .HasColumnName("lon")
                    .HasColumnType("decimal(12,9)");

                entity.Property(e => e.TripID)
                    .HasColumnName("tripID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.TripName)
                    .HasColumnName("tripName")
                    .HasMaxLength(100);

                entity.Property(e => e.UserID)
                    .HasColumnName("userID")
                    .HasMaxLength(100);

                entity.Property(e => e.WaypointID)
                    .HasColumnName("waypointID")
                    .HasColumnType("int(11)");
            });

            modelBuilder.Entity<Userapikey>(entity =>
            {
                entity.ToTable("Userapikey");


                entity.Property(e => e.ApiKeyId)
                    .HasColumnName("apikeyId")
                    .HasMaxLength(13);

                entity.Property(e => e.UserId)
                    .HasColumnName("userID")
                    .HasMaxLength(100);

                entity.Property(e => e.Email)
                      .HasColumnName("email")
                      .HasMaxLength(100);

                entity.Property(e => e.Verified)
                      .HasColumnName("verified");

                entity.Property(e => e.Lastused)
                      .HasColumnName("lastused");

                entity.Property(e => e.GeneratekeyId)
                    .HasColumnName("generatekeyId");

                entity.HasKey( t => new { t.ApiKeyId, t.UserId });

            });
        }
    }
}
