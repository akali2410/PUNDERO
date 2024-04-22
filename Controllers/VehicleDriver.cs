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
    public class VehicleDriverController : ControllerBase
    {
        private readonly PunderoContext _context;

        public VehicleDriverController(PunderoContext context)
        {
            _context = context;
        }

        // GET: api/VehicleDrivers
        [HttpGet]
        public IActionResult GetVehicleDrivers()
        {
            var vehicleDrivers = _context.VehicleDrivers.OrderByDescending(vd => vd.IdVehicleDriver).ToList();
            return Ok(vehicleDrivers);
        }

        // GET: api/VehicleDriver/5
        [HttpGet("{id}")]
        public IActionResult GetVehicleDriver(int id)
        {
            var vehicleDriver = _context.VehicleDrivers.FirstOrDefault(vd => vd.IdVehicleDriver == id);

            if (vehicleDriver == null)
            {
                return NotFound();
            }

            return Ok(vehicleDriver);
        }

        // POST: api/VehicleDriver
        [HttpPost]
        public IActionResult PostVehicleDriver([FromBody] VehicleDriver vehicleDriver)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.VehicleDrivers.Add(vehicleDriver);
            _context.SaveChanges();

            return CreatedAtRoute("GetVehicleDriver", new { id = vehicleDriver.IdVehicleDriver }, vehicleDriver);
        }

        // PUT: api/VehicleDriver/5
        [HttpPut("{id}")]
        public IActionResult PutVehicleDriver(int id, [FromBody] VehicleDriver vehicleDriver)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != vehicleDriver.IdVehicleDriver)
            {
                return BadRequest();
            }

            _context.Entry(vehicleDriver).State = EntityState.Modified;
            _context.SaveChanges();

            return NoContent();
        }

        // DELETE: api/VehicleDriver/5
        [HttpDelete("{id}")]
        public IActionResult DeleteVehicleDriver(int id)
        {
            var vehicleDriver = _context.VehicleDrivers.FirstOrDefault(vd => vd.IdVehicleDriver == id);
            if (vehicleDriver == null)
            {
                return NotFound();
            }

            _context.VehicleDrivers.Remove(vehicleDriver);
            _context.SaveChanges();

            return Ok(vehicleDriver);
        }
    }
}
