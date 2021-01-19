using System;
using System.Collections.Generic;
using System.Text;

namespace Onsite_API_Example_Code.Models.Response
{
    public class NotificationResponse
    {
        public int NotificationID { get; set; }

        public string NotificationType { get; set; }

        public DateTime CreatedTime { get; set; }

        public NotificationConfigResponse Config { get; set; }

        public FdaConfiguration FdaConfig { get; set; }

        public S3ConfigResponse S3Config { get; set; }
    }
}
