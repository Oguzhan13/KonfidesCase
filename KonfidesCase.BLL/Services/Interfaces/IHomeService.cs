using KonfidesCase.Authentication.Dtos;
using KonfidesCase.BLL.Utilities;
using KonfidesCase.DTO.Activity;
using KonfidesCase.DTO.Ticket;
using KonfidesCase.Entity.Entities;

namespace KonfidesCase.BLL.Services.Interfaces
{
    public interface IHomeService
    {
        Task<DataResult<AppUser>> CreateAppUser(UserInfoDto userInfo);
        Task<DataResult<ICollection<Category>>> GetCategories(string currentUserName);
        Task<DataResult<ICollection<City>>> GetCities(string currentUserName);
        Task<DataResult<Activity>> CreateActivity(CreateActivityDto createActivityDto, string currentUserName);
        Task<DataResult<ICollection<Activity>>> GetActivities(string currentUserName);
        Task<DataResult<ICollection<Activity>>> GetMyCreatedActivities(string currentUserName);
        Task<DataResult<ICollection<Activity>>> GetAttendedActivities(string currentUserName);
        Task<DataResult<Activity>> UpdateActivity(UpdateActivityDto updateActivityDto, string currentUserName);
        Task<DataResult<Activity>> CancelActivity(CancelActivityDto cancelActivityDto, string currentUserName);
        Task<DataResult<Ticket>> BuyTicket(CreateTicketDto createTicketDto, string currentUserName);
    }
}
