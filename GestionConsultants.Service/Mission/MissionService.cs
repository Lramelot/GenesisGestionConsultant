using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using GestionConsultants.Core.Constant;
using GestionConsultants.Core.Domain;
using GestionConsultants.Core.Enum;
using GestionConsultants.Core.Exception;
using GestionConsultants.Data.Context;
using GestionConsultants.Service.Mission.Request;
using GestionConsultants.Service.Mission.Response;
using Microsoft.EntityFrameworkCore;

namespace GestionConsultants.Service.Mission
{
    public class MissionService : IMissionService
    {
        private readonly ConsultantContext _consultantContext;
        private readonly IMapper _mapper;

        public MissionService(ConsultantContext consultantContext, IMapper mapper)
        {
            _consultantContext = consultantContext;
            _mapper = mapper;
        }

        public GetRecapitulatifMissionResponse GetRecapitulatifMissions()
        {
            var missions = _consultantContext
                .Missions
                .Include(m => m.MissionsConsultants)
                .ThenInclude(mc => mc.Consultant)
                .ToList();

            missions.ForEach(m => m.MissionsConsultants = m.MissionsConsultants.Where(mc => mc.EstActif).ToList());

            var recapitulatifMission = new GetRecapitulatifMissionResponse
            {
                Missions = _mapper.Map<IEnumerable<GetRecapitulatifMissionResponse.Mission>>(missions)
            };

            return recapitulatifMission;
        }

        public void PlacerConsultant(PlacerConsultantRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.PosteInterne))
            {
                throw new ValidationException(PlacerConsultantValidationMessage.PosteInterneNonRenseigne);
            }

            if (request.Rate == default)
            {
                throw new ValidationException(PlacerConsultantValidationMessage.RateNonRenseigne);
            }

            var consultant = _consultantContext.Consultants.FirstOrDefault(c => c.Id == request.ConsultantId);
            if (consultant is null)
            {
                throw new ValidationException(PlacerConsultantValidationMessage.ConsultantIntrouvable);
            }

            var mission = _consultantContext.Missions.FirstOrDefault(m => m.Id == request.MissionId);
            if (mission is null)
            {
                throw new ValidationException(PlacerConsultantValidationMessage.MissionIntrouvable);
            }

            var commissionEntreprise = CalculerCommissionEntreprise(consultant.Experience);
            var rateTotal = (request.Rate / 100) * (100 + commissionEntreprise);

            if (rateTotal > mission.RateMaximum)
            {
                throw new ValidationException(PlacerConsultantValidationMessage.RateTropEleve);
            }

            if (consultant.Experience < mission.ExperienceMinimumRequise)
            {
                throw new ValidationException(PlacerConsultantValidationMessage.ExperienceNonSuffisante);
            }

            var ancienneMission = _consultantContext
                .MissionConsultants
                .Where(mc => mc.ConsultantId == request.ConsultantId)
                .FirstOrDefault(mc => mc.EstActif);

            if (ancienneMission != null)
            {
                ancienneMission.EstActif = false;
            }

            var missionConsultant = new MissionConsultant
            {
                MissionId = request.MissionId,
                ConsultantId = request.ConsultantId,
                Rate = request.Rate,
                CommissionEntreprise = commissionEntreprise,
                EstActif = true,
                PosteInterne = request.PosteInterne
            };

            _consultantContext.MissionConsultants.Add(missionConsultant);
            _consultantContext.SaveChanges();
        }

        public double CalculerCommissionEntreprise(Experience experience)
        {
            switch (experience)
            {
                case Experience.Junior: return 15;
                case Experience.Medior: return 10;
                case Experience.Senior: return 5;
                default: throw new ArgumentOutOfRangeException(nameof(experience), experience, null);
            }
        }
    }
}