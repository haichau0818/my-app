using Chat.API.Data.Entities;
using Chat.API.DesignPattern.Abstract;
using Chat.API.Extensions;
using DTOs;
using Microsoft.AspNetCore.Identity;
using System.Net.NetworkInformation;
using System.Security.Claims;

namespace Chat.API.Services
{
    public interface IUserServices
    {
        Task<StatusCode> CheckLogin(LoginDTO loginDTO);
    }
    public class UserServices : IUserServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<User> _signInManager;
        public UserServices(IUnitOfWork unitOfWork, UserManager<User> userManager ,RoleManager<IdentityRole> roleManager, SignInManager<User> signInManager)
        {                                                                         
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
        }

        public async Task<StatusCode> CheckLogin(LoginDTO loginDTO)
        {
            var user = await _userManager.FindByNameAsync(loginDTO.Email);
            //var mapper = MapperConfig.InitializeAutomapper();
            //var userMapp = mapper.Map<LoginDTO>(user);
            var status = new StatusCode();
            if (user == null)
            {
                status.Status = 0;
                status.Message = "Invalid username";
                return status;
            }

            if (!await _userManager.CheckPasswordAsync(user, loginDTO.Password))
            {
                status.Status = 0;
                status.Message = "Invalid Password";
                return status;
            }

            var signInResult = await _signInManager.PasswordSignInAsync(user, loginDTO.Password, true, true);
            if (signInResult.Succeeded)
            {
                var userRoles = await _userManager.GetRolesAsync(user);
                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                };

                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }
                status.Status = 1;
                status.Message = "Logged in successfully";
            }
            else if (signInResult.IsLockedOut)
            {
                status.Status = 0;
                status.Message = "User is locked out";
            }
            else
            {
                status.Status = 0;
                status.Message = "Error on logging in";
            }
            return status;
        }
        public async Task<StatusCode> RegisterAsync(RegistrationDTO model)
        {
            var status = new StatusCode();
            var userExists = await _userManager.FindByNameAsync(model.Email);
            if (userExists != null)
            {
                status.Status = 0;
                status.Message = "User already exist";
                return status;
            }
            User user = new User()
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                FirstName = model.FirstName,
                LastName= model.LastName,
                EmailConfirmed = true,
            };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
            {
                status.Status = 0;
                status.Message = "User creation failed";
                return status;
            }

            if (!await _roleManager.RoleExistsAsync(model.Role))
                await _roleManager.CreateAsync(new IdentityRole(model.Role));


            if (await _roleManager.RoleExistsAsync(model.Role))
            {
                await _userManager.AddToRoleAsync(user, model.Role);
            }

            status.Status = 1;
            status.Message = "You have registered successfully";
            return status;
        }

    }
}
