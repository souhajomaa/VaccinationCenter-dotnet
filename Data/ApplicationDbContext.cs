using Microsoft.EntityFrameworkCore;
using VaccinationCenter.Models;

namespace VaccinationCenter.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<Citoyen> Citoyens { get; set; }
        public DbSet<Addresse> Adresses { get; set; }
        public DbSet<Vaccin> Vaccins { get; set; }
        public DbSet<CentreVaccination> CentresVaccination { get; set; }
        public DbSet<RendezVous> RendezVous { get; set; }
        public DbSet<Compte> Comptes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // ========== Addresse ==========
            modelBuilder.Entity<Addresse>(entity =>
            {
                entity.HasKey(a => a.AdresseId);
                entity.Property(a => a.Ville)
                    .IsRequired()
                    .HasMaxLength(100);
                entity.Property(a => a.Rue)
                    .IsRequired();
                entity.Property(a => a.CodePostal)
                    .IsRequired();
            });

            // ========== Citoyen ==========
            modelBuilder.Entity<Citoyen>(entity =>
            {
                entity.HasKey(c => c.CiToyenId);
                entity.Property(c => c.CIN)
                    .IsRequired()
                    .HasMaxLength(20);
                entity.HasIndex(c => c.CIN)
                    .IsUnique();
                entity.Property(c => c.Nom)
                    .IsRequired()
                    .HasMaxLength(50);
                entity.Property(c => c.Prenom)
                    .IsRequired()
                    .HasMaxLength(50);
                entity.Property(c => c.Age)
                    .IsRequired();
                entity.Property(c => c.Telephone)
                    .IsRequired();
                entity.Property(c => c.NumeroEvax)
                    .IsRequired();
                entity.HasIndex(c => c.NumeroEvax)
                    .IsUnique();

                entity.HasOne(c => c.Addresse)
                    .WithMany(a => a.Citoyens)
                    .HasForeignKey(c => c.AdresseId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // ========== CentreVaccination ==========
            modelBuilder.Entity<CentreVaccination>(entity =>
            {
                entity.HasKey(cv => cv.CentreVaccinationId);
                entity.Property(cv => cv.Capacite)
                    .IsRequired();
                entity.Property(cv => cv.NbChaises)
                    .IsRequired();
                entity.Property(cv => cv.NumTelephone)
                    .IsRequired();
                entity.Property(cv => cv.ResponsableCentre)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            // ========== Vaccin ==========
            modelBuilder.Entity<Vaccin>(entity =>
            {
                entity.HasKey(v => v.VaccinId);
                entity.Property(v => v.DateValidite)
                    .IsRequired();
                entity.Property(v => v.Fournisseur)
                    .IsRequired()
                    .HasMaxLength(100);
                entity.Property(v => v.Quantite)
                    .IsRequired();
                entity.Property(v => v.TypeVaccin)
                    .IsRequired()
                    .HasConversion<string>();

                entity.HasOne(v => v.CentreVaccination)
                    .WithMany(cv => cv.Vaccins)
                    .HasForeignKey(v => v.CentreVaccinationId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // ========== RendezVous ==========
            modelBuilder.Entity<RendezVous>(entity =>
            {
                entity.HasKey(r => r.RendezVousId);
                entity.Property(r => r.CodeInfirmiere)
                    .IsRequired()
                    .HasMaxLength(20);
                entity.Property(r => r.DateVaccination)
                    .IsRequired();
                entity.Property(r => r.NbrDoses)
                    .IsRequired();

                entity.HasOne(r => r.Citoyen)
                    .WithMany(c => c.RendezVous)
                    .HasForeignKey(r => r.CiToyenId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(r => r.Vaccin)
                    .WithMany(v => v.RendezVous)
                    .HasForeignKey(r => r.VaccinId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // ========== Compte ==========
            modelBuilder.Entity<Compte>(entity =>
            {
                entity.HasKey(c => c.Id);
                entity.Property(c => c.Login)
                    .IsRequired()
                    .HasMaxLength(50);
                entity.HasIndex(c => c.Login)
                    .IsUnique();
                entity.Property(c => c.Password)
                    .IsRequired()
                    .HasMaxLength(255);
                entity.Property(c => c.Role)
                    .IsRequired()
                    .HasConversion<string>();
            });

            // ========== Seed Data ==========
            SeedData(modelBuilder);
        }

        private static void SeedData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CentreVaccination>().HasData(
                new CentreVaccination
                {
                    CentreVaccinationId = 1,
                    Capacite = 500,
                    NbChaises = 50,
                    NumTelephone = 74123456,
                    ResponsableCentre = "Dr. Ahmed Ben Ali"
                },
                new CentreVaccination
                {
                    CentreVaccinationId = 2,
                    Capacite = 300,
                    NbChaises = 30,
                    NumTelephone = 74654321,
                    ResponsableCentre = "Dr. Fatma Triki"
                }
            );

            modelBuilder.Entity<Vaccin>().HasData(
                new Vaccin
                {
                    VaccinId = 1,
                    DateValidite = new DateTime(2026, 12, 31),
                    Fournisseur = "Pfizer Inc.",
                    Quantite = 1000,
                    TypeVaccin = TypeVaccin.PFizer,
                    CentreVaccinationId = 1
                },
                new Vaccin
                {
                    VaccinId = 2,
                    DateValidite = new DateTime(2026, 6, 30),
                    Fournisseur = "Moderna Inc.",
                    Quantite = 500,
                    TypeVaccin = TypeVaccin.Moderna,
                    CentreVaccinationId = 1
                },
                new Vaccin
                {
                    VaccinId = 3,
                    DateValidite = new DateTime(2025, 12, 31),
                    Fournisseur = "Johnson & Johnson",
                    Quantite = 300,
                    TypeVaccin = TypeVaccin.Jhonson,
                    CentreVaccinationId = 2
                }
            );

            // Seed admin account (password: Admin@123)
            modelBuilder.Entity<Compte>().HasData(
                new Compte
                {
                    Id = 1,
                    Login = "admin",
                    Password = BCrypt.Net.BCrypt.HashPassword("Admin@123"),
                    Role = Role.Admin
                }
            );
        }
    }
}
