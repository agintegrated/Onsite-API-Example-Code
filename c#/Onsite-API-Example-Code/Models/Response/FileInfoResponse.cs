using System;
using System.Collections.Generic;
using System.Text;

namespace Onsite_API_Example_Code.Models.Response
{
    public class FileInfoResponse
    {
        public string Name { get; set; }
        public string ID { get; set; }
        public string FileType { get; set; }
        public DateTime Modified { get; set; }
        public DateTime Created { get; set; }
        public string Hash { get; set; }
        public string Size { get; set; }
        public DateTime DateAddedToSystem { get; set; }
        public string Manufacturer { get; set; }
        public string Source { get; set; }
        public string Format { get; set; }
    }
}
