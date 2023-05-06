using Domain.SeedWork;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Setup.Ioc;
using System;
using System.Security.Claims;

namespace Domain.AggregateModels.PageAggregate
{
    public class Page : BaseEntity, IAggregateRoot
    {
        public Guid UserId { get; private set; }
        public string Content { get; private set; }
        public string Header { get; private set; }
        public string PageDescription { get; private set; }
        public Guid PageSeoId { get; private set; }
        public PageSeo PageSeo { get; private set; }

        private IHttpContextAccessor _httpContextAccessor { get; set; }
        public Page()
        {
            AddUserId();
        }

        public Page(string content, string header, string pageDescription, Guid pageSeoId):this()
        {
            Content = content;
            Header = header;
            PageDescription = pageDescription;
            PageSeoId = pageSeoId;
        }

        public void AddPageSeo(PageSeo pageSeo)
        {
            PageSeo = pageSeo;
        }

        private void AddUserId()
        {
            try
            {
                _httpContextAccessor = ServiceTool.ServiceProvider.GetRequiredService<IHttpContextAccessor>();
                var userId = Guid.Parse(_httpContextAccessor?.HttpContext?.User?
                    .FindFirst(x => x.Type == ClaimTypes.NameIdentifier)?
                    .Value);
                UserId = userId;
            }
            catch (Exception){ }
     
        }

    }
}
