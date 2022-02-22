using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Authorization;
namespace DataService.Controllers;
[ApiController]
[Route("api/[controller]")]
public class LoginController : ControllerBase{
    private readonly ILogger<LoginController> _logger;
    public LoginController(ILogger<LoginController> logger){
        _logger = logger;
    }
    [HttpPost]
    [Authorize(Policy = AuthPolicy.RequireAdmin)]
    public void Post(){
        _logger.LogInformation("auth test");
    }
}