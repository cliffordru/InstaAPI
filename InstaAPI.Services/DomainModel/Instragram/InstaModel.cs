using System.Collections.Generic;

namespace InstaAPI.Services.DomainModel.Instragram
{
    public class Meta
    {
        public int Code { get; set; }
    }
    public class User
    {
        public string Id { get; set; }
        public string Username { get; set; }
    }

    public class InstaPostData
    {
        public string Link { get; set; }

        public string Id { get; set; }

        public User User { get; set; }
    }

    public class InstaPostsRoot
    {
        public Meta InstaMeta { get; set; }
        public List<InstaPostData> Data { get; set; }
    }

    public class InstaPostRoot
    {
        public InstaPostData Data { get; set; }
    }

}
