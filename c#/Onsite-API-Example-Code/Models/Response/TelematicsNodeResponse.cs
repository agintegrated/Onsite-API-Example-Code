using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace Onsite_API_Example_Code.Models.Response
{
    public class TelematicsNodeResponse
    {
        public int TelematicsNodeID { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Type { get; set; }
        public bool FileRead { get; set; }
        public bool FileWrite { get; set; }
        public bool LocationAccess { get; set; }
        public bool ExportSummary { get; set; }
        public bool BoundaryRead { get; set; }
        public bool FieldRead { get; set; }
        public int AssetID { get; set; }
        public string NodeType { get; set; }
        public JToken Metadata { get; set; }
        public string AssetName { get; set; }
        public string APIName { get; set; }
        public bool Expired { get; set; }
        public bool Deletable { get; set; }
        public List<TelematicsNodeResponse> Children { get; set; }
    }
}
