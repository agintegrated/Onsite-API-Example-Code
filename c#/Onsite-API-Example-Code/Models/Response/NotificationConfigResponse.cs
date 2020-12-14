using System;
using System.Collections.Generic;
using System.Text;

namespace Onsite_API_Example_Code.Models.Response
{
    public class NotificationConfigResponse
    {
        public string Endpoint { get; set; }
        public NotificationMessageResponse Message { get; set; }
    }
}
