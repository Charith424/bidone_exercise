using Repositories.Models;
using Repositories.RequestStatus;

namespace Repositories.Contracts
{
    public interface ICustomerRepository
    {
        Task<List<Customer>> GetAllCustomers();

        Task<Customer?> GetCustomerById(int id);

        Task<Status> InsertCustomer(Customer customer);
        Task<Status> UpdateCustomer(Customer customer);

        Task<Status> DeleteCustomer(int CustomerId);
    }
}
