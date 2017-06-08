using BusinessLogic.BotConfiguration.QueryDomainBuilder;
using BusinessLogic.Entities.FactoryDomain;
using BusinessLogic.Entities.Infrastructure;
using BusinessLogic.Infrastructure.DAL;
using BusinessLogic.Models;
using BusinessLogic.Services;
using InteractiveBot.BotConnector.Controllers;
using Microsoft.Bot.Connector;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Yandex.Speller.Api;

namespace InteractiveBot.Tests.ControllersTests
{
    [TestFixture]
    public class MessageControllerTest
    {
        [Test]
        public void GetListOfProducts_WhenProductsExist_ShouldReturnListOfProducts()
        {
            // Arrange
            var messageRepoMock = new Mock<IRepository<Message>>();
            var matcherProviderNock = new Mock<EntityMatcherProvider>(
                new EntityMatcherProvider(Array.Empty<EntityMatcher>().AsEnumerable()));
            var databaseCommandContext = new Mock<IDatabaseCommandContext>();

            var messageService = new MessageService(
                messageRepoMock.Object, 
                matcherProviderNock.Object, 
                databaseCommandContext.Object);

            // Act
            var result = messageService.GetListOfProducts(new EntityMatcher(typeof(Product)));

            // Assert
            Assert.That(result, Is.EquivalentTo(new List<Product>()));
        }
    }
}
