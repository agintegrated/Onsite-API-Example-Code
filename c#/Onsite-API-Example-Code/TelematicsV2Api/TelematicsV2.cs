using Api;
using Onsite_API_Example_Code.Models.Request;
using Onsite_API_Example_Code.Models.Response;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Text.Json;
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

        /// <summary>
        /// The GET TelematicsNodeV2/Tree endpoint returns the entire telematics node tree for all of the equipment assets the user can access. 
        ///  These assets are formatted in a tree structure with zero to many child nodes for each parent node.
        /// </summary>
        /// <returns></returns>

        public async Task<GetTreeResponse> GetTree()
        {
            Dictionary<string, string> headers = ApiUtilities.BuildHeaders(userKey, publicKey, privateKey, "telematicsnodev2/tree");

            HttpResponseMessage response = await Api.Get("telematicsnodev2/tree", headers);

            GetTreeResponse result = await Api.DeserializeContent<GetTreeResponse>(response);

            return result;
        }

        /// <summary>
        /// The GET TelematicsNodeV2/Equipment/Register endpoint retrieves a url that redirects the user to the Data Exchange 2.0 Registration page. 
        ///  There the user may register with the enabled equipment manufacturers.
        /// </summary>
        /// <returns></returns>

        public async Task<GetEquipmentRegisterResponse> GetEquipmentRegister()
        {
            Dictionary<string, string> headers = ApiUtilities.BuildHeaders(userKey, publicKey, privateKey, "telematicsnodev2/equipment/register");

            HttpResponseMessage response = await Api.Get("telematicsnodev2/equipment/register", headers);

            GetEquipmentRegisterResponse result = await Api.DeserializeContent<GetEquipmentRegisterResponse>(response);

            System.Diagnostics.Process.Start(@"C:\Program Files\Internet Explorer\IExplore.exe", result.Url);

            return result;
        }

        /// <summary>
        /// The POST TelematicsNodeV2/{telematicsNodeID}/Files (Send File) endpoint uploads one file to the specified API equipment node. 
        /// If the file is in a format not accepted by the equipment, and the user has permissions for FDA conversions, 
        /// a request is sent to the FDA conversion service to convert the file before it is uploaded. 
        /// This endpoint returns a status indicating successful and unsuccessful uploads and pending conversions with a tracking code. 
        /// The trackingCode can be used in the GET TelematicsNodeV2/{trackingCode}/FileStatus endpoint to get the status of the FDA conversion.
        /// </summary>
        /// <param name="nodeId"></param>
        /// <param name="pathToFile"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>

        public async Task<PostSendFileResponse> PostSendFiles( int nodeId, string pathToFile, string fileName)
        {
            Dictionary<string, string> headers = ApiUtilities.BuildHeaders(userKey, publicKey, privateKey, $"telematicsnodev2/{nodeId}/files", "POST");

            MultipartFormDataContent content = new MultipartFormDataContent();

            HttpContent bytes = new ByteArrayContent(File.ReadAllBytes(pathToFile));

            content.Add(bytes, "fileToSend", fileName);

            HttpResponseMessage response = await Api.Post($"telematicsnodev2/{nodeId}/files", headers, content);

            PostSendFileResponse result = await Api.DeserializeContent<PostSendFileResponse>(response);

            return result;
        }

        /// <summary>
        /// * The GET TelematicsNodeV2/{telematicsNodeID}/Files/{fileId} (Download File) endpoint downloads the specified file from the API equipment. 
        /// The user can optionally request that the file be converted by the FDA service, if the user has FDA access.
        /// </summary>
        /// <param name="nodeId"></param>
        /// <param name="fileId"></param>

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


        /// <summary>
        /// The GET TelematicsNodeV2/{telematicsNodeID}/Files/{fileId} (Download File) endpoint downloads the specified file from the API equipment. 
        /// The user can optionally request that the file be converted by the FDA service, if the user has FDA access.
        /// </summary>
        /// <param name="nodeId"></param>
        /// <param name="fileId"></param>
        /// <param name="callbackUrl"></param>
        /// <param name="conversionType"></param>
        /// <returns></returns>

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

        /// <summary>
        /// The POST TelematicsNodeV2/Notifications/{telematicsNodeID}/Sns endpoint enrolls the Telematics Node for SNS notifications. 
        /// Api partners with FDA permissions can include an Fda Configuration in the request to have the notification converted to a specified output preference.
        /// </summary>
        /// <param name="nodeId"></param>
        /// <param name="postNotificationSnsRequest"></param>
        /// <returns></returns>

        public async Task<NodeNotificationResponse> PostNotificationEnrollmentSns(int nodeId, SNSRequest postNotificationSnsRequest)
        {
            Dictionary<string, string> headers = ApiUtilities.BuildHeaders(userKey, publicKey, privateKey, $"telematicsnodev2/notifications/{nodeId}/sns", "POST");

            string json = JsonSerializer.Serialize(postNotificationSnsRequest);

            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await Api.Post($"telematicsnodev2/notifications/{nodeId}/sns", headers, content);

            NodeNotificationResponse result = await Api.DeserializeContent<NodeNotificationResponse>(response);

            return result;
        }

        /// <summary>
        /// The POST TelematicsNodeV2/Notifications/{telematicsNodeID}/Post endpoint enrolls the Telematics Node for POST notifications, notifications will be sent to the designated POST URI. 
        /// Api partners with FDA permissions can include an FDA Configuration in the request to have the notification converted to a specified output preference.
        /// </summary>
        /// <param name="nodeId"></param>
        /// <param name="postNotificationRequest"></param>
        /// <returns></returns>
        public async Task<NodeNotificationResponse> PostNotificationEnrollment(int nodeId, PostRequest postNotificationRequest)
        {
            Dictionary<string, string> headers = ApiUtilities.BuildHeaders(userKey, publicKey, privateKey, $"telematicsnodev2/notifications/{nodeId}/post", "POST");

            string json = JsonSerializer.Serialize(postNotificationRequest);

            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await Api.Post($"telematicsnodev2/notifications/{nodeId}/Post", headers, content);

            NodeNotificationResponse result = await Api.DeserializeContent<NodeNotificationResponse>(response);

            return result;
        }

        /// <summary>
        /// The DELETE TelematicsNodeV2/Enrollment/Disenroll endpoints removes a previously enrolled notification for a specific node id.
        /// </summary>
        /// <param name="nodeId"></param>
        /// <returns></returns>
        public async Task<EquipmentResponse> DeleteNotificationDisenrollment(int nodeId)
        {
            Dictionary<string, string> headers = ApiUtilities.BuildHeaders(userKey, publicKey, privateKey, $"telematicsnodev2/notifications/{nodeId}/disenroll", "DELETE");

            HttpResponseMessage response = await Api.Delete($"telematicsnodev2/notifications/{nodeId}/disenroll", headers);

            EquipmentResponse result = await Api.DeserializeContent<EquipmentResponse>(response);

            return result;
        }

        /// <summary>
        /// The GET TelematicsNodeV2/Notifications/User endpoint retrieves all enrolled notifications for the current user's telematics nodes.
        /// </summary>
        /// <returns></returns>
        public async Task<UserNotificationResponse> GetNotificationUser()
        {
            Dictionary<string, string> headers = ApiUtilities.BuildHeaders(userKey, publicKey, privateKey, $"telematicsnodev2/notifications/user");

            HttpResponseMessage response = await Api.Get($"telematicsnodev2/notifications/user", headers);

            UserNotificationResponse result = await Api.DeserializeContent<UserNotificationResponse>(response);

            return result;
        }

        /// <summary>
        /// The GET TelematicsNodeV2/Notifications/User/{telematicsNodeID} endpoint retrieves all enrolled notifications for the specified telematics node.
        /// </summary>
        /// <param name="nodeId"></param>
        /// <returns></returns>
        public async Task<NodeNotificationResponse> GetNotificationUserForNode(int nodeId)
        {
            Dictionary<string, string> headers = ApiUtilities.BuildHeaders(userKey, publicKey, privateKey, $"telematicsnodev2/notifications/user/{nodeId}");

            HttpResponseMessage response = await Api.Get($"telematicsnodev2/notifications/user/{nodeId}", headers);

            NodeNotificationResponse result = await Api.DeserializeContent<NodeNotificationResponse>(response);

            return result;
        }

        /// <summary>
        /// The GET TelematicsNodeV2/Notifications endpoint retrieves all enrolled notifications for the api partner.
        /// </summary>
        /// <returns></returns>
        public async Task<UserNotificationResponse> GetNotificationForApiPartner()
        {
            Dictionary<string, string> headers = ApiUtilities.BuildHeaders(userKey, publicKey, privateKey, $"telematicsnodev2/notifications");

            HttpResponseMessage response = await Api.Get($"telematicsnodev2/notifications", headers);

            UserNotificationResponse result = await Api.DeserializeContent<UserNotificationResponse>(response);

            return result;
        }

         /// <summary>
         /// The GET TelematicsNodeV2/Notifications/{nodeId} endpoint retrieves all enrolled notifications for a single telematics node for all api partner users.
         /// </summary>
         /// <param name="nodeId"></param>
         /// <returns></returns>
        public async Task<NodeNotificationResponse> GetNotificationForApiPartnerByNode(int nodeId)
        {
            Dictionary<string, string> headers = ApiUtilities.BuildHeaders(userKey, publicKey, privateKey, $"telematicsnodev2/notifications/{nodeId}");

            HttpResponseMessage response = await Api.Get($"telematicsnodev2/notifications/{nodeId}", headers);

            NodeNotificationResponse result = await Api.DeserializeContent<NodeNotificationResponse>(response);

            return result;
        }

        /// <summary>
        /// The POST /TelematicsNodeV2/Enrollment/File endpoint enrolls a list of Telematics Node(s) which have notifications for File Conversion and/or S3 Storage. 
        /// Api partners with FDA permissions can include an FDA Configuration in the request to have the notification converted to a specified output preference
        /// </summary>
        /// <param name="fileRequest"></param>
        /// <returns></returns>
        public async Task<EnrollFilesResponse> PostNotificationFile(FileRequest fileRequest)
        {
            Dictionary<string, string> headers = ApiUtilities.BuildHeaders(userKey, publicKey, privateKey, $"telematicsnodev2/notifications/file", "POST");

            string json = JsonSerializer.Serialize(fileRequest);

            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await Api.Post($"telematicsnodev2/notifications/file", headers, content);

            EnrollFilesResponse result = await Api.DeserializeContent<EnrollFilesResponse>(response);

            return result;
        }

        /// <summary>
        /// View the telematics node data for a single node.
        /// </summary>
        /// <param name="nodeId"></param>
        /// <returns></returns>
        public async Task<TelematicsNodeResponse> GetTelematicsNode(int nodeId)
        {
            Dictionary<string, string> headers = ApiUtilities.BuildHeaders(userKey, publicKey, privateKey, $"telematicsnodev2/{nodeId}");

            HttpResponseMessage response = await Api.Get($"telematicsnodev2/{nodeId}", headers);

            TelematicsNodeResponse result = await Api.DeserializeContent<TelematicsNodeResponse>(response);

            return result;
        }

        /// <summary>
        /// The DELETE TelematicsNodeV2/{telematicsNodeID} endpoint allows a user to delete their connection to an equipment. It only removes the users connection to the endpoint. If the user wishes to get the equipment data back they just need to use the Data Exchange 2 registration workflow and re-register the equipment.
        /// Only root nodes can be deleted. Once a user deletes their connection to the root node they lose access to that entire node tree unless they re-register.
        /// </summary>
        /// <param name="nodeId"></param>
        /// <returns></returns>
        public async Task<EquipmentResponse> DeleteTelematicsNode(int nodeId)
        {
            Dictionary<string, string> headers = ApiUtilities.BuildHeaders(userKey, publicKey, privateKey, $"telematicsnodev2/{nodeId}", "DELETE");

            HttpResponseMessage response = await Api.Delete($"telematicsnodev2/{nodeId}", headers);

            EquipmentResponse result = await Api.DeserializeContent<EquipmentResponse>(response);

            return result;
        }

        /// <summary>
        /// The GET TelematicsNodeV2/{telematicsNodeID}/Files (List Files) endpoint gets a list of all of the files that are available on API equipment asset for the specified telematics node. 
        /// File lists for all nodes are updated every 15 minutes.
        /// </summary>
        /// <param name="nodeId"></param>
        /// <returns></returns>
        public async Task<GetTelematicsNodeFilesResponse> GetTelematicsNodeFiles(int nodeId)
        {
            Dictionary<string, string> headers = ApiUtilities.BuildHeaders(userKey, publicKey, privateKey, $"telematicsnodev2/{nodeId}/files");

            HttpResponseMessage response = await Api.Get($"telematicsnodev2/{nodeId}/files", headers);

            GetTelematicsNodeFilesResponse result = await Api.DeserializeContent<GetTelematicsNodeFilesResponse>(response);

            return result;
        }

        /// <summary>
        /// The GET TelematicsNodeV2/{trackingCode}/FileStatus endpoint enables a user to obtain the current status of an FDA conversion using the provided trackingCode. 
        /// The trackingCode was provided when the user downloaded a file from one of their API equipment connections, specifying to convert the file, or when a file was uploaded, when it required conversion first.
        /// </summary>
        /// <param name="trackingCode"></param>
        /// <returns></returns>
        public async Task<GetFileStatusResponse> GetFileStatus(string trackingCode)
        {
            Dictionary<string, string> headers = ApiUtilities.BuildHeaders(userKey, publicKey, privateKey, $"telematicsnodev2/{trackingCode}/filestatus");

            HttpResponseMessage response = await Api.Get($"telematicsnodev2/{trackingCode}/filestatus", headers);

            GetFileStatusResponse result = await Api.DeserializeContent<GetFileStatusResponse>(response);

            return result;
        }

         /// <summary>
         /// The GET TelematicsNodeV2/{telematicsNodeId}/Fields endpoint retrieves a list of fields associated with the provided telematics node.
         /// </summary>
         /// <param name="nodeId"></param>
         /// <returns></returns>
        public async Task<GetFieldResponse> GetTelematicsNodeFields(int nodeId)
        {
            Dictionary<string, string> headers = ApiUtilities.BuildHeaders(userKey, publicKey, privateKey, $"telematicsnodev2/{nodeId}/fields");

            HttpResponseMessage response = await Api.Get($"telematicsnodev2/{nodeId}/fields", headers);

            GetFieldResponse result = await Api.DeserializeContent<GetFieldResponse>(response);

            return result;
        }

         /// <summary>
         /// The GET TelematicsNodeV2/{telematicsNodeID}/FieldBoundary (Get Boundary) endpoint retrieves a boundary for a specified telematics node. 
         /// This endpoint is currently only supported for Climate equipment nodes.
         /// </summary>
         /// <param name="nodeId"></param>
         /// <returns></returns>
        public async Task<GetFieldBoundaryResponse> GetTelematicsNodeFieldBoundary(int nodeId)
        {
            Dictionary<string, string> headers = ApiUtilities.BuildHeaders(userKey, publicKey, privateKey, $"telematicsnodev2/{nodeId}/fieldboundary");

            HttpResponseMessage response = await Api.Get($"telematicsnodev2/{nodeId}/fieldboundary", headers);

            GetFieldBoundaryResponse result = await Api.DeserializeContent<GetFieldBoundaryResponse>(response);

            return result;
        }

        /// <summary>
        /// The POST TelematicsNodeV2/{telematicsNodeID}/FieldBoundary (Send Boundary) endpoint sends boundary data to the specified API equipment node. 
        /// This endpoint is currently only supported for Climate equipment nodes.
        /// </summary>
        /// <param name="nodeId"></param>
        /// <param name="postTelematicsNodeFieldBoundaryRequest"></param>
        /// <returns></returns>
        public async Task<EquipmentResponse> PostTelematicsNodeFieldBoundary(int nodeId, BoundaryRequest postTelematicsNodeFieldBoundaryRequest)
        {
            Dictionary<string, string> headers = ApiUtilities.BuildHeaders(userKey, publicKey, privateKey, $"telematicsnodev2/{nodeId}/fieldboundary", "POST");

            string json = JsonSerializer.Serialize(postTelematicsNodeFieldBoundaryRequest);

            HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await Api.Post($"telematicsnodev2/{nodeId}/fieldboundary", headers, content);

            EquipmentResponse result = await Api.DeserializeContent<EquipmentResponse>(response);

            return result;
        }

         /// <summary>
         /// The POST TelematicsNodeV2/{telematicsNodeID}/PlantingSummary endpoint will 
         /// allow the user to request a summary of their planting information based on a set of passed parameters.
         /// </summary>
         /// <param name="nodeId"></param>
         /// <param name="postPlantingSummaryRequest"></param>
         /// <returns></returns>
        public async Task<PostPlantingSummaryResponse> PostTelematicsNodePlantingSummary(int nodeId, PostPlantingSummaryRequest postPlantingSummaryRequest)
        {
            Dictionary<string, string> headers = ApiUtilities.BuildHeaders(userKey, publicKey, privateKey, $"telematicsnodev2/{nodeId}/plantingsummary", "POST");

            string json = JsonSerializer.Serialize(postPlantingSummaryRequest);

            HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await Api.Post($"telematicsnodev2/{nodeId}/plantingsummary", headers, content);

            PostPlantingSummaryResponse result = await Api.DeserializeContent<PostPlantingSummaryResponse>(response);

            return result;
        }

         /// <summary>
         /// The GET TelematicsNodeV2/{telematicsNodeID}/PlantingSummary/{trackingCode}/Status endpoint will allow the user to check 
         /// the status of their planting summary request using the tracking code they received as a result of their previous Planting Summary request.
         /// </summary>
         /// <param name="nodeId"></param>
         /// <param name="trackingCode"></param>
         /// <returns></returns>
        public async Task<GetPlantingSummaryStatusResponse> GetTelematicsNodePlantingSummaryStatus(int nodeId, string trackingCode)
        {
            Dictionary<string, string> headers = ApiUtilities.BuildHeaders(userKey, publicKey, privateKey, $"telematicsnodev2/{nodeId}/plantingsummary/{trackingCode}/status");

            HttpResponseMessage response = await Api.Get($"telematicsnodev2/{nodeId}/plantingsummary/{trackingCode}/status", headers);

            GetPlantingSummaryStatusResponse result = await Api.DeserializeContent<GetPlantingSummaryStatusResponse>(response);

            return result;
        }

    }
}
