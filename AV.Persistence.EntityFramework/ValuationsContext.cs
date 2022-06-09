using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AV.Common.Entities;
using AV.Contracts.Enums;
using AV.Persistence.EntityFramework.DataSeeds;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace AV.Persistence.EntityFramework
{
    public class ValuationsContext : IdentityDbContext<User, Role, Guid>
    {
        public ValuationsContext()
        { }

        public ValuationsContext(DbContextOptions options) : base(options)
        { }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //    => optionsBuilder.LogTo(message => Debug.WriteLine(message));

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.UseCollation("latin1_bin");
            base.OnModelCreating(modelBuilder);
            //  This method will be called after migrating to the latest version.
            modelBuilder.Entity<District>().HasData(MigrationSeedData.GetDistricts().ToArray());
            modelBuilder.Entity<Location>().HasData(MigrationSeedData.GetLocations().ToArray());
            modelBuilder.Entity<Locality>().HasData(MigrationSeedData.GetLocalities().ToArray());
            modelBuilder.Entity<ComparableBandSize>().HasData(MigrationSeedData.GetBandSizes().ToArray());
            modelBuilder.Entity<Role>().HasData(MigrationSeedData.GetUserRoles().ToArray());

            modelBuilder.Entity<User>(b =>
            {
                // Each User can have many entries in the UserRole join table
                b.HasMany<UserRole>().WithOne().HasForeignKey(ur => ur.UserId).IsRequired();
                // A user has many account subscriptions
                b.HasMany(u => u.Accounts);
            });

            modelBuilder.Entity<Account>(a =>
            {
                a.HasOne(a => a.User);
                a.HasMany(a => a.Members);
            });

            modelBuilder.Entity<Role>(b =>
                // Primary key
            {
                b.HasKey(r => r.Id);

                // Index for "normalized" role name to allow efficient lookups
                b.HasIndex(r => r.NormalizedName).HasDatabaseName("RoleNameIndex").IsUnique();

                // Maps to the AspNetRoles table
                b.ToTable("AspNetRoles");

                // A concurrency token for use with the optimistic concurrency checking
                b.Property(r => r.ConcurrencyStamp).IsConcurrencyToken();

                // Limit the size of columns to use efficient database types
                b.Property(u => u.Name).HasMaxLength(256);
                b.Property(u => u.NormalizedName).HasMaxLength(256);

                // The relationships between Role and other entity types
                // Note that these relationships are configured with no navigation properties

                // Each Role can have many entries in the UserRole join table
                b.HasMany<UserRole>().WithOne().HasForeignKey(ur => ur.RoleId).IsRequired();

                // Each Role can have many associated RoleClaims
                //b.HasMany<>().WithOne().HasForeignKey(rc => rc.RoleId).IsRequired();
            });

            modelBuilder.Entity<UserRole>()
                .HasOne(e => e.User)
                .WithMany(e => e.Roles)
                .HasForeignKey(e => e.UserId);

            modelBuilder.Entity<UserRole>()
                .HasOne(e => e.Role)
                .WithMany(e => e.Users)
                .HasForeignKey(e => e.RoleId);


            //modelBuilder.Entity<UserReferral>()
            //    .HasKey(t => new { t.Id, t.UserId, t.ReferredById });

            //modelBuilder.Entity<UserReferral>()
            //    .HasOne(cr => cr.User)
            //    .WithMany(r => r.Referrals);


            modelBuilder.Entity<ComparableResultComparable>()
                .HasKey(t => new { t.ComparableId, t.ComparableResultId });

            modelBuilder.Entity<ComparableResultComparable>()
                .HasOne(c => c.ComparableResult)
                .WithMany(cr => cr.Comparables)
                .HasForeignKey(crc => crc.ComparableResultId);

            modelBuilder.Entity<ComparableResultComparable>()
                .HasOne(c => c.Comparable)
                .WithMany(c => c.ComparableResults)
                .HasForeignKey(c => c.ComparableId);

            modelBuilder.Entity<InstructionDocument>()
                .HasOne(i => i.Instruction)
                .WithMany(ds => ds.SupportingDocuments)
                .HasForeignKey(d => d.InstructionId);

            modelBuilder.Entity<InstructionDocument>()
                .HasOne(d => d.DocumentStream);

            modelBuilder.Entity<BasketItemInputData>().HasOne(b => b.BasketItem);

        }

        public override int SaveChanges()
        {
            AddToInstructionHistory(ChangeTracker);

            foreach (var statusEntity in ChangeTracker
                         .Entries()
                         .Where(e => e.Entity is BasketStatus))
            {
                statusEntity.State = EntityState.Detached;
            }

            var entries = ChangeTracker
                .Entries()
                .Where(e => e.Entity is BaseEntity && (
                    e.State == EntityState.Added
                    || e.State == EntityState.Modified));

            foreach (var entityEntry in entries)
            {
                ((BaseEntity)entityEntry.Entity).UpdatedDate = DateTime.Now;

                if (entityEntry.State == EntityState.Added)
                {
                    ((BaseEntity)entityEntry.Entity).CreatedDate = DateTime.Now;
                }
            }
            return base.SaveChanges();
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            AddToInstructionHistory(ChangeTracker);

            var entityEntries = ChangeTracker.Entries();
            if (entityEntries == null)
                return 0;

            foreach (var statusEntity in ChangeTracker
                         .Entries()
                         .Where(e => e.Entity is BasketStatus))
            {
                statusEntity.State = EntityState.Detached;
            }

            var entries = entityEntries
                .Where(e => e.Entity is BaseEntity && (
                    e.State == EntityState.Added
                    || e.State == EntityState.Modified));

            foreach (var entityEntry in entries)
            {
                ((BaseEntity)entityEntry.Entity).UpdatedDate = DateTime.Now;

                if (entityEntry.State == EntityState.Added)
                {
                    ((BaseEntity)entityEntry.Entity).CreatedDate = DateTime.Now;
                }
            }

            foreach (var entry in entityEntries)
            {
                if (entry.State == EntityState.Modified)
                {
                    Update(entry.Entity);
                }
                if (entry.State == EntityState.Added)
                {
                    Add(entry.Entity);
                }
            }

            return await base.SaveChangesAsync(cancellationToken);
        }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<CompanyLogoDocument> AccountLogoDocuments { get; set; }
        public DbSet<BankAccount> BankAccounts { get; set; }
        public DbSet<PaymentHistory> Payments { get; set; }
        public DbSet<AccountTransaction> Transactions { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<District> Districts { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Locality> Localities { get; set; }
        public DbSet<Street> Streets { get; set; }
        public DbSet<Plot> Plots { get; set; }
        public DbSet<ImportHeader> Imports { get; set; }
        public DbSet<MarketInformation> MarketInformation { get; set; }
        public DbSet<LandRate> LandRates { get; set; }
        public DbSet<BuildingCost> BuildingCosts { get; set; }
        public DbSet<BuildingMaterialCost> BuildingMaterialCosts { get; set; }
        public DbSet<Valuation> Valuations { get; set; }
        public DbSet<Basket> Baskets { get; set; }
        public DbSet<BasketItem> BasketItems { get; set; }
        public DbSet<Document> Documents { get; set; }
        public DbSet<DocumentStream> DocumentStreams { get; set; }
        public DbSet<Amenity> Amenities { get; set; }
        public DbSet<Instruction> Instructions { get; set; }
        public DbSet<InstructionHistory> InstructionHistory { get; set; }
        public DbSet<InstructionAppointment> InstructionAppointments { get; set; }
        public DbSet<ComparableBandSize> ComparableBandSizes { get; set; }
        public DbSet<ComparableResult> ComparableResults { get; set; }
        public DbSet<ReportRequest> ReportRequests { get; set; }
        public DbSet<Organisation> Organisations { get; set; }
        public DbSet<Comparable> Comparables { get; set; }
        public DbSet<SiteException> Exceptions { get; set; }
        public DbSet<SystemConfiguration> SystemConfiguration { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Package> Packages { get; set; }
        public DbSet<Company> Companies { get; set; }

        public override DbSet<Role> Roles { get; set; }
        public override DbSet<User> Users { get; set; }

        private void AddToInstructionHistory(ChangeTracker changeTracker)
        {
            var instructions = changeTracker
                .Entries()
                .Where(e => e.Entity is Instruction && (
                    e.State == EntityState.Added
                    || e.State == EntityState.Modified));

            foreach (var instruction in instructions)
            {
                var entity = Cast<Instruction>(instruction).Entity;
                var history = CreateHistory(entity);
                if (entity.History == null)
                    entity.History = new List<InstructionHistory>();
                entity.History.Add(history);
            }
        }

        private EntityEntry<TEntity> Cast<TEntity>(EntityEntry entry) where TEntity : class
            => entry.Context.Entry((TEntity)entry.Entity);

        private InstructionHistory CreateHistory(Instruction instruction)
        {
            return new InstructionHistory(instruction.Id, instruction.Status, generatedBy: instruction.UpdatedBy);
        }
    }
}