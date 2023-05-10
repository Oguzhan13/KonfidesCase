using AutoMapper;
using KonfidesCase.BLL.Services.Interfaces;
using KonfidesCase.BLL.Utilities;
using KonfidesCase.DAL.Contexts;
using KonfidesCase.DTO.Activity;
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
        private readonly IMapper _mapper;
        public AdminService(KonfidesCaseDbContext context, IMapper mapper)
        {
            _context = context;            
            _mapper = mapper;
        }
        #endregion

        public async Task<bool> IsAdmin(string currentUserName)
        {               
            var currentUser = string.IsNullOrEmpty(currentUserName) ? null : await _context.Users.FirstAsync(u => u.Email == currentUserName);
            if (currentUser is null)
            {
                return false;
            }
            return currentUser.RoleName.Equals("admin") ? true : false;
        }

        #region Category Methods
        public async Task<DataResult<Category>> CreateCategory(CreateCategoryDto createCategoryDto, string currentUserName)
        {
            bool isCategoryExists = await _context.Categories.AnyAsync(c => c.Name == createCategoryDto.Name);
            if (isCategoryExists)
            {
                return new DataResult<Category>() { IsSuccess = false, Message = "Kategori sistemde kayıtlı" };
            }
            bool isAuthorize = await IsAdmin(currentUserName);
            if (!isAuthorize)
            {
                return new DataResult<Category>() { IsSuccess = false, Message = "Yetkili değilsiniz" };
            }
            Category newCategory = _mapper.Map(createCategoryDto, new Category());
            await _context.Categories.AddAsync(newCategory);            
            await _context.SaveChangesAsync();
            return new DataResult<Category>() { IsSuccess = true, Message = "Kategori ekleme işlemi başarılı", Data = newCategory };
        }        
        public async Task<DataResult<Category>> UpdateCategory(UpdateCategoryDto updateCategoryDto, string currentUserName)
        {
            bool isCategoryExists = await _context.Categories.AnyAsync(c => c.Name == updateCategoryDto.Name);
            if (isCategoryExists)
            {
                return new DataResult<Category>() { IsSuccess = false, Message = "Kategori sistemde kayıtlı" };
            }
            bool isAuthorize = await IsAdmin(currentUserName);
            if (!isAuthorize)
            {
                return new DataResult<Category>() { IsSuccess = false, Message = "Yetkili değilsiniz" };
            }
            Category updateCategory = await _context.Categories.FirstAsync(c => c.Id ==  updateCategoryDto.Id);
            _mapper.Map(updateCategoryDto, updateCategory);
            _context.Categories.Update(updateCategory);
            await _context.SaveChangesAsync();
            return new DataResult<Category>() { IsSuccess = true, Message = "Kategori güncellendi", Data = updateCategory };
        }
        #endregion

        #region City Methods
        public async Task<DataResult<City>> CreateCity(CreateCityDto createCityDto, string currentUserName)
        {
            bool isCityExists = await _context.Cities.AnyAsync(c => c.Name == createCityDto.Name);
            if (isCityExists)
            {
                return new DataResult<City>() { IsSuccess = false, Message = "Şehir sistemde kayıtlı" };
            }
            bool isAuthorize = await IsAdmin(currentUserName);
            if (!isAuthorize)
            {
                return new DataResult<City>() { IsSuccess = false, Message = "Yetkili değilsiniz" };
            }
            City newCity = _mapper.Map(createCityDto, new City());
            await _context.Cities.AddAsync(newCity);
            await _context.SaveChangesAsync();
            return new DataResult<City>() { IsSuccess = true, Message = "Şehir ekleme işlemi başarılı", Data = newCity };
        }        
        public async Task<DataResult<City>> UpdateCity(UpdateCityDto updateCityDto, string currentUserName)
        {
            bool isCityExists = await _context.Cities.AnyAsync(c => c.Name == updateCityDto.Name);
            if (isCityExists)
            {
                return new DataResult<City>() { IsSuccess = false, Message = "Şehir sistemde kayıtlı" };
            }
            bool isAuthorize = await IsAdmin(currentUserName);
            if (!isAuthorize)
            {
                return new DataResult<City>() { IsSuccess = false, Message = "Yetkili değilsiniz" };
            }
            City updateCity = await _context.Cities.FirstAsync(c => c.Id ==  updateCityDto.Id);
            _mapper.Map(updateCityDto, updateCity);
            _context.Cities.Update(updateCity);
            await _context.SaveChangesAsync();
            return new DataResult<City>() { IsSuccess = true, Message = "Şehir güncellendi", Data = updateCity };
        }
        #endregion

        #region Activity Method
        public async Task<DataResult<Activity>> ConfirmActivity(ConfirmActivityDto confirmActivityDto, string currentUserName)
        {
            bool isAuthorize = await IsAdmin(currentUserName);
            if (!isAuthorize)
            {
                return new DataResult<Activity>() { IsSuccess = false, Message = "Yetkili değilsiniz" };
            }
            Activity activity = await _context.Activities.FirstAsync(a => a.Id == confirmActivityDto.Id);
            if (activity is null)
            {
                return new DataResult<Activity>() { IsSuccess = false, Message = "Etkinik bulunamadı!" };
            }
            activity!.IsConfirm = confirmActivityDto.IsConfirm;
            _context.Activities.Update(activity);
            await _context.SaveChangesAsync();
            return new DataResult<Activity>() { IsSuccess = true, Message = "Etkinlik onaylama işlemi başarılı", Data = activity };
        }
        #endregion

    }
}
