using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StoreFront.Data;
using System.Collections.Generic;
using System.Web;

namespace StoreFront.UnitTest
{
    /* Use arrange-act-assert to create a series of unit tests to test data layer's abilities to CRUD data from DB. */
    /* Need to do unit tests for the new UserRepository and SqlSecurityManager */
    [TestClass]
    public class StoreFrontUnitTest
    {

        StoreFrontDataModel dm = new StoreFrontDataModel();
        InventoryRepository inv = new InventoryRepository();
        OrderRepository ord = new OrderRepository();
        users user = new users();
        SqlSecurityManager sqlSM = new SqlSecurityManager();

        //
        // StoreFrontDataModel
        //
        [TestMethod]
        public void StoreFrontDataModel_addToOrder_TotalIsNegative_ShouldThrowArgumentOutOfRangeException()
        {
            try
            {
                dm.addToOrder(9, 1, -1.00, new List<shoppingCartProduct>());
            }
            catch (ArgumentOutOfRangeException ex)
            {
                StringAssert.Contains(ex.Message, StoreFrontDataModel.TOTALLESSTHANZERO);
                return; // If correct error is caught, return and test passes.
            }
            Assert.Fail("ArgumentOutOfRangeException was not thrown");
        }

        [TestMethod]
        public void StoreFrontDataModel_addToOrder_TotalIsZero_ShouldNotThrowException()
        {
            try
            {
                dm.addToOrder(9, 1, 0.00, new List<shoppingCartProduct>());
            }
            catch (ArgumentOutOfRangeException AORE)
            {
                Assert.Fail(AORE.Message + " - ArgumentOutOfRangeException was thrown");
            }
            catch (NullReferenceException NRE)
            {
                Assert.Fail(NRE.Message + " - NullReferenceException was thrown");
            }
        }

        [TestMethod]
        public void StoreFrontDataModel_addToOrder_TotalIsPositive_ShouldNotThrowException()
        {
            try
            {
                dm.addToOrder(9, 1, 3.00, new List<shoppingCartProduct>());
            }
            catch (ArgumentOutOfRangeException AORE)
            {
                Assert.Fail(AORE.Message + " - ArgumentOutOfRangeException was thrown");
            }
            catch (NullReferenceException NRE)
            {
                Assert.Fail(NRE.Message + " - NullReferenceException was thrown");
            }
        }

        [TestMethod]
        public void StoreFrontDataModel_getShoppingCartID_IDIsLessThanZero_ShouldThrowArgumentOutOfRangeException()
        {
            try
            {
                dm.getShoppingCartID(-8);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                StringAssert.Contains(ex.Message, StoreFrontDataModel.INVALIDID);
                return;
            }

            Assert.Fail("ArgumentOutOfRangeException was not thrown");
        }

        [TestMethod]
        public void StoreFrontDataModel_getShoppingCartID_IDIsZero_ShouldThrowArgumentOutOfRangeException()
        {
            try
            {
                dm.getShoppingCartID(0);
            }
            catch (ArgumentOutOfRangeException AORE)
            {
                StringAssert.Contains(AORE.Message, StoreFrontDataModel.INVALIDID);
                return; // If correct error is caught, return and test passes.
            }

            Assert.Fail("ArgumentOutOfRangeException was not thrown");
        }

        [TestMethod]
        public void StoreFrontDataModel_getShoppingCartID_IDIsPositive_ShouldNotThrowException()
        {
            try
            {
                dm.getShoppingCartID(9);
            }
            catch (ArgumentOutOfRangeException AORE)
            {
                Assert.Fail(AORE.Message + " - ArgumentOutOfRangeEcxeption thrown");
            }
        }

        [TestMethod]
        public void StoreFrontDataModel_getShoppingCartProducts_IDIsLessThanZero_ShouldThrowArgumentOutOfRangeException()
        {
            try
            {
                dm.getShoppingCartProducts(-8);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                StringAssert.Contains(ex.Message, StoreFrontDataModel.INVALIDID);
                return;
            }

            Assert.Fail("ArgumentOutOfRangeException was not thrown");
        }

