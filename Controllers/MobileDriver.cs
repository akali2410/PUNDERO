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
    public class MobileDriverController : ControllerBase
    {
        private readonly PunderoContext _context;

        public MobileDriverController(PunderoContext context)
        {
            _context = context;
        }

        // GET: api/MobileDrivers
        [HttpGet]
        public IActionResult GetMobileDrivers()
        {
            var mobileDrivers = _context.MobileDrivers.OrderByDescending(md => md.IdMobileDriver).ToList();
            return Ok(mobileDrivers);
        }

        // GET: api/MobileDriver/5
        [HttpGet("{id}")]
        public IActionResult GetMobileDriver(int id)
        {
            var mobileDriver = _context.MobileDrivers.FirstOrDefault(md => md.IdMobileDriver == id);

            if (mobileDriver == null)
            {
                return NotFound();
            }

            return Ok(mobileDriver);
        }

        // POST: api/MobileDriver
        [HttpPost]
        public IActionResult PostMobileDriver([FromBody] MobileDriver mobileDriver)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.MobileDrivers.Add(mobileDriver);
            _context.SaveChanges();

            return CreatedAtRoute("GetMobileDriver", new { id = mobileDriver.IdMobileDriver }, mobileDriver);
        }

        // PUT: api/MobileDriver/5
        [HttpPut("{id}")]
        public IActionResult PutMobileDriver(int id, [FromBody] MobileDriver mobileDriver)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != mobileDriver.IdMobileDriver)
            {
                return BadRequest();
            }

            _context.Entry(mobileDriver).State = EntityState.Modified;
            _context.SaveChanges();

            return NoContent();
        }

        // DELETE: api/MobileDriver/5
        [HttpDelete("{id}")]
        public IActionResult DeleteMobileDriver(int id)
        {
            var mobileDriver = _context.MobileDrivers.FirstOrDefault(vd => vd.IdMobileDriver == id);
            if (mobileDriver == null)
            {
                return NotFound();
            }

            _context.MobileDrivers.Remove(mobileDriver);
            _context.SaveChanges();

            return Ok(mobileDriver);
        }
    }
}
