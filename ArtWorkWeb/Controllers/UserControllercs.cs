using ArtWorkWeb.Service.Implement;
using ArtWorkWeb.Service.Interfaces;
using DataTier.View.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace ArtWorkWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("profile/update")]
        public IActionResult ProfileUpdate(ProfileUpdateRequest model)
        {
            var reponse = _userService.UpdateProfile(model);
            return Ok(reponse);
        }

    }
}
