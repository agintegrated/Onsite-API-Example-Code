using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Onsite_API_Example_Code.Models.Request
{
    public class FeatureRequest
    {

        [Required]
        public string type { get; set; }

        [Required]
        public Dictionary<string, string> properties { get; set; }

        [Required]
        public GeometryRequest geometry { get; set; }
    }
}
