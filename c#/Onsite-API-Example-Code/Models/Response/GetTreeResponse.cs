using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace Onsite_API_Example_Code.Models.Response
{
    public class Metadata
    {
        public string company_fmid { get; set; }
        public object company_source_id { get; set; }
        public string name { get; set; }
        public string timezone { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public string Owner { get; set; }
        public string Version { get; set; }
        public string Name { get; set; }
        public int? Timezone { get; set; }
        public string machine { get; set; }
    }

    public class Child2
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
        public object Metadata { get; set; }
        public string AssetName { get; set; }
        public string APIName { get; set; }
        public bool Expired { get; set; }
        public bool Deletable { get; set; }
        public List<object> Children { get; set; }
    }

    public class Child
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
        public Metadata Metadata { get; set; }
        public string AssetName { get; set; }
        public string APIName { get; set; }
        public bool Expired { get; set; }
        public bool Deletable { get; set; }
        public List<Child2> Children { get; set; }
    }

    public class Data
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
        public object Metadata { get; set; }
        public string AssetName { get; set; }
        public string APIName { get; set; }
        public bool Expired { get; set; }
        public bool Deletable { get; set; }
        public List<Child> Children { get; set; }
    }

    public class GetTreeResponse
    {
        public int Count { get; set; }
        public List<Data> Data { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
    }

}
