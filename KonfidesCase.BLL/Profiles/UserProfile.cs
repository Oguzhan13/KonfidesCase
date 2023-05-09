using AutoMapper;
using KonfidesCase.Authentication.Dtos;
using KonfidesCase.Authentication.Entities;
using KonfidesCase.DTO;
using KonfidesCase.Entity.Entities;

namespace KonfidesCase.BLL.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserInfoDto, AppUser>()
                .ForSourceMember(uid => uid.Password, dst => dst.DoNotValidate());
            
        }
    }
}
