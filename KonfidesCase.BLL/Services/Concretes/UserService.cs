using AutoMapper;
using KonfidesCase.Authentication.Dtos;
using KonfidesCase.Authentication.Entities;
using KonfidesCase.BLL.Services.Interfaces;
using KonfidesCase.DAL.Contexts;
using KonfidesCase.Entity.Entities;

namespace KonfidesCase.BLL.Services.Concretes
{
    public class UserService : IUserService
    {
        #region Constructor
        private readonly KonfidesCaseDbContext _context;
        private readonly IMapper _mapper;
        public UserService(KonfidesCaseDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public Task CreateActivity()
        {
            throw new NotImplementedException();
        }
        #endregion

        public async Task CreateAppUser(UserInfoDto userInfo)
        {
            //AppUser newUser = new()
            //{
            //    Id = authUser.Id,
            //    FirstName = authUser.FirstName,
            //    LastName = authUser.LastName,
            //    Email = authUser.Email!,
            //    RoleName = "user", 
            //};
            AppUser newUser = _mapper.Map(userInfo, new AppUser());
            await _context.AddAsync(newUser);            
            await _context.SaveChangesAsync();            
        }

        public Task UpdateActivity()
        {
            throw new NotImplementedException();
        }
    }
}
