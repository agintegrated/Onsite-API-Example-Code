using System;
using System.Collections.Generic;
using System.Text;

namespace Onsite_API_Example_Code.Models.Response
{
    public class S3ConfigResponse
    {
        public string BucketName { get; set; }
        
        public string BucketPath { get; set; }
        
        public string AccessKey { get; set; }
    }
}
