namespace RobocopsWebAPI.Models
{
    public class EditProfileRequestModel
    {
        public string userid {  get; set; }
        public string fullName { get; set; }
        public string bio { get; set; }
        public IFormFile ProfilePic { get; set; }
    }
}
