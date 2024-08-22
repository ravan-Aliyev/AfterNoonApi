using AfterNoonV2.Application.Abstractions.Services;
using AfterNoonV2.Application.CustomAttributes;
using AfterNoonV2.Application.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AfterNoonV2.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Admin")]    
    public class AppServicesController : ControllerBase
    {
        readonly IAppService _appService;

        public AppServicesController(IAppService appService)
        {
            _appService = appService;
        }

        [HttpGet]
        [AuthorizeDefinition(Menu = "App Services", Definition = "Get Authorize Definition End Points", Action = ActionsEnum.Reading)]
        public IActionResult GetAuthorizeDefinitionEndPoints()
        {
            var data = _appService.GetAuthorizeDefinitionEndPoint(typeof(Program));
            return Ok(data);
        }
    }
}
