namespace RobocopsWebAPI.Models
{
    public class CommentsModelClass
    {
        public string? PostId { get; set; }    //post id of the post on which comment is pushed
        public string? UserId { get; set; }    //user id of the user who comments
        public string? CommentText { get; set; }
    }
}
