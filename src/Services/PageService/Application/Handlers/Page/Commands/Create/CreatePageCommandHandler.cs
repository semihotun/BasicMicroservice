using Application.IntegrationEvents.Page;
using AutoMapper;
using Domain.AggregateModels.PageAggregate;
using Domain.SeedWork;
using Domain.Utilities;
using Domain.Utilities.Helpers;
using EventBus.Base.Abstraction;
using MediatR;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Handlers.Page.Commands.Create
{
    public class CreatePageCommandHandler : IRequestHandler<CreatePageCommand, IResult>
    {
        private readonly IRepository _repository;
        private readonly IMapper _mapper;
        private readonly IEventBus _eventBus;

        public CreatePageCommandHandler(IRepository repository, IMapper mapper, IEventBus eventBus)
        {
            _repository = repository;
            _mapper = mapper;
            _eventBus = eventBus;
        }
        public async Task<IResult> Handle(CreatePageCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var data = _mapper.Map<Domain.AggregateModels.PageAggregate.Page>(request);
                var pageSeo = new PageSeo(request.PageSeoTag, request.PageDescription);
                data.AddPageSeo(pageSeo);
                _repository.Page.Add(data);
                await _repository.SaveEntitiesAsync();

                _eventBus.Publish(new SiteMapCreatedIntegrationEvent(StringHelpers.UrlFormatConverter(data.Header), data.Id));

                return new SuccessResult("Registered");
            }
            catch (System.Exception)
            {
                return new ErrorResult("Not Registered");
                throw;
            }
           
        }
    }
}
