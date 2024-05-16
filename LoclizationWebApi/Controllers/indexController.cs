using LoclizationWebApi.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace LoclizationWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class indexController : ControllerBase
    {
        private readonly IStringLocalizer<indexController> _localization;

        public indexController(IStringLocalizer<indexController> localization)
        {
            _localization = localization;
        }
        [HttpGet (template:"{name}")]
        public IActionResult test(string name)
        {
            var res= string.Format(_localization["welcome"] ,name);
            return Ok(res);
        }
        [HttpPost]
        public IActionResult CreateDto(TestDTO dTO)
        {
            var res = string.Format(_localization["welcome"],dTO.Name);
            return Ok(res);

        }

    }
}
