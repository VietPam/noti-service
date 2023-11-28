using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using noti_service.APIs;
using System.Collections.Generic;

namespace noti_service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotiController : ControllerBase
    {
        [HttpGet]
        [Route("{code}/getListNoti")]
        public async Task<IActionResult> getListNoti(string code)
        {
            return Ok(Program.api_noti.GetListNoti(code));
        }

        [HttpPost]
        [Route("{code}/createNoti")]
        public async Task<IActionResult> createNoti(string code, string body)
        {
            bool tmp = Program.api_noti.createNotiAsync(code, body);
            if(tmp)
            {
                return Ok();
            }
            return BadRequest();
        }
    }
}
