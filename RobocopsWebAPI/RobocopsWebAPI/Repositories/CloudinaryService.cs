using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace RobocopsWebAPI.Repository
{

    public class CloudinaryService
    {
        private readonly Cloudinary _cloudinary;

        public CloudinaryService(IConfiguration configuration)
        {
            var cloudName = Environment.GetEnvironmentVariable("CLOUDINARY_CLOUD_NAME");
            var apiKey = Environment.GetEnvironmentVariable("CLOUDINARY_API_KEY");
            var apiSecret = Environment.GetEnvironmentVariable("CLOUDINARY_API_SECRET");
            var account = new Account(cloudName, apiKey, apiSecret);
            _cloudinary = new Cloudinary(account);
        }

        public async Task<ImageUploadResult> UploadImageAsync( string fileName, Stream fileStream, string folderName)
        {

            try
            {
            

                var uploadParams = new ImageUploadParams()
                {
                    File = new FileDescription(fileName, fileStream),
                    
                    UseFilename = true,
                    UniqueFilename = true,
                    Overwrite = false,
                    Folder = folderName

                };

                var uploadResult = await _cloudinary.UploadAsync(uploadParams);
                if (uploadResult.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    return  (uploadResult);
                }
                else
                {
                    throw new Exception  ("Error uploading image to Cloudinary.");

                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<VideoUploadResult> UploadVideoAsync(string fileName, Stream videoFile, string folderName, string existingPublicId)
        {

            string publicId = "";
                 publicId = string.IsNullOrEmpty(existingPublicId) 
                ? "user_intro_vid"
                : existingPublicId;

            if (publicId.StartsWith(folderName))
            {
                int slashIndex = publicId.IndexOf('/');
                if (slashIndex >= 0)
                {
                    publicId = publicId.Substring(slashIndex + 1);
                }
            }

            var uploadParams = new VideoUploadParams()
            {
                File = new FileDescription(fileName , videoFile),
                
                 PublicId = publicId,
                Overwrite = true,
                Folder = folderName,
                UniqueFilename = false,
                

            };
            var uploadResult = await _cloudinary.UploadAsync(uploadParams);

            if(uploadResult.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return uploadResult;
            }
            else
            {
                throw new Exception ("Video not uploaded");
            }
        }

        public async Task<DeletionResult> DeleteImageAsync(string publicId)
        {
            if (string.IsNullOrEmpty(publicId))
            {
                throw new ArgumentException("Public ID cannot be null or empty.", nameof(publicId));
               

            }
            try
            {
                var destroyParams = new DeletionParams(publicId)
                {
                    ResourceType = ResourceType.Image // Specify resource type
                };
                var result = await _cloudinary.DestroyAsync(destroyParams);
               
                    return result;


                //return new DeletionResult
                //{
                //    Result = "Failed to delete the image, as public ID seems null or empty",

                //};


            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while deleting the image: {ex.Message}", ex);
            }
        }

       
    }
}
