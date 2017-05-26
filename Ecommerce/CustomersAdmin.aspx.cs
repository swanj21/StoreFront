using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Ecommerce
{
    public partial class CustomersAdmin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void DetailsView1_ItemCreated(object sender, EventArgs e)
        {
            GridView1.Sort("UserID", SortDirection.Ascending);
        }

        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            Response.Redirect("CustomerAdminDetails.aspx?userid=" + e.NewEditIndex);
        }
    }
}