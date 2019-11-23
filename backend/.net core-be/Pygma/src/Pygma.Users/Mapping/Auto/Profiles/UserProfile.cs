using AutoMapper;
using Pygma.Users.ViewModels.Requests;
using Pygma.Users.ViewModels.Responses;

namespace Pygma.Users.Mapping.Auto.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserListVm>(MemberList.Destination);
            CreateMap<User, UserVm>(MemberList.Destination)
                .ForMember(d => d.Partner,
                    opt => opt.MapFrom(
                        s => s.PartnerId.HasValue
                            ? new BaseVm {Id = s.PartnerId.Value, Name = s.Partner.Name}
                            : null));

            CreateMap<UpdateUserVm, User>(MemberList.Source);
        }
    }
}