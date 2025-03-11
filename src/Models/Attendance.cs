using Newtonsoft.Json;
using System.Collections.Generic;

namespace todo.Models
{
    public class Attendance
    {
        [JsonProperty(PropertyName = "present")]
        public HashSet<string> Present { get; set; } = new HashSet<string>();

        [JsonProperty(PropertyName = "absent")]
        public HashSet<string> Absent { get; set; } = new HashSet<string>();

        [JsonProperty(PropertyName = "late")]
        public HashSet<string> Late { get; set; } = new HashSet<string>();

        [JsonProperty(PropertyName = "unexcused")]
        public HashSet<string> Unexcused { get; set; } = new HashSet<string>();
    }
}