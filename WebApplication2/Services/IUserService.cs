using WebApplication2.Controllers;
using WebApplication2.Models;

namespace WebApplication2.Services
{
    
        public interface IUserService
        {
        object? GetUserDetails();
        bool IsValidUserInformation(LoginModel model);
            
        }
    
}
