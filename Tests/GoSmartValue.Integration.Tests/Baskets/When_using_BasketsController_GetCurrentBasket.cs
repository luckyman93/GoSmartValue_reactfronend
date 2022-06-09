using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using AngleSharp.Html.Dom;
using AV.Contracts;
using GoSmartValue.Integration.Tests.Setup;
using GoSmartValue.Web;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace GoSmartValue.Integration.Tests.Baskets
{
    public class When_using_BasketsController_GetCurrentBasket : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;
        private readonly CustomWebApplicationFactory<Startup> _factory;

        public When_using_BasketsController_GetCurrentBasket(CustomWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
            _client = factory.CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            });
        }

        [Fact]
        public async Task Should_return_bad_response_if_user_not_authenticated()
        {
            // Arrange
            //Act
            var response = await _client.GetAsync(ApiUrlConstants.Basket.GET_CurrentBasket);
            var content = await HtmlHelpers.GetDocumentAsync(response);

            // Assert
            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }
    }
}
