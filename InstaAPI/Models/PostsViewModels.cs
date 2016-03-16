using Newtonsoft.Json;

namespace InstaAPI.Models
{
    public class PostsTagViewModel
    {    
        [JsonProperty(PropertyName = "instagram_id")]
        public string InstagramId { get; set; }

        [JsonProperty(PropertyName = "url")]        
        public string Url { get; set; }

        [JsonProperty(PropertyName = "username")]        
        public string Username { get; set; }
    }
}