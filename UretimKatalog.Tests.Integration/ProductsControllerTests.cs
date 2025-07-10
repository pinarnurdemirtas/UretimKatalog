using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;
using UretimKatalog.Api;

namespace UretimKatalog.Tests.Integration
{
    public class ProductsControllerTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;

        public ProductsControllerTests(WebApplicationFactory<Program> factory)
            => _factory = factory;

        [Fact]
        public async Task Get_AllProducts_ReturnsOkAndJson()
        {
            var client = _factory.CreateClient();
            var response = await client.GetAsync("/api/products");

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            response.Content.Headers.ContentType.MediaType
                .Should().Be("application/json");
        }
    }
}
