using System;
using System.Collections.Generic;
using System.Text;

namespace Onsite_API_Example_Code.Models.Response
{
    public class GetFieldBoundaryResponse
    {
        public bool Success { get; set; }
        
        public string Message { get; set; }
        
        public string BoundaryID { get; set; }
        
        public object BoundaryName { get; set; }
        
        public double Area { get; set; }
        
        public string AreaUnits { get; set; }
        
        public string FieldID { get; set; }
        
        public string FieldName { get; set; }
        
    }
}
