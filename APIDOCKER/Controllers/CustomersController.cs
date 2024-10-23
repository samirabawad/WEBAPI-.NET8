using APIDOCKER.Database;
using APIDOCKER.Models;
using APIDOCKER.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace APIDOCKER.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {

        private readonly ICustomerService _icustomerService;


        public CustomersController(ICustomerService icustomerService)
        {
            _icustomerService = icustomerService;
        }



        //Lista todos los clientes de la tabla Customer de manera asíncrona.
        [HttpGet]
        public async Task<IActionResult> GetAllCustomers()
        {
            var customers = await _icustomerService.GetAllCustomersAsync();
            Console.WriteLine("EN CONTROLER");
            return Ok(customers);
        }




        //Lista cliente por su id, de la tabla Customer de manera asíncrona.
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var customer = await _icustomerService.GetCustomerByIdAsync(id);
            return Ok(customer);    
        }



        // Crea un nuevo Cliente.
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Customer customer)
        {
            var createdCustomer = await _icustomerService.PostCustomerAsync(customer);
            return Ok(createdCustomer);
        }



        //Actualiza un Cliente mediante su id
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Customer customer)
        {
            var updatedCustomer = await _icustomerService.UpdateCustomerByIdAsync(id, customer);
            return Ok(updatedCustomer);
        }





        // DELETE api/<CustomersController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deletedCustomer = await _icustomerService.DeleteCustomerByIdAsync(id);
            return Ok();
        }
    }
}
