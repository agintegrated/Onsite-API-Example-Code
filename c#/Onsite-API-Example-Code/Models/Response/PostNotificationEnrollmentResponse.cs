using System;
using System.Collections.Generic;
using System.Text;

namespace Onsite_API_Example_Code.Models.Response
{
    public class PostNotificationEnrollmentResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public int TelematicsNodeId { get; set; }
        public int Count { get; set; }
        public List<NotificationDataSnsResponse> Data { get; set; }
    }
}
