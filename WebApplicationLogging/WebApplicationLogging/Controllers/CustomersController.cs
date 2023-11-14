using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplicationLogging.Models;
using WebApplicationLogging.Models.DTO;

namespace WebApplicationLogging.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly NorthwindContext _context;
        private readonly WebApplicationLogging.Loggings.ILoggerManager _logger;

        public CustomersController(NorthwindContext context, WebApplicationLogging.Loggings.ILoggerManager logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: api/Customers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Customer>>> GetCustomers()
        {
            _logger.LogInfo("User get all Customer");

          if (_context.Customers == null)
          {
              return NotFound();
          }
            return await _context.Customers.ToListAsync();
        }

        // GET: api/Customers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Customer>> GetCustomer(string id)
        {
          if (_context.Customers == null)
          {
              return NotFound();
          }
            var customer = await _context.Customers.FindAsync(id);

            if (customer == null)
            {
                return NotFound();
            }

            return customer;
        }

        // PUT: api/Customers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCustomer(string id, Customer customer)
        {
            if (id != customer.CustomerId)
            {
                return BadRequest();
            }

            _context.Entry(customer).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Customers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Customer>> PostCustomer(CustomerDTO customer)
        {
            // Convert product DTO to Product : Mapping
            Customer newCustomer = new Customer();
            newCustomer.CompanyName = customer.CompanyName;
            newCustomer.ContactName = customer.ContactName;
            newCustomer.ContactTitle = customer.ContactTitle;
            newCustomer.Address = customer.Address;
            newCustomer.City = customer.City;
            newCustomer.Region = customer.Region;
            newCustomer.PostalCode = customer.PostalCode;
            newCustomer.Country = customer.Country;
            newCustomer.Phone = customer.Phone;
            newCustomer.Fax = customer.Fax;

            if (_context.Customers == null)
            {
                return Problem("Entity set 'NorthwindContext.Cutomers'  is null.");
            }
            _context.Customers.Add(newCustomer);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCustomer", new { id = newCustomer.CustomerId }, newCustomer);
        }


        // DELETE: api/Customers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(string id)
        {
            if (_context.Customers == null)
            {
                return NotFound();
            }
            var customer = await _context.Customers.FindAsync(id);
            if (customer == null)
            {
                return NotFound();
            }

            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CustomerExists(string id)
        {
            return (_context.Customers?.Any(e => e.CustomerId == id)).GetValueOrDefault();
        }
    }
}
