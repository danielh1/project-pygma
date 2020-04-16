using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pygma.Common.Constants;
using Pygma.Common.Models.Base;
using Pygma.Data.Abstractions.Repositories;
using Pygma.Users.ViewModels.Requests;
using Pygma.Users.ViewModels.Requests.Users;
using Pygma.Users.ViewModels.Responses;
using Pygma.Users.ViewModels.Responses.Users;

namespace Pygma.Users.Api
{
    [Route("api/users")]
    [Authorize(Roles = Roles.Admin)]
    public class UsersController : CommonControllerBase
    {
        private readonly IUsersRepository _usersRepository;
        private readonly IMapper _mapper;

        public UsersController(IUsersRepository usersRepository,
            IMapper mapper)
        {
            _usersRepository = usersRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<UserListItemVm[]>> GetAll()
        {
            return _mapper.Map<UserListItemVm[]>(await _usersRepository.ReadAllAsync());
        }

        [HttpGet("{userId:int:min(1)}")]
        public async Task<ActionResult<UserVm>> GetUser(int userId)
        {
            return _mapper.Map<UserVm>(await _usersRepository.ReadByIdAsync(userId));
        }

        [HttpPut("{userId:int:min(1)}")]
        public async Task<ActionResult> UpdateUserAsync(int userId, UpdateUserVm updateUserVm)
        {
            var user = await _usersRepository.ReadByIdAsync(userId);

            if (user == null)
                return NotFound();

            _mapper.Map(updateUserVm, user);

            await _usersRepository.UpdateAsync(user);

            return NoContent();
        }

        [HttpDelete("{userId:int:min(1)}")]
        public async Task<ActionResult> DeleteUserAsync(int userId)
        {
            var user = await _usersRepository.ReadByIdAsync(userId);

            if (user == null)
                return NotFound();

            // You can not delete an Admin
            if (user.Role == Roles.Admin)
            {
                return new BadRequestResult();
            }

            await _usersRepository.DeleteAsync(user);

            return NoContent();
        }
    }
}