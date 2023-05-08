using KonfidesCase.Authentication.Dtos;
using KonfidesCase.BLL.Utilities;
using KonfidesCase.DTO.Activity;
using KonfidesCase.Entity.Entities;

namespace KonfidesCase.BLL.Services.Interfaces
{
    public interface IHomeService
    {
        Task<DataResult<AppUser>> CreateAppUser(UserInfoDto userInfo);
        Task<DataResult<ICollection<Category>>> GetCategories();
        Task<DataResult<ICollection<City>>> GetCities();
        Task<DataResult<Activity>> CreateActivity(CreateActivityDto createActivityDto);
        Task<DataResult<ICollection<Activity>>> GetActivities();
        Task<DataResult<Activity>> UpdateActivity(UpdateActivityDto updateActivityDto);
        Task<DataResult<Activity>> CancelActivity(Guid activityId);
        Task<DataResult<ICollection<Activity>>> GetMyCreatedActivities();
        Task<DataResult<ICollection<Activity>>> GetAttendedActivities();
        Task<DataResult<Ticket>> BuyTicket(Guid activityId);
    }
}
