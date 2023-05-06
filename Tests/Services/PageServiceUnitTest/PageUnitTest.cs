using Application.Handlers.Page.Commands.Create;
using Application.Handlers.Page.Mapping;
using AutoMapper;
using Domain.AggregateModels.PageAggregate;
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
    public class PageUnitTest: BaseUnitTest
    {

        [Test]
        public void Page_Created_BeTrue()
        {
            // Arrange
            var eventBus = new Mock<IEventBus>()
                .SetupAllProperties();

            var command = new CreatePageCommand()
            {
                Content = "TestContent",
                Header = "TestHeader",
                PageDescription = "TestPageDescription",
                PageSeoDescription = "TestPageSeoDescription",
                PageSeoTag = "TestPageSeoTag"
            };        
            var handler = new CreatePageCommandHandler(_context, _mapper, eventBus.Object);
            // Act
            var result = handler.Handle(command, new System.Threading.CancellationToken());
            // Assert
            result.Result.Success.Should().BeTrue();
        }

    }
}