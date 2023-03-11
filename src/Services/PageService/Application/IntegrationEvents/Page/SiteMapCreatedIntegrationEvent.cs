using EventBus.Base.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.IntegrationEvents.Page
{
    public class SiteMapCreatedIntegrationEvent : IntegrationEvent
    {
        public string SiteMapLink { get; set; }

        public Guid PageId { get; set; }

        public SiteMapCreatedIntegrationEvent(string siteMapLink, Guid pageId)
        {
            SiteMapLink = siteMapLink;
            PageId = pageId;
        }
    }
}
