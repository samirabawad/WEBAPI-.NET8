using APIDOCKER.Models;
using Microsoft.AspNetCore.Mvc;

namespace APIDOCKER.Services
{
    public interface ICustomerService
    {
        Task<IEnumerable<Customer>> GetAllCustomersAsync();
        Task <Customer> GetCustomerByIdAsync(int id);
        Task<Customer> UpdateCustomerByIdAsync(int id, Customer customer);
        Task<Customer> PostCustomerAsync(Customer customer);
        Task<Customer> DeleteCustomerByIdAsync(int id);

    }
}
