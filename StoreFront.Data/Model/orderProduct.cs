namespace StoreFront.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("orderProduct")]
    public partial class orderProduct
    {
        public int OrderProductID { get; set; }

        public int? OrderID { get; set; }

        public int? ProductID { get; set; }

        public int? Quantity { get; set; }

        [Column(TypeName = "money")]
        public decimal? Price { get; set; }

        public DateTime? DateCreated { get; set; }

        [StringLength(50)]
        public string CreatedBy { get; set; }

        public DateTime? DateModfied { get; set; }

        [StringLength(50)]
        public string ModifiedBy { get; set; }

        public virtual orders orders { get; set; }

        public virtual product product { get; set; }
    }
}
