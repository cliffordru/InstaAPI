using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace InstaAPI.Services.DomainModel
{
    public class Post : BaseDomain
    {        
        public string InstagramId { get; set; }
        public string InstagramUserId { get; set; }
        public string InstagramUserName { get; set; }
        public string Url { get; set; }
        public DateTime CreatedOn { get; set; }     
    }
}
