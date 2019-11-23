using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pygma.Common.Models.Base;
using Pygma.Data.Abstractions.Cache;
using Pygma.Data.Abstractions.Repositories;
using Pygma.Users.ViewModels.Requests;
using Pygma.Users.ViewModels.Responses;

namespace Pygma.Users.Api
{
    [Route("api/users")]
    [Authorize]    
    public class UsersController: CommonControllerBase
    {
        private readonly IUsersRepository _usersRepository;
        private readonly IUsersCache _usersCache;
        private readonly IMapper _mapper;


        public UsersController(IUsersRepository usersRepository,
            IUsersCache usersCache,
            IMapper mapper)
        {
            _usersRepository = usersRepository;
            _usersCache = usersCache;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<UserListVm[]> SearchUsers()
        {
            return _mapper.Map<UserListVm[]>(_usersCache.GetAll());
        }

        [HttpGet("{userId:int:min(1)}")]
        [Authorize]    
        public ActionResult<UserVm> GetUser(int userId)
        {
            return _mapper.Map<UserVm>(_usersCache.GetById(userId));
        }

        [HttpPut("{userId:int:min(1)}")]
        [Authorize]    
        public async Task<ActionResult> UpdateUserAsync(int userId, UpdateUserVm updateUserVm)
        {
            var user = await _usersRepository.ReadByIdAsync(userId);

            if (user == null)
                return NotFound();

            _mapper.Map(updateUserVm, user);
            
            await _usersRepository.UpdateAsync(user);
            
            return Ok();
        }
    }
}
