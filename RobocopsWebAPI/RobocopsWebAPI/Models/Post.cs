using System.ComponentModel.DataAnnotations;

namespace RobocopsWebAPI.Models
{
	public class Post
	{
		[Key]
		public string? PostId {  get; set; }    //Post id of the post
		public string? UserId {  get; set; }   // userid of user who puts this post
		public string? PostImageURL {  get; set; }
		public string? PostCaption {  get; set; }
		
		public DateTime PostTimeStamp { get; set; }
		public bool? IsPinned { get; set; } = false;
		public List<Comment> Comments { get; set; }
		public List<Like> Likes { get; set; }
		public UserProfile UserProfile { get; set; }

	}
}
