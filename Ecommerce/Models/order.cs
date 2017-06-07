namespace Ecommerce.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class order
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public order()
        {
            orderProducts = new HashSet<orderProduct>();
        }

        public int OrderID { get; set; }

        public int? UserID { get; set; }

        public int? AddressID { get; set; }

        public DateTime? OrderDate { get; set; }

        [Column(TypeName = "money")]
        public decimal? Total { get; set; }

        public int? StatusID { get; set; }

        public DateTime? DateCreated { get; set; }

        [StringLength(50)]
        public string CreatedBy { get; set; }

        public DateTime? DateModified { get; set; }

        [StringLength(50)]
        public string ModifiedBy { get; set; }

        public virtual address address { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<orderProduct> orderProducts { get; set; }

        public virtual status status { get; set; }

        public virtual users user { get; set; }
    }
}
