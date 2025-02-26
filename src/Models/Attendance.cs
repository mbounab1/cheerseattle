using Newtonsoft.Json;
using System.Collections.Generic;

namespace todo.Models
{
    public class Attendance
    {
        [JsonProperty(PropertyName = "present")]
        public List<string> Present { get; set; }

        [JsonProperty(PropertyName = "absent")]
        public List<string> Absent { get; set; }

        [JsonProperty(PropertyName = "late")]
        public List<string> Late { get; set; }

        [JsonProperty(PropertyName = "unexcused")]
        public List<string> Unexcused { get; set; }
    }
}