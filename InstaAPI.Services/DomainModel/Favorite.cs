using System;

namespace InstaAPI.Services.DomainModel
{
    public class Favorite : BaseDomain
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string InstagramId { get; set; }
        public string TagName { get; set; }
        public DateTime CreatedOn { get; set; }
        public Post Post { get; set; }
    }
}