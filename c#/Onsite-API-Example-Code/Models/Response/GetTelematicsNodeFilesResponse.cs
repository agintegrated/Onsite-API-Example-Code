using System;
using System.Collections.Generic;
using System.Text;

namespace Onsite_API_Example_Code.Models.Response
{
    public class GetTelematicsNodeFilesResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public int TelematicsNodeId { get; set; }
        public DateTime LastRetrieved { get; set; }
        public string ContinuationToken { get; set; }
        public bool PendingFileListing { get; set; }
        public int Count { get; set; }
        public List<FileInfoResponse> Data { get; set; }

    }
}
