using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Ecommerce
{
    public partial class ProductAdminDetails : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void DetailsView1_ItemDeleted(object sender, DetailsViewDeletedEventArgs e)
        {
            Response.Redirect("ProductsAdmin.aspx");
        }

        protected void UploadFileButton_Click(object sender, EventArgs e)
        {
            if (ImageFileUpload.HasFile)
            {
                try
                {
                    ImageFileUpload.SaveAs(Server.MapPath("~\\ProductImages\\" + ImageFileUpload.FileName));
                    infoLabel.Text = "File successfully uploaded! <br><br> <b><u>FileName</u></b>: " + ImageFileUpload.FileName;
                }
                catch (Exception ex)
                {
                    infoLabel.Text = "Error: " + ex.Message.ToString();
                }
            }
            else
            {// Nothing was selected in the browse box
                infoLabel.Text = "No file selected to upload!";
            }
        }
    }
}