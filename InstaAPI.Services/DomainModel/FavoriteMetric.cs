using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstaAPI.Services.DomainModel
{
    public class FavoriteMetric:BaseDomain
    {
        public string TagName { get; set; }
        public int Count { get; set; }
    }
}
