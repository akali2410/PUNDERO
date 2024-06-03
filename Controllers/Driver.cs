using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PUNDERO.Models;

namespace PUNDERO.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class DriverController : ControllerBase
    {
        private readonly PunderoContext _context;

        public DriverController(PunderoContext context)
        {
            _context = context;
        }

        // GET: api/Driver
        [HttpGet]
        public IActionResult GetDrivers()
        {
            var drivers = _context.Drivers
                .Include(d => d.IdAccountNavigation)
                .Include(d => d.IdTachographNavigation)
                .Include(d => d.MobileDrivers).ThenInclude(md => md.IdMobileNavigation)
                .Include(d => d.VehicleDrivers).ThenInclude(vd => vd.IdVehicleNavigation)
                .Select(d => new
                {
                    d.IdDriver,
                    FirstName = d.IdAccountNavigation.FirstName,
                    LastName = d.IdAccountNavigation.LastName,
                    Email = d.IdAccountNavigation.Email,
                    Type = "Driver",
                    d.LicenseNumber,
                    d.LicenseCategory,
                    TachographLabel = d.IdTachographNavigation.Label,
                    AssignedMobile = d.MobileDrivers.Select(md => md.IdMobileNavigation.PhoneNumber).FirstOrDefault(),
                    AssignedVehicle = d.VehicleDrivers.Select(vd => vd.IdVehicleNavigation.Registration).FirstOrDefault()
                })
                .ToList();

            return Ok(drivers);
        }

        // GET: api/Driver
        [HttpGet]
        public IActionResult GetDriversCoordinator()
        {
            var drivers = _context.Drivers
                .Include(d => d.IdAccountNavigation)
                .Include(d => d.IdTachographNavigation)
                .Include(d => d.MobileDrivers).ThenInclude(md => md.IdMobileNavigation)
                .Include(d => d.VehicleDrivers).ThenInclude(vd => vd.IdVehicleNavigation)
                .Select(d => new
                {
                    d.IdDriver,
                    FirstName = d.IdAccountNavigation.FirstName,
                    LastName = d.IdAccountNavigation.LastName,
                    Email = d.IdAccountNavigation.Email,
                    Type = "Driver",
                    d.LicenseCategory,
                    TachographLabel = d.IdTachographNavigation.Label
                })
                .ToList();

            return Ok(drivers);
        }



        [HttpGet]
        public async Task<IActionResult> GetDriversWithName()
        {
            var drivers = await _context.Drivers
                .Include(d => d.IdAccountNavigation)
                .Select(d => new {
                    d.IdDriver,
                    d.IdAccountNavigation.FirstName,
                    d.IdAccountNavigation.LastName
                })
                .ToListAsync();
            return Ok(drivers);
        }



        // POST: api/Driver/AddDriver
        [HttpPost]
        public async Task<IActionResult> AddDriver([FromBody] DriverViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.SelectMany(x => x.Value.Errors.Select(e => e.ErrorMessage)).ToList();
                foreach (var error in errors)
                {
                    Console.WriteLine(error);
                }
                return BadRequest(ModelState);
            }

            var account = new Account
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                Password = model.Password,
                Type = 2, // Driver type
                Image = model.Image // Optional image field
            };

            _context.Accounts.Add(account);
            await _context.SaveChangesAsync();

            var tachograph = new Tachograph
            {
                Label = model.TachographLabel,
                IssueDate = model.TachographIssueDate,
                ExpiryDate = model.TachographExpiryDate
            };

            _context.Tachographs.Add(tachograph);
            await _context.SaveChangesAsync();

            var driver = new Driver
            {
                IdAccount = account.IdAccount,
                LicenseNumber = model.LicenseNumber,
                LicenseCategory = model.LicenseCategory,
                IdTachograph = tachograph.IdTachograph,
                PrivateMobile = 0
            };

            _context.Drivers.Add(driver);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                driver.IdDriver,
                account.FirstName,
                account.LastName,
                account.Email,
                account.Type,
                driver.LicenseNumber,
                driver.LicenseCategory,
                TachographLabel = tachograph.Label,
                account.Image
            });
        }
    }
}
