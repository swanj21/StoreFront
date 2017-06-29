<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ProductAdminDetails.aspx.cs" Inherits="Ecommerce.ProductAdminDetails" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cpPageTitle" runat="server">
    Product Details
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cpMainContentLeft" runat="server">

    <asp:DetailsView ID="DetailsView1" runat="server" AutoGenerateRows="False" CellPadding="4" DataKeyNames="ProductID" DataSourceID="eCommerceDB_DV1" ForeColor="#333333" GridLines="Vertical" Height="50px" Width="400px" OnItemDeleted="DetailsView1_ItemDeleted">
        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
        <CommandRowStyle BackColor="#E2DED6" Font-Bold="True" />
        <EditRowStyle BackColor="#999999" />
        <FieldHeaderStyle BackColor="#E9ECF1" Font-Bold="True" />
        <Fields>
            <asp:BoundField DataField="ProductID" HeaderText="ProductID" ReadOnly="True" SortExpression="ProductID" InsertVisible="False" />
            <asp:BoundField DataField="ProductName" HeaderText="ProductName" SortExpression="ProductName" />
            <asp:CheckBoxField DataField="IsPublished" HeaderText="IsPublished" SortExpression="IsPublished" />
            <asp:BoundField DataField="ProductDescription" HeaderText="ProductDescription" SortExpression="ProductDescription" />
<asp:BoundField DataField="Quantity" HeaderText="Quantity" SortExpression="Quantity"></asp:BoundField>
            <asp:BoundField DataField="Price" HeaderText="Price" SortExpression="Price" />
            <asp:ImageField ControlStyle-Height="100px" ControlStyle-Width="100px" DataImageUrlField="ImageFile" DataImageUrlFormatString="~/ProductImages/{0}"  HeaderText="ImageFile" SortExpression="ImageFile" />
            <asp:CommandField ShowDeleteButton="True" ShowEditButton="True" />
        </Fields>
        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
    </asp:DetailsView>
    <asp:SqlDataSource ID="eCommerceDB_DV1" runat="server" ConnectionString="<%$ ConnectionStrings:eCommerceDBConnectionString %>" DeleteCommand="spDeleteProduct" DeleteCommandType="StoredProcedure" SelectCommand="spGetProduct" SelectCommandType="StoredProcedure" UpdateCommand="spUpdateProduct" UpdateCommandType="StoredProcedure">
        <DeleteParameters>
            <asp:Parameter Name="ProductID" Type="Int32" />
        </DeleteParameters>
        <SelectParameters>
            <asp:QueryStringParameter DefaultValue="0" Name="ProductID" QueryStringField="productid" Type="Int32" />
        </SelectParameters>
        <UpdateParameters>
            <asp:Parameter Name="ProductID" Type="Int32" />
            <asp:Parameter Name="ProductName" Type="String" />
            <asp:Parameter Name="Description" Type="String" />
            <asp:Parameter Name="IsPublished" Type="Boolean" />
            <asp:Parameter Name="Price" Type="Decimal" />
            <asp:Parameter Name="ImageFile" Type="String" />
        </UpdateParameters>
    </asp:SqlDataSource>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cpMainContentRight" runat="server">
    Upload Image File:
    <asp:FileUpload ID="ImageFileUpload" runat="server" />
    <br />
    <asp:Button ID="UploadFileButton" runat="server" Text="Upload File" OnClick="UploadFileButton_Click" />
    <br />
    <asp:Label ID="infoLabel" runat="server" Text=""></asp:Label>

</asp:Content>
