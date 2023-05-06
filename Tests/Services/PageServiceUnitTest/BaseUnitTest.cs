using Application.Handlers.Page.Commands.Create;
using Application.Handlers.Page.Mapping;
using AutoMapper;
using Domain.SeedWork;
using EventBus.Base.Abstraction;
using FluentAssertions;
using Insfrastructure.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;

namespace PageServiceUnitTest
{
    public class BaseUnitTest
    {
        public IMapper _mapper;

        public PageDbContext _context;

        [SetUp]
        public virtual void SetUp()
        {
            if (_mapper == null)
            {
                var mockMapper = new MapperConfiguration(cfg =>
                {
                    cfg.AddProfile(new PageMappingProfile()); 
                });
                _mapper = mockMapper.CreateMapper();
            }
            if(_context != null)
            {
                var optionsBuilder = new DbContextOptionsBuilder<PageDbContext>();
                optionsBuilder.UseSqlServer(@"data source=127.0.0.1,1457;initial catalog=PageDb;persist security info=False;user id=sa;password=semihO123.;");
                _context = new PageDbContext(optionsBuilder.Options, null);
            }
        }

    
    }
}