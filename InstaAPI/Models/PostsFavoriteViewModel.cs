using Newtonsoft.Json;

namespace InstaAPI.Models
{
    public class PostsFavoriteViewModel
    {
        [JsonProperty(PropertyName = "instagram_id")]
        public string InstagramId { get; set; }
        [JsonProperty(PropertyName = "url")]
        public string Url { get; set; }
        [JsonProperty(PropertyName = "user_name")]
        public string UserName { get; set; }
        [JsonProperty(PropertyName = "tag_name")]
        public string TagName { get; set; }
    }
}