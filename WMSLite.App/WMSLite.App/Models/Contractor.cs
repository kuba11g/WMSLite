using System.Text.Json.Serialization;

namespace WMSLite.App.Models
{
    public class Contractor
    {
        public int Id { get; set; }
        public string? Symbol { get; set; }
        public string? Name { get; set; }

        [JsonIgnore]
        public string DisplayName => $"{Symbol} - {Name}";
    }
}