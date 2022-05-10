using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ApiProject.Model
{
    public class Asset
    {
        [Key]
        public Guid AccessK { get; set; }

        [StringLength(100)]
        public string Accessname { get; set; }

        public double? AccessPercent { get; set; }

        public double? TimeRead { get; set; }

        [StringLength(100)]
        public string Title { get; set; }

        public Guid? AssetTypeK { get; set; }

        public Guid? LocationK { get; set; }

        [ForeignKey("AssetTypeK")]
        public virtual AssetType AssetType { get; set; }

        [ForeignKey("LocationK")]
        public virtual LocationTB LocationTB { get; set; }
    }
}
