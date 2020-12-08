using Api;
using Onsite_API_Example_Code.Models.Response;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Onsite_API_Example_Code
{
    public class TelematicsV2
    {
        string publicKey;
        string privateKey;
        string userKey;

        public TelematicsV2(string publicKey, string privateKey, string userKey)
        {
            this.publicKey = publicKey;
            this.privateKey = privateKey;
            this.userKey = userKey;
        }

        /* 
         * The GET TelematicsNodeV2/Tree endpoint returns the entire telematics node tree for all of the equipment assets the user can access. 
         * These assets are formatted in a tree structure with zero to many child nodes for each parent node.
         */

        public async Task<GetTreeResponse> GetTree()
        {
            Dictionary<string, string> headers = ApiUtilities.BuildHeaders(userKey, publicKey, privateKey, "telematicsnodev2/tree");

            HttpResponseMessage response = await Api.Get("telematicsnodev2/tree", headers);

            GetTreeResponse result = await Api.DeserializeContent<GetTreeResponse>(response);

            return result;
        }

        /*
         * The GET TelematicsNodeV2/Equipment/Register endpoint retrieves a url that redirects the user to the Data Exchange 2.0 Registration page. 
         * There the user may register with the enabled equipment manufacturers.
         */
        public async Task<GetEquipmentRegisterResponse> GetEquipmentRegister()
        {
            Dictionary<string, string> headers = ApiUtilities.BuildHeaders(userKey, publicKey, privateKey, "telematicsnodev2/equipment/register");

            HttpResponseMessage response = await Api.Get("telematicsnodev2/equipment/register", headers);

            GetEquipmentRegisterResponse result = await Api.DeserializeContent<GetEquipmentRegisterResponse>(response);

            return result;
        }

        /*
         * The POST TelematicsNodeV2/{telematicsNodeID}/Files (Send File) endpoint uploads one file to the specified API equipment node. 
         * If the file is in a format not accepted by the equipment, and the user has permissions for FDA conversions, 
         * a request is sent to the FDA conversion service to convert the file before it is uploaded. 
         * This endpoint returns a status indicating successful and unsuccessful uploads and pending conversions with a tracking code. 
         * The trackingCode can be used in the GET TelematicsNodeV2/{trackingCode}/FileStatus endpoint to get the status of the FDA conversion.
        */
        public async Task<PostSendFileResponse> PostSendFiles( int nodeId, string filename)
        {
            Dictionary<string, string> headers = ApiUtilities.BuildHeaders(userKey, publicKey, privateKey, $"telematicsnodev2/{nodeId}/files", "POST");

            MultipartFormDataContent content = new MultipartFormDataContent();

            HttpContent bytes = new ByteArrayContent(ApiUtilities.ReadAllBytes(filename));

            content.Add(bytes, "fileToSend", filename);

            HttpResponseMessage response = await Api.Post($"telematicsnodev2/{nodeId}/files", headers, content);

            PostSendFileResponse result = await Api.DeserializeContent<PostSendFileResponse>(response);

            return result;
        }

        /*
         * The GET TelematicsNodeV2/{telematicsNodeID}/Files/{fileId} (Download File) endpoint downloads the specified file from the API equipment. 
         * The user can optionally request that the file be converted by the FDA service, if the user has FDA access.
        */
        public async void GetDownloadFilesWithoutConversion( int nodeId, string fileId)
        {
            Dictionary<string, string> headers = ApiUtilities.BuildHeaders(userKey, publicKey, privateKey, $"telematicsnodev2/{nodeId}/files/{fileId}");

            HttpResponseMessage response = await Api.Get($"telematicsnodev2/{nodeId}/files/{fileId}", headers);

            string tempFile = Path.Combine(Path.GetTempPath(), fileId + ".zip");
            using (var fileStream = new FileStream(tempFile, FileMode.Create, FileAccess.Write))
            {
                Stream file = await response.Content.ReadAsStreamAsync();
                file.CopyTo(fileStream);
            }
        }

        /*
         * The GET TelematicsNodeV2/{telematicsNodeID}/Files/{fileId} (Download File) endpoint downloads the specified file from the API equipment. 
         * The user can optionally request that the file be converted by the FDA service, if the user has FDA access.
        */
        public async Task<GetFileDownloadResponse> GetDownloadFilesWithConversion(int nodeId, string fileId, string callbackUrl, string conversionType)
        {
            Dictionary<string, string> headers = ApiUtilities.BuildHeaders(userKey, publicKey, privateKey, $"telematicsnodev2/{nodeId}/files/{fileId}");

            Dictionary<string, string> queryParams = new Dictionary<string, string>();

            queryParams.Add("callBackUrl", callbackUrl);

            queryParams.Add("conversionType", conversionType);

            HttpResponseMessage response = await Api.Get($"telematicsnodev2/{nodeId}/files/{fileId}", headers, queryParams);

            GetFileDownloadResponse result = await Api.DeserializeContent<GetFileDownloadResponse>(response);

            return result;
        }

        /*
         * The POST TelematicsNodeV2/Notifications/{telematicsNodeID}/Sns endpoint enrolls the Telematics Node for SNS notifications. 
         * Api partners with FDA permissions can include an Fda Configuration in the request to have the notification converted to a specified output preference.
         */
        public async Task<PostNotificationEnrollmentResponse> PostNotificationEnrollmentSns(int nodeId, string json)
        {
            Dictionary<string, string> headers = ApiUtilities.BuildHeaders(userKey, publicKey, privateKey, $"telematicsnodev2/notifications/{nodeId}/sns", "POST");

            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await Api.Post($"telematicsnodev2/notifications/{nodeId}/sns", headers, content);

            PostNotificationEnrollmentResponse result = await Api.DeserializeContent<PostNotificationEnrollmentResponse>(response);

            return result;
        }

        /*
         * The POST TelematicsNodeV2/Notifications/{telematicsNodeID}/Post endpoint enrolls the Telematics Node for POST notifications, notifications will be sent to the designated POST URI. 
         * Api partners with FDA permissions can include an FDA Configuration in the request to have the notification converted to a specified output preference.
         */
        public async Task<PostNotificationEnrollmentResponse> PostNotificationEnrollment(int nodeId, string json)
        {
            Dictionary<string, string> headers = ApiUtilities.BuildHeaders(userKey, publicKey, privateKey, $"telematicsnodev2/notifications/{nodeId}/post", "POST");

            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await Api.Post($"telematicsnodev2/notifications/{nodeId}/Post", headers, content);

            PostNotificationEnrollmentResponse result = await Api.DeserializeContent<PostNotificationEnrollmentResponse>(response);

            return result;
        }

        /*
         * The DELETE TelematicsNodeV2/Enrollment/Disenroll endpoints removes a previously enrolled notification for a specific node id.
         */
        public async Task<PostNotificationEnrollmentResponse> DeleteNotificationDisenrollment( int nodeId)
        {
            Dictionary<string, string> headers = ApiUtilities.BuildHeaders(userKey, publicKey, privateKey, $"telematicsnodev2/notifications/{nodeId}/disenroll", "DELETE");

            HttpResponseMessage response = await Api.Delete($"telematicsnodev2/notifications/{nodeId}/disenroll", headers);

            PostNotificationEnrollmentResponse result = await Api.DeserializeContent<PostNotificationEnrollmentResponse>(response);

            return result;
        }


    }
}
