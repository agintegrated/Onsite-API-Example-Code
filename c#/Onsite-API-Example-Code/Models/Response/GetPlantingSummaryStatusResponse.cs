using System;
using System.Collections.Generic;
using System.Text;

namespace Onsite_API_Example_Code.Models.Response
{
    public class GetPlantingSummaryStatusResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public string Status { get; set; }
        public string TrackingCode { get; set; }
    }
}
