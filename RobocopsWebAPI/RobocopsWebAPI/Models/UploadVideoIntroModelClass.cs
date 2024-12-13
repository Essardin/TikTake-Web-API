namespace RobocopsWebAPI.Models
{
    public class UploadVideoIntroModelClass
    {
        public string? userId {  get; set; }
        public string? videoPublicId {  get; set; }
        public IFormFile? videoFile {  get; set; }
    }
}
