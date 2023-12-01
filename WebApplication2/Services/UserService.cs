

using WebApplication2.Controllers;
using WebApplication2.Models;

namespace WebApplication2.Services
{
    public class UserService:IUserService
    {
        public bool IsValidUserInformation(LoginModel model)
        {
            if (model.UserName.Equals("Tuan") && model.Password.Equals("123456")) return true;
            else return false;
        }

        object? IUserService.GetUserDetails()
        {
            throw new NotImplementedException();
        }
    }
}
