using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PUNDERO.Models;

namespace PUNDERO.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class InvoiceProductController : ControllerBase
    {
        private readonly PunderoContext _context;

        public InvoiceProductController(PunderoContext context)
        {
            _context = context;
        }

        // GET: api/InvoiceProducts
        [HttpGet]
        public IActionResult GetInvoiceProducts()
        {
            var invoiceProducts = _context.InvoiceProducts.OrderByDescending(ip => ip.IdInvoiceProduct).ToList();
            return Ok(invoiceProducts);
        }

        //// GET: api/InvoiceProduct/5
        //[HttpGet("{id}")]
        //public IActionResult GetInvoiceProduct(int id)
        //{
        //    var invoiceProduct = _context.InvoiceProducts.FirstOrDefault(ip => ip.IdInvoiceProduct == id);

        //    if (invoiceProduct == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(invoiceProduct);
        //}
        [HttpGet("{idInvoice}")]
        public IActionResult GetInvoiceProducts(int idInvoice)
        {
            var invoiceProducts = _context.InvoiceProducts
              .Where(ip => ip.IdInvoice == idInvoice)
              .ToList();

            if (invoiceProducts.Count == 0)
            {
                return NotFound();
            }

            return Ok(invoiceProducts);
        }

        // POST: api/InvoiceProduct
        [HttpPost]
        public IActionResult PostInvoiceProduct([FromBody] InvoiceProduct invoiceProduct)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.InvoiceProducts.Add(invoiceProduct);
            _context.SaveChanges();

            return CreatedAtRoute("GetInvoiceProduct", new { id = invoiceProduct.IdInvoiceProduct }, invoiceProduct);
        }

        // PUT: api/InvoiceProduct/5
        [HttpPut("{id}")]
        public IActionResult PutInvoiceProduct(int id, [FromBody] InvoiceProduct invoiceProduct)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != invoiceProduct.IdInvoiceProduct)
            {
                return BadRequest();
            }

            _context.Entry(invoiceProduct).State = EntityState.Modified;
            _context.SaveChanges();

            return NoContent();
        }

        // DELETE: api/InvoiceProduct/5
        [HttpDelete("{id}")]
        public IActionResult DeleteInvoiceProduct(int id)
        {
            var invoiceProduct = _context.InvoiceProducts.FirstOrDefault(ip => ip.IdInvoiceProduct == id);
            if (invoiceProduct == null)
            {
                return NotFound();
            }

            _context.InvoiceProducts.Remove(invoiceProduct);
            _context.SaveChanges();

            return Ok(invoiceProduct);
        }
    }
}
