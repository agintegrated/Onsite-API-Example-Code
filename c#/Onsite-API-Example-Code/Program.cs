using Onsite_API_Example_Code;
using Onsite_API_Example_Code.Models.Request;
using Onsite_API_Example_Code.Models.Response;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace OnsiteAPIExample
{
    static class Program
    {
        const string userKey = "a7ffe6fb-d55b-4167-991a-4b0c05bbe7eb";
        const string publicKey = "86bc1a57-8b51-4f27-9f9b-0f31a9c5b982";
        const string privateKey = "429ff61d-a60a-4e13-bd21-92777ba4528d";
        public static TelematicsV2 DataExchangeAPI;


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
            DataExchangeAPI = new TelematicsV2(publicKey, privateKey, userKey);
        }

        public static async Task RegisterExample()
        {
            GetEquipmentRegisterResponse response = await DataExchangeAPI.GetEquipmentRegister();
            if (response.Success)
            {
                Process.Start(response.Url);
            }

            await Task.CompletedTask;
        }

        public static async Task GetTreeExample()
        {
            GetTreeResponse response = await DataExchangeAPI.GetTree();
            if(!response.Success)
            {
                throw new Exception("Did not successfully get the Tree from DE2");
            }

            await Task.CompletedTask;
        }

        public static async Task UploadFileExample()
        {
            string testFilePath = Directory.GetCurrentDirectory();
            FileInfo file = new FileInfo(Path.Combine(testFilePath, "Files/ExampleAsApplied.zip"));
            if(!file.Exists)
            {
                throw new Exception("Missing file");
            }

            GetTreeResponse tree = await DataExchangeAPI.GetTree();
            if (tree.Success)
            {
                const string nodeNameToSearchFor = "Tim Butcher Farms";
                TelematicsNodeResponse nodeToSendFileTo = FindNode(tree.Data, nodeNameToSearchFor);

                PostSendFileResponse response = await DataExchangeAPI.PostSendFiles(nodeToSendFileTo.TelematicsNodeID, file);
                if (response.Status.Equals("Failed"))
                {
                    throw new Exception("Did not send the file correctly");
                }
            }

            await Task.CompletedTask;
        }

        private static TelematicsNodeResponse FindNode(List<TelematicsNodeResponse> nodeCollection, string name)
        {
            foreach (TelematicsNodeResponse node in nodeCollection)
            {
                if (node.Name.Equals(name, StringComparison.OrdinalIgnoreCase))
                {
                    return node;
                }

                if(node.Children != null)
                {
                    TelematicsNodeResponse matchingChildNode = FindNode(node.Children, name);
                    if(matchingChildNode != null)
                    {
                        return matchingChildNode;
                    }
                }
            }

            return null;
        }

        public static async Task DownloadFileExample()
        {
            // first we need to locate a node 
            const int telematicsNodeID = 0;

            // Then we can get that node's file listing
            GetTelematicsNodeFilesResponse fileResponse = await DataExchangeAPI.GetTelematicsNodeFiles(telematicsNodeID);
            if (fileResponse.Success)
            {
                // Now we can download that file using the ID
                string fileID = fileResponse.Data[0].ID;
                FileInfo fileInfo = await DataExchangeAPI.GetDownloadFilesWithoutConversion(telematicsNodeID, fileID);
                if (fileInfo.Exists)
                {
                    ProcessStartInfo startInfo = new ProcessStartInfo
                    {
                        Arguments = fileInfo.Directory.FullName,
                        FileName = fileInfo.Name
                    };

                    Process.Start(startInfo);
                }
            }

            await Task.CompletedTask;
        }

        public static async Task<PostNotificationEnrollmentResponse> EnrollmentSnsExample(int telematicsNode, PostNotificationSnsRequest postNotificationSnsRequest)
        {
            PostNotificationEnrollmentResponse response = await DataExchangeAPI.PostNotificationEnrollmentSns(telematicsNode, postNotificationSnsRequest);

            return response;
        }

        public static async Task<PostNotificationEnrollmentResponse> EnrollmentExample(int telematicsNode, PostRequest postRequest)
        {
            PostNotificationEnrollmentResponse response = await DataExchangeAPI.PostNotificationEnrollment(telematicsNode, postRequest);

            return response;
        }

        public static async Task<SuccessMessageResponse> DeleteEnrollmentExample(int nodeId)
        {
            SuccessMessageResponse response = await DataExchangeAPI.DeleteNotificationDisenrollment(nodeId);

            return response;
        }

        public static async Task<GetNotificationResponse> GetNotificationUserExample()
        {
            GetNotificationResponse response = await DataExchangeAPI.GetNotificationUser();

            return response;
        }

        public static async Task<GetNotificationResponse> GetNotificationUserForNodeExample(int nodeId)
        {
            GetNotificationResponse response = await DataExchangeAPI.GetNotificationUserForNode(nodeId);

            return response;
        }

        public static async Task<GetNotificationResponse> GetNotificationUserForApiPartnerExample()
        {
            GetNotificationResponse response = await DataExchangeAPI.GetNotificationForApiPartner();

            return response;
        }

        public static async Task<GetNotificationResponse> GetNotificationUserForApiPartnerByNodeExample(int nodeId)
        {
            GetNotificationResponse response = await DataExchangeAPI.GetNotificationForApiPartnerByNode(nodeId);

            return response;
        }

        public static async Task<PostNotificationEnrollmentResponse> PostNotificationFile(FileRequest fileRequest)
        {
            PostNotificationEnrollmentResponse response = await DataExchangeAPI.PostNotificationFile(fileRequest);

            return response;
        }

        public static async Task<TelematicsNodeResponse> GetTelematicsNode(int nodeId)
        {
            TelematicsNodeResponse response = await DataExchangeAPI.GetTelematicsNode(nodeId);

            return response;
        }

        public static async Task<SuccessMessageResponse> DeleteTelematicsNode(int nodeId)
        {
            SuccessMessageResponse response = await DataExchangeAPI.DeleteTelematicsNode(nodeId);

            return response;
        }

        public static async Task<GetTelematicsNodeFilesResponse> GetTelematicsNodeFilesExample(int nodeId)
        {
            GetTelematicsNodeFilesResponse response = await DataExchangeAPI.GetTelematicsNodeFiles(nodeId);

            return response;
        }

        public static async Task<GetFileStatusResponse> GetFileStatusExample(string trackingCode)
        {
            GetFileStatusResponse response = await DataExchangeAPI.GetFileStatus(trackingCode);

            return response;
        }

        public static async Task<GetFieldResponse> GetTelematicsNodeFieldsExample(int nodeId)
        {
            GetFieldResponse response = await DataExchangeAPI.GetTelematicsNodeFields(nodeId);

            return response;
        }

        public static async Task<GetFieldBoundaryResponse> GetTelematicsNodeFieldBoundaryExample(int nodeId)
        {
            GetFieldBoundaryResponse response = await DataExchangeAPI.GetTelematicsNodeFieldBoundary(nodeId);

            return response;
        }

        public static async Task<SuccessMessageResponse> PostTelematicsNodeFieldBoundary(int nodeId, PostTelematicsNodeFieldBoundaryRequest postTelematicsNodeFieldBoundaryRequest)
        {
            SuccessMessageResponse response = await DataExchangeAPI.PostTelematicsNodeFieldBoundary(nodeId, postTelematicsNodeFieldBoundaryRequest);

            return response;
        }

        public static async Task<PostPlantingSummaryResponse> PostTelematicsNodePlantingSummaryExample(int nodeId, PostPlantingSummaryRequest postPlantingSummaryRequest)
        {
            PostPlantingSummaryResponse response = await DataExchangeAPI.PostTelematicsNodePlantingSummary(nodeId, postPlantingSummaryRequest);

            return response;
        }

        public static async Task<GetPlantingSummaryStatusResponse> GetTelematicsNodePlantingSummaryStatusExample(int nodeId, string trackingCode)
        {
            GetPlantingSummaryStatusResponse response = await DataExchangeAPI.GetTelematicsNodePlantingSummaryStatus(nodeId, trackingCode);

            return response;
        }
    }
}
