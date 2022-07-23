using Domain.Models;
using Firebase.Auth;
using Firebase.Storage;
using Infrastructure.DTO.ImageDTOs;
using Infrastructure.Setting;
using Microsoft.Extensions.Configuration;
using SixLabors.ImageSharp.Formats;

namespace Intergration.FireStoreService
{
    public class FireStorageService : IFireStorageService
    {
        //private readonly FirebaseAuthLink _authLink;
        private readonly FirebaseStorage _firebaseStorage;
        private readonly FirebaseAuthLink? _authLink;

        public FireStorageService(IConfiguration configuration)
        {
            string apiKey = configuration["FireStore:apiKey"];
            string email = configuration["FireStore:email"];
            string password = configuration["FireStore:password"];
            string bucket = configuration["FireStore:storageBucket"];
            var e = FireStoreSettings.Email;

            var auth = new FirebaseAuthProvider(
                new FirebaseConfig(
                    apiKey
                    ));

            //auth.RefreshAuthAsync()
           
            if(FireBaseTokenClass.Exp == null || FireBaseTokenClass.Exp < DateTime.UtcNow)
            {
                _authLink = auth.SignInWithEmailAndPasswordAsync(email, password).Result;
                FireBaseTokenClass.Token = _authLink.FirebaseToken;
                FireBaseTokenClass.Exp = DateTime.UtcNow.AddSeconds(_authLink.ExpiresIn);

            }
            /*_authLink.RefreshUserDetails()

            auth.RefreshAuthAsync(auth.)*/
     
            _firebaseStorage = new FirebaseStorage(
                bucket,
                new FirebaseStorageOptions
                {
                    AuthTokenAsyncFactory = () => Task.FromResult(FireBaseTokenClass.Token),
                    ThrowOnCancel = true,
                });
            

        }

        /*private FireStorageService(FirebaseStorage firebaseStorage)
        {
            _firebaseStorage = firebaseStorage;
        }

        public async Task<FireStorageService> Initialize(IConfiguration configuration)
        {
            if (Instance != null)
                return Instance;
             

            string apiKey = configuration["FireStore:apiKey"];
            string email = configuration["FireStore:email"];
            string password = configuration["FireStore:password"];
            string bucket = configuration["FireStore:storageBucket"];

            var auth = new FirebaseAuthProvider(
                new FirebaseConfig(
                    apiKey
                    ));

            var authLink = await auth.SignInWithEmailAndPasswordAsync(email, password);

            var firebaseStorageObject = new FirebaseStorage(
                bucket,
                new FirebaseStorageOptions
                {
                    AuthTokenAsyncFactory = () => Task.FromResult(authLink.FirebaseToken),
                    ThrowOnCancel = true,
                });

            Instance =  new FireStorageService(firebaseStorageObject);

            return Instance;


        }*/

        public async Task<Response<Image>> UploadImage(CreateImageDTO imageDTO)
        {

            byte[] bytes = Convert.FromBase64String(imageDTO.Blob);

            // 1mb = 1 000 000 bytes
            if (bytes.Length > 5000000)
                return new Response<Image>(null, true, "image cannot exceed 5MBs");


            var stream = new MemoryStream(bytes);

            IImageFormat format = SixLabors.ImageSharp.Image.DetectFormat(bytes);

            string? type = MimeType(format.Name);

            if (type == null)
                return new Response<Image>(null, true, "Image may only be jpg,png or gif");

            string name = $"{imageDTO.Name}.{type}";

            var response = await _firebaseStorage
                                .Child(name)
                                .PutAsync(stream);

            var image = new Image
            {
                Url = response,
                Name = name
            };

            // await the task to wait until upload completes and get the download url
            return new Response<Image>(image, false, "complete");
        }

        public async Task DeleteImage(string name)
        {
            await _firebaseStorage.Child(name).DeleteAsync();
        }

        private string? MimeType(string format)
        {
            if (format == "JPEG")
                return "jpg";

            if (format == "PNG")
                return "png";

            if (format == "GIF")
                return "gif";

            return null;

        }

    }

    public static class FireBaseTokenClass
    {
        public static string Token { get; set; } = string.Empty;

        public static DateTime? Exp {get; set; } = null!;
    }
}
