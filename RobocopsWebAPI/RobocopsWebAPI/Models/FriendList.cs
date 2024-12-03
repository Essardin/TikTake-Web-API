using System.ComponentModel.DataAnnotations;

namespace RobocopsWebAPI.Models
{
	public class FriendList
	{
		[Key]
		public string? FriendshipId { get; set; }
		public string? UserId {  get; set; }        // user id of the useritself
		public string? FriendsUserId {  get; set; }   // user id of friend
		public DateTime? FriendSince { get; set; }
	

        public UserProfile UserProfile { get; set; }
	}
}
