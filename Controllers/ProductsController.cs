using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SampleProductInventoryApi.Models;

namespace SampleProductInventoryApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProductsController(AppDbContext context)
        {
            _context = context;
            context.Database.EnsureCreated();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts([FromQuery] ProductQueryParameters query)
        {
            var products =  _context.Products.AsQueryable();
           
            if (query.MinPrice != null)
                products = products.Where(p => p.Price >= query.MinPrice);
            if (query.MaxPrice != null)
                products = products.Where(p => p.Price <= query.MaxPrice);

            if (!string.IsNullOrEmpty(query.SearchTerm))
                products = products.Where(p => p.Name.ToLower().Contains(query.SearchTerm.ToLower()) ||
                        p.Sku.ToLower().Contains(query.SearchTerm.ToLower())
                );

            if (!string.IsNullOrEmpty(query.Name))
                products = products.Where(p => p.Name.ToLower().Contains(query.Name.ToLower()));
            if (!string.IsNullOrEmpty(query.Sku))
                products = products.Where(p => p.Sku == query.Sku);

            if (!string.IsNullOrEmpty(query.SortBy))
            {
                if(typeof(Product).GetProperty(query.SortBy) != null)
                {
                    products = products.OrderByCustom(query.SortBy, query.SortOrder);
                }
            }
                products = products.Where(p => p.Name.ToLower().Contains(query.Name.ToLower()));
            if (!string.IsNullOrEmpty(query.Sku))
                products = products.Where(p => p.Sku == query.Sku);

            products = _context.Products
                .Skip(query.Size * (query.Page - 1))
                .Take(query.Size);
            return await products.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            return product;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(int id, Product product)
        {
            if (id != product.Id)
            {
                return BadRequest();
            }

            _context.Entry(product).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(id))
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

        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProduct", new { id = product.Id }, product);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProductExists(int id)
        {
            return (_context.Products?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
