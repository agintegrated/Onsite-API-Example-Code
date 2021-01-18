using System.Collections.Generic;

namespace Onsite_API_Example_Code.Models.Request
{
    public class FdaConfigurationRequest
    {
        public string OutputAdapter { get; set; }

        public Dictionary<string, string> OutputPreferences { get; set; }
    }
}
