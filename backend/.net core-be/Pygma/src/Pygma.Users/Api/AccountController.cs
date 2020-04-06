using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pygma.Common.Models.Base;
using Pygma.Data.Abstractions.Repositories;
using Pygma.Data.Domain.Entities;
using Pygma.Users.Services;
using Pygma.Users.ViewModels.Requests;

namespace Pygma.Users.Api
{
    [Route("api/account")]
    [Authorize]
    public class AccountController : CommonControllerBase
    {
        private readonly IUsersRepository _usersRepository;
        private readonly IJwtTokenService _jwtTokenService;
        
        public AccountController(
            IUsersRepository usersRepository,
            IJwtTokenService jwtTokenService)
        {
            _usersRepository = usersRepository;
            _jwtTokenService = jwtTokenService;
        }

        [HttpPost("registration")]
        public async Task<ActionResult> RegisterAsync([FromBody] RegisterAccountVm registerAccountVm)
        {
            var appUser = (await _usersRepository.ReadAsync(x => x.Email == registerAccountVm.Email)).FirstOrDefault();

            if (appUser?.Email != null && appUser.Email == registerAccountVm?.Email)
                return Ok();
            
            if(appUser?.Email != null && appUser.Email != registerAccountVm?.Email)
                return new ForbidResult();

            var newUser = new User()
            {
                Email = registerAccountVm.Email,
                Password = registerAccountVm.Password,
                Active = false,
                CreatedAt = DateTime.Now
            };

            await _usersRepository.CreateAsync(newUser);

            return Ok();
        }
        
        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login([FromBody] LoginVm loginVm)
        {
            IActionResult response;
            
            var user = _usersRepository.LoginAsync(loginVm.Email, loginVm.Password);
 
            if (user != null)
            {
                var tokenString = _jwtTokenService.BuildToken(user.Id);
                
                response = Ok( new { token = tokenString });
            }
            else
            {
                response = Unauthorized();
            }
 
            return response;
        }
    }
}