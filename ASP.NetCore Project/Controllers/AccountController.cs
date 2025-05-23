﻿using BusinessLogic.Managers.AccountManager;
using Contracts.Models;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;

namespace ASP.NetCore_Project.Controllers
{
    public class AccountController : BaseController
    {
        private readonly IAccountManager _accountManager;

        public AccountController(IAccountManager accountManager) =>
            _accountManager = accountManager;


        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginModel loginModel)
        {
            var token = await _accountManager.Login(loginModel);
            if (token is not null)
            {
                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = token.ValidTo
                });
            }
            return Unauthorized();
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromForm] UserModel userModel)
        {
            await _accountManager.Register(userModel);
            return Created();
        }


        [HttpPatch("ChangePassword/{userId}", Name = "ChangePassword")]
        public async Task<IActionResult> ChangePassword(string userId, ChangePasswordModel model)
        {
            await _accountManager.ChangePassword(userId, model);
            return Ok();
        }
    }
}
