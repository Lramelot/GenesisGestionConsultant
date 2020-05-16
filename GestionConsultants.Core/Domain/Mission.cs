using System.Collections;
using System.Collections.Generic;
using GestionConsultants.Core.Enum;

namespace GestionConsultants.Core.Domain
{
    public class Mission
    {
        public int Id { get; set; }
        public string NomEntreprise { get; set; }
        public double RateMaximum { get; set; }
        public Experience ExperienceMinimumRequise { get; set; }
        public ICollection<MissionConsultant> MissionsConsultants { get; set; } = new List<MissionConsultant>();
    }
}