        [TestMethod]
        public void StoreFrontDataModel_getShoppingCartProducts_IDIsZero_ShouldThrowArgumentOutOfRangeException()
        {
            try
            {
                dm.getShoppingCartProducts(0);
            }
            catch (ArgumentOutOfRangeException AORE)
            {
                StringAssert.Contains(AORE.Message, StoreFrontDataModel.INVALIDID);
                return; // If correct error is caught, return and test passes.
            }

            Assert.Fail("ArgumentOutOfRangeException was not thrown");
        }

        [TestMethod]
        public void StoreFrontDataModel_getShoppingCartProducts_IDIsPositive_ShouldNotThrowException()
        {
            try
            {
                dm.getShoppingCartProducts(3);
            }
            catch (ArgumentOutOfRangeException AORE)
            {
                Assert.Fail(AORE.Message + " - ArgumentOutOfRangeEcxeption thrown");
            }
        }

        [TestMethod]
        public void StoreFrontDataModel_removeFromCart_CartIDIsLessThanZero_ShouldThrowArgumentOutOfRangeException()
        {
            try
            {
                dm.removeFromCart(-3, 1, 3, 2.50);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                StringAssert.Contains(ex.Message, StoreFrontDataModel.INVALIDID);
                return;
            }

            Assert.Fail("ArgumentOutOfRangeException was not thrown");
        }

        [TestMethod]
        public void StoreFrontDataModel_removeFromCart_UserIDIsLessThanZero_ShouldThrowArgumentOutOfRangeException()
        {
            try
            {
                dm.removeFromCart(3, -1, 3, 2.50);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                StringAssert.Contains(ex.Message, StoreFrontDataModel.INVALIDID);
                return;
            }

            Assert.Fail("ArgumentOutOfRangeException was not thrown");
        }

        [TestMethod]
        public void StoreFrontDataModel_removeFromCart_AddressIDIsLessThanZero_ShouldThrowArgumentOutOfRangeException()
        {
            try
            {
                dm.removeFromCart(3, 1, -3, 2.50);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                StringAssert.Contains(ex.Message, StoreFrontDataModel.INVALIDID);
                return;
            }

            Assert.Fail("ArgumentOutOfRangeException was not thrown");
        }

        [TestMethod]
        public void StoreFrontDataModel_removeFromCart_TotalIsLessThanZero_ShouldThrowArgumentOutOfRangeException()
        {
            try
            {
                dm.removeFromCart(3, 1, 3, -2.50);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                StringAssert.Contains(ex.Message, StoreFrontDataModel.TOTALLESSTHANZERO);
                return;
            }

            Assert.Fail("ArgumentOutOfRangeException was not thrown");
        }

        [TestMethod]
        public void StoreFrontDataModel_removeFromCart_AllValuesGreaterThanZero_ShouldNotThrowException()
        {
            try
            {
                dm.removeFromCart(1, 9, 1, 2.50);
            }
            catch (ArgumentOutOfRangeException)
            {
                Assert.Fail("ArgumentOutOfRangeException was thrown");
            }
        }

        [TestMethod]
        public void StoreFrontDataModel_removeFromCart_AllValuesAreLessThanZero_ShouldThrowArgumentOutOfRangeException()
        {
            try
            {
                dm.removeFromCart(-3, -1, -3, -2.50);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                StringAssert.Contains(ex.Message, StoreFrontDataModel.INVALIDID);
                return;
            }

            Assert.Fail("ArgumentOutOfRangeException was not thrown");
        }

