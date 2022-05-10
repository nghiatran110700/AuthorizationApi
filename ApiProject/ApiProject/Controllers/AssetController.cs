using ApiProject.Model;
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
    public class AssetController : ControllerBase
    {
        private readonly NccData _context ;
        public AssetController(NccData Context)
        {
            _context = Context;
        }


        [HttpGet]
        public async Task<IEnumerable<Asset>> Get()
        {
            return await _context.Asset.ToListAsync();
        }

        [HttpGet("GetByTenancyK/{id}")]
        public async Task<ActionResult<Asset>> GetByTenancyK(Guid id)
        {
            var asset = await _context.Asset
                                .Join(_context.LocationTB, a => a.LocationK, l => l.LocationK, (a, l) => new { asset = a, TenancyK = l.TenancyK })
                                .Where(l=>l.TenancyK == id).ToListAsync();
            if (asset == null)
            {
                return NotFound($"Asset with {id} not found");
            }
            else
            {
                return Ok(asset);
            }
        }

        [HttpGet("GetByLocationK/{tenancyK}/{locationK}")]
        public async Task<ActionResult<Asset>> GetByLocationK(Guid locationK, Guid tenancyK)
        {
            var assetBytenancyK = await _context.Asset
                                .Join(_context.LocationTB, a => a.LocationK, l => l.LocationK, (a, l) => new { asset = a, TenancyK = l.TenancyK })
                                .Where(l => l.TenancyK == tenancyK).ToListAsync();
            if(assetBytenancyK == null)
            {
                return NotFound($"Asset with {tenancyK} not found");
            }
            var asset =  assetBytenancyK.Where(s => s.asset.LocationK == locationK);
            if (asset == null)
            {
                return NotFound($"Asset with {locationK} not found");
            }
            
            return Ok(asset);
            
        }

        [HttpPost]
        [Route("Create")]
        public async Task<ActionResult<Asset>> Create([FromBody] Asset asset)
        {
            if(asset == null || string.IsNullOrWhiteSpace(asset.Accessname) 
                || string.IsNullOrWhiteSpace(asset.AccessPercent.ToString()) 
                || string.IsNullOrWhiteSpace(asset.Accessname)
                || string.IsNullOrWhiteSpace(asset.TimeRead.ToString())
                || string.IsNullOrWhiteSpace(asset.Title))
            {
                return BadRequest("Invalid input");
            }
            asset.AccessK = Guid.NewGuid();
             _context.Asset.Add(asset);
             await _context.SaveChangesAsync();
            return Ok(asset);
        }

        [HttpPut("Update")]
        public async Task<ActionResult<Asset>> Update([FromBody] Asset asset)
        {
            if (asset == null || string.IsNullOrWhiteSpace(asset.Accessname)
                || string.IsNullOrWhiteSpace(asset.AccessPercent.ToString())
                || string.IsNullOrWhiteSpace(asset.Accessname)
                || string.IsNullOrWhiteSpace(asset.TimeRead.ToString())
                || string.IsNullOrWhiteSpace(asset.Title))
            {
                return BadRequest("Invalid input");
            }
            var exittingAsset = await _context.Asset.FirstOrDefaultAsync(s=>s.AssetTypeK.Equals(asset.AssetTypeK));
            if(exittingAsset == null)
            {
                return NotFound($"Asset with {asset.AssetTypeK} not found");
            }
            exittingAsset.Accessname = asset.Accessname;
            exittingAsset.AccessPercent = asset.AccessPercent;
            exittingAsset.AssetTypeK = asset.AssetTypeK;
            exittingAsset.LocationK = asset.LocationK;
            exittingAsset.TimeRead = asset.TimeRead;
            exittingAsset.Title = asset.Title;
            await _context.SaveChangesAsync();
            return Ok(exittingAsset);
        }

        [HttpDelete("Delete/{id}")]
        public async Task<ActionResult<Asset>> Delete(Guid? id)
        {
            var exittingAsset = await _context.Asset.FirstOrDefaultAsync(s=>s.AssetTypeK.Equals(id));
            if (exittingAsset == null)
            {
                return NotFound($"Asset with {id} not found");
            }
            _context.Asset.Remove(exittingAsset);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
