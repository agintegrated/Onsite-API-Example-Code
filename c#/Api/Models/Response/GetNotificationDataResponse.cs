using System;
using System.Collections.Generic;
using System.Text;

namespace Onsite_API_Example_Code.Models.Response
{
    public class GetNotificationDataResponse
    {
        public int NotificationID { get; set; }
        public string NotificationType { get; set; }
        public DateTime CreatedTime { get; set; }
        public NotificationConfigResponse Config { get; set; }
        public NotificationFdaConfigResponse FdaConfig { get; set; }
        public NotificationS3ConfigResponse S3Config { get; set; }
    }
}
