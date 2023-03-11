using Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.AggregateModels.PageAggregate
{
    public class PageSeo : BaseEntity
    {
        public string PageTag { get; private set; }
        public string PageDescription { get; private set; }

        public PageSeo()
        {
        }
        public PageSeo(string pageTag, string pageDescription)
        {
            PageTag = pageTag;
            PageDescription = pageDescription;
        }
    }
}
