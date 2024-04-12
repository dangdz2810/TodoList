using Authentication.Entity;
using Authentication.ViewModels;

namespace Authentication.Services
{
    public interface IUserService
    {
        Task<User?> GetUserById(int userId);
        Task<User?> Register(RegisterViewModel registerViewModel);
        //jwt string
        Task<string> Login(LoginViewModel loginViewModel);
    }
}
