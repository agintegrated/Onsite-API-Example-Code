using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Onsite_API_Example_Code.Models.Request
{
    public class FileRequest
    {

        [Required]
        public List<int> NotificationIds { get; set; }

        public FdaConfigurationRequest FdaConfiguration { get; set; }

        public RawFileStorageRequest RawFileStorage { get; set; }
    }
}
