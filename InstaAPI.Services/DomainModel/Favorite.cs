using System;

namespace InstaAPI.Services.DomainModel
{
    public class Favorite
    {
        public int Id { get; set; }
        public DateTime CreatedOn { get; set; }

        public override string ToString()
        {
            return $"Id: {Id} | CreatedOn (UTC): {CreatedOn.ToString("dd MMM yyyy - HH:mm:ss")}";
        }
    }
}