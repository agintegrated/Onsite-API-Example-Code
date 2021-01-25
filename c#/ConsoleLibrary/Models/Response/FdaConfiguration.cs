using System;
using System.Collections.Generic;
using System.Text;

namespace Onsite_API_Example_Code.Models.Response
{
    public class FdaConfiguration
    {
        public string OutputAdapter { get; set; }
        
        public Dictionary<string, string> OutputPreferences { get; set; }
    }
}
