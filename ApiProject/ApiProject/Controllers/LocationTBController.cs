using ApiProject.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationTBController : ControllerBase
    {
        private readonly NccData _context;
       
        public LocationTBController(NccData context)
        {
            _context = context;
        }
         

        [HttpGet]
        public async  Task<IEnumerable<LocationTB>> Get()
        {
            return await _context.LocationTB.ToListAsync();
        }
        [HttpGet]
        [Route("{id}")] 
        public async Task<IEnumerable<LocationTB>> Get(Guid id)
        {
            return await _context.LocationTB.Where(s=>s.LocationK.Equals(id)).ToListAsync();
        }

        [HttpGet("GetByTenancyK/{id}")]
        public async Task<IEnumerable<LocationTB>> GetByTenancyK(Guid id)
        {
            return await _context.LocationTB.Where(s=>s.TenancyK.Equals(id)).ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<LocationTB>> Post([FromBody] LocationTB locationtb)
        {
            if (locationtb == null || string.IsNullOrWhiteSpace(locationtb.LocationName))
            {
                return BadRequest("Isvalid input");
            }
            
            locationtb.LocationK = Guid.NewGuid();
            _context.LocationTB.Add(locationtb);
            await _context.SaveChangesAsync();
            return Ok(locationtb);
        }
    }
}
