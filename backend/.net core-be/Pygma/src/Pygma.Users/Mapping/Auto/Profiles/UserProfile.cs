using AutoMapper;
using Pygma.Data.Domain.Entities;
using Pygma.Users.ViewModels.Requests;
using Pygma.Users.ViewModels.Responses;

namespace Pygma.Users.Mapping.Auto.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserListItemVm>(MemberList.Destination);
            CreateMap<User, UserVm>(MemberList.Destination);
            
            CreateMap<UpdateUserVm, User>(MemberList.Source);
        }
    }
}