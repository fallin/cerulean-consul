using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Cerulean.Consul.KeyValueStore;
using Cerulean.Consul.Tests.TestingUtilities;
using FluentAssertions;
using Moq;
using Newtonsoft.Json.Linq;
using NUnit.Framework;

namespace Cerulean.Consul.Tests
{
    [TestFixture]
    public class KeyValueOperationsTests
    {
        readonly EmbeddedResourceLoader _loader = new EmbeddedResourceLoader("TestCases");

        [Test]
        public async Task GetAllAsyncShouldReturnEmptyContentWhenNoKeys()
        {
            // Arrange
            var handler = new Mock<HttpMessageHandler>()
                .WithResponse(HttpStatusCode.NotFound, "");
            HttpClient client = new HttpClient(handler.Object) {BaseAddress = new Uri("http://localhost/")};
            var ops = new KeyValueOperations(client);

            // Act
            var response = await ops.GetAllAsync();

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
            response.Content.Should().BeEmpty();
        }

        [Test]
        public async Task GetAllAsyncShouldReturnContentWhenKeysExist()
        {
            // Arrange
            JToken json = _loader.LoadEmbeddedJson("KeyValueGetWithRecurseResponse");

            var handler = new Mock<HttpMessageHandler>()
                .WithResponse(HttpStatusCode.OK, json);
            HttpClient client = new HttpClient(handler.Object) { BaseAddress = new Uri("http://localhost/") };
            var ops = new KeyValueOperations(client);

            // Act
            var response = await ops.GetAllAsync();

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            response.Content.Should().NotBeNull().And.HaveCount(2);
        }

        [Test]
        public async Task GetAsyncShouldReturnNullContentWhenKeyDoesNotExist()
        {
            // Arrange
            var handler = new Mock<HttpMessageHandler>()
                .WithResponse(HttpStatusCode.NotFound, "");
            HttpClient client = new HttpClient(handler.Object) { BaseAddress = new Uri("http://localhost/") };
            var ops = new KeyValueOperations(client);

            // Act
            var response = await ops.GetAsync("does-not-exist");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
            response.Content.Should().BeEmpty();
        }

        [Test]
        public async Task GetAsyncShouldReturnContentWhenKeyExists()
        {
            // Arrange
            JToken json = _loader.LoadEmbeddedJson("KeyValueGetSingleKeyResponse");

            var handler = new Mock<HttpMessageHandler>()
                .WithResponse(HttpStatusCode.OK, json);
            HttpClient client = new HttpClient(handler.Object) { BaseAddress = new Uri("http://localhost/") };
            var ops = new KeyValueOperations(client);

            // Act
            var response = await ops.GetAllAsync();

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            response.Content.Should().NotBeNull().And.HaveCount(1);
        }
    }
}