        //
        // InventoryRepository
        //
        [TestMethod]
        public void InventoryRepository_GetProduct_IDIsLessThanZero_ShouldThrowArgumentOutOfRangeException()
        {
            try
            {
                inv.GetProduct(-2);
            }
            catch (ArgumentOutOfRangeException AORE)
            {
                StringAssert.Contains(AORE.Message, StoreFrontDataModel.INVALIDID);
                return; // If correct error is caught, return and test passes.
            }

            Assert.Fail("ArgumentOutOfRangeException was not thrown");
        }

        [TestMethod]
        public void InventoryRepository_GetProduct_IDIsZero_ShouldThrowArgumentOutOfRangeException()
        {
            try
            {
                inv.GetProduct(0);
            }
            catch (ArgumentOutOfRangeException AORE)
            {
                StringAssert.Contains(AORE.Message, StoreFrontDataModel.INVALIDID);
                return; // If correct error is caught, return and test passes.
            }

            Assert.Fail("ArgumentOutOfRangeException was not thrown");
        }

        [TestMethod]
        public void InventoryRepository_GetProduct_IDIsPositive_ShouldNotThrowException()
        {
            try
            {
                inv.GetProduct(4);
            }
            catch (ArgumentOutOfRangeException AORE)
            {
                Assert.Fail(AORE.Message + " - ArgumentOutOfRangeEcxeption thrown");
            }
        }

        //
        // OrderRepository
        //
        [TestMethod]
        public void OrderRepository_GetOrder_IDIsLessThanZero_ShouldThrowArgumentOutOfRangeException()
        {
            try
            {
                ord.GetOrder(-2);
            }
            catch(ArgumentOutOfRangeException ex)
            {
                StringAssert.Contains(ex.Message, StoreFrontDataModel.INVALIDID);
                return;
            }

            Assert.Fail("ArgumentOutOfRangeException was not thrown");
        }

        [TestMethod]
        public void OrderRepository_GetOrder_IDIsZero_ShouldThrowArgumentOutOfRangeException()
        {
            try
            {
                ord.GetOrder(0);
            }
            catch(ArgumentOutOfRangeException ex)
            {
                StringAssert.Contains(ex.Message, StoreFrontDataModel.INVALIDID);
                return;
            }

            Assert.Fail("ArgumentOutOfRangeException was not thrown");
        }

        [TestMethod]
        public void OrderRepository_GetOrder_IDIsPositive_ShouldThrowArgumentOutOfRangeException()
        {
            try
            {
                ord.GetOrder(2);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Assert.Fail(ex.Message + " - ArgumentOutOfRangeException thrown");
            }
        }

        //
        // Users
        //
        [TestMethod]
        public void users_FindUser_UsernameIsNull_ShouldThrowNullReferenceException()
        {
            try
            {
                user.FindUser(null);
            }
            catch (NullReferenceException)
            {
                return;
            }

            Assert.Fail("NullReferenceException was not thrown");
        }

        [TestMethod]
        public void users_FindUser_UsernameNotNull_ShouldNotThrowException()
        {
            try
            {
                user.FindUser("gswan");
            }
            catch(NullReferenceException NRE)
            {
                Assert.Fail(NRE.Message + " - NullReferenceException was thrown");
            }
        }

        [TestMethod]
        public void users_GetShoppingCartID_UserIsNull_ShouldThrowNullReferenceException()
        {
            try
            {
                user.GetShoppingCartID(null);
            }
            catch (NullReferenceException)
            {
                return;
            }

            Assert.Fail("NullReferenceException not thrown");
        }

        [TestMethod]
        public void users_GetShoppingCartID_UserIsNotNull_ShouldNotThrowException()
        {
            try
            {
                users newuser = dm.users.Find(9);
                user.GetShoppingCartID(newuser);
            }
            catch (NullReferenceException ex)
            {
                Assert.Fail(ex.Message + " - NullReferenceException was thrown");
            }
        }

