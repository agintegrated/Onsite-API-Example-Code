using Onsite_API_Example_Code.Models.Response;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Onsite_API_Example_Code.Models.Request
{
    public class FileRequest
    {

        [Required]
        public List<int> NotificationIds { get; set; }

        public FdaConfiguration FdaConfiguration { get; set; }

        public RawFileStorage RawFileStorage { get; set; }
    }
}
