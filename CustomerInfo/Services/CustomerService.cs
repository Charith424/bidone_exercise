using Repositories.Contracts;
using Repositories.Models;
using Repositories.RequestStatus;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;

        }
        public async Task<Status> DeleteCustomer(int CustomerId)
        {
            return await _customerRepository.DeleteCustomer(CustomerId);
        }

        public async Task<List<Customer>> AllCustomers()
        {
           return await _customerRepository.GetAllCustomers(); ;
            
        }

        public async Task<Customer?> GetCustomerById(int id)
        {
           return await _customerRepository.GetCustomerById(id);
        }

        public async Task<Status> InsertCustomer(Customer customer)
        {
            var customerDetails = await _customerRepository.GetAllCustomers();
            if(customerDetails.Count != 0)
            {
                customer.Id = customerDetails.Max(x => x.Id) + 1;
            }
            else
            {
                customer.Id = 1;
            }
            
            return await _customerRepository.InsertCustomer(customer);
        }

        public async Task<Status> UpdateCustomer(Customer customer)
        {
            return await _customerRepository.UpdateCustomer(customer);
        }

    }
}
