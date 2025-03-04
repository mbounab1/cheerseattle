namespace todo.Models
{
    using Newtonsoft.Json;

    public class BaseVolunteerInfo
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "firstName")]
        public string FirstName { get; set; }

        [JsonProperty(PropertyName = "lastName")]
        public string LastName { get; set; }

        [JsonProperty(PropertyName = "Name")]
        public string Name
        {
            get
            {
                return $"{FirstName} {LastName}";
            }
        }

        [JsonProperty(PropertyName = "email")]
        public string Email { get; set; }

        [JsonProperty(PropertyName = "pronouns")]
        public string Pronouns { get; set; }
    }
}