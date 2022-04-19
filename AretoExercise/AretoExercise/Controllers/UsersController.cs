using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AretoExercise.Application.Interfaces;
using AretoExercise.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AretoExercise.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {

        private IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [Authorize]
        [HttpGet("getUser")]
        public IActionResult GetUser(int userId)
        {

            var user = _userService.GetUser(userId);

            if (user == null)
            {
                return BadRequest(new { message = "User not found" });
            }
            else
            {
                return Ok(user);
            }
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody]AuthenticateModel model)
        {
           
            var user = await _userService.AuthenticateUser(model.Username, model.Password);

            if (user == null)
            {
                return BadRequest(new { message = "Invalid username or password" });
            }
            else
            {
                return Ok(user);
            }  
        }
    }
}
