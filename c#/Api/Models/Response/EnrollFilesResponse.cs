using System;
using System.Collections.Generic;
using System.Text;

namespace Onsite_API_Example_Code.Models.Response
{
    public class EnrollFilesResponse
    {
        public EnrollFilesResponse()
        {
            EnrollmentSuccess = new List<NotificationResponse>();
            EnrollmentFailed = new List<NotificationResponse>();
            Message = "";
        }

        public bool Success { get; set; }

        public string Message { get; set; }

        public List<NotificationResponse> EnrollmentSuccess { get; set; }

        public List<NotificationResponse> EnrollmentFailed { get; set; }
    }
}
