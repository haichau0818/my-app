using Chat.API.Services;
using DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Runtime.InteropServices;

namespace Chat.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private UserServices _userServices;
        public UserController(UserServices userServices)
        {
            _userServices = userServices;
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginDTO loginDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            StatusCode result = await _userServices.CheckLogin(loginDTO);
            if (result.Status == 1)
                return Ok(result);
            else
                return BadRequest(result);
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegistrationDTO registerDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            StatusCode result = await _userServices.RegisterAsync(registerDTO);
            if (result.Status == 1)
                return Ok(result);
            else
                return BadRequest(result);
        }
    }
}
