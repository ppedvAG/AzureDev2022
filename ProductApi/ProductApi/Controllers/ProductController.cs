using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json.Linq;
using ProductApi.Model;

namespace ProductApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        EfContext _context = new();

        // GET: api/<ProductController>
        [HttpGet]
        public IEnumerable<Product> Get()
        {
            return _context.Products.ToList();
            // yield return new Product() { Id = 1, Name = "Test1", Price = 12.7m };
            // yield return new Product() { Id = 2, Name = "Test2", Price = 27m };
            // yield return new Product() { Id = 3, Name = "Test3", Price = 1.67m };
        }

        // GET api/<ProductController>/5
        [HttpGet("{id}")]
        public Product? Get(int id)
        {
            return _context.Products.Find(id);

            //todo
            var result = _context.Products.Find(id) ?? null;

            if (result == null)
                return null;
            else
                return result;
        }

        // POST api/<ProductController>
        [HttpPost]
        public void Post([FromBody] Product value)
        {
            _context.Products.Add(value);
            _context.SaveChanges();
        }
        public class ProductIMPORT
        {
            public string Name { get; set; }
            public string Category { get; set; }
            public string Material { get; set; }
        }


        [HttpPost("Import")]
        public void Import([FromBody] ProductIMPORT import)
        {
            var prod = new Product() { Name = import.Name, Category = import.Category };
            prod.Id = _context.Products.Max(x => x.Id) + 1;

            _context.Products.Add(prod);
            _context.SaveChanges();
        }

        // PUT api/<ProductController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Product value)
        {
            _context.Products.Update(value);
            _context.SaveChanges();
        }

        // DELETE api/<ProductController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _context.Products.Remove(Get(id));
            _context.SaveChanges();
        }
    }
}
