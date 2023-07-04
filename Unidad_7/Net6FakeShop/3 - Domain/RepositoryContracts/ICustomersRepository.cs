using DomainEntities;

namespace RepositoryContracts
{
    public interface ICustomersRepository
    {
        bool GenerateUsers(List<Customer> customers);

        bool ComprobeNotExist(List<Customer> customers);

        Customer FoundUserById(int id);
    }
}