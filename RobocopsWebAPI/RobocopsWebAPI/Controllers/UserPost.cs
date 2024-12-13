using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RobocopsWebAPI.Data;
using RobocopsWebAPI.Models;
using RobocopsWebAPI.Repository;
using System.Diagnostics.Eventing.Reader;
using static System.Net.Mime.MediaTypeNames;

namespace RobocopsWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserPost : ControllerBase
    {
        private readonly MainDbContext _mainDbContext;
        private readonly CloudinaryService _cloudinaryService;

        public UserPost(MainDbContext mainDbContext, CloudinaryService cloudinaryService)
        {
            _mainDbContext = mainDbContext;
            _cloudinaryService = cloudinaryService;
        }

        [HttpPost("Add-Post")]

        public async Task <IActionResult> AddPost(PostRequestModel postRequestModel)
        {
            try
            {
                if(postRequestModel.postImage == null || postRequestModel.postImage.Length == 0)
                {
                    return BadRequest("No image file uploaded.");
                }

                var fileStream = postRequestModel.postImage.OpenReadStream();
                var postUrl = await _cloudinaryService.UploadImageAsync(postRequestModel.postImage.FileName, fileStream,$"Posts/{postRequestModel.userid}");
               
                var newPost = new Post { 
                
                    PostId = Guid.NewGuid().ToString(),
                    UserId= postRequestModel.userid,
                    PostImageURL = postUrl.SecureUrl.ToString(),
                    PostCaption = postRequestModel.postCaption,
                    PostTimeStamp = DateTime.Now
                };

                    
              var postResult= await _mainDbContext.Posts.AddAsync(newPost);
                _mainDbContext.SaveChanges();

                if (postResult == null)
                {
                    return BadRequest("Failed to POST.");
                }

                //return Ok(new { Message = "Posted successfully", Result = postResult });
                return Ok("Posted Successfully");

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }


        }

        [HttpGet("Explore-Posts")]
        public async Task<IActionResult> ExplorePosts()
        {
            try
            {


                //var latestPosts = await _mainDbContext.Users
                //              .Include(x => x.Posts.OrderByDescending(p => p.PostTimeStamp).Take(3))
                //              .Include(x => x.Likes)
                //              .Include(x => x.Comments).ToListAsync();


                var usersWithPosts = await _mainDbContext.Users
            .Select(user => new
            {
                User = user,
                Posts = user.Posts.OrderByDescending(post => post.PostTimeStamp).Take(3),
                Comments = user.Comments,
                Likes = user.Likes
            
           
            })
    .ToListAsync();

                return Ok(usersWithPosts);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
           
        }
    }
}
