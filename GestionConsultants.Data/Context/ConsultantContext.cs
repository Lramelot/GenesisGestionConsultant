using System.Collections.Generic;
using GestionConsultants.Core.Domain;
using GestionConsultants.Core.Enum;
using Microsoft.EntityFrameworkCore;

namespace GestionConsultants.Data.Context
{
    public class ConsultantContext : DbContext
    {
        public DbSet<Consultant> Consultants { get; set; }

        public ConsultantContext(DbContextOptions<ConsultantContext> options) : base(options) { }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Consultant>().ToTable("Consultants");
            modelBuilder.Entity<Mission>().ToTable("Missions");
            modelBuilder.Entity<MissionConsultant>().ToTable("MissionsConsultants");

            modelBuilder.Entity<MissionConsultant>().HasOne(mc => mc.Mission).WithMany(m => m.MissionsConsultants);
            modelBuilder.Entity<MissionConsultant>().HasOne(mc => mc.Consultant).WithMany(m => m.Missions);

            modelBuilder.Entity<Consultant>().HasData(new List<Consultant>
            {
                new Consultant
                {
                    Id = 1,
                    Nom = "Ramelot",
                    Prenom = "Loïc",
                    Experience = Experience.Medior,
                    Rate = 500
                },
                new Consultant
                {
                    Id = 2,
                    Nom = "Nguyen",
                    Prenom = "Duy",
                    Experience = Experience.Senior,
                    Rate = 600
                },
                new Consultant
                {
                    Id = 3,
                    Nom = "Gaa",
                    Prenom = "Corentin",
                    Experience = Experience.Junior,
                    Rate = 450
                }
            });

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

            modelBuilder.Entity<MissionConsultant>().HasData(new List<MissionConsultant>
            {
                new MissionConsultant
                {
                    Id = 1,
                    MissionId = 1,
                    ConsultantId = 1,
                    PosteInterne = "Lead Developer",
                    Rate = 500,
                    CommissionEntreprise = 10
                },
                new MissionConsultant
                {
                    Id = 2,
                    MissionId = 1,
                    ConsultantId = 2,
                    PosteInterne = "Architecte",
                    Rate = 600,
                    CommissionEntreprise = 5
                },
                new MissionConsultant
                {
                    Id = 3,
                    MissionId = 1,
                    ConsultantId = 3,
                    PosteInterne = "Développzye",
                    Rate = 450,
                    CommissionEntreprise = 15
                },
                new MissionConsultant
                {
                    Id = 4,
                    MissionId = 2,
                    ConsultantId = 1,
                    PosteInterne = "Lead Developer",
                    Rate = 475,
                    CommissionEntreprise = 15
                },
                new MissionConsultant
                {
                    Id = 5,
                    MissionId = 2,
                    ConsultantId = 3,
                    PosteInterne = "Développeur",
                    Rate = 450,
                    CommissionEntreprise = 15
                }
            });
        }
    }
}