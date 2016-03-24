using System.Data.Entity.ModelConfiguration;

namespace InstaAPI.Services.DatabaseContext
{
    public abstract class BaseMap<T> : EntityTypeConfiguration<T> where T : class
    {
        protected const int InstagramIdMaxLen = 256;
        protected const int InstagramUserIdMaxLen = 256;
        protected const int InstagramUserNameMaxLen = 30;
        protected const int UserIdMaxLen = 128;
        protected const int TagNameMaxLen = 100;
    }
}
