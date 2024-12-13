namespace RobocopsWebAPI.Models
{
    public class EditProfilePictureRequestModel
    {
        public string userid { get; set; }
        public IFormFile ProfilePic { get; set; }
        public string? ProfilePicPublicId { get; set; }
    }
}
