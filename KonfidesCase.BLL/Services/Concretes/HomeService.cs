using AutoMapper;
using KonfidesCase.Authentication.Dtos;
using KonfidesCase.BLL.Services.Interfaces;
using KonfidesCase.BLL.Utilities;
using KonfidesCase.DAL.Contexts;
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

        #region Methods
        public bool IsAuthorize()
        {
            string currentUserName = _contextAccessor.HttpContext!.User.Identity!.Name!;
            if (string.IsNullOrEmpty(currentUserName))
            {
                return false;
            }           
            return true;
        }
        public async Task CreateAppUser(UserInfoDto userInfo)
        {
            AppUser newUser = _mapper.Map(userInfo, new AppUser());
            await _context.AddAsync(newUser);
            await _context.SaveChangesAsync();
        }

        #endregion

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
    }
}
