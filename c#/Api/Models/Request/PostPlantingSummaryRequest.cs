using System;
using System.ComponentModel.DataAnnotations;

namespace Onsite_API_Example_Code.Models.Request
{
    public class PostPlantingSummaryRequest
    {
        [DataType(DataType.DateTime, ErrorMessage = "Dates must be entered in the ISO8")]
        public DateTime? StartDate { get; set; }

        [DataType(DataType.DateTime, ErrorMessage = "Dates must be entered in the ISO8")]
        public DateTime? EndDate { get; set; }

        [Required]
        public string CallbackURL { get; set; }
    }
}
