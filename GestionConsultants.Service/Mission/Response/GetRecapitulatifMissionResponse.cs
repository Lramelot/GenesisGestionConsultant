using System.Collections.Generic;
using System.Linq;
using GestionConsultants.Core.Enum;

namespace GestionConsultants.Service.Mission.Response
{
    public class GetRecapitulatifMissionResponse
    {
        public double ChiffreAffaireTotal => Missions.Sum(m => m.ChiffreAffaire);
        public double BeneficeTotal => Missions.Sum(m => m.Benefice);

        public IEnumerable<Mission> Missions { get; set; }
        
        public class Mission
        {
            public double ChiffreAffaire => Consultants.Sum(c => c.Rate / 100 * (100 + c.CommissionEntreprise));
            public double Benefice => ChiffreAffaire - Consultants.Sum(c => c.Rate);

            public string Entreprise { get; set; }
            public double RateMaximum { get; set; }
            public Experience ExperienceMinimumRequise { get; set; }
            public IEnumerable<Consultant> Consultants { get; set; } = new List<Consultant>();

            public class Consultant
            {
                public string Nom { get; set; }
                public string Prenom { get; set; }
                public string PosteInterne { get; set; }
                public Experience Experience { get; set; }
                public double Rate { get; set; }
                public double CommissionEntreprise { get; set; }
            }
        }
    }
}