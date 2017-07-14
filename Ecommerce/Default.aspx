<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Ecommerce._Default" %>

<asp:Content ID ="PageTitle" ContentPlaceHolderID="cpPageTitle" runat="server">
    Administration 
</asp:Content>
<asp:Content ID="BodyContentLeft" ContentPlaceHolderID="cpMainContentLeft" runat="server">
    <h2 style="text-align:center">Navigation</h2>
    <br />
    <ul>
        <li>
            Home
            <ul>
                <li>
                    Takes you back to this page
                </li>
            </ul>
        </li>
        <li>
            Product Info
            <ul>
                <li>
                    Manage information about products in the database
                </li>
            </ul>
        </li>
        <li>
            Customer Info
            <ul>
                <li>
                    Manage information about customers in the database
                </li>
            </ul>
        </li>
    </ul>

</asp:Content>
<asp:Content ID="BodyContentRight" ContentPlaceHolderID="cpMainContentRight" runat="server">
    <h2 style="text-align:center">Support for any problems</h2>
    <ul>
        <li>M. Douglas
            <ul>
                <li>Work Phone: <strong>300-809-9982 ext. 555</strong></li>
                <li>Cell Phone: <strong>250-811-1111</strong></li>
                <li>E-Mail: <strong>mDouglas@DTB.com</strong></li>
            </ul>
        </li>
        <li>M. Scott
            <ul>
                <li>Work Phone: <strong>300-809-9982 ext. 556</strong></li>
                <li>Cell Phone: <strong>288-122-5131</strong></li>
                <li>E-Mail: <strong>mScott@DTB.com</strong></li>
            </ul>
        </li>
        <li>D. Glover
            <ul>
                <li>Work Phone: <strong>300-809-9982 ext. 560</strong></li>
                <li>Cell Phone: <strong>205-815-1232</strong></li>
                <li>E-Mail: <strong>dGlover@DTB.com</strong></li>
            </ul>
        </li>
    </ul>
</asp:Content>