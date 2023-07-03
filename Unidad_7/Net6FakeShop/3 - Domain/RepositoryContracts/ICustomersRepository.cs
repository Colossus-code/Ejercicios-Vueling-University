using DomainEntities;

namespace RepositoryContracts
{
    public interface ICustomersRepository
    {
        bool GenerateUsers(List<Customer> customers); 
    }
}