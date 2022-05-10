using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ApiProject.Model
{
    public class LocationTB
    {
        [Key]
        public Guid LocationK { get; set; }

        [StringLength(100)]
        public string LocationName { get; set; }

        public Guid? TenancyK { get; set; }

        [ForeignKey("TenancyK")]
        public virtual Tenancy Tenancy { get; set; }

        public virtual ICollection<Asset> Asset { get; set; }
    }
}
