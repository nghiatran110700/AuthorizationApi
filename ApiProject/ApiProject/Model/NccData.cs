using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;

namespace ApiProject.Model
{
    public class NccData :IdentityDbContext
    {
        public NccData(DbContextOptions<NccData> options) : base(options)
        {

        }
        public virtual DbSet<Asset> Asset { get; set; }
        public virtual DbSet<AssetType> AssetType { get; set; }
        public virtual DbSet<LocationTB> LocationTB { get; set; }
        public virtual DbSet<Tenancy> Tenancy { get; set; }
        public virtual DbSet<UserTB> UserTB { get; set; }

    }
}
