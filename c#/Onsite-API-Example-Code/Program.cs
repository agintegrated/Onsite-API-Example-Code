using Onsite_API_Example_Code;
using Onsite_API_Example_Code.Models.Response;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
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
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static async Task Main()
        {
            // string exampleJsonSns = "{'ApiKey': '','SecretKey': '','TopicArn': '','Region': ''}";
            // string exampleJsonPost = "{'Url': 'url'}";
            //string fileId;

             //await GetTreeExample();
            // DownloadFileExample(fileId);
            // await UploadFileExample("ExampleAsApplied.zip");
            // await EnrollmentSnsExample(exampleJsonSns);
            // await EnrollmentExample(exampleJsonPost);
            // await DeleteEnrollmentExample(telematicsNode);

        }

        public static async Task<GetTreeResponse> GetTreeExample()
        {
            TelematicsV2 telematicsV2 = new TelematicsV2(publicKey, privateKey, userKey);
            GetTreeResponse response = await telematicsV2.GetTree();

            return response;
        }
        public static async Task<PostSendFileResponse> UploadFileExample(string fileName)
        {
            TelematicsV2 telematicsV2 = new TelematicsV2(publicKey, privateKey, userKey);
            PostSendFileResponse response = await telematicsV2.PostSendFiles(telematicsNode, fileName);

            return response;
        }
        public static bool DownloadFileExample(string fileId)
        {
            TelematicsV2 telematicsV2 = new TelematicsV2(publicKey, privateKey, userKey);
            telematicsV2.GetDownloadFilesWithoutConversion(telematicsNode, fileId);

            return true;
        }

        public static async Task<PostNotificationEnrollmentResponse> EnrollmentSnsExample(string json)
        {
            TelematicsV2 telematicsV2 = new TelematicsV2(publicKey, privateKey, userKey);
            PostNotificationEnrollmentResponse response = await telematicsV2.PostNotificationEnrollmentSns(telematicsNode, json);

            return response;
        }

        public static async Task<PostNotificationEnrollmentResponse> EnrollmentExample(string json)
        {
            TelematicsV2 telematicsV2 = new TelematicsV2(publicKey, privateKey, userKey);
            PostNotificationEnrollmentResponse response = await telematicsV2.PostNotificationEnrollment(telematicsNode, json);

            return response;
        }

        public static async Task<PostNotificationEnrollmentResponse> DeleteEnrollmentExample(int nodeId)
        {
            TelematicsV2 telematicsV2 = new TelematicsV2(publicKey, privateKey, userKey);
            PostNotificationEnrollmentResponse response = await telematicsV2.DeleteNotificationDisenrollment(nodeId);

            return response;
        }
    }
}
