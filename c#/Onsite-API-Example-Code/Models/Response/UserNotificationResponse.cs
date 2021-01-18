using System;
using System.Collections.Generic;
using System.Text;

namespace Onsite_API_Example_Code.Models.Response
{
    public class UserNotificationResponse
    {
        public bool Success { get; set; }
        
        public string Message { get; set; }
        
        public int Count { get; set; }
        
        public List<NodeNotificationResponse> Data { get; set; }
    }
}
