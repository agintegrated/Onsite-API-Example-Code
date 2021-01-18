using System.ComponentModel.DataAnnotations;

namespace Onsite_API_Example_Code.Models.Request
{
    public class PostRequest
    {
        [Required]
        public string Url { get; set; }

        public FdaConfigurationRequest FdaConfiguration { get; set; }

        public RawFileStorageRequest RawFileStorage { get; set; }
    }
}
