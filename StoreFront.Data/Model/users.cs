namespace StoreFront.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class users
    {
        StoreFrontDataModel db = new StoreFrontDataModel();

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public users()
        {
            address = new HashSet<address>();
            orders = new HashSet<orders>();
            shoppingCart = new HashSet<shoppingCart>();
        }

        public users FindUser(string userName)
        {
            foreach (var user in db.users)
            {
                if (user.UserName.Equals(userName))
                    return user;
            }
            return null;
        }

        public int GetShoppingCartID(users user)
        {
            // SQL query to get the shoppingCartID from a specific UserID.
            var cartID = db.Database.SqlQuery<int>(
                "SELECT shoppingCart.ShoppingCartID " +
                "FROM shoppingCart " +
                "WHERE shoppingCart.UserID = {0} ",
                user.UserID).SingleAsync();
            int cartIDresult = cartID.Result;

            return cartIDresult;
        }

        public int GetItemsInCart(users user)
        {
            int cartIDresult = GetShoppingCartID(user);

            var numOfItems = db.Database.SqlQuery<int>(
                "SELECT count(*) from (SELECT shoppingCartProduct.ProductID " +
                "FROM shoppingCartProduct inner join product on shoppingCartProduct.ProductID = product.ProductID " +
                "WHERE shoppingCartProduct.ShoppingCartID = {0}) as num;",
                cartIDresult);// shopCartID
            var result = numOfItems.SingleOrDefaultAsync().Result;
            return result;
        }

        [Key]
        public int UserID { get; set; }

        [StringLength(200)]
        public string UserName { get; set; }

        [StringLength(50)]
        public string Password { get; set; }

        [StringLength(255)]
        public string EmailAddress { get; set; }

        public bool? IsAdmin { get; set; }

        public DateTime? DateCreated { get; set; }

        [StringLength(50)]
        public string CreatedBy { get; set; }

        public DateTime? DateModified { get; set; }

        [StringLength(50)]
        public string ModifiedBy { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<address> address { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<orders> orders { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<shoppingCart> shoppingCart { get; set; }
    }
}
