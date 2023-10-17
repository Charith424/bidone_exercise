using Moq;
using Repositories.Contracts;
using Repositories.Models;
using Repositories.RequestStatus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;

namespace Repositories.Test
{
    public class CustomerDetailsFromFileTest
    {
        private const string testFilePath = "test_data.json";
        private List<Customer> customers = new List<Customer>
        {
            new Customer { Id = 1, FirstName = "John", LastName = "Doe" },
            new Customer { Id = 2, FirstName = "Jane", LastName = "Smith" },
        };
        private void CreateTestDataJsonFile()
        {
            // Create your test data as a list of customers
            var testCustomers = customers;

            // Serialize and write the test data to a JSON file
            var json = JsonSerializer.Serialize(testCustomers);
            File.WriteAllText(testFilePath, json);
        }

        [Fact]
        public async Task DeleteCustomer_ExistingCustomer_ShouldReturnSuccess()
        {
            // Arrange
            CreateTestDataJsonFile(); // Create the test data JSON file
            var customerRepository = getRepositoryInstence();

            var existingCustomerId = 1;

            // Act
            var result = await customerRepository.DeleteCustomer(existingCustomerId);

            // Assert
            Assert.Equal(Status.Succuess, result);
        }

        [Fact]
        public async Task DeleteCustomer_NonExistingCustomer_ShouldReturnNotFound()
        {
            // Arrange
            CreateTestDataJsonFile(); // Create the test data JSON file
            var customerRepository = getRepositoryInstence();
            var nonExistingCustomerId = 999;

            // Act
            var result = await customerRepository.DeleteCustomer(nonExistingCustomerId);

            // Assert
            Assert.Equal(Status.NotFound, result);
        }

        [Fact]
        public async Task GetAllCustomers_ShouldReturnAllCustomers()
        {
            // Arrange
            CreateTestDataJsonFile(); // Create the test data JSON file
            var customerRepository = getRepositoryInstence();

            // Act
            var result = await customerRepository.GetAllCustomers();

            // Assert
            Assert.Equal(customers.Count, result.Count);
        }

        [Fact]
        public async Task GetCustomerById_ShouldReturnCustomers()
        {
            // Arrange
            var customerId = 1;
            CreateTestDataJsonFile(); // Create the test data JSON file
            var customerRepository = getRepositoryInstence();
            var customerDetail = customers.FirstOrDefault(x => x.Id == customerId);

            // Act
            var result = await customerRepository.GetCustomerById(customerId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(customerId, result.Id);
            

        }

        [Fact]
        public async Task GetCustomerById_Non_Existence_User_ShouldReturnNull()
        {
            // Arrange
            var customerId = 9999;
            CreateTestDataJsonFile(); // Create the test data JSON file
            var customerRepository = getRepositoryInstence();
            var customerDetail = customers.FirstOrDefault(x => x.Id == customerId);

            // Act
            var result = await customerRepository.GetCustomerById(customerId);

            // Assert
            Assert.Null(result);      

        }


        [Fact]
        public async Task InsertCustomer_WhenInserNewUser_ShouldReturnStatusCode()
        {
            // Arrange
            var newCustomer = new Customer { Id = 3, FirstName = "Alice", LastName = "Johnson" };
            CreateTestDataJsonFile(); // Create the test data JSON file
            var customerRepository = getRepositoryInstence();

            // Act
            var result = await customerRepository.InsertCustomer(newCustomer);

            // Assert
            Assert.Equal(result, Status.Succuess);
            var inserCustomerDetail = customerRepository.GetAllCustomers().Result.FirstOrDefault( x=> x.Id == newCustomer.Id);
            Assert.NotNull(inserCustomerDetail);           

        }

        [Fact]
        public async Task UpdateCustomerr_WhenUpdateUser_ShouldReturnStatusCode()
        {
            // Arrange
            var updateCustomer = new Customer { Id = 2, FirstName = "Alice", LastName = "Johnson" };
            CreateTestDataJsonFile(); // Create the test data JSON file
            var customerRepository = getRepositoryInstence();
          

            // Act
            var result = await customerRepository.UpdateCustomer(updateCustomer);

            // Assert
            Assert.Equal(result, Status.Succuess);
            var inserCustomerDetail = customerRepository.GetAllCustomers().Result.FirstOrDefault(x => x.Id == updateCustomer.Id);
            Assert.NotNull(inserCustomerDetail);

        }

        private CustomerDetailsFromFile getRepositoryInstence() { 
            return new CustomerDetailsFromFile(testFilePath);

        }




    }
}
