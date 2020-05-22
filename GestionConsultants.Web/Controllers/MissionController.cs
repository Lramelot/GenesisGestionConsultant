using GestionConsultants.Service.Mission;
using GestionConsultants.Service.Mission.Request;
using Microsoft.AspNetCore.Mvc;

namespace GestionConsultants.Web.Controllers
{
    [Route("api/missions")]
    public class MissionController : ControllerBase
    {
        private readonly IMissionService _missionService;

        public MissionController(IMissionService missionService)
        {
            _missionService = missionService;
        }

        [Route("")]
        public IActionResult Get()
        {
            var missions = _missionService.GetRecapitulatifMissions();
            return Ok(missions);
        }

        [HttpPost]
        [Route("{missionId}/consultants")]
        public IActionResult PlacerConsultant([FromRoute] int missionId, [FromBody] PlacerConsultantRequest request)
        {
            request.MissionId = missionId;
            _missionService.PlacerConsultant(request);
            return Ok();
        }
    }
}