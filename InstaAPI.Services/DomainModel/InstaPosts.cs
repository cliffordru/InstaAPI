using System.Collections.Generic;

namespace InstaAPI.Services.DomainModel
{
    public class Meta
    {
        public int Code { get; set; }
    }
    public class User
    {
        public string Username { get; set; }    
    }

    public class InstaPostsData
    {
        public string Link { get; set; }
        
        public string Id { get; set; }

        public User User { get; set; }
    }

    public class InstaPosts
    {
       public Meta Meta { get; set; }
        public List<InstaPostsData> Data { get; set; }
    }

}
