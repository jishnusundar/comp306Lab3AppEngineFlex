using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Google.Cloud.Storage.V1;
using Microsoft.AspNetCore.Http;

namespace Lab3AppEngineFlex
{
    public class ImageUploader
    {

        private readonly StorageClient _storageClient;

        public ImageUploader()
        {
           
            // [START storageclient]
            _storageClient = StorageClient.Create();
            // [END storageclient]
        }

        // [START uploadimage]
        public async Task<String> UploadImage(IFormFile image, string Fname)
        {
            var imageAcl = PredefinedObjectAcl.PublicRead;

            var imageObject = await _storageClient.UploadObjectAsync(
                bucket: "white-resolver-179619.appspot.com",
                objectName: "user-uploads/"+Fname,
                contentType: image.ContentType,
                source: image.OpenReadStream(),
                options: new UploadObjectOptions { PredefinedAcl = imageAcl }
            );

            return imageObject.MediaLink;
        }
        // [END uploadimage]

        //public async Task DeleteUploadedImage(long id)
        //{
        //    try
        //    {
        //        await _storageClient.DeleteObjectAsync(_bucketName, id.ToString());
        //    }
        //    catch (Google.GoogleApiException exception)
        //    {
        //        // A 404 error is ok.  The image is not stored in cloud storage.
        //        if (exception.Error.Code != 404)
        //            throw;
        //    }
        //}
    }
}

