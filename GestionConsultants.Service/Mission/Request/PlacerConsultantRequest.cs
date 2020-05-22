namespace GestionConsultants.Service.Mission.Request
{
    public class PlacerConsultantRequest
    {
        public int MissionId { get; set; }
        public int ConsultantId { get; set; }
        public string PosteInterne { get; set; }
        public double Rate { get; set; }
    }
}