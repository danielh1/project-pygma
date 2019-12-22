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

        public AccountController(
            IUsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
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
    }
}