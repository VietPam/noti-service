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
        [Route("getListNoti")]
        public IActionResult getListNoti(string code)
        {
            return Ok(Program.api_noti.GetListNoti(code));
        }
        public class NotiDTORequest
        {
            public string code { get; set; }
            public string body { get; set; }
        }
        [HttpPost]
        [Route("createNoti")]
        public async Task<IActionResult> createNoti([FromBody] NotiDTORequest noti)
        {
            bool tmp =await Program.api_noti.createNotiAsync(noti.code, noti.body);
            if(tmp)
            {
                return Ok();
            }
            return BadRequest();
        }
    }
}
