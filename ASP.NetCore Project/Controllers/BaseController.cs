using BusinessLogic.Filter;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ASP.NetCore_Project.Controllers
{
    [Route("Project/[controller]")]
    [ApiController]
    [GlobalExceptionHandler]
    public class BaseController : ControllerBase
    {
    }
}
