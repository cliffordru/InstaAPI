using System;
using System.Data.Entity.Migrations.Model;

namespace InstaAPI.Services.CommandModel
{
    public class FavoriteCreationSpec
    {
        public string UserId { get; set; }
        public string InstagramId { get; set; }
        
        public string TagName { get; set; }

        public void Validate()
        {
            if(string.IsNullOrEmpty(InstagramId))
                throw new ArgumentNullException(nameof(InstagramId));
            if (string.IsNullOrEmpty(TagName))
                throw new ArgumentNullException(nameof(TagName));
        }
    }
}