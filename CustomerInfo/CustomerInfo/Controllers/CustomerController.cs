using CustomerInfo.Models;
using Microsoft.AspNetCore.Mvc;
using Repositories.Models;
using Repositories.RequestStatus;
using Services.Contracts;

namespace CustomerInfo.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ICustomerService _customerService;
        private readonly ILogger<CustomerController> _logger;
        public CustomerController(ICustomerService customerService, ILogger<CustomerController> logger)
        {
            _customerService = customerService;
            _logger = logger;
        }

        //List All users
        public async Task<IActionResult> Index()
        {
            var allCustomers = await _customerService.AllCustomers();
            return View(getCustomerViewModelFromCustomer(allCustomers));
        }

        //Show Customer Details
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var customer = await _customerService.GetCustomerById((int)id);


            if (customer == null)
            {
                return NotFound();
            }
            return View("CustomerDetails", getCustomerViewModelFromCustomer(customer));


        }

        public async Task<IActionResult> Create()
        {
            return View();
        }

        //Create a new Customer
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FirstName,LastName")] CustomerDetailViewModel customerDetail)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var customer= new Customer
                    {
                        FirstName = customerDetail.FirstName,
                        LastName = customerDetail.LastName,
                    };
                    await _customerService.InsertCustomer(customer);
                    return RedirectToAction(nameof(Index));
                }
            }
            catch
            {
                ModelState.AddModelError("", "Error While saving the content");
            }
            return View(customerDetail);

        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var customer = await _customerService.GetCustomerById((int)id);


            if (customer == null)
            {
                return NotFound();
            }
            return View("EditCustomer", getCustomerViewModelFromCustomer(customer));


        }


        // Edit customer by Customer ID
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPost(int? id, [Bind("CustomerId,FirstName,LastName")] CustomerDetailViewModel customerDetail)
        {
           
            if (id == null)
            {
                return NotFound();
            }
            try
            {
                if (ModelState.IsValid)
                {
                    var updatedCustomer = new Customer
                    {
                        Id = (int)id,
                        FirstName = customerDetail.FirstName,
                        LastName = customerDetail.LastName,
                    };
                    await _customerService.UpdateCustomer(updatedCustomer);
                    return RedirectToAction(nameof(Index));
                }
            }
            catch
            {
                ModelState.AddModelError("", "Error While saving the content");
            }


            return View("EditCustomer", customerDetail);

        }

        // Delete customer by Customer ID       
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var status = await _customerService.DeleteCustomer((int)id);
            if (status == Status.NotFound) {
                return NotFound();
            }else if(status == Status.Fail){
                ViewData["ErrorMessage"] = "Customer Deletion dailed Please try again";
            }
            return RedirectToAction(nameof(Index)); 


        }

        private CustomerDetailViewModel getCustomerViewModelFromCustomer(Customer customer) {
            return new CustomerDetailViewModel
            {
                CustomerId = customer.Id,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
            };        
        }

        private Customer getCustomerFromCustomerDetailViewModel(CustomerDetailViewModel customer)
        {
            return new Customer
            {
                Id = customer.CustomerId,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
            };
        }

        private IEnumerable<CustomerDetailViewModel> getCustomerViewModelFromCustomer(List<Customer> customer)
        {
            return customer.Select( x=>  new CustomerDetailViewModel
            {
                CustomerId = x.Id,
                LastName= x.LastName,
                FirstName = x.FirstName,
            }
            );

            
        }

    }
}
