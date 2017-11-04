using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab3AppEngineFlex
{
    public class CloudStorageOptions
    {
        public string BucketName { get; set; }
        public string ObjectName { get; set; } = "sample.txt";
    }
}
