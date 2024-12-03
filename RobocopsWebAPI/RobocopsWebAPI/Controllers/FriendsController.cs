using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using RobocopsWebAPI.Data;
using RobocopsWebAPI.Models;
using RobocopsWebAPI.Repository;

namespace RobocopsWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FriendsController : ControllerBase
    {
        private readonly MainDbContext _mainDbContext;
        private readonly CloudinaryService _cloudinaryService;

        public FriendsController(MainDbContext mainDbContext, CloudinaryService cloudinaryService)
        {
            _mainDbContext = mainDbContext;
            _cloudinaryService = cloudinaryService;
        }

        [HttpGet("Find-Friends")]
        public async Task<IActionResult> FindFriends(string name, string  id)
        {
            try
            {
                var result = await _mainDbContext.Users.Where(x => x.FullName.Contains(name) && x.UserId != id).ToListAsync();
                if (result.Count > 0)
                {
                    return Ok(result);
                }
                else
                {
                    return NotFound();
                }

            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
            
        }

       

        [HttpPost("SendFriendRequest")]
        public async Task<IActionResult> FriendRequest(FriendRequestModelClass friendrequest)
        {


            try
            {
                if (string.IsNullOrEmpty(friendrequest.UserId) || string.IsNullOrEmpty(friendrequest.ReceiverUserId))
                {
                    return BadRequest("Sender and Receiver User IDs are required.");
                }

                var alreadyRequested = await _mainDbContext.FriendRequests.Where(x => x.UserId == friendrequest.UserId && x.ReceiverUserId == friendrequest.ReceiverUserId).ToListAsync();
                if (alreadyRequested.Count > 0)
                {
                    return BadRequest("Request Already Send");
                }

                var request = new FriendRequest
                {
                    UserId = friendrequest.UserId,
                    ReceiverUserId = friendrequest.ReceiverUserId,
                    FriendRequestReceived = true,
                    RequestApproval ="REQUESTED"
                    
                };
                
                 await _mainDbContext.FriendRequests.AddAsync(request);

                    await _mainDbContext.SaveChangesAsync();
                    return Ok("Request Sent Successfully");
               
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetFriendRequest")]
        public async Task<IActionResult> GetFriendRequests(string userid)
        {
            try
            {

                if (string.IsNullOrEmpty(userid))
                {
                    return BadRequest("UserID can not be null or empty");
                }

                var result = await _mainDbContext.FriendRequests.Where(x => x.ReceiverUserId ==  userid)
                    .Include(x => x.SenderUserProfile).ToListAsync();
                if (result.Count > 0)
                {
                    return Ok(result);
                }
                else
                {
                    return NotFound("No Request");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPost("RequestApproval")]
        public async Task<IActionResult> FriendRequestApproval(RequestApprovalModelClass requestapproval)
        {
            try
            {
                var request = await _mainDbContext.FriendRequests
                        .Where(x => x.RequestId == requestapproval.RequestId && x.FriendRequestReceived == true).FirstOrDefaultAsync();
                if (request == null)
                {
                    throw new Exception("Friend request not found or already accepted/rejected");

                }
                if (requestapproval.ApprovalAction == "ACCEPTED")
                {
                    
                    
                        var friendListUser1 = new FriendList
                        {
                            FriendshipId = Guid.NewGuid().ToString(),
                            UserId = request.UserId,
                            FriendsUserId = request.ReceiverUserId,
                            FriendSince = DateTime.Now
                        };

                        var friendListUser2 = new FriendList
                        {
                            FriendshipId = Guid.NewGuid().ToString(),
                            UserId = request.ReceiverUserId,
                            FriendsUserId = request.UserId,
                            FriendSince = DateTime.Now
                        };

                        var result1 = await _mainDbContext.Friends.AddAsync(friendListUser1);
                        var result2 = await _mainDbContext.Friends.AddAsync(friendListUser2);

                        if (result1 != null && result2 != null)
                        {
                            _mainDbContext.FriendRequests.Remove(request);
                            await _mainDbContext.SaveChangesAsync();
                            return Ok("Accepted");
                        }
                      
                        else
                        {
                            return BadRequest();
                        }
                        
                   
                   
                }else if(requestapproval.ApprovalAction == "CANCELLED")
                {
                    _mainDbContext.FriendRequests.Remove(request);
                    await _mainDbContext.SaveChangesAsync();
                    return Ok("Requested Cancelled Successfully");
                }
                else
                {
                    return BadRequest();
                }
                

            }
            catch (Exception ex)
            {
                return BadRequest($"{ex.Message}");
            }
        }
    }
}
