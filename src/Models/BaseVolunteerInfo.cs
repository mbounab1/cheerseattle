namespace todo.Models
{
    using Newtonsoft.Json;

    public class BaseVolunteerInfo
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "email")]
        public string Email { get; set; }

        [JsonProperty(PropertyName = "pronouns")]
        public string Pronouns { get; set; }
    }
}
