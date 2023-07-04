using DomainEntities;
using JSonModels;
using Microsoft.Extensions.Configuration;
using RepositoryContracts;
using System.Text.Json;


namespace InfrastructureData
{
    public class CustomersRepository : ICustomersRepository
    {
        private readonly HttpClient _httpClient;

        private readonly string _apiRoute;

        private readonly string _fileRoute;
    
        public CustomersRepository(IConfiguration config)
        {
            _httpClient = new HttpClient();

            _apiRoute = config.GetSection("ApiCalls:RandomUsers").Value;

            _fileRoute = config.GetSection("PathFiles:CustomerPathFile").Value;

            //string temp = config.GetSection("PathFiles:CustomerPathFile").Value;

            //_fileRoute = Path.Combine(hostEnviroment.ContentRootPath, temp);

            
        }

        public bool ComprobeNotExist(List<Customer> customers)
        {
            string request = File.ReadAllText(_fileRoute);

            if (!string.IsNullOrEmpty(request))
            {
                List<Customer> actualCustomers = JsonSerializer.Deserialize<List<Customer>>(request);

                foreach (Customer actualCustomer in actualCustomers)
                {
                    if (customers.FirstOrDefault(e => e.Id == actualCustomer.Id) != null)
                    {
                        return false;
                    }
                }

            }
                
            return true; 
        }
        public bool GenerateUsers(List<Customer> customers)
        {

            foreach (Customer customer in customers)
            {
                JSonModelRandomUser ? randomUser = CallApi();

                if (randomUser != null)
                {

                    customer.CustomerName = randomUser. first_name;
                    customer.CustomerSurname = randomUser.last_name;

                }
            }

            PersistFile(customers);

            return true; 
        }

        private JSonModelRandomUser ? CallApi()
        {
            HttpRequestMessage webRequest = new HttpRequestMessage(HttpMethod.Get, _apiRoute);

            var response = _httpClient.Send(webRequest);

            using var reader = new StreamReader(response.Content.ReadAsStream());

            string content = reader.ReadToEnd();

            return JsonSerializer.Deserialize<JSonModelRandomUser>(content);

        }

        private void PersistFile(List<Customer> customers)
        {
            string actualCustomersAsString = File.ReadAllText(_fileRoute);

            if (!string.IsNullOrEmpty(actualCustomersAsString))
            {
                List<Customer> actualCustomers = JsonSerializer.Deserialize<List<Customer>>(actualCustomersAsString);

                customers.ForEach(e => actualCustomers.Add(e)); // todo arreglar 

                string jSonCustomers = JsonSerializer.Serialize(actualCustomers.OrderBy(e => e.Id));

                File.WriteAllText(_fileRoute, jSonCustomers);
            }
            else
            {
                string customersList = JsonSerializer.Serialize(customers);

                File.WriteAllText(_fileRoute, customersList);
            }        

        }
    }
}