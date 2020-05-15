using GestionConsultants.Data.Context;

namespace GestionConsultants.Service.Consultant
{
    public class ConsultantService : IConsultantService
    {
        private readonly ConsultantContext _consultantContext;

        public ConsultantService(ConsultantContext consultantContext)
        {
            _consultantContext = consultantContext;
        }
    }
}