<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ProductsAdmin.aspx.cs" Inherits="Ecommerce.ProductsAdmin" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cpPageTitle" runat="server">
    Products
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cpMainContentLeft" runat="server">
    <asp:Label ID="ProductListLabel" runat="server" font-underline="true" font-italic="true" Text="<h2>Product List</h2>"></asp:Label>
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CellPadding="4" DataKeyNames="ProductID" DataSourceID="eCommerceDB_GV" ForeColor="#333333" GridLines="Vertical" AllowPaging="True" AllowSorting="True" OnRowEditing="GridView1_RowEditing">
        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
        <Columns>
            <asp:CommandField ShowEditButton="True" />
            <asp:BoundField DataField="ProductID" HeaderText="ID" ReadOnly="True" SortExpression="ProductID" />
            <asp:BoundField DataField="ProductName" HeaderText="Name" SortExpression="ProductName" />
            <asp:CheckBoxField DataField="IsPublished" HeaderText="IsPublished" SortExpression="IsPublished" />
            <asp:BoundField DataField="Price" HeaderText="Price" SortExpression="Price" />
        </Columns>
        <EditRowStyle BackColor="#999999" />
        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
        <SortedAscendingCellStyle BackColor="#E9E7E2" />
        <SortedAscendingHeaderStyle BackColor="#506C8C" />
        <SortedDescendingCellStyle BackColor="#FFFDF8" />
        <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
    </asp:GridView>
    <asp:SqlDataSource ID="eCommerceDB_GV" runat="server" ConnectionString="<%$ ConnectionStrings:eCommerceDBConnectionString %>" SelectCommand="spGetProducts" SelectCommandType="StoredProcedure" UpdateCommand="spUpdateProduct" UpdateCommandType="StoredProcedure">
        <SelectParameters>
            <asp:Parameter DefaultValue="false" Name="PublishedOnly" Type="Boolean" />
        </SelectParameters>
        <UpdateParameters>
            <asp:Parameter Name="ProductID" Type="Int32" />
            <asp:Parameter Name="ProductName" Type="String" />
            <asp:Parameter Name="Description" Type="String" />
            <asp:Parameter Name="IsPublished" Type="Boolean" />
            <asp:Parameter Name="Price" Type="Decimal" />
        </UpdateParameters>
    </asp:SqlDataSource>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cpMainContentRight" runat="server">
    <asp:Label ID="Label2" runat="server" font-underline="true" font-italic="true" Text="<h2>Add new product</h2>"></asp:Label>
    <asp:DetailsView ID="DetailsView1" runat="server" AutoGenerateRows="False" CellPadding="4" DataSourceID="eCommerceDB_DV" ForeColor="#333333" GridLines="Vertical" Height="50px" Width="125px" OnItemCreated="DetailsView1_ItemCreated">
        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
        <CommandRowStyle BackColor="#E2DED6" Font-Bold="True" />
        <EditRowStyle BackColor="#999999" />
        <FieldHeaderStyle BackColor="#E9ECF1" Font-Bold="True" />
        <Fields>
            <asp:BoundField DataField="ProductName" HeaderText="Name" SortExpression="ProductName" />
            <asp:BoundField DataField="ProductDescription" HeaderText="Description" SortExpression="ProductDescription" />
            <asp:CheckBoxField DataField="IsPublished" HeaderText="IsPublished" SortExpression="IsPublished" />
            <asp:BoundField DataField="Price" HeaderText="Price" SortExpression="Price" />
            <asp:CommandField ButtonType="Button" NewText="New Product" ShowInsertButton="True" />
        </Fields>
        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
    </asp:DetailsView>
    <asp:SqlDataSource ID="eCommerceDB_DV" runat="server" ConnectionString="<%$ ConnectionStrings:eCommerceDBConnectionString %>" InsertCommand="spAddProduct" InsertCommandType="StoredProcedure" SelectCommand="select ProductName, ProductDescription, IsPublished, Price from product;">
        <InsertParameters>
            <asp:Parameter Name="ProductName" Type="String" />
            <asp:Parameter Name="ProductDescription" Type="String" />
            <asp:Parameter Name="IsPublished" Type="Boolean" />
            <asp:Parameter Name="Price" Type="Decimal" />
        </InsertParameters>
    </asp:SqlDataSource>
</asp:Content>
