using System.Linq;
using AutoMapper;
using GestionConsultants.Data.Context;
using GestionConsultants.Service.Consultant.Response;
using Microsoft.EntityFrameworkCore;

namespace GestionConsultants.Service.Consultant
{
    public class ConsultantService : IConsultantService
    {
        private readonly ConsultantContext _consultantContext;
        private readonly IMapper _mapper;

        public ConsultantService(ConsultantContext consultantContext, IMapper mapper)
        {
            _consultantContext = consultantContext;
            _mapper = mapper;
        }

        public HistoriqueByConsultantResponse GetHistoriqueByConsultant(int consultantId)
        {
            var consultant = _consultantContext
                .Consultants
                .Include(c => c.Missions)
                .ThenInclude(mc => mc.Mission)
                .First(c => c.Id == consultantId);

            var historique = _mapper.Map<HistoriqueByConsultantResponse>(consultant);
            return historique;
        }
    }
}