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
    [Route("[controller]")]
    [ApiController]
    public class TenancyController : ControllerBase
    {
       private readonly NccData _context;

        public TenancyController (NccData context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tenancy>>> Get()
        {
            var lst = await _context.Tenancy.ToListAsync();
            return Ok(lst);
        }
    }
}
