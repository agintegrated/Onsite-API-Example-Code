using System;
using System.Collections.Generic;
using System.Text;

namespace Onsite_API_Example_Code.Models.Response
{
    public class PostSendFileResponse
    {
        public string FileName { get; set; }
        public string Status { get; set; }
        public int FileSize { get; set; }
        public string TrackingCode { get; set; }
        public string Message { get; set; }
    }
}
