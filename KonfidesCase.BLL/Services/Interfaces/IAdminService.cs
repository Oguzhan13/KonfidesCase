using KonfidesCase.BLL.Utilities;
using KonfidesCase.DTO.Activity;
using KonfidesCase.DTO.Category;
using KonfidesCase.DTO.City;
using KonfidesCase.Entity.Entities;

namespace KonfidesCase.BLL.Services.Interfaces
{
    public interface IAdminService
    {
        Task<DataResult<Category>> CreateCategory(CreateCategoryDto createCategoryDto, string currentUserName);        
        Task<DataResult<Category>> UpdateCategory(UpdateCategoryDto updateCategoryDto, string currentUserName);
        Task<DataResult<City>> CreateCity(CreateCityDto createCityDto, string currentUserName);        
        Task<DataResult<City>> UpdateCity(UpdateCityDto updateCityDto, string currentUserName);        
        Task<DataResult<Activity>> ConfirmActivity(ConfirmActivityDto confirmActivityDto, string currentUserName);
    }
}
