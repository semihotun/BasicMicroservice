using EventBus.Base.Abstraction;
using SiteMapService.SeedWork;
using System.Threading.Tasks;

namespace SiteMapService.IntegrationEvents
{
    public class SiteMapCreatedIntegrationEventHandler :IIntegrationEventHandler<SiteMapCreatedIntegrationEvent>
    {
        private readonly IRepository _repository;

        public SiteMapCreatedIntegrationEventHandler(IRepository repository)
        {
            _repository = repository;
        }

        public async Task Handle(SiteMapCreatedIntegrationEvent integrationEvent)
        {
            _repository.SiteMap.Add(new AggregateModels.SiteMap(integrationEvent.SiteMapLink, integrationEvent.PageId));
            await _repository.SaveEntitiesAsync();
        }
    }
}
