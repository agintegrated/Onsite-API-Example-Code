using Onsite_API_Example_Code.Models.Response;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Onsite_API_Example_Code.Models.Request
{
    public class SNSRequest
    {

        [Required]
        public string ApiKey { get; set; }

        [Required]
        public string SecretKey { get; set; }


        [Required]
        public string TopicArn { get; set; }


        public string Region { get; set; }


        public FdaConfiguration FdaConfiguration { get; set; }


        public RawFileStorage RawFileStorage { get; set; }
    }
}
