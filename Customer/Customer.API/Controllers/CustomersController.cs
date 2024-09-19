using AutoMapper;
using Customer.API.CustomActionFilters;
using Customer.API.Data;
using Customer.API.Models.Domain;
using Customer.API.Models.DTO;
using Customer.API.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Customer.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly CustomerDBContext _dbContext;
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;

        public CustomersController(CustomerDBContext dBContext, ICustomerRepository customerRepository, IMapper mapper)
        {
            this._dbContext = dBContext;
            this._customerRepository = customerRepository;
            this._mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllCustomers()
        {
            var customers =  await _customerRepository.GetAllCustomersAsync();

            var customersDto = _mapper.Map<List<CustomerDTO>>(customers);

            return Ok(customersDto);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetCustomerById([FromRoute] Guid id)
        {
            var customer = await _customerRepository.GetCustomerByIdAsync(id);
            if (customer == null) 
            {
                return NotFound();
            }

            var customerDto = _mapper.Map<CustomerDTO>(customer);

            return Ok(customerDto);
        }

        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> AddCustomer([FromBody] AddCustomerRequestDto addCustomerRequestDto)
        {
            var customer = _mapper.Map<CustomerDB>(addCustomerRequestDto);
            customer = await _customerRepository.AddCustomerAsync(customer);

            var customerDto = _mapper.Map<CustomerDTO>(customer);

            return CreatedAtAction(nameof(GetCustomerById), new { id = customer.Id }, customerDto);
           
        }

        [HttpPut]
        [Route("{id:Guid}")]
        [ValidateModel]
        public async Task<IActionResult> UpdateCustomer([FromRoute] Guid id, [FromBody] UpdateCustomerRequestDto updateCustomerRequestDto)
        {
            var customer = _mapper.Map<CustomerDB>(updateCustomerRequestDto);
            customer = await _customerRepository.UpdateCustomerAsync(id, customer);
            if (customer == null)
            {
                return NotFound();
            }

            var customerDto = _mapper.Map<CustomerDTO>(customer);

            return Ok(customerDto);
            
        }
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteCustomer([FromRoute] Guid id)
        {
            var customer = await _customerRepository.DeleteCustomerByIdAsync(id);
            if (customer == null)
            {
                return NotFound();
            }

            var customerDto = _mapper.Map<CustomerDTO>(customer);

            return Ok(customerDto);
        }
    }
}
