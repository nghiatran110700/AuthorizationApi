using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApiProject.Model
{
    public class AssetType
    {
        [Key]
        public Guid AssetTypeK { get; set; }

        [StringLength(100)]
        public string AssetTypeName { get; set; }

        public virtual ICollection<Asset> Asset { get; set; }
    }
}
