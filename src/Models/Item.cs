namespace todo.Models
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;

    public class Item
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }

        [JsonProperty(PropertyName = "isComplete")]
        public bool Completed { get; set; }

        [JsonProperty(PropertyName = "email")]
        public string Email { get; set; }

        [JsonProperty(PropertyName = "pronouns")]
        public string Pronouns { get; set; }

        [JsonProperty(PropertyName = "team")]
        public string Team { get; set; }

        [JsonProperty(PropertyName = "financialContribution")]
        public float FinancialContribution { get; set; }

        [JsonProperty(PropertyName = "attendance")]
        public Attendance Attendance { get; set; }
    }
}
