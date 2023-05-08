using KonfidesCase.BLL.Utilities;
using KonfidesCase.DTO.Category;
using KonfidesCase.DTO.City;
using KonfidesCase.Entity.Entities;

namespace KonfidesCase.BLL.Services.Interfaces
{
    public interface IAdminService
    {
        Task<DataResult<Category>> CreateCategory(CreateCategoryDto createCategoryDto);
        Task<DataResult<Category>> UpdateCategory(UpdateCategoryDto updateCategoryDto);
        Task<DataResult<City>> CreateCity(CreateCityDto createCityDto);
        Task<DataResult<City>> UpdateCity(UpdateCityDto updateCityDto);        
        Task<DataResult<Activity>> ConfirmActivity(Guid activityId, bool confirmActivity);
    }
}
