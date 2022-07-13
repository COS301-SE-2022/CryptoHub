using Domain.Models;
using Firebase.Auth;
using Firebase.Storage;
using Infrastructure.DTO.ImageDTOs;
using Microsoft.Extensions.Configuration;
using SixLabors.ImageSharp.Formats;

namespace Intergration.FireStoreService
{
    public class FireStorageService : IFireStorageService
    {
        //private readonly FirebaseAuthLink _authLink;
        private readonly FirebaseStorage _firebaseStorage;

        public FireStorageService(IConfiguration configuration)
        {
            string apiKey = configuration["FireStore:apiKey"];
            string email = configuration["FireStore:email"];
            string password = configuration["FireStore:password"];
            string bucket = configuration["FireStore:storageBucket"];

            var auth = new FirebaseAuthProvider(
                new FirebaseConfig(
                    configuration["FireStore:apiKey"]
                    ));

            var _authLink = auth.SignInWithEmailAndPasswordAsync(email,password).Result;

            _firebaseStorage = new FirebaseStorage(
                bucket,
                new FirebaseStorageOptions
                {
                    AuthTokenAsyncFactory = () => Task.FromResult(_authLink.FirebaseToken),
                    ThrowOnCancel = true,
                });

        }

        public async Task<Response<string>> UploadImage(ImageDTO imageDTO)
        {

            byte[] bytes = Convert.FromBase64String(imageDTO.Blob);

            // 1mb = 1 000 000 bytes
            if(bytes.Length > 5000000)
                return new Response<string>(null, true, "image cannot exceed 5MBs");


            var stream = new MemoryStream(bytes);

            IImageFormat format = SixLabors.ImageSharp.Image.DetectFormat(bytes);

            string? type = MimeType(format.Name);

            if(type==null)
                return new Response<string>(null, true, "Image may only be jpg,png or gif");



            var response = await _firebaseStorage
                                .Child(imageDTO.Name)
                                .PutAsync(stream);


            // await the task to wait until upload completes and get the download url
            return new Response<string>(response, false, "complete");
        }

        private string? MimeType(string format)
        {
            if (format == "JPG")
                return "jpg";

            if (format == "PNG")
                return "png";

            if (format == "GIF")
                return "gif";

            return null;

        }
    }
}
