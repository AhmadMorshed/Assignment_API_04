﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Store.Data.Entities.IdentityEntities;
using Store.Services.HandleResponses;
using Store.Services.Services.UserService;
using Store.Services.Services.UserService.Dtos;

namespace Store.Web.Controllers
{

    public class AccountController : BaseController
    {
        private readonly IUserService _userService;
        private readonly UserManager<AppUser> _userManager;

        public AccountController(IUserService userService,UserManager<AppUser> userManager)
        {
            _userService = userService;
            _userManager = userManager;
        }
        [HttpPost]
        public async Task<ActionResult<UserDto>> Login(LoginDto input)
        {
            var user = await _userService.Login(input);
            if (user == null)
                return BadRequest(new CustomException(400, "Email Does Not Exist"));
            return Ok(user);
        }
        [HttpPost]
        public async Task<ActionResult<UserDto>> Register(RegisterDto input)
        {
            var user = await _userService.Register(input);
            if (user == null)
                return BadRequest(new CustomException(400, "Email Already Exist"));
            return Ok(user);
        }
        [HttpPost]
        [Authorize]
        public async Task<UserDto> GetCureentUserDetails()
        {
            var userId = User?.FindFirst("UserId");
            var user = await _userManager.FindByIdAsync(userId.Value);

            return new UserDto
            {
                Id = Guid.Parse(user.Id),
                DisplayName = user.DisplayName,
                Email = user.Email,
            };

        }

    }
}