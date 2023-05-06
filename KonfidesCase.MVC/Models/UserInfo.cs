using KonfidesCase.ViewModel;
using System.ComponentModel.DataAnnotations;

namespace KonfidesCase.MVC.Models
{
    public class UserInfo
    {
        #region Properties
        public Guid Id { get; set; }        
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        public string RoleName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        #endregion
    }
}
