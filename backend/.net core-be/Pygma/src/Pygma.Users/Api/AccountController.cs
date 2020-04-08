using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pygma.Common.Constants;
using Pygma.Common.Models.Base;
using Pygma.Data.Abstractions.Repositories;
using Pygma.Data.Domain;
using Pygma.Data.Domain.Entities;
using Pygma.Data.Domain.Models;
using Pygma.Users.Services;
using Pygma.Users.ViewModels.Requests;

namespace Pygma.Users.Api
{
    [Route("api/account")]
    [AllowAnonymous]
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
            if (registerAccountVm == null)
            {
                return BadRequest();
            }
            
            var appUser = (await _usersRepository.ReadAsync(x => x.Email == registerAccountVm.Email)).FirstOrDefault();

            if (appUser?.Email != null && appUser.Email == registerAccountVm?.Email)
                return NoContent();
            
            var newUser = new User()
            {
                FirstName = registerAccountVm.Firstname,
                LastName = registerAccountVm.Lastname,
                Email = registerAccountVm.Email,
                Password = registerAccountVm.Password,
                Role = Roles.Author,
                Active = false,
                CreatedAt = DateTime.Now
            };

            await _usersRepository.CreateAsync(newUser);

            return NoContent();
        }
        
        [HttpPost("login")]
        public async Task<ActionResult<Jwt>> LoginAsync([FromBody] LoginVm loginVm)
        {
            var user = await _usersRepository.LoginAsync(loginVm.Email, Encryption.Encrypt(loginVm.Password));

            if (user == null)
            {
                return Unauthorized();
            }
            var tokenString = _jwtTokenService.BuildToken(user);
                
            return Ok(new Jwt() { Token = tokenString });

        }
    }
}