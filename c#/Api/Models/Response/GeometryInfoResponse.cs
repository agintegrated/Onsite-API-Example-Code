using System;
using System.Collections.Generic;
using System.Text;

namespace Onsite_API_Example_Code.Models.Response
{
   public class GeometryInfoResponse
    {
        public string Type { get; set; }
        public List<List<List<List<double>>>> Coordinates { get; set; }
    }
}
