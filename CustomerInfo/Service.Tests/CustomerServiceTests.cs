using Castle.Core.Resource;
using Moq;
using Repositories.Contracts;
using Repositories.Models;
using Repositories.RequestStatus;
using Services;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Service.Tests
{
    public class CustomerServiceTests
    {
        [Fact]
        public async Task DeleteCustomer_WhenCalled_ReturnsDeleteStatusFromRepository()
        {
            // Arrange
            var customerId = 1;
            var status = Status.Succuess;

            var mockRepository = new Mock<ICustomerRepository>();
            mockRepository.Setup(repo => repo.DeleteCustomer(customerId)).ReturnsAsync(status);
            var customerService = new CustomerService(mockRepository.Object);
            // Act
            var result = await customerService.DeleteCustomer(customerId);

            // Assert
            Assert.Equal(status, result);
            // Verify that the Delete method was called
            mockRepository.Verify(repo => repo.DeleteCustomer(customerId), Times.Once());

      

        }


        [Fact]
        public async Task AllCustomers_WhenCalled_ReturnsAllCustomersFromRepository()
        {
            // Arrange
            var customers = new List<Customer> { /* create a list of customer objects */ };

            var mockRepository = new Mock<ICustomerRepository>();
            mockRepository.Setup(repo => repo.GetAllCustomers()).ReturnsAsync(customers);

            var customerService = new CustomerService(mockRepository.Object);

            // Act
            var result = await customerService.AllCustomers();

            // Assert
            Assert.Equal(customers, result);
            // Verify that the GetAllCustomers method was called
            mockRepository.Verify(repo => repo.GetAllCustomers(), Times.Once());
        }

        [Fact]
        public async Task GetCustomerById_WhenCalled_ReturnsCustomerDetailFromRepository()
        {
            //Arrange
            var customerId = 1;
            var customer = new Customer { Id = 1 ,FirstName ="Moc User", LastName ="Moc Name"};
            var mockRepository = new Mock<ICustomerRepository>();
            mockRepository.Setup(repo => repo.GetCustomerById(customerId)).ReturnsAsync(customer);
            var customerService = new CustomerService(mockRepository.Object);
            // Act
            var result = await customerService.GetCustomerById(customerId);

            // Assert
            Assert.Equal(customer, result);
            // Verify that the GetCustomerById method was called
            mockRepository.Verify(repo => repo.GetCustomerById(customerId), Times.Once());




        }


        [Fact]
        public async Task InsertCustomer_WhenCalled_GeneratesNewCustomerAndCallsRepository()
        {
            //Arrange
            var customer = new Customer { Id = 2, FirstName = "Moc User", LastName = "Moc Name" };
            var customerList = new List<Customer> { 
                new Customer{ Id = 1,FirstName="First Name" , LastName = "Last Name"}
            };
            var insertStatus = Status.Succuess;
            var mockRepository = new Mock<ICustomerRepository>();
            mockRepository.Setup(repo => repo.GetAllCustomers()).ReturnsAsync(customerList);
            mockRepository.Setup(repo => repo.InsertCustomer(customer)).ReturnsAsync(insertStatus);
            var customerService = new CustomerService(mockRepository.Object);

            // Act
            var result = await customerService.InsertCustomer(customer);

            // Assert
            // Ensure that the customer's ID has been incremented correctly
            Assert.Equal(customerList.Max(x => x.Id) + 1, customer.Id);

            // Verify that the InsertCustomer method was called
            mockRepository.Verify(repo => repo.InsertCustomer(customer), Times.Once());

            // Verify that the GetAllCustomers method was called
            mockRepository.Verify(repo => repo.GetAllCustomers(), Times.Once());
            // Assert the result
            Assert.Equal(insertStatus, result);

        }

        [Fact]
        public async Task UpdateCustomer_WhenCalled_ReturnsUpdateStatusFromRepository()
        {
            // Arrange
            
            var status = Status.Succuess;
            var customer = new Customer { Id = 2, FirstName = "Moc User", LastName = "Moc Name" };
            var mockRepository = new Mock<ICustomerRepository>();
            mockRepository.Setup(repo => repo.UpdateCustomer(customer)).ReturnsAsync(status);
            var customerService = new CustomerService(mockRepository.Object);
            // Act
            var result = await customerService.UpdateCustomer(customer);

            // Assert
            Assert.Equal(status, result);
            // Verify that the UpdateCustomer method was called
            mockRepository.Verify(repo => repo.UpdateCustomer(customer), Times.Once());

        }

    }
}
