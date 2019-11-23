using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pygma.Common.Models.Base;
using Pygma.Data.Abstractions.Repositories;
using Pygma.Data.Domain.Entities;
using Pygma.Users.ViewModels.Requests;

namespace Pygma.Users.Api
{
    [Route("api/account")]
    [Authorize]
    public class AccountController : CommonControllerBase
    {
        private readonly IUsersRepository _usersRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AccountController(
            IUsersRepository usersRepository,
            IHttpContextAccessor httpContextAccessor)
        {
            _usersRepository = usersRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet]
        [Route("test")]
        public IActionResult Test()
        {
            //[FromBody]RegistrationRequest registerAccountVm
            //IActionResult response = Unauthorized();
            //var user = Authenticate(login);

            //if (user != null)
            //{
            //    var tokenString = BuildToken(user);
            //    response = Ok(new { token = tokenString });
            //}

            return Ok();
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
                Active = false,
                CreatedAt = DateTime.Now
            };

            await _usersRepository.CreateAsync(newUser);

            return Ok();
        }
    }
}