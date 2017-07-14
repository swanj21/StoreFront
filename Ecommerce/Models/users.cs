namespace Ecommerce.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity;
    using System.Data.Entity.Spatial;
    using StoreFront.Data;

    public partial class users
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public users()
        {
            address = new HashSet<address>();
            orders = new HashSet<orders>();
            shoppingCart = new HashSet<shoppingCart>();
        }

        StoreFrontDataModel db = new StoreFrontDataModel();

        public StoreFront.Data.users FindUser(string userName)
        {
            foreach(var user in db.users)
            {
                if (user.UserName.Equals(userName))
                    return user;
            }
            return null;
        }

        public bool UserAlreadyExist(string uName)
        {
            foreach(var user in db.users)
            {
                if (user.UserName.Equals(uName))
                    return true;
            }

            return false;
        }

        public int GetItemsInCart(users user)
        {
            // If the user does not have a shopping cart, create one.
            if (user.shoppingCart.Count == 0)
            {
                shoppingCart cart = new shoppingCart();
                cart.CreatedBy = user.UserName;
                cart.DateCreated = DateTime.Now;
                cart.DateModified = null;
                cart.ModifiedBy = null;
                cart.UserID = user.UserID;

                user.shoppingCart.Add(cart);

                db.SaveChanges();
                return 0;
            }
            else
            {
                int cartIDresult = GetShoppingCartID(user);
                
                // SQL query to get the number of items in a specified cart.
                var numOfItems = db.Database.SqlQuery<int>(
                    "SELECT count(*) from (SELECT shoppingCartProduct.ProductID " +
                    "FROM shoppingCartProduct inner join product on shoppingCartProduct.ProductID = product.ProductID " +
                    "WHERE shoppingCartProduct.ShoppingCartID = {0}) as num;",
                    cartIDresult);// shopCartID
                var result = numOfItems.SingleOrDefaultAsync().Result;
                return result;
                //return numOfItems.Result;
            }
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

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserID { get; set; }

        [Required]
        [StringLength(200)]
        [Display(Name ="User Name")]
        public string UserName { get; set; }

        [Required]
        [MaxLength(500)]
        [MinLength(7)]
        public string Password { get; set; }

        [Compare("Password")]
        [Display(Name ="Validate Password")]
        [NotMapped]
        public string ConfirmPass { get; set; }

        [Required]
        [StringLength(255)]
        [EmailAddress]
        [Display(Name ="Email Address")]
        // [RegularExpression("^[a-zA-Z_]([a-zA-Z0-9_\\.\\-])+@([a-zA-Z0-9\\-\\.])+([a-zA-Z0-9])+")]
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
