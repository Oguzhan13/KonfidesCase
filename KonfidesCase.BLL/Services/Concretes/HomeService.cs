using AutoMapper;
using KonfidesCase.Authentication.Dtos;
using KonfidesCase.BLL.Services.Interfaces;
using KonfidesCase.BLL.Utilities;
using KonfidesCase.DAL.Contexts;
using KonfidesCase.DTO.Activity;
using KonfidesCase.DTO.Ticket;
using KonfidesCase.Entity.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace KonfidesCase.BLL.Services.Concretes
{
    public class HomeService : IHomeService
    {
        #region Fields & Constructor
        private readonly KonfidesCaseDbContext _context;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IMapper _mapper;
        public HomeService(KonfidesCaseDbContext context, IHttpContextAccessor contextAccessor, IMapper mapper)
        {
            _context = context;
            _contextAccessor = contextAccessor;
            _mapper = mapper;
        }
        #endregion
                
        public bool IsAuthorize()
        {
            //string currentUserName = _contextAccessor.HttpContext!.User.Identity!.Name!;
            //if (string.IsNullOrEmpty(currentUserName))
            //{
            //    return false;
            //}           
            return true;
        }
        public AppUser FindAppUser()
        {
            return new AppUser(); 
        }

        #region AppUser Methods
        public async Task<DataResult<AppUser>> CreateAppUser(UserInfoDto userInfo)
        {
            bool isUserExists = await _context.Users.AnyAsync(u => u.Email == userInfo.Email);
            if (isUserExists)
            {
                return new DataResult<AppUser>() { IsSuccess = false, Message = "Kullanıcı sistemde kayıtlı" };
            }
            AppUser newUser = _mapper.Map(userInfo, new AppUser());
            await _context.AddAsync(newUser);
            await _context.SaveChangesAsync();
            return new DataResult<AppUser>() { IsSuccess = true, Message = "Kullanıcı oluşturuldu", Data = newUser };
        }
        #endregion

        #region Category Method
        public async Task<DataResult<ICollection<Category>>> GetCategories()
        {
            bool isAuthorize = IsAuthorize();
            if (!isAuthorize)
            {
                return new DataResult<ICollection<Category>>() { IsSuccess = false, Message = "Yetkili değilsiniz" };
            }
            ICollection<Category> categories = await _context.Categories.ToListAsync();
            return new DataResult<ICollection<Category>>() { IsSuccess = true, Message = "Kategorileri listeleme işlemi başarılı", Data = categories };
        }
        #endregion

        #region City Method
        public async Task<DataResult<ICollection<City>>> GetCities()
        {
            bool isAuthorize = IsAuthorize();
            if (!isAuthorize)
            {
                return new DataResult<ICollection<City>>() { IsSuccess = false, Message = "Yetkili değilsiniz" };
            }
            ICollection<City> cities = await _context.Cities.ToListAsync();
            return new DataResult<ICollection<City>>() { IsSuccess = true, Message = "Şeirleri listeleme işlemi başarılı", Data = cities };
        }
        #endregion

        #region Activity Methods 
        public async Task<DataResult<Activity>> CreateActivity(CreateActivityDto createActivityDto)
        {
            bool isActivityExists = await _context.Activities.AnyAsync(a => a.Name == createActivityDto.Name);
            if (isActivityExists)
            {
                return new DataResult<Activity>() { IsSuccess = false, Message = "Etkinlik sistemde mevcut" };
            }
            bool isAuthorize = IsAuthorize();
            if (!isAuthorize)
            {
                return new DataResult<Activity>() { IsSuccess = false, Message = "Yetkili değilsiniz" };
            }
            if (createActivityDto.ActivityDate.AddDays(1) < DateTime.Now)
            {
                return new DataResult<Activity>() { IsSuccess = false, Message = "Etkinlik tarihi en az 24 saat sonra olmalı" };
            }
            Activity newActivity = _mapper.Map(createActivityDto, new Activity());            
            string userEmail = _contextAccessor.HttpContext!.User.Identity!.Name!;            
            newActivity.Organizer = userEmail;
            await _context.Activities.AddAsync(newActivity);
            await _context.SaveChangesAsync();
            return new DataResult<Activity>() { IsSuccess = true, Message = "Etkinlik ekleme işlemi başarılı", Data = newActivity };
        }
        public async Task<DataResult<ICollection<Activity>>> GetActivities()
        {
            bool isAuthorize = IsAuthorize();
            if (!isAuthorize)
            {
                return new DataResult<ICollection<Activity>>() { IsSuccess = false, Message = "Yetkili değilsiniz" };
            }
            ICollection<Activity> activities = await _context.Activities.ToListAsync();
            return new DataResult<ICollection<Activity>>() { IsSuccess = true, Message = "Etkinlikleri listeleme işlemi başarılı", Data = activities };
        }
        public async Task<DataResult<ICollection<Activity>>> GetMyCreatedActivities()
        {
            bool isAuthorize = IsAuthorize();
            if (!isAuthorize)
            {
                return new DataResult<ICollection<Activity>>() { IsSuccess = false, Message = "Yetkili değilsiniz" };
            }
            string currentUserName = _contextAccessor.HttpContext!.User.Identity!.Name!;
            var myCreatedActivities = await _context.Activities.Where(a => a.Organizer == currentUserName).ToListAsync();
            return new DataResult<ICollection<Activity>>() { IsSuccess = true, Message = "Oluşturduğunuz etkinlikler listelendi", Data = myCreatedActivities };
        }
        public async Task<DataResult<ICollection<Activity>>> GetAttendedActivities()
        {
            bool isAuthorize = IsAuthorize();
            if (!isAuthorize)
            {
                return new DataResult<ICollection<Activity>>() { IsSuccess = false, Message = "Yetkili değilsiniz" };
            }
            string currentUserName = _contextAccessor.HttpContext!.User.Identity!.Name!;
            var currentUser = await _context.Users.FirstAsync(u => u.Email == currentUserName);
            var userActivityList = await _context.UserActivity.Where(a => a.UserId == currentUser.Id).ToListAsync();
            List<Activity> attendedActivities = new();
            foreach (var item in userActivityList)
            {
                var activity = await _context.Activities.FirstAsync(a => a.Id == item.ActivityId);
                attendedActivities.Add(activity);
            }
            return new DataResult<ICollection<Activity>>() { IsSuccess = true, Message = "Katıldığınız etkinlikler listelendi", Data = attendedActivities };
        }
        public async Task<DataResult<Activity>> UpdateActivity(UpdateActivityDto updateActivityDto)
        {            
            bool isAuthorize = IsAuthorize();
            if (!isAuthorize)
            {
                return new DataResult<Activity>() { IsSuccess = false, Message = "Yetkili değilsiniz" };
            }
            Activity updateActivity = await _context.Activities.FirstAsync(a => a.Id == updateActivityDto.Id);
            if (updateActivity.ActivityDate <= DateTime.Now.AddDays(5))
            {
                return new DataResult<Activity>() { IsSuccess = false, Message = "Etkinliğe 5 gün kala güncelleme işlemi yapamazsınız!" };
            }
            _mapper.Map(updateActivityDto, updateActivity);
            _context.Activities.Update(updateActivity);
            await _context.SaveChangesAsync();
            return new DataResult<Activity>() { IsSuccess = true, Message = "Etkinlik güncellendi", Data = updateActivity };
        }
        public async Task<DataResult<Activity>> CancelActivity(CancelActivityDto cancelActivityDto)
        {
            bool isAuthorize = IsAuthorize();
            if (!isAuthorize)
            {
                return new DataResult<Activity>() { IsSuccess = false, Message = "Yetkili değilsiniz" };
            }
            Activity cancelActivity = await _context.Activities.FirstAsync(a => a.Id == cancelActivityDto.Id);
            if (cancelActivity.ActivityDate <= DateTime.Now.AddDays(5))
            {                
                return new DataResult<Activity>() { IsSuccess = false, Message = "Etkinliğe 5 gün kala iptal edemezsiniz!" };
            }
            _context.Activities.Remove(cancelActivity);
            await _context.SaveChangesAsync();
            return new DataResult<Activity>() { IsSuccess = true, Message = "Etkinlik iptal edildi", Data = cancelActivity };
        }
        #endregion

        #region Ticket Method
        public async Task<DataResult<Ticket>> BuyTicket(CreateTicketDto createTicketDto)
        {
            bool isAuthorize = IsAuthorize();
            if (!isAuthorize)
            {
                return new DataResult<Ticket>() { IsSuccess = false, Message = "Yetkili değilsiniz" };
            }            
            string currentUserName = _contextAccessor.HttpContext!.User.Identity!.Name!;
            var currentUser = await _context.Users.FirstAsync(u => u.Email == currentUserName);            
            Ticket newTicket = new()
            {
                ActivityId = createTicketDto.ActivityId,
                UserId = currentUser.Id,
                TicketNo = $"{currentUser.Id}-{createTicketDto.ActivityId}",                
            };
            var isTicketExists = await _context.Tickets.AnyAsync(t => t.TicketNo == newTicket.TicketNo);
            if (isTicketExists)
            {
                return new DataResult<Ticket>() { IsSuccess = false, Message = "Etkinlik için bilet oluşturulmuş!" };
            }
            AppUserActivity userActivity = new()
            {
                ActivityId = createTicketDto.ActivityId,
                UserId = currentUser.Id,
            };
            await _context.Tickets.AddAsync(newTicket);
            await _context.SaveChangesAsync();
            await _context.UserActivity.AddAsync(userActivity);
            await _context.SaveChangesAsync();
            return new DataResult<Ticket>() { IsSuccess = true, Message = "Bilet alma işlemi başarılı", Data = newTicket };
        }
        #endregion
        
    }
}
