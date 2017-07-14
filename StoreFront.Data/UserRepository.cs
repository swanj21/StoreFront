using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StoreFront.Data
{
    // What is the UserRepository's purpose?
    public class UserRepository
    {
        SqlSecurityManager sqlSM = new SqlSecurityManager();
        StoreFrontDataModel db = new StoreFrontDataModel();

        public List<users> GetAllUsers()
        {
            return db.users.Where(usr => usr.UserID > 0).ToList<users>();
        }
    }
}