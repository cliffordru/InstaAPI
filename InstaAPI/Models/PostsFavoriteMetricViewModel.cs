using Newtonsoft.Json;

namespace InstaAPI.Models
{
    public class PostsFavoriteMetricsViewModel
    {
        [JsonProperty(PropertyName = "tag_name")]
        public string TagName { get; set; }
        [JsonProperty(PropertyName = "count")]
        public string Count { get; set; }
    }
}