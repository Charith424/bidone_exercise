using Repositories.Contracts;
using Repositories.Models;
using Repositories.RequestStatus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Repositories
{
    public class CustomerDetailsFromFile : ICustomerRepository
    {
        //private List<Customer> data = new List<Customer>();
        private  string _filePath ="";
        public CustomerDetailsFromFile(string fileCon="")
        {
            _filePath = fileCon;
        }

        // Delete a customer by their ID
        public async Task<Status> DeleteCustomer(int customerId)
        {
            try
            {

                var allCustomers = await LoadDataAsync();
                var customer = allCustomers.SingleOrDefault(x => x.Id == customerId);
                if (customer == null)
                {
                    return Status.NotFound;
                }
                allCustomers.Remove(customer);
                await SaveDataAsync(allCustomers);
                return Status.Succuess;
            }
            catch (FileNotFoundException)
            {
                return Status.NotFound;
            }
            catch (Exception)
            {
                return Status.Fail;
            }


        }

        // Get all customers
        public async Task<List<Customer>> GetAllCustomers()
        {
            return await LoadDataAsync();
        }

        //Get Customer by Customer Id

        public async Task<Customer?> GetCustomerById(int id)
        {
            var allCustomers = await LoadDataAsync();
            return allCustomers.SingleOrDefault(x => x.Id == id);
        }

        // Insert a new Customer
        public async Task<Status> InsertCustomer(Customer customer)
        {
            var allCustomers = await LoadDataAsync();
            allCustomers.Add(customer);
            return await SaveDataAsync(allCustomers);
        }

        //Update Customer by Customer Id
        public async Task<Status> UpdateCustomer(Customer customerDetail)
        {
            var allCustomers = await LoadDataAsync();
            var customer = allCustomers.SingleOrDefault(x => x.Id == customerDetail.Id);
            if (customer == null)
            {
                return Status.NotFound;
            }
            customer.FirstName = customerDetail.FirstName;
            customer.LastName = customerDetail.LastName;
            return await SaveDataAsync(allCustomers);

        }

        // Load customer data from the file
        private async Task<List<Customer>> LoadDataAsync()
        {
            if (File.Exists(_filePath))
            {
                using (var stream = File.OpenRead(_filePath))
                {
                    return await JsonSerializer.DeserializeAsync<List<Customer>>(stream);
                }
            }
            else
            {
                // if File not exit create the file
                using (var stream = File.Create(_filePath))
                {
                   
                    var initialData = new List<Customer>();
                    await JsonSerializer.SerializeAsync(stream, initialData);
                }

                // Read from file
                using (var stream = File.OpenRead(_filePath))
                {
                    return await JsonSerializer.DeserializeAsync<List<Customer>>(stream);
                }


            }           
        }

        // Save customer data to the file
        private async Task<Status> SaveDataAsync(List<Customer> customers)
        {
            try
            {
                using (var stream = File.Create(_filePath))
                {
                    await JsonSerializer.SerializeAsync(stream, customers);
                }
                return Status.Succuess;

            }
            catch (Exception ex)
            {
                return Status.Fail;
            }

        }
    }
}
