<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CustomerAdminDetails.aspx.cs" Inherits="Ecommerce.CustomerAdminDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cpPageTitle" runat="server">
    Customer Details
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cpMainContentLeft" runat="server">

    <asp:DetailsView ID="DetailsView1" runat="server" float="left" AutoGenerateRows="False" DataKeyNames="UserID" DataSourceID="eCommerceDB_DV1" Height="44px" Width="140px" CellPadding="4" ForeColor="#333333" GridLines="Vertical" OnItemDeleted="DetailsView1_ItemDeleted">
        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
        <CommandRowStyle BackColor="#E2DED6" Font-Bold="True" />
        <EditRowStyle BackColor="#999999" />
        <FieldHeaderStyle BackColor="#E9ECF1" Font-Bold="True" />
        <Fields>
            <asp:BoundField DataField="UserID" HeaderText="UserID" ReadOnly="True" SortExpression="UserID" />
            <asp:BoundField DataField="UserName" HeaderText="UserName" SortExpression="UserName" />
            <asp:BoundField DataField="EmailAddress" HeaderText="EmailAddress" SortExpression="EmailAddress" />
            <asp:CommandField ShowEditButton="true" ShowDeleteButton="true" ButtonType="Button" />
        </Fields>
        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
    </asp:DetailsView>
    <asp:SqlDataSource ID="eCommerceDB_DV1" runat="server" ConnectionString="<%$ ConnectionStrings:eCommerceDBConnectionString2 %>" SelectCommand="spGetUser" SelectCommandType="StoredProcedure" UpdateCommand="spUpdateUser" UpdateCommandType="StoredProcedure" DeleteCommand="spDeleteUser" DeleteCommandType="StoredProcedure">
        <SelectParameters>
            <asp:QueryStringParameter DefaultValue="0" Name="UserID" QueryStringField="userid" Type="Int32" />
        </SelectParameters>
        <UpdateParameters>
            <asp:Parameter Name="UserID" Type="Int32" />
            <asp:Parameter Name="UserName" Type="String" />
            <asp:Parameter Name="EmailAddress" Type="String" />
        </UpdateParameters>
    </asp:SqlDataSource>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cpMainContentRight" runat="server">

    <asp:GridView ID="GridView1" runat="server" AllowPaging="True" PageSize="50" AllowSorting="True" AutoGenerateColumns="False" CellPadding="4" DataSourceID="EcommerceDB_GV" ForeColor="#333333" GridLines="Vertical">
        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
        <Columns>
            <asp:BoundField DataField="AddressID" HeaderText="ID" SortExpression="AddressID" />
            <asp:BoundField DataField="Address1" HeaderText="Address1" SortExpression="Address1" />
            <asp:BoundField DataField="Address2" HeaderText="Address2" SortExpression="Address2" />
            <asp:BoundField DataField="Address3" HeaderText="Address3" SortExpression="Address3" />
            <asp:BoundField DataField="City" HeaderText="City" SortExpression="City" />
            <asp:BoundField DataField="StateAbbrev" HeaderText="State" SortExpression="StateAbbrev" />
            <asp:BoundField DataField="ZipCode" HeaderText="Zip" SortExpression="ZipCode" />
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
    <asp:SqlDataSource ID="EcommerceDB_GV" runat="server" ConnectionString="<%$ ConnectionStrings:eCommerceDBConnectionString %>" SelectCommand="spGetUserAddresses" SelectCommandType="StoredProcedure">
        <SelectParameters>
            <asp:QueryStringParameter DefaultValue="0" Name="UserID" QueryStringField="userid" Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>

</asp:Content>
