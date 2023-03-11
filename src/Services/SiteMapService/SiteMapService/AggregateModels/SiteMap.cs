using SiteMapService.SeedWork;
using System;

namespace SiteMapService.AggregateModels
{
    public class SiteMap : BaseEntity, IAggregateRoot
    {
        public string SiteMapLink { get; set; }

        public Guid PageId { get; set; }

        public SiteMap(string siteMapLink, Guid pageId)
        {
            SiteMapLink = siteMapLink;
            PageId = pageId;
        }
    }
}
