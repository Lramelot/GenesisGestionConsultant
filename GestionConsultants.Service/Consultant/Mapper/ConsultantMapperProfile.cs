using AutoMapper;
using GestionConsultants.Core.Domain;
using GestionConsultants.Service.Consultant.Response;

namespace GestionConsultants.Service.Consultant.Mapper
{
    public class ConsultantMapperProfile : Profile
    {
        public ConsultantMapperProfile()
        {
            CreateMap<Core.Domain.Consultant, HistoriqueByConsultantResponse>()
                .ForMember(h => h.Missions, opt => opt.MapFrom(c => c.Missions));

            CreateMap<MissionConsultant, HistoriqueByConsultantResponse.Mission>()
                .ForMember(hm => hm.Entreprise, opt => opt.MapFrom(mc => mc.Mission.NomEntreprise));
        }
    }
}