using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using RobocopsWebAPI.Data;
using RobocopsWebAPI.Models;

namespace RobocopsWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddUser : ControllerBase
    {

        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly MainDbContext _mainDbContext;
        public AddUser(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, MainDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _mainDbContext = context;
        }

        [HttpPost("add-user")]
        public async Task<IActionResult> RegisterUser(Register register)
        {

            try
            {
                var pass = register.Password;

            
              


                if (pass.Length > 8 && pass.Any(char.IsDigit) && pass.Any(char.IsLetter) && pass.Any(char.IsLower) &&
                    pass.Any(char.IsUpper) && pass.Any(char.IsSymbol) || pass.Any(char.IsPunctuation))
                {
                    var isPresent = await _userManager.FindByEmailAsync(register.Email);
                    
                    if (isPresent != null)
                    {
                        return Ok ("User already present");
                    }

                    var user = new IdentityUser
                    {
                        UserName = register.UserName,
                        Email = register.Email

                    };
                    var result = await _userManager.CreateAsync(user, pass);
                    if (result.Succeeded)
                    {
                        var userDetails = await _userManager.FindByEmailAsync(register.Email);

                        var userProfile = new UserProfile
                        {
                            UserId = userDetails.Id,
                            Email = userDetails.Email,
                            UserCreationDate = DateTime.Now
                            
                        };

                        var result2 = await _mainDbContext.Users.AddAsync(userProfile);

                        if (result2 != null)
                        {
                            _mainDbContext.SaveChanges();

                            return Ok("User registered successfully");
                        }
                        else
                        {
                            return BadRequest("Failed to register.");
                        }



                    }
                    else
                    {
                        return BadRequest("Failed to register. Kindly try again");
                    }

                }
                else
                {
                    return BadRequest("Passwords must have at least 8 characters and must contain at least 1 Uppercase letter, 1 Lowercase letter, 1 Number, and 1 Special character");
                }

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }


        [HttpPost("user-login")]
        public async Task<IActionResult> Login(Login userlogin)
        {
            try
            {
                var isUserPresent = await _userManager.FindByNameAsync(userlogin.UserName);
                if (isUserPresent == null)
                {
                    return NotFound("User not found");
                }

                //if (isUserPresent.PasswordHash != userlogin.Password)
                //{
                //    return BadRequest("Your password is wrong");
                //}
                var result = await _signInManager.PasswordSignInAsync(userlogin.UserName, userlogin.Password, false, false);

                if (result != null && result.Succeeded)
                {
                    var getUserDetails = await _userManager.FindByEmailAsync(userlogin.UserName);
                    
                    return Ok(getUserDetails.Id);
                }
                else
                {
                    return BadRequest("Login Failed. Your Password seems incorrect");
                }

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}
