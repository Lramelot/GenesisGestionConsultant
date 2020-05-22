using GestionConsultants.Service.Mission.Request;
using GestionConsultants.Service.Mission.Response;

namespace GestionConsultants.Service.Mission
{
    public interface IMissionService
    {
        GetRecapitulatifMissionResponse GetRecapitulatifMissions();
        void PlacerConsultant(PlacerConsultantRequest request);
    }
}