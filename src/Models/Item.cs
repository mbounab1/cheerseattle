namespace todo.Models
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;

    public class Item : BaseVolunteerInfo
    {
        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }

        [JsonProperty(PropertyName = "isComplete")]
        public bool Completed { get; set; }

        [JsonProperty(PropertyName = "team")]
        public string Team { get; set; }

        [JsonProperty(PropertyName = "financialContribution")]
        public float FinancialContribution { get; set; }

        [JsonProperty(PropertyName = "attendance")]
        public Attendance Attendance { get; set; }

        [JsonProperty(PropertyName = "eventCredits")]
        public float EventCredits { get; set; } = 0;
    }
}
