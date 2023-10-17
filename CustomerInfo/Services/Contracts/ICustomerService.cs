using Repositories.Models;
using Repositories.RequestStatus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contracts
{
    public interface ICustomerService
    {
        Task<List<Customer>> AllCustomers();

        Task<Customer?> GetCustomerById(int id);

        Task<Status> InsertCustomer(Customer customer);
        Task<Status> UpdateCustomer(Customer customer);

        Task<Status> DeleteCustomer(int CustomerId);
    }
}
