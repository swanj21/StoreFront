<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CustomersAdmin.aspx.cs" Inherits="Ecommerce.CustomersAdmin" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cpPageTitle" runat="server">
    Customer Administration
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cpMainContentLeft" runat="server">
    <asp:Label ID="Label1" runat="server" font-italic="true" Font-Underline="true" Text="<h2>User Records</h2>"></asp:Label>
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="UserID" DataSourceID="eCommerceDB_GV" Height="140px" CellPadding="4" ForeColor="#333333" GridLines="Vertical" AllowPaging="True" AllowSorting="True" PageSize="50" OnRowEditing="GridView1_RowEditing"> 
        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
    <Columns>
        <asp:CommandField ShowEditButton="True"/>
        <asp:BoundField DataField="UserID" HeaderText="UserID" ReadOnly="True" SortExpression="UserID" />
        <asp:BoundField DataField="UserName" HeaderText="UserName" SortExpression="UserName" />
        <asp:BoundField DataField="EmailAddress" HeaderText="EmailAddress" SortExpression="EmailAddress" />
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

    <asp:SqlDataSource ID="eCommerceDB_GV" runat="server" ConnectionString="<%$ ConnectionStrings:eCommerceDBConnectionString %>" SelectCommand="spGetUsers" SelectCommandType="StoredProcedure" InsertCommand="spAddUser" InsertCommandType="StoredProcedure">
    <InsertParameters>
        <asp:Parameter Name="UserName" Type="String" />
        <asp:Parameter Name="EmailAddress" Type="String" />
    </InsertParameters>
    </asp:SqlDataSource>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="cpMainContentRight" runat="server">
    <asp:Label ID="Label2" runat="server" font-italic="true" Font-Underline="true" Text="<h2>Add User Record</h2>"></asp:Label>
    <asp:DetailsView ID="DetailsView1" runat="server" AutoGenerateRows="False" DataSourceID="eCommerceDB_DV" Height="50px" Width="125px" CellPadding="4" ForeColor="#333333" GridLines="Vertical" DataKeyNames="UserID" OnItemCreated="DetailsView1_ItemCreated">
        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
        <CommandRowStyle BackColor="#E2DED6" Font-Bold="True" />
        <EditRowStyle BackColor="#999999" />
        <FieldHeaderStyle BackColor="#E9ECF1" Font-Bold="True" />
        <Fields>
            <asp:BoundField DataField="UserName" HeaderText="UserName" SortExpression="UserName"/>
            <asp:BoundField DataField="EmailAddress" HeaderText="EmailAddress" SortExpression="EmailAddress"/>
            <asp:CommandField ShowInsertButton="True" NewText="New User" ButtonType="Button"/>
        </Fields>
        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
    </asp:DetailsView>
    <asp:SqlDataSource ID="eCommerceDB_DV" runat="server" ConnectionString="<%$ ConnectionStrings:eCommerceDBConnectionString %>" SelectCommand="spGetUsers" SelectCommandType="StoredProcedure" InsertCommand="spAddUser" InsertCommandType="StoredProcedure">
    <InsertParameters>
        <asp:Parameter Name="UserName" Type="String" />
        <asp:Parameter Name="EmailAddress" Type="String" />
    </InsertParameters>
    </asp:SqlDataSource>
</asp:Content>