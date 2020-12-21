using System;
using System.Collections.Generic;
using System.Text;

namespace Onsite_API_Example_Code.Models.Response
{
    public class GetFileDownloadResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public string TrackingCode { get; set; }
        public string Status { get; set; }
        public bool ExportStarted { get; set; }
    }
}
