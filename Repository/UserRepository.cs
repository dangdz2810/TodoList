using Authentication.Dao.Specifications.UserSpec;
using Authentication.Dao.UnitofWork;
using Authentication.Data;
using Authentication.Entity;
using Authentication.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Text;

namespace Authentication.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly IConfiguration _config;
        private readonly IUnitofWork _unitofwork;

        public UserRepository( IConfiguration configuration, IUnitofWork unitofwork)
        {           
            _config = configuration;
            _unitofwork = unitofwork;
        }

        public async Task<User?> Create(RegisterViewModel registerViewModel)
        {
            var newUser = new User
            {
                UserName = registerViewModel.Username,
                Password = BCrypt.Net.BCrypt.HashPassword(registerViewModel.Password),
                Email = registerViewModel.Email,
                RoleId = 2
            };
            await _unitofwork.Repository<User>().CreateAsync(newUser);
            await _unitofwork.Complete();
            //_context.Users.Add(newUser);
            //await _context.SaveChangesAsync();
            return newUser;
        }

        public async Task<User?> GetByEmail(string email)
        {
            Expression<Func<User, bool>> filter = u => u.Email == email;
            var user = await _unitofwork.Repository<User>().Get(filter);
            //from user in await _unitofwork.Repository<User>().GetAllAsync()
            //              where user.Email == email
            //              select user;
            return (User?)user;
        }

        public async Task<User?> GetById(int id)
        {
            return await _unitofwork.Repository<User>().GetByIdAsync(id);
        }

        public async Task<User?> GetByUsername(string username)
        {
            Expression<Func<User, bool>> filter = u => u.Email == username;
            var user = await _unitofwork.Repository<User>().Get(filter);
            //from user in await _unitofwork.Repository<User>().GetAllAsync()
            //where user.UserName == username
            //select user;
            return (User?)user;
            //return _context.Users.FirstOrDefaultAsync(u => u.UserName.Equals(username));
        }

        public async Task<string> Login(LoginViewModel loginViewModel)
        {
            Expression<Func<User, bool>> filter = u => u.Email == loginViewModel.Email;
            var user = await _unitofwork.Repository<User>().Get(filter);
            //var roleId = await _context.Roles.Where(r => r.Id == user.RoleId).Select(r => r.Name).FirstOrDefaultAsync();
            if (user != null && BCrypt.Net.BCrypt.Verify(loginViewModel.Password, user.Password))
            {
                // Nếu xác thực thành công, tạo JWT token
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_config["Jwt:SecretKey"] ?? "");
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                        new Claim(ClaimTypes.Role, user.RoleId.ToString())
                    }),
                    Expires = DateTime.UtcNow.AddDays(30),
                    SigningCredentials = new SigningCredentials
                        (new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                var jwtToken = tokenHandler.WriteToken(token);
                return jwtToken;
            }
            else
            {
                throw new ArgumentException("Wrong email or password");
            }
        }
    }
}
