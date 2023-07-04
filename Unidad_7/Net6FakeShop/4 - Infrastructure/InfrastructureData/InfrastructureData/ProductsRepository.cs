using DomainEntities;
using Microsoft.Extensions.Configuration;
using RepositoryContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace InfrastructureData
{
    public class ProductsRepository : IProductsRepository
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiRoute;

        public ProductsRepository(IConfiguration config)
        {
            _apiRoute = config.GetSection("ApiCalls:FakeStore").Value;

            _httpClient = new HttpClient();
        }

        public Product ? GetProduct(int productId)
        {
            HttpRequestMessage webRequest = new HttpRequestMessage(HttpMethod.Get, _apiRoute + productId);

            var response = _httpClient.Send(webRequest);

            using var reader = new StreamReader(response.Content.ReadAsStream());

            string content = reader.ReadToEnd();

            return JsonSerializer.Deserialize<Product>(content);

        }
    }
}
