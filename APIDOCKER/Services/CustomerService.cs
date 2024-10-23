using APIDOCKER.Database;
using APIDOCKER.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using APIDOCKER.Middleware;

namespace APIDOCKER.Services
{
    public class CustomerService : ICustomerService
    {


        //Asignamos el contexto
        private readonly ApplicationDbContext _context;
        public CustomerService(ApplicationDbContext context)
        {
            _context = context;
        }




        public async Task<IEnumerable<Customer>> GetAllCustomersAsync()
        {
            try
            {
                var result = await _context.Customers.ToListAsync();
                Console.WriteLine("EN TRY");
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine("EN EXP");
                throw new InvalidOperationException("An error occurred while getting all customers.", ex);
            }
        }



        public async Task<Customer> GetCustomerByIdAsync(int id)
        {
            try
            {
               var result = await _context.Customers.FindAsync(id);
                if (result == null) {
                    throw new KeyNotFoundException($"Customer with ID {id} not found.");
                }
                return result;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("An error occurred while getting the customer.", ex);
            }
        }



        public async Task<Customer> UpdateCustomerByIdAsync(int id, Customer customer)
        {
            try
            {
                var customerInfo = await _context.Customers.FindAsync(id);
                if (customerInfo == null)
                {
                    throw new KeyNotFoundException($"Customer with ID {id} not found.");
                }

                customerInfo.Name = customer.Name;
                customerInfo.Email = customer.Email;

                _context.Attach(customerInfo);
                await _context.SaveChangesAsync();

                return customerInfo;
            }
            catch(Exception ex)
            {
                throw new InvalidOperationException("An error occurred while updating the customer.", ex);
            }
        }



        public async Task<Customer> PostCustomerAsync(Customer customer) {
            try
            {
                if (customer == null){
                    throw new ArgumentNullException(nameof(customer), "Customer cannot be null.");
                }
                await _context.Customers.AddAsync(customer);
                await _context.SaveChangesAsync();
                return customer;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("An error occurred while saving the customer.", ex);
            }
        }

        public async Task<Customer> DeleteCustomerByIdAsync(int id) {
            try
            {
                var customerInfo = await _context.Customers.FindAsync(id);
                if (customerInfo == null)
                {
                    throw new KeyNotFoundException($"Customer with ID {id} not found.");
                }

                _context.Customers.Remove(customerInfo);
                await _context.SaveChangesAsync();
                return customerInfo;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("An error occurred while deleting the customer.", ex);
            }
        }
    }
}
