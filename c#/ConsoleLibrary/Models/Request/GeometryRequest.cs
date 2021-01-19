using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Onsite_API_Example_Code.Models.Request
{
    public class GeometryRequest
    {
        [Required]
        public string type { get; set; }

        public Dictionary<string, string> properties { get; set; }

        [Required]
        public JArray Coordinates { get; set; }
    }
}
