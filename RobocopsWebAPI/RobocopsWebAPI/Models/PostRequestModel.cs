namespace RobocopsWebAPI.Models
{
    public class PostRequestModel
    {
        public string userid {  get; set; }
        public IFormFile postImage { get; set; }
        public string postCaption {  get; set; }
    }
}
