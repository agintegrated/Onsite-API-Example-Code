using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Onsite_API_Example_Code.Models.Request
{
    public class PostNotificationSnsRequest
    {

        [Required]
        public string ApiKey { get; set; }

        [Required]
        public string SecretKey { get; set; }


        [Required]
        public string TopicArn { get; set; }


        public string Region { get; set; }


        public FdaConfigurationRequest FdaConfiguration { get; set; }


        public RawFileStorageRequest RawFileStorage { get; set; }
    }
}
