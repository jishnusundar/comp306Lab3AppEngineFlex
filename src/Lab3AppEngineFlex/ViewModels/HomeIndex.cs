using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Lab3AppEngineFlex.ViewModels
{
    public class HomeIndex
    {
        public bool MissingBucketName { get; set; } = false;
        public string Content { get; set; } = "";
        public bool SavedNewContent { get; set; } = false;
        public string MediaLink { get; set; } = "";
    }
}
