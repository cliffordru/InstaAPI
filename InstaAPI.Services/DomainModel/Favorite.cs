using System;

namespace InstaAPI.Services.DomainModel
{
    public class Favorite
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string InstagramId { get; set; }
        public string TagName { get; set; }
        public DateTime CreatedOn { get; set; }

        public override string ToString()
        {
            return $"Id: {Id} | UserId: {UserId} | InstagramId: {InstagramId} | TagName: {TagName} | CreatedOn (UTC): {CreatedOn.ToString("dd MMM yyyy - HH:mm:ss")}";
        }
    }
}