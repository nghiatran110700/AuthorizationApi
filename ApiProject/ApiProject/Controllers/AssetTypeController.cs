using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ApiProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;


namespace Apiroject.Controllers
{
    [Route("[controller]")]
    [ApiController]
   
    public class AssetTypeController : ControllerBase
    {
        private readonly NccData _context ;
        public AssetTypeController(NccData Context)
        {
            _context = Context;
        }

        [HttpGet]
        public async Task<IEnumerable<AssetType>>  Get()
        {
            var lst = await _context.AssetType.ToListAsync();
            return lst;
        }

        [HttpPost("CreateAssetType")]
        public async Task<ActionResult<AssetType>> CreateAssetType([FromBody] AssetType assetType)
        {
            if(assetType == null || string.IsNullOrWhiteSpace(assetType.AssetTypeName))
            {
                return BadRequest("Isvalid input");
            }
            assetType.AssetTypeK = Guid.NewGuid();
            _context.AssetType.Add(assetType);
            await _context.SaveChangesAsync();
            return Ok(assetType);
        }
        [HttpPut("UpdateAssetType")]
        public async Task<ActionResult<AssetType>> UpdateAssetType([FromBody] AssetType assetType)
        {
            if (assetType == null || string.IsNullOrWhiteSpace(assetType.AssetTypeName))
            {
                return BadRequest("Isvalid input");
            }
            var  assettype = await _context.AssetType.FirstOrDefaultAsync(s => s.AssetTypeK.Equals(assetType.AssetTypeK));
            if(assettype == null)
            {
                return NotFound($"is AssetType with {assetType.AssetTypeK} not found");
            }
            assettype.AssetTypeName = assetType.AssetTypeName;
            await _context.SaveChangesAsync();
            return Ok(assettype);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<AssetType>> Delete(Guid? id)
        {
            var Assettype = await _context.AssetType.SingleOrDefaultAsync(s=>s.AssetTypeK == id);
            if(Assettype!= null)
            {
                _context.AssetType.Remove(Assettype);
                await _context.SaveChangesAsync();
                return Ok();
            }
            return NotFound($"is AssetType with {id} not found");
        }
    }
}
