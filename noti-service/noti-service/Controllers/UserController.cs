using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using noti_service.APIs;

namespace noti_service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        [HttpPost]
        [Route("createUser")]
        public async Task<IActionResult> createUser(string code )
        {
            bool tmp = await Program.api_user.createUserAsync(code);
            if (tmp)
            {
                return Ok();
            }

            return BadRequest();
        }

        [HttpGet]
        [Route("listUser")]
        public IActionResult getListUser()
        {
             return Ok( Program.api_user.getListUser());
        }
    }
}
