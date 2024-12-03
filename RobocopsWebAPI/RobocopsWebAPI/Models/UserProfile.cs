using System.ComponentModel.DataAnnotations;

namespace RobocopsWebAPI.Models
{
	public class UserProfile
	{
		[Key]
		public string? UserId {  get; set; }
		public string? Email {  get; set; }
		public string? FullName {  get; set; }
		public string? Bio { get; set; }

		public string? ProfilePicURL {  get; set; }
		public DateTime UserCreationDate { get; set; }

		public List<Post>? Posts { get; set; }
		public List<Comment>? Comments { get; set; }
		public List<Like>? Likes { get; set; }

		public List<FriendList>? FriendList { get; set; }
		public List<FriendRequest>? SentFriendRequests { get; set; }
        public List<FriendRequest>? ReceivedFriendRequests { get; set; }

    }
}
