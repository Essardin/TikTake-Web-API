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

        [HttpPost("Edit-Profile-Picture")]
        public async Task<IActionResult> UpdateProfilePicture(EditProfilePictureRequestModel editPicture)
        {
            try
            {
                var user = await _mainDbContext.Users.FindAsync(editPicture.userid);
                if (user != null)
                {
                    if (editPicture.ProfilePic != null)
                    {
                        var destroyPrevImg = await _cloudinaryService.DeleteImageAsync(editPicture.ProfilePicPublicId);
                        if (destroyPrevImg != null && destroyPrevImg.StatusCode == System.Net.HttpStatusCode.OK)
                        {

                            var fileStream = editPicture.ProfilePic.OpenReadStream();

                            var uploadResult = await _cloudinaryService.UploadImageAsync(editPicture.ProfilePic.FileName, fileStream, $"profilepic/{editPicture.userid}");

                            user.ProfilePicURL = uploadResult.SecureUrl.ToString();
                            user.ProfilePicPublicID = uploadResult.PublicId;
                            _mainDbContext.SaveChanges();
                        }
                    }

                    return Ok("Profile Picture Updated successfully");
                }
                else
                {
                    throw new Exception("Some error occured while updating Profilr Pic");
                }
               
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("Edit-Profile")]
        public async Task<IActionResult> UpdateUserProfile(EditProfileRequestModel editProfile)
        {

            try
            {

                var user = await _mainDbContext.Users.FindAsync(editProfile.userid);
                if (user != null)
                {
                    if (editProfile.fullName != null)
                    {
                        user.FullName = editProfile.fullName;
                    }
                    else
                    {
                        user.FullName = user.FullName;
                    }

                    if (editProfile.bio != null)
                    {
                        user.Bio = editProfile.bio;
                    }
                    else
                    {
                        user.Bio = user.Bio;
                    }
                   
                    _mainDbContext.SaveChanges();
                    return Ok("Profile uploaded successfully");

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

        [HttpPost("UploadVideoIntro")]
        public async Task<IActionResult> VideoIntro(UploadVideoIntroModelClass intro)
        {
            try
            {
                if(intro.videoFile == null || intro.videoFile.Length == 0)
                {
                    return BadRequest("Intro file empty");
                }
               
              
                var fileStream = intro.videoFile.OpenReadStream();
                var uploadVideoResult = await _cloudinaryService.UploadVideoAsync(intro.videoFile.FileName, fileStream, "VideoIntro", intro.videoPublicId);

                var user = await _mainDbContext.Users.FindAsync(intro.userId);
                if (user != null)
                {
                    user.VideoIntroURL = uploadVideoResult.SecureUrl.ToString();
                    user.VideoIntroPublicID = uploadVideoResult.PublicId;

                   _mainDbContext.SaveChanges();
                }
                return Ok("Intro uploaded Successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
