using GestionConsultants.Service.Consultant;
using Microsoft.AspNetCore.Mvc;

namespace GestionConsultants.Web.Controllers
{
    [Route("api/consultants")]
    public class ConsultantController : ControllerBase
    {
        private readonly IConsultantService _consultantService;

        public ConsultantController(IConsultantService consultantService)
        {
            _consultantService = consultantService;
        }

        [Route("{consultantId}")]
        public IActionResult Get(int consultantId)
        {
            return Ok();
        }
    }
}