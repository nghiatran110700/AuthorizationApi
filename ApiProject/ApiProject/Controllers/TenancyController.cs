using ApiProject.Model;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;



namespace ApiProject.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
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
        [HttpPost]
        [Route("createTenancy")]
        public async Task<ActionResult> CreateTenancy([FromBody] Tenancy tenacy)
        {
            if(tenacy != null && string.IsNullOrWhiteSpace(tenacy.TenancyName))
            {
                tenacy.TenancyK = Guid.NewGuid();
                _context.Tenancy.Add(tenacy);
                await _context.SaveChangesAsync();
                return Ok("Create success");
            }
            return BadRequest("create fails");
        }
    }
}
