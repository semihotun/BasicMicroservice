using Application.Handlers.Page.Commands.Create;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Handlers.Page.Mapping
{
    public class PageMappingProfile : Profile
    {
        public PageMappingProfile()
        {
            CreateMap<Domain.AggregateModels.PageAggregate.Page, CreatePageCommand>().ReverseMap();
        }
    }
}
