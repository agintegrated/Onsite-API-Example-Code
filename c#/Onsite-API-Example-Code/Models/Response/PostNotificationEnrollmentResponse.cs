using System;
using System.Collections.Generic;
using System.Text;

namespace Onsite_API_Example_Code.Models.Response
{
    public class Message
    {
        public object Description { get; set; }
        public int NumberOfFiles { get; set; }
        public int NodeId { get; set; }
        public object DownloadUrl { get; set; }
        public object UserKeys { get; set; }
    }

    public class Config
    {
        public string Endpoint { get; set; }
        public Message Message { get; set; }
    }

    public class FdaConfig
    {
        public object OutputAdapter { get; set; }
        public object OutputPreferences { get; set; }
    }

    public class DataSns
    {
        public int NotificationID { get; set; }
        public string NotificationType { get; set; }
        public DateTime CreatedTime { get; set; }
        public Config Config { get; set; }
        public FdaConfig FdaConfig { get; set; }
        public object S3Config { get; set; }
    }

    public class PostNotificationEnrollmentResponse
    {
        public int TelematicsNodeId { get; set; }
        public bool Success { get; set; }
        public int Count { get; set; }
        public List<DataSns> Data { get; set; }
        public string Message { get; set; }
    }
}
