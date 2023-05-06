using KonfidesCase.Authentication.Dtos;
using KonfidesCase.Authentication.Entities;
using KonfidesCase.Entity.Entities;

namespace KonfidesCase.BLL.Services.Interfaces
{
    public interface IUserService
    {
        Task CreateAppUser(UserInfoDto userInfo);
        Task CreateActivity();
        Task UpdateActivity();        
    }
}
