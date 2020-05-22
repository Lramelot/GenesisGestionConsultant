using System.Collections.Generic;
using GestionConsultants.Core.Domain;
using GestionConsultants.Core.Enum;
using Microsoft.EntityFrameworkCore;

namespace GestionConsultants.Data.Context
{
    public class ConsultantContext : DbContext
    {
        public DbSet<Consultant> Consultants { get; set; }
        public DbSet<Mission> Missions { get; set; }
        public DbSet<MissionConsultant> MissionConsultants { get; set; }

        public ConsultantContext(DbContextOptions<ConsultantContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Consultant>().ToTable("Consultants");
            modelBuilder.Entity<Mission>().ToTable("Missions");
            modelBuilder.Entity<MissionConsultant>().ToTable("MissionsConsultants");

            modelBuilder.Entity<MissionConsultant>().HasOne(mc => mc.Mission).WithMany(m => m.MissionsConsultants);
            modelBuilder.Entity<MissionConsultant>().HasOne(mc => mc.Consultant).WithMany(m => m.Missions);

            PopulateConsultants(modelBuilder);
            PopulateMissions(modelBuilder);
            PopulateMissionsConsultants(modelBuilder);
        }
        
        #region Méthodes de seeding

        private static void PopulateMissionsConsultants(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MissionConsultant>().HasData(new List<MissionConsultant>
            {
                new MissionConsultant
                {
                    Id = 1,
                    MissionId = 1,
                    ConsultantId = 1,
                    PosteInterne = "Lead Developer",
                    Rate = 500,
                    CommissionEntreprise = 10,
                    EstActif = false
                },
                new MissionConsultant
                {
                    Id = 2,
                    MissionId = 1,
                    ConsultantId = 2,
                    PosteInterne = "Architecte",
                    Rate = 600,
                    CommissionEntreprise = 5,
                    EstActif = true
                },
                new MissionConsultant
                {
                    Id = 3,
                    MissionId = 1,
                    ConsultantId = 3,
                    PosteInterne = "Développzye",
                    Rate = 450,
                    CommissionEntreprise = 15,
                    EstActif = false
                },
                new MissionConsultant
                {
                    Id = 4,
                    MissionId = 2,
                    ConsultantId = 1,
                    PosteInterne = "Lead Developer",
                    Rate = 475,
                    CommissionEntreprise = 15,
                    EstActif = true
                },
                new MissionConsultant
                {
                    Id = 5,
                    MissionId = 2,
                    ConsultantId = 3,
                    PosteInterne = "Développeur",
                    Rate = 450,
                    CommissionEntreprise = 15,
                    EstActif = true
                }
            });
        }

        private static void PopulateMissions(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Mission>().HasData(new List<Mission>
            {
                new Mission
                {
                    Id = 1,
                    NomEntreprise = "Forem",
                    RateMaximum = 700,
                    ExperienceMinimumRequise = Experience.Medior
                },
                new Mission
                {
                    Id = 2,
                    NomEntreprise = "SPW",
                    RateMaximum = 650,
                    ExperienceMinimumRequise = Experience.Junior
                }
            });
        }

        private static void PopulateConsultants(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Consultant>().HasData(new List<Consultant>
            {
                new Consultant
                {
                    Id = 1,
                    Nom = "Ramelot",
                    Prenom = "Loïc",
                    Experience = Experience.Medior,
                },
                new Consultant
                {
                    Id = 2,
                    Nom = "Nguyen",
                    Prenom = "Duy",
                    Experience = Experience.Senior,
                },
                new Consultant
                {
                    Id = 3,
                    Nom = "Gaa",
                    Prenom = "Corentin",
                    Experience = Experience.Junior,
                }
            });
        }

        #endregion
    }
}