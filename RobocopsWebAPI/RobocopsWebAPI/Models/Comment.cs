using System.ComponentModel.DataAnnotations;

namespace RobocopsWebAPI.Models
{
	public class Comment
	{
		[Key]
		public string? CommentId {  get; set; }
		public string? PostId {  get; set; }    //post id of the post on which comment is pushed
		public string? UserId {  get; set; }    //user id of the user who comments
		public string? CommentText { get; set; }
		public DateTime CommentTimeStamp { get; set; }

		
		public UserProfile UserProfile { get; set; }
	}
}
