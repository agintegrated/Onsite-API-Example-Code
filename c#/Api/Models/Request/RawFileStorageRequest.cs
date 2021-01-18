using System.ComponentModel.DataAnnotations;

namespace Onsite_API_Example_Code.Models.Request
{
    public class RawFileStorageRequest
    {

        [Required]
        public string BucketName { get; set; }

        [Required]
        public string BucketPath { get; set; }

        [Required]
        public string AccessKey { get; set; }

        [Required]
        public string SecretKey { get; set; }

        public string Region { get; set; }
    }
}
