using System.Collections.Generic;
using GestionConsultants.Core.Enum;

namespace GestionConsultants.Service.Consultant.Response
{
    public class HistoriqueByConsultantResponse
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public IEnumerable<Mission> Missions { get; set; }

        public class Mission
        {
            public string Entreprise { get; set; }
            public string PosteInterne { get; set; }
            public double Rate { get; set; }
            public bool EstActif { get; set; }
            public Experience Experience { get; set; }
        }
    }
}