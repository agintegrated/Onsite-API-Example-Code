using Onsite_API_Example_Code;
using Onsite_API_Example_Code.Models.Request;
using Onsite_API_Example_Code.Models.Response;
using System;
using System.Threading.Tasks;

namespace OnsiteAPIExample
{
    
    static class Program
    {
        static string  userKey    = "";
        static string  publicKey  = "";
        static string  privateKey = "";

        static int telematicsNode;

        /// <summary>
        /// Example post classes used for the various endpoints
        /// </summary>
        //  static PostTelematicsNodeFieldBoundaryRequest postTelematicsNodeFieldBoundary = new PostTelematicsNodeFieldBoundaryRequest { type = "FeatureCollection", features = };
        //  static RawFileStorageRequest rawFileStorage = new RawFileStorageRequest { AccessKey = "", BucketName = "", Region = "", SecretKey = "", BucketPath = "" };
        //  static FileRequest fileRequest = new FileRequest { NotificationIds = , RawFileStorage = };
        //  static PostRequest postRequest = new PostRequest { Url = "" };
        //  static PostPlantingSummaryRequest postPlantingSummaryRequest = new PostPlantingSummaryRequest { StartDate = DateTime.Parse("2020-04-06T15:34:36.600Z"), EndDate = DateTime.Parse("2020-04-06T15:34:36.600Z"), CallbackURL = "" };
        //  static PostNotificationSnsRequest postNotificationSnsRequest = new PostNotificationSnsRequest { ApiKey = "", SecretKey = "", TopicArn = "", Region = "" };

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static async Task Main()
        {
            

        }

        public static async Task<GetEquipmentRegisterResponse> RegisterExample()
        {
            TelematicsV2 telematicsV2 = new TelematicsV2(publicKey, privateKey, userKey);
            GetEquipmentRegisterResponse response = await telematicsV2.GetEquipmentRegister();

            return response;
        }

        public static async Task<GetTreeResponse> GetTreeExample()
        {
            TelematicsV2 telematicsV2 = new TelematicsV2(publicKey, privateKey, userKey);
            GetTreeResponse response = await telematicsV2.GetTree();

            return response;
        }
        /*public static async Task<PostSendFileResponse> UploadFileExample(string fileName, string filePath)
        {
            TelematicsV2 telematicsV2 = new TelematicsV2(publicKey, privateKey, userKey);
            PostSendFileResponse response = await telematicsV2.PostSendFiles(telematicsNode, filePath, fileName);

            return response;
        }*/
        public static bool DownloadFileExample(string fileId)
        {
            TelematicsV2 telematicsV2 = new TelematicsV2(publicKey, privateKey, userKey);
            telematicsV2.GetDownloadFilesWithoutConversion(telematicsNode, fileId);

            return true;
        }

        public static async Task<PostNotificationEnrollmentResponse> EnrollmentSnsExample(int telematicsNode, PostNotificationSnsRequest postNotificationSnsRequest)
        {
            TelematicsV2 telematicsV2 = new TelematicsV2(publicKey, privateKey, userKey);
            PostNotificationEnrollmentResponse response = await telematicsV2.PostNotificationEnrollmentSns(telematicsNode, postNotificationSnsRequest);

            return response;
        }

        public static async Task<PostNotificationEnrollmentResponse> EnrollmentExample(int telematicsNode, PostRequest postRequest)
        {
            TelematicsV2 telematicsV2 = new TelematicsV2(publicKey, privateKey, userKey);
            PostNotificationEnrollmentResponse response = await telematicsV2.PostNotificationEnrollment(telematicsNode, postRequest);

            return response;
        }

        public static async Task<SuccessMessageResponse> DeleteEnrollmentExample(int nodeId)
        {
            TelematicsV2 telematicsV2 = new TelematicsV2(publicKey, privateKey, userKey);
            SuccessMessageResponse response = await telematicsV2.DeleteNotificationDisenrollment(nodeId);

            return response;
        }

        public static async Task<GetNotificationResponse> GetNotificationUserExample()
        {
            TelematicsV2 telematicsV2 = new TelematicsV2(publicKey, privateKey, userKey);
            GetNotificationResponse response = await telematicsV2.GetNotificationUser();

            return response;
        }

        public static async Task<GetNotificationResponse> GetNotificationUserForNodeExample(int nodeId)
        {
            TelematicsV2 telematicsV2 = new TelematicsV2(publicKey, privateKey, userKey);
            GetNotificationResponse response = await telematicsV2.GetNotificationUserForNode(nodeId);

            return response;
        }

