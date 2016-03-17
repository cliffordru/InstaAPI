using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
namespace InstaAPI.Models
{
    /// <summary>
    /// Tag information
    /// </summary>
    public class PostsTagBindingModel
    {
        /// <summary>
        /// The tag name
        /// </summary>
        [Required]
        // ReSharper disable once InconsistentNaming
        public string tag_name { get; set; }        
    }

    public class PostsFavoriteBindingModel
    {       
        [Required]        
        [Display(Name = "instagram_id")]
        [JsonProperty(PropertyName = "instagram_id")]
        public string InstagramId { get; set; }

        [Required]        
        [Display(Name = "tag_name")]
        [JsonProperty(PropertyName = "tag_name")]
        public string TagName { get; set; }
    }
}