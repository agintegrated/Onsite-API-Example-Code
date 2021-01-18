using Onsite_API_Example_Code.Models.Response;
using System.ComponentModel.DataAnnotations;

namespace Onsite_API_Example_Code.Models.Request
{
    public class PostRequest
    {
        [Required]
        public string Url { get; set; }

        public FdaConfiguration FdaConfiguration { get; set; }

        public RawFileStorage RawFileStorage { get; set; }
    }
}
