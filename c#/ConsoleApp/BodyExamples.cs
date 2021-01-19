using Onsite_API_Example_Code.Models.Request;
using System;
using System.Collections.Generic;
using System.Text;

namespace Onsite_API_Example_Code
{
    public class BodyExamples
    {
        /// <summary>
        /// Example post classes used for the various endpoints
        /// </summary>
       // static PostTelematicsNodeFieldBoundaryRequest postTelematicsNodeFieldBoundary = new PostTelematicsNodeFieldBoundaryRequest { type = "FeatureCollection", features = new List<FeatureRequest> features};
        static RawFileStorage rawFileStorage = new RawFileStorage { AccessKey = "", BucketName = "", Region = "", SecretKey = "", BucketPath = "" };
        static FileRequest fileRequest = new FileRequest { NotificationIds = { }, RawFileStorage = new RawFileStorage()};
        static PostRequest postRequest = new PostRequest { Url = "" };
        static PostPlantingSummaryRequest postPlantingSummaryRequest = new PostPlantingSummaryRequest { StartDate = DateTime.Parse("2020-04-06T15:34:36.600Z"), EndDate = DateTime.Parse("2020-04-06T15:34:36.600Z"), CallbackURL = "" };
        static SNSRequest postNotificationSnsRequest = new SNSRequest { ApiKey = "", SecretKey = "", TopicArn = "", Region = "" };
    }
}
