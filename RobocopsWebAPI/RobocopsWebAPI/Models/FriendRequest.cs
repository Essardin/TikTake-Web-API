using System.ComponentModel.DataAnnotations;

namespace RobocopsWebAPI.Models
{
    public class FriendRequest
    {
        [Key]
        public int RequestId { get; set; }

        public string? UserId {  get; set; }  // user id of user who send the request

        public string? ReceiverUserId { get; set; }   //  user id of whome the request was sent

        public bool FriendRequestReceived { get; set; } = false;

        public string? RequestApproval { get; set; }  //Approved , Requested, Cancelled
        public UserProfile SenderUserProfile { get; set; }
        public UserProfile ReceiverUserProfile { get; set; }

    }
}
