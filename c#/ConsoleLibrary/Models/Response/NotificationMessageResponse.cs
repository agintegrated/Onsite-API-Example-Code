using System;
using System.Collections.Generic;
using System.Text;

namespace Onsite_API_Example_Code.Models.Response
{
    public class NotificationMessageResponse
    {
        public object Description { get; set; }
        public int NumberOfFiles { get; set; }
        public int NodeId { get; set; }
        public object DownloadUrl { get; set; }
        public object UserKeys { get; set; }
    }
}