        public static async Task<GetNotificationResponse> GetNotificationUserForApiPartnerExample()
        {
            TelematicsV2 telematicsV2 = new TelematicsV2(publicKey, privateKey, userKey);
            GetNotificationResponse response = await telematicsV2.GetNotificationForApiPartner();

            return response;
        }

        public static async Task<GetNotificationResponse> GetNotificationUserForApiPartnerByNodeExample(int nodeId)
        {
            TelematicsV2 telematicsV2 = new TelematicsV2(publicKey, privateKey, userKey);
            GetNotificationResponse response = await telematicsV2.GetNotificationForApiPartnerByNode(nodeId);

            return response;
        }

        public static async Task<PostNotificationEnrollmentResponse> PostNotificationFile(FileRequest fileRequest)
        {
            TelematicsV2 telematicsV2 = new TelematicsV2(publicKey, privateKey, userKey);
            PostNotificationEnrollmentResponse response = await telematicsV2.PostNotificationFile(fileRequest);

            return response;
        }

        public static async Task<TelematicsNodeResponse> GetTelematicsNode(int nodeId)
        {
            TelematicsV2 telematicsV2 = new TelematicsV2(publicKey, privateKey, userKey);
            TelematicsNodeResponse response = await telematicsV2.GetTelematicsNode(nodeId);

            return response;
        }

        public static async Task<SuccessMessageResponse> DeleteTelematicsNode(int nodeId)
        {
            TelematicsV2 telematicsV2 = new TelematicsV2(publicKey, privateKey, userKey);
            SuccessMessageResponse response = await telematicsV2.DeleteTelematicsNode(nodeId);

            return response;
        }

        public static async Task<GetTelematicsNodeFilesResponse> GetTelematicsNodeFilesExample(int nodeId)
        {
            TelematicsV2 telematicsV2 = new TelematicsV2(publicKey, privateKey, userKey);
            GetTelematicsNodeFilesResponse response = await telematicsV2.GetTelematicsNodeFiles(nodeId);

            return response;
        }

        public static async Task<GetFileStatusResponse> GetFileStatusExample(string trackingCode)
        {
            TelematicsV2 telematicsV2 = new TelematicsV2(publicKey, privateKey, userKey);
            GetFileStatusResponse response = await telematicsV2.GetFileStatus(trackingCode);

            return response;
        }

        public static async Task<GetFieldResponse> GetTelematicsNodeFieldsExample(int nodeId)
        {
            TelematicsV2 telematicsV2 = new TelematicsV2(publicKey, privateKey, userKey);
            GetFieldResponse response = await telematicsV2.GetTelematicsNodeFields(nodeId);

            return response;
        }

        public static async Task<GetFieldBoundaryResponse> GetTelematicsNodeFieldBoundaryExample(int nodeId)
        {
            TelematicsV2 telematicsV2 = new TelematicsV2(publicKey, privateKey, userKey);
            GetFieldBoundaryResponse response = await telematicsV2.GetTelematicsNodeFieldBoundary(nodeId);

            return response;
        }

        public static async Task<SuccessMessageResponse> PostTelematicsNodeFieldBoundary(int nodeId, PostTelematicsNodeFieldBoundaryRequest postTelematicsNodeFieldBoundaryRequest)
        {
            TelematicsV2 telematicsV2 = new TelematicsV2(publicKey, privateKey, userKey);
            SuccessMessageResponse response = await telematicsV2.PostTelematicsNodeFieldBoundary(nodeId, postTelematicsNodeFieldBoundaryRequest);

            return response;
        }

        public static async Task<PostPlantingSummaryResponse> PostTelematicsNodePlantingSummaryExample(int nodeId, PostPlantingSummaryRequest postPlantingSummaryRequest)
        {
            TelematicsV2 telematicsV2 = new TelematicsV2(publicKey, privateKey, userKey);
            PostPlantingSummaryResponse response = await telematicsV2.PostTelematicsNodePlantingSummary(nodeId, postPlantingSummaryRequest);

            return response;
        }

        public static async Task<GetPlantingSummaryStatusResponse> GetTelematicsNodePlantingSummaryStatusExample(int nodeId, string trackingCode)
        {
            TelematicsV2 telematicsV2 = new TelematicsV2(publicKey, privateKey, userKey);
            GetPlantingSummaryStatusResponse response = await telematicsV2.GetTelematicsNodePlantingSummaryStatus(nodeId, trackingCode);

            return response;
        }
    }
}
