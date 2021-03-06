﻿using Onsite_API_Example_Code;
using Onsite_API_Example_Code.Models.Request;
using Onsite_API_Example_Code.Models.Response;
using System;
using System.IO;
using System.Threading.Tasks;

namespace OnsiteAPIExample
{
    
    static class Program
    {
        static string userKey = "";
        static string publicKey = "";
        static string privateKey = "";

        static int telematicsNode;

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
        public static async Task<PostSendFileResponse> UploadFileExample(string fileName, FileInfo file)
        {
            TelematicsV2 telematicsV2 = new TelematicsV2(publicKey, privateKey, userKey);
            PostSendFileResponse response = await telematicsV2.PostSendFiles(telematicsNode, file);

            return response;
        }

        public static async Task<FileInfo> DownloadFileExample(string fileId)
        {
            TelematicsV2 telematicsV2 = new TelematicsV2(publicKey, privateKey, userKey);
            FileInfo fileInfo = await telematicsV2.GetDownloadFilesWithoutConversion(telematicsNode, fileId);

            return fileInfo;
        }

        public static async Task<NodeNotificationResponse> EnrollSnsNotificationExample(int telematicsNode, SNSRequest snsRequest)
        {
            TelematicsV2 telematicsV2 = new TelematicsV2(publicKey, privateKey, userKey);
            NodeNotificationResponse response = await telematicsV2.PostNotificationEnrollmentSns(telematicsNode, snsRequest);

            return response;
        }

        public static async Task<NodeNotificationResponse> EnrollPostNotificationExample(int telematicsNode, PostRequest postRequest)
        {
            TelematicsV2 telematicsV2 = new TelematicsV2(publicKey, privateKey, userKey);
            NodeNotificationResponse response = await telematicsV2.EnrollPostNotification(telematicsNode, postRequest);

            return response;
        }

        public static async Task<EquipmentResponse> DeleteNotificationExample(int nodeId)
        {
            TelematicsV2 telematicsV2 = new TelematicsV2(publicKey, privateKey, userKey);
            EquipmentResponse response = await telematicsV2.Disenroll(nodeId);

            return response;
        }

        public static async Task<UserNotificationResponse> GetNotificationUserExample()
        {
            TelematicsV2 telematicsV2 = new TelematicsV2(publicKey, privateKey, userKey);
            UserNotificationResponse response = await telematicsV2.GetNotificationUser();

            return response;
        }

        public static async Task<NodeNotificationResponse> GetNotificationUserForNodeExample(int nodeId)
        {
            TelematicsV2 telematicsV2 = new TelematicsV2(publicKey, privateKey, userKey);
            NodeNotificationResponse response = await telematicsV2.GetNotificationUserForNode(nodeId);

            return response;
        }

        public static async Task<UserNotificationResponse> GetNotificationsForApiPartnerExample()
        {
            TelematicsV2 telematicsV2 = new TelematicsV2(publicKey, privateKey, userKey);
            UserNotificationResponse response = await telematicsV2.GetNotificationForApiPartner();

            return response;
        }

        public static async Task<NodeNotificationResponse> GetNotificationsForApiPartnerByNodeExample(int nodeId)
        {
            TelematicsV2 telematicsV2 = new TelematicsV2(publicKey, privateKey, userKey);
            NodeNotificationResponse response = await telematicsV2.GetNotificationForApiPartnerByNode(nodeId);

            return response;
        }

        public static async Task<EnrollFilesResponse> PostNotificationFile(FileRequest fileRequest)
        {
            TelematicsV2 telematicsV2 = new TelematicsV2(publicKey, privateKey, userKey);
            EnrollFilesResponse response = await telematicsV2.PostNotificationFile(fileRequest);

            return response;
        }

        public static async Task<TelematicsNodeResponse> GetTelematicsNode(int nodeId)
        {
            TelematicsV2 telematicsV2 = new TelematicsV2(publicKey, privateKey, userKey);
            TelematicsNodeResponse response = await telematicsV2.GetTelematicsNode(nodeId);

            return response;
        }

        public static async Task<EquipmentResponse> DeleteTelematicsNode(int nodeId)
        {
            TelematicsV2 telematicsV2 = new TelematicsV2(publicKey, privateKey, userKey);
            EquipmentResponse response = await telematicsV2.DeleteTelematicsNode(nodeId);

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
