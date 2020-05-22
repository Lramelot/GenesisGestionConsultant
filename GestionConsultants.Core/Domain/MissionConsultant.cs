using GestionConsultants.Core.Enum;

namespace GestionConsultants.Core.Domain
{
    public class MissionConsultant
    {
        public int Id { get; set; }
        public string PosteInterne { get; set; }
        public double Rate { get; set; }
        public double CommissionEntreprise { get; set; }

        public int ConsultantId { get; set; }
        public virtual Consultant Consultant { get; set; }

        public int MissionId { get; set; }
        public virtual Mission Mission { get; set; }

        public bool EstActif { get; set; }
    }
}