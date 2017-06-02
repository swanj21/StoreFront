namespace Ecommerce.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("shoppingCartProduct")]
    public partial class shoppingCartProduct
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ShoppingCartProductID { get; set; }

        public int? ShoppingCartID { get; set; }

        public int? ProductID { get; set; }

        public int Quantity { get; set; }

        public DateTime? DateCreated { get; set; }

        [StringLength(50)]
        public string CreatedBy { get; set; }

        public DateTime? DateModified { get; set; }

        [StringLength(50)]
        public string ModifiedBy { get; set; }

        public virtual product product { get; set; }

        public virtual shoppingCart shoppingCart { get; set; }
    }
}
