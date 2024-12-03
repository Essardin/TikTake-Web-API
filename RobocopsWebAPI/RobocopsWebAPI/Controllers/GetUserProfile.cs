using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RobocopsWebAPI.Data;
using RobocopsWebAPI.Models;

namespace RobocopsWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GetUserProfile : ControllerBase
    {

        private readonly MainDbContext _mainDbContext;

        public GetUserProfile(MainDbContext mainDbContext)
        {
            _mainDbContext = mainDbContext;

        }


        [HttpGet("Get-Profile/{id}")]
        public async Task<IActionResult> GetUser(string userid)
        {
            try
            {
                var result = _mainDbContext.Users.Where(x => x.UserId == userid)
                     .Include(x => x.Posts)
                     .Include(x => x.Likes)
                     .Include(x => x.Comments)
                     .Include(x => x.FriendList)
                     .Include(x => x.ReceivedFriendRequests)
                     .Include(x => x.SentFriendRequests).FirstOrDefault();  // find user using userid (Primary key)
                if (result == null)
                {
                    return NotFound();
                }
               
                    return Ok(result);
               
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);  // return
            }
            
        }
    }
}
