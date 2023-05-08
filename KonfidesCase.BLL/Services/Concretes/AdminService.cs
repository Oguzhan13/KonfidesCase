using AutoMapper;
using KonfidesCase.BLL.Services.Interfaces;
using KonfidesCase.BLL.Utilities;
using KonfidesCase.DAL.Contexts;
using KonfidesCase.DTO.Category;
using KonfidesCase.DTO.City;
using KonfidesCase.Entity.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace KonfidesCase.BLL.Services.Concretes
{
    public class AdminService : IAdminService
    {
        #region Fields & Constructor
        private readonly KonfidesCaseDbContext _context;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IMapper _mapper;
        public AdminService(KonfidesCaseDbContext context, IHttpContextAccessor contextAccessor, IMapper mapper)
        {
            _context = context;
            _contextAccessor = contextAccessor;
            _mapper = mapper;
        }
        #endregion

        #region Methods
        public async Task<bool> IsAdmin()
        {
            string currentUserName = _contextAccessor.HttpContext!.User.Identity!.Name!;
            if (string.IsNullOrEmpty(currentUserName))
            {
                return false;
            }
            var currentUser =  await _context.Users.FirstAsync(u => u.Email == currentUserName);
            if (currentUser is null)
            {
                return false;
            }
            return currentUser.RoleName.Equals("admin") ? true : false;
        }
        #endregion

        #region Methods for Actions
        public async Task<DataResult<Category>> CreateCategory(CreateCategoryDto createCategoryDto)
        {
            bool isAuthorize = await IsAdmin();
            if (!isAuthorize)
            {
                return new DataResult<Category>() { IsSuccess = false, Message = "Yetkili değilsiniz" };
            }
            Category newCategory = _mapper.Map(createCategoryDto, new Category());
            await _context.Categories.AddAsync(newCategory);            
            await _context.SaveChangesAsync();
            return new DataResult<Category>() { IsSuccess = true, Message = "Kategori ekleme işlemi başarılı", Data = newCategory };
        }        
        public async Task<DataResult<Category>> UpdateCategory(UpdateCategoryDto updateCategoryDto)
        {
            bool isAuthorize = await IsAdmin();
            if (!isAuthorize)
            {
                return new DataResult<Category>() { IsSuccess = false, Message = "Yetkili değilsiniz" };
            }
            Category updateCategory = _mapper.Map(updateCategoryDto, new Category());
            _context.Categories.Update(updateCategory);
            await _context.SaveChangesAsync();
            return new DataResult<Category>() { IsSuccess = true, Message = "Kategori güncellendi", Data = updateCategory };
        }

        public async Task<DataResult<City>> CreateCity(CreateCityDto createCityDto)
        {
            bool isAuthorize = await IsAdmin();
            if (!isAuthorize)
            {
                return new DataResult<City>() { IsSuccess = false, Message = "Yetkili değilsiniz" };
            }
            City newCity = _mapper.Map(createCityDto, new City());
            await _context.Cities.AddAsync(newCity);
            await _context.SaveChangesAsync();
            return new DataResult<City>() { IsSuccess = true, Message = "Şehir ekleme işlemi başarılı", Data = newCity };
        }
        public async Task<DataResult<City>> UpdateCity(UpdateCityDto updateCityDto)
        {
            bool isAuthorize = await IsAdmin();
            if (!isAuthorize)
            {
                return new DataResult<City>() { IsSuccess = false, Message = "Yetkili değilsiniz" };
            }
            City updateCity = _mapper.Map(updateCityDto, new City());
            _context.Cities.Update(updateCity);
            await _context.SaveChangesAsync();
            return new DataResult<City>() { IsSuccess = true, Message = "Şehir ekleme işlemi başarılı", Data = updateCity };
        }



        #endregion        
    }
}
