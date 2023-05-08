using KonfidesCase.Authentication.Dtos;
using KonfidesCase.BLL.Utilities;
using KonfidesCase.Entity.Entities;

namespace KonfidesCase.BLL.Services.Interfaces
{
    public interface IHomeService
    {
        Task CreateAppUser(UserInfoDto userInfo);
        Task<DataResult<ICollection<Category>>> GetCategories();
        Task<DataResult<ICollection<City>>> GetCities();
    }
}
