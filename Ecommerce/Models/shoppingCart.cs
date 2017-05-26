namespace Ecommerce.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("shoppingCart")]
    public partial class shoppingCart
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public shoppingCart()
        {
            shoppingCartProduct = new HashSet<shoppingCartProduct>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ShoppingCartID { get; set; }

        public int? UserID { get; set; }

        public DateTime? DateCreated { get; set; }

        [StringLength(50)]
        public string CreatedBy { get; set; }

        public DateTime? DateModified { get; set; }

        [StringLength(50)]
        public string ModifiedBy { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<shoppingCartProduct> shoppingCartProduct { get; set; }

        public virtual users users { get; set; }
    }
}
