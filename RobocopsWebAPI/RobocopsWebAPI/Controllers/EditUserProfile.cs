using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using dotenv.net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RobocopsWebAPI.Data;
using RobocopsWebAPI.Models;
using RobocopsWebAPI.Repository;

namespace RobocopsWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EditUserProfile : ControllerBase
    {
       
        private readonly MainDbContext _mainDbContext;
       private readonly CloudinaryService _cloudinaryService;
       

        public EditUserProfile(MainDbContext mainDbContext, CloudinaryService cloudinaryService)
        {
         
            _mainDbContext = mainDbContext;
             _cloudinaryService = cloudinaryService;

          
        }

        [HttpPost("Edit-Profile")]
        public async Task<IActionResult> UpdateUserProfile(EditProfileRequestModel editProfile)
        {

            try
            {

               // var userProfile = new UserProfile();
              

                var user = await _mainDbContext.Users.FindAsync(editProfile.userid);
                if (user != null)
                {
                    if (editProfile.ProfilePic != null)
                    {

                        var fileStream = editProfile.ProfilePic.OpenReadStream();

                        var uploadImageUrl = await _cloudinaryService.UploadImageAsync(editProfile.ProfilePic.FileName, fileStream, "profilepic");

                        user.ProfilePicURL = uploadImageUrl;
                    }
                    if (editProfile.fullName != null)
                    {
                        user.FullName = editProfile.fullName;
                    }
                  
                    if (editProfile.bio != null)
                    {
                        user.Bio = editProfile.bio;
                    }
                    
                    _mainDbContext.SaveChanges();
                    return Ok(new { Message = "Profile uploaded successfully" });

                    //return Ok("Details updated successfully");

                }
                else
                {
                    return BadRequest("Not updated");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }
          
        }

    }
}
