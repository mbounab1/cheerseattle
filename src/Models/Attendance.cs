using Newtonsoft.Json;
using System.Collections.Generic;

namespace todo.Models
{
    public class Attendance : BaseVolunteerInfo
    {
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "present")]
        public List<string> Present { get; set; } = new List<string> { "" };

        [JsonProperty(PropertyName = "absent")]
        public List<string> Absent { get; set; } = new List<string> { "" };

        [JsonProperty(PropertyName = "late")]
        public List<string> Late { get; set; } = new List<string> { "" };

        [JsonProperty(PropertyName = "unexcused")]
        public List<string> Unexcused { get; set; } = new List<string> { "" };
    }
}