        [TestMethod]
        public void users_GetItemsInCart_UserIsNull_ShouldThrowNullReferenceException()
        {
            try
            {
                user.GetItemsInCart(null);
            }
            catch (NullReferenceException)
            {
                return;
            }

            Assert.Fail("NullReferenceException not thrown");
        }

        [TestMethod]
        public void users_GetItemsInCart_UserIsNotNull_ShouldNotThrowException()
        {
            try
            {
                users newuser = dm.users.Find(9);
                user.GetItemsInCart(newuser);
            }
            catch (NullReferenceException ex)
            {
                Assert.Fail(ex.Message + " - NullReferenceException was thrown");
            }
        }

        //
        // API
        //
        [TestMethod]
        public void api_order_OrderController_MarkOrderShipped_GoodInput()
        {// Create OrderController and give it good input for function
            try
            {
                OrderRepository ord = new OrderRepository();

                ord.MarkOrderShipped(1003);
            }
            catch (Exception ex)
            {
                Assert.Fail("Exception was thrown - " + ex.Message);
            }
        }

        [TestMethod]
        public void api_order_OrderController_GetOrders_GoodInput()
        {// Create OrderController and give it a good time for creating a list of orders
            try
            {
                OrderRepository ord = new OrderRepository();
                DateTime start = new DateTime(2017, 6, 1); // June 6, 2017
                DateTime end = new DateTime(2017, 7, 7);   // July 7, 2017

                List<orders> newList = ord.GetOrders(start, end);
            }
            catch (Exception ex)
            {
                Assert.Fail("Exception was thrown - " + ex.Message);
            }
        }

        //
        // sqlSecurityManager
        //
        [TestMethod]
        public void sqlSecurityManager_AuthenticateUser()
        {
            // AuthenticateUser(string username, string password)

            if (!sqlSM.AuthenticateUser("mac", "paddyspub") || // User good, pass good
            sqlSM.AuthenticateUser("mac", "patrickspub") ||   // User good, pass bad
            sqlSM.AuthenticateUser("ma", "paddyspub") ||     // User bad, pass good
            sqlSM.AuthenticateUser("ma", "patrickspub"))    // User bad, pass bad
                Assert.Fail("sqlSecuritymanager_AuthenticateUser() failed");
        }

        [TestMethod]
        public void sqlSecurityManager_IsAdmin()
        {
            // IsAdmin()
            HttpContext.Current.Session["UserName"] = "gswan";
            if (!sqlSM.IsAdmin())
                Assert.Fail("sqlSecurityManager_IsAdmin() failed for user that is admin, returned that user is not admin.");

            HttpContext.Current.Session["UserName"] = "mac";
            if (sqlSM.IsAdmin())
                Assert.Fail("sqlSecurityManager_IsAdmin() failed for user that is not admin, returned that user is admin.");
        }

        [TestMethod]
        public void sqlSecurityManager_LoadUser()
        {
            // LoadUser(string username) returns a users object
            //   good input
            //   bad input

            users gswanUser = sqlSM.LoadUser("gswan");
            users invalidUser = sqlSM.LoadUser("Invalid");

            if (gswanUser.UserName.Equals(null))
                Assert.Fail("sqlSecurityManager_LoadUser() gswan.UserName is null.");
            if (!invalidUser.UserName.Equals(null))
                Assert.Fail("sqlSecurityManager_LoadUser() invalidUser.UserName is not null.");
        }

        [TestMethod]
        public void sqlSecurityManager_SaveUser()
        {

        }

        [TestMethod]
        public void sqlSecurityManager_RegisterUser_DeleteUser()
        {
            // RegisterUser then DeleteUser on the newly registered user
            //   

            user.UserName = "testUser";
            user.Password = "test";
            user.EmailAddress = "testUser@test.gov";
            user.ConfirmPass = "test";

            if (!sqlSM.RegisterUser(user))
                Assert.Fail("sqlSecurityManager_RegisterUser_DeleteUser() failed registration");

            sqlSM.DeleteUser(user.UserName);
        }
    }
}