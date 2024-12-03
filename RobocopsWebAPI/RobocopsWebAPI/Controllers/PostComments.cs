using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RobocopsWebAPI.Data;
using RobocopsWebAPI.Models;

namespace RobocopsWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostComments : ControllerBase
    {
        private readonly MainDbContext _mainDbContext;

        public PostComments(MainDbContext mainDbContext)
        {
            _mainDbContext = mainDbContext;
        }

        [HttpPost("Add-Comment")]
        public async Task<IActionResult> AddComment(CommentsModelClass comment)
        {
            try
            {
                var newComment = new Comment
                {
                    CommentId = Guid.NewGuid().ToString(),
                    PostId = comment.PostId,
                    UserId = comment.UserId,
                    CommentText = comment.CommentText,
                    CommentTimeStamp = DateTime.Now,

                };
                 await _mainDbContext.Comments.AddAsync(newComment);
                await _mainDbContext.SaveChangesAsync();
                return Ok("Comment added successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
