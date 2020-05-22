using System.Collections.Generic;
using GestionConsultants.Core.Enum;

namespace GestionConsultants.Core.Domain
{
    public class Consultant
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public Experience Experience { get; set; }
        public ICollection<MissionConsultant> Missions { get; set; } = new List<MissionConsultant>();
    }
}