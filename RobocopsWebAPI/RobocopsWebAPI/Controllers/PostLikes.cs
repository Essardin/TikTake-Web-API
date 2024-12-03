using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RobocopsWebAPI.Data;
using RobocopsWebAPI.Models;

namespace RobocopsWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostLikes : ControllerBase
    {
        private readonly MainDbContext _mainDbContext;

        public PostLikes(MainDbContext mainDbContext)
        {
            _mainDbContext = mainDbContext;
        }

        [HttpPost("Add-Like")]
        public async Task<IActionResult> AddLike(LikesModelClass like)
        {
            try
            {
                var result = await _mainDbContext.Likes.Where(x => x.UserId == like.UserId && x.PostId == like.PostId).FirstOrDefaultAsync();

                if (result == null)
                {
                    var newLike = new Like { 
                    UserId = like.UserId,
                    PostId = like.PostId
                    };

                    await _mainDbContext.Likes.AddAsync(newLike);
                    await _mainDbContext.SaveChangesAsync();
                    return Ok("Liked");
                }
                else
                {
                     _mainDbContext.Likes.Remove(result);
                    await _mainDbContext.SaveChangesAsync();
                    return Ok("Unliked");
                }
               
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
