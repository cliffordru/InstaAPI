using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InstaAPI.Services.DomainModel;

namespace InstaAPI.Services.DatabaseContext
{
    public class FavoriteFluentMap : EntityTypeConfiguration<Favorite>
    {
        public FavoriteFluentMap()
        {
            Property(m => m.Id).IsRequired();
            HasKey(m => m.Id);
            Property(m => m.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

        }
    }
}
