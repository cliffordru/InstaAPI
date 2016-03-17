using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
