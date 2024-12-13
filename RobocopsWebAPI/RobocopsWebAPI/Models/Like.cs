namespace RobocopsWebAPI.Models
{
	public class Like
	{
		public int Id { get; set; }
		public string? UserId {  get; set; }  // userid of the person who likes
		public string? PostId {  get; set; }   // post id of post on which like is pushed
		
		public UserProfile UserProfile { get; set; }
	}
}
