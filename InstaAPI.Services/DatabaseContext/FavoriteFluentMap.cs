using InstaAPI.Services.DomainModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;

namespace InstaAPI.Services.DatabaseContext
{
    public class FavoriteFluentMap : BaseMap<Favorite> // EntityTypeConfiguration
    {
        public FavoriteFluentMap()
        {
            Property(m => m.Id).IsRequired();
            HasKey(m => m.Id);
            Property(m => m.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(m => m.UserId).HasMaxLength(UserIdMaxLen).IsRequired();
            Property(m => m.InstagramId).HasMaxLength(InstagramIdMaxLen).IsRequired();
            Property(m => m.TagName).HasMaxLength(TagNameMaxLen).IsRequired();

            const string ixName = "IX_InstagramId_UserId_TagName_Unique";
            Property(m => m.InstagramId)
                .HasColumnAnnotation("Index",
                    new IndexAnnotation(new IndexAttribute(ixName, 1) { IsUnique = true }));
            Property(m => m.UserId)
                .HasColumnAnnotation("Index",
                    new IndexAnnotation(new IndexAttribute(ixName, 2) { IsUnique = true }));
            Property(m => m.TagName)
                .HasColumnAnnotation("Index",
                    new IndexAnnotation(new IndexAttribute(ixName, 3) { IsUnique = true }));
        }
    }

    public class PostFluentMap : BaseMap<Post>
    {
        public PostFluentMap()
        {
            Property(m => m.InstagramId).HasMaxLength(InstagramIdMaxLen).IsRequired();
            HasKey(m => m.InstagramId);
            Property(m => m.InstagramUserId).HasMaxLength(InstagramUserIdMaxLen).IsRequired();
            Property(m => m.InstagramUserName).HasMaxLength(InstagramUserNameMaxLen).IsRequired();
        }
    }
}
