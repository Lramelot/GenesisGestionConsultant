using AutoMapper;
using GestionConsultants.Core.Domain;
using GestionConsultants.Service.Mission.Response;

namespace GestionConsultants.Service.Mission.Mapper
{
    public class MissionMapperProfile : Profile
    {
        public MissionMapperProfile()
        {
            CreateMap<Core.Domain.Mission, GetRecapitulatifMissionResponse.Mission>()
                .ForMember(rm => rm.Entreprise, opt => opt.MapFrom(m => m.NomEntreprise))
                .ForMember(rm => rm.Consultants, opt => opt.MapFrom(m => m.MissionsConsultants));

            CreateMap<MissionConsultant, GetRecapitulatifMissionResponse.Mission.Consultant>()
                .ForMember(rmc => rmc.Nom, opt => opt.MapFrom(mc => mc.Consultant.Nom))
                .ForMember(rmc => rmc.Prenom, opt => opt.MapFrom(mc => mc.Consultant.Prenom));
        }
    }
}