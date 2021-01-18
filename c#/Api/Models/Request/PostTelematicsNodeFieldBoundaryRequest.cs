using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Onsite_API_Example_Code.Models.Request
{
    public class PostTelematicsNodeFieldBoundaryRequest
    {
        [Required]
        public string type { get; set; }

        [Required]
        public List<FeatureRequest> features { get; set; }
    }
}
