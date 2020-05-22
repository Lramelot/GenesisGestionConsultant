using GestionConsultants.Service.Consultant.Response;

namespace GestionConsultants.Service.Consultant
{
    public interface IConsultantService
    {
        HistoriqueByConsultantResponse GetHistoriqueByConsultant(int consultantId);
    }
}