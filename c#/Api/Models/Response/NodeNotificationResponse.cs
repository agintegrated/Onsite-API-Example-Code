using System;
using System.Collections.Generic;
using System.Text;

namespace Onsite_API_Example_Code.Models.Response
{
    public class NodeNotificationResponse
    {
        public int TelematicsNodeId { get; set; }
        
        public bool Success { get; set; }
        
        public int Count { get; set; }
        
        public List<NotificationResponse> Data { get; set; }
        
        public string Message { get; set; }
    }
}
