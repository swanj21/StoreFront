namespace Ecommerce.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("state")]
    public partial class state
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public state()
        {
            address = new HashSet<address>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int StateID { get; set; }

        [StringLength(2)]
        public string StateAbbrev { get; set; }

        [StringLength(200)]
        public string StateName { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<address> address { get; set; }
    }
}
