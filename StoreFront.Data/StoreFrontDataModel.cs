namespace StoreFront.Data
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Collections.Generic;

    public partial class StoreFrontDataModel : DbContext
    {
        public StoreFrontDataModel()
            : base("name=StoreFrontDataModel")
        {
        }

        // Get shopping cart ID
        public int getShoppingCartID(int userID)
        {
            shoppingCart newCart = shoppingCart.Where(cart => cart.UserID == userID).SingleOrDefault();
            return newCart.ShoppingCartID;
        }

        // Get products in specific shopping cart using its ID
        public List<shoppingCartProduct> getShoppingCartProducts(int cartID)
        {
            shoppingCart tempCart = shoppingCart.Where(cart => cart.ShoppingCartID == cartID).SingleOrDefault();
            return tempCart.shoppingCartProduct.ToList();
        }

        // Remove items from specified cart given the cartID
        public void removeFromCart(int cartID, int userID, int addressID, double total)
        {
            shoppingCart cart = this.shoppingCart.Where(temp => temp.ShoppingCartID == cartID).SingleOrDefault();
            
            List<shoppingCartProduct> productList = new List<shoppingCartProduct>();
            foreach(var item in cart.shoppingCartProduct)
            {
                productList.Add(item); // Add to the productList
            }
            // Need to pass userID, addressID, total
            addToOrder(userID, addressID, total, productList);

            // MAKE A PARAMETERIZED QUERY FOR THIS STATEMENT
            Database.ExecuteSqlCommand("delete from shoppingCartProduct " +
                           "where ShoppingCartID = {0}",
                           cart.ShoppingCartID);

        }

        // Add an item to a specified order given the userID, selected AddressID, Total for the order.
        public void addToOrder(int userID, int addressID, double total, List<shoppingCartProduct> productList)
        {
            // Create the order first
            orders order = new orders();

            order.AddressID = addressID;
            order.CreatedBy = users.Where(usr => usr.UserID == userID).SingleOrDefault().UserName;
            order.DateCreated = DateTime.Now;
            order.DateModified = null;
            order.ModifiedBy = null;
            order.OrderDate = DateTime.Now;
            order.StatusID = 1;
            order.UserID = userID;
            order.Total = (decimal)total;
            this.orders.Add(order);

            // Add the items in the product list(formerly shopping cart) to the orderProducts table in the db.
            foreach (var item in productList)
            {
                orderProduct prod = new orderProduct();

                prod.OrderID = order.OrderID;
                prod.ProductID = item.ProductID;
                prod.Quantity = item.Quantity;
                prod.Price = item.product.Price;
                prod.DateCreated = DateTime.Now;
                prod.CreatedBy = users.Where(usr => usr.UserID == userID).SingleOrDefault().UserName;
                prod.DateModfied = null; // Misspelled modified in the db.
                prod.ModifiedBy = null;
                orderProduct.Add(prod);
            }
            SaveChanges();
        }

        public enum states
        {// enum starts w/ 2 since that is how it is in the DB.
            AL = 2, AK, AZ, AR, CA, CO, CT, DE, FL, GA, HI, ID, IL, IN, IA, KS, KY, LA, ME, MD, MA, MI, MN, MS, MO, MT,
            NE, NV, NH, NJ, NM, NY, NC, ND, OH, OK, OR, PA, RI, SC, SD, TN, TX, UT, VT, VA, WA, WV, WI, WY
        };

        public virtual DbSet<address> address { get; set; }
        public virtual DbSet<orderProduct> orderProduct { get; set; }
        public virtual DbSet<orders> orders { get; set; }
        public virtual DbSet<product> product { get; set; }
        public virtual DbSet<shoppingCart> shoppingCart { get; set; }
        public virtual DbSet<shoppingCartProduct> shoppingCartProduct { get; set; }
        public virtual DbSet<state> state { get; set; }
        public virtual DbSet<status> status { get; set; }
        public virtual DbSet<users> users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<address>()
                .Property(e => e.Address1)
                .IsUnicode(false);

            modelBuilder.Entity<address>()
                .Property(e => e.Address2)
                .IsUnicode(false);

            modelBuilder.Entity<address>()
                .Property(e => e.Address3)
                .IsUnicode(false);

            modelBuilder.Entity<address>()
                .Property(e => e.City)
                .IsUnicode(false);

            modelBuilder.Entity<address>()
                .Property(e => e.ZipCode)
                .IsUnicode(false);

            modelBuilder.Entity<address>()
                .Property(e => e.CreatedBy)
                .IsUnicode(false);

            modelBuilder.Entity<address>()
                .Property(e => e.ModifiedBy)
                .IsUnicode(false);

            modelBuilder.Entity<orderProduct>()
                .Property(e => e.Price)
                .HasPrecision(19, 4);

            modelBuilder.Entity<orderProduct>()
                .Property(e => e.CreatedBy)
                .IsUnicode(false);

            modelBuilder.Entity<orderProduct>()
                .Property(e => e.ModifiedBy)
                .IsUnicode(false);

            modelBuilder.Entity<orders>()
                .Property(e => e.Total)
                .HasPrecision(19, 4);

            modelBuilder.Entity<orders>()
                .Property(e => e.CreatedBy)
                .IsUnicode(false);

            modelBuilder.Entity<orders>()
                .Property(e => e.ModifiedBy)
                .IsUnicode(false);

            modelBuilder.Entity<product>()
                .Property(e => e.ProductName)
                .IsUnicode(false);

            modelBuilder.Entity<product>()
                .Property(e => e.ProductDescription)
                .IsUnicode(false);

            modelBuilder.Entity<product>()
                .Property(e => e.Price)
                .HasPrecision(19, 4);

            modelBuilder.Entity<product>()
                .Property(e => e.ImageFile)
                .IsUnicode(false);

            modelBuilder.Entity<product>()
                .Property(e => e.CreatedBy)
                .IsUnicode(false);

            modelBuilder.Entity<product>()
                .Property(e => e.ModifiedBy)
                .IsUnicode(false);

            modelBuilder.Entity<shoppingCart>()
                .Property(e => e.CreatedBy)
                .IsUnicode(false);

            modelBuilder.Entity<shoppingCart>()
                .Property(e => e.ModifiedBy)
                .IsUnicode(false);

            modelBuilder.Entity<shoppingCartProduct>()
                .Property(e => e.CreatedBy)
                .IsUnicode(false);

            modelBuilder.Entity<shoppingCartProduct>()
                .Property(e => e.ModifiedBy)
                .IsUnicode(false);

            modelBuilder.Entity<state>()
                .Property(e => e.StateAbbrev)
                .IsUnicode(false);

            modelBuilder.Entity<state>()
                .Property(e => e.StateName)
                .IsUnicode(false);

            modelBuilder.Entity<status>()
                .Property(e => e.StatusDescription)
                .IsUnicode(false);

            modelBuilder.Entity<users>()
                .Property(e => e.UserName)
                .IsUnicode(false);

            modelBuilder.Entity<users>()
                .Property(e => e.Password)
                .IsUnicode(false);

            modelBuilder.Entity<users>()
                .Property(e => e.EmailAddress)
                .IsUnicode(false);

            modelBuilder.Entity<users>()
                .Property(e => e.CreatedBy)
                .IsUnicode(false);

            modelBuilder.Entity<users>()
                .Property(e => e.ModifiedBy)
                .IsUnicode(false);
        }
    }
}