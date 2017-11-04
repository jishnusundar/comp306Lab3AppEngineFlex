using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Google.Cloud.Storage.V1;
using Google;
using Microsoft.Extensions.Options;
using Lab3AppEngineFlex.ViewModels;
using System.IO;
using System.Text;
using Microsoft.AspNetCore.Http;


// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Lab3AppEngineFlex.Controllers
{
    public class GalleryController : Controller
    {
        // Contains the bucket name and object name
        readonly CloudStorageOptions _options;

        // The Google Cloud Storage client.
        readonly StorageClient _storage;

        private readonly ImageUploader _imageUploader = new ImageUploader();

        public GalleryController(IOptions<CloudStorageOptions> options)
        {
            _options = options.Value;
            _storage = StorageClient.Create();
        }

        [HttpGet]
        public IActionResult Index()
        {
           
            return View();
        }


        //[HttpPost]
        //public async Task<IActionResult> Index(Form sendForm)
        //{
        //    var model = new HomeIndex();
        //    // Take the content uploaded in the form and upload it to
        //    // Google Cloud Storage.
        //    await _storage.UploadObjectAsync(
        //        _options.BucketName, _options.ObjectName, "text/plain",
        //        new MemoryStream(Encoding.UTF8.GetBytes(sendForm.Content)));
        //    model.Content = sendForm.Content;
        //    model.SavedNewContent = true;
        //    var storageObject =
        //        await _storage.GetObjectAsync(_options.BucketName, _options.ObjectName);
        //    model.MediaLink = storageObject.MediaLink;
        //    return View(model);
        //}

        [HttpPost]
        public async Task<IActionResult> Index(IList<IFormFile> files)
        {
            IFormFile file = files.ElementAt(0);
            
            string linkToImage = await _imageUploader.UploadImage(file, file.FileName);
           
            return View();
        }
    }
}
