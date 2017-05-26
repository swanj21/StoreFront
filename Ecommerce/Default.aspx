<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Ecommerce._Default" %>

<asp:Content ID ="PageTitle" ContentPlaceHolderID="cpPageTitle" runat="server">
    Downtown Brown Ice Rink
</asp:Content>
<asp:Content ID="BodyContentLeft" ContentPlaceHolderID="cpMainContentLeft" runat="server">
    <h2>Welcome to the site!</h2>
    <strong>We sell custom made, new, or used gear such as</strong>
    <ul>
        <li><i>Helmets</i></li>
        <li><i>Sticks</i></li>
        <li><i>Skates</i></li>
        <li><i>Pads</i></li>
    </ul>
    <strong>We also offer lessons, clinics, and leagues to all skill levels!</strong>
    <ul>
        <li>Learn to skate
            <ul>
                <li>Wednesday, Thursday, Saturday at 12:45pm</li>
            </ul>
        </li>
        <li>High School Hockey
            <ul>
                <li>Tuesday, Friday, Saturday, Sunday from 5-9pm</li>
            </ul>
        </li>
        <li>Stick Time
            <ul>
                <li>Saturday and Sunday at 9am</li>
            </ul>
        </li>
        <li>Adult League
            <ul>
                <li>A -> Sunday from 12-9pm</li>
                <li>B -> Friday and Saturday from 1-6pm</li>
                <li>C -> Monday and Wednesday from 6-11pm</li>
                <li>D -> Tuesday and Thursday from 6-11pm</li>
            </ul>
        </li>
    </ul>

</asp:Content>
<asp:Content ID="BodyContentRight" ContentPlaceHolderID="cpMainContentRight" runat="server">
    <h2>Contact us at any time!</h2>
    <ul>
        <li>By phone at 
            <ul>
                <li>
                    Toll-Free: <strong>1-800-809-9982</strong>
                </li>
                <li>
                    Not Toll-Free: <strong>1-800-811-1111</strong>
                </li>
                <li>Employee cell phone: <strong>1-313-505-9292</strong></li>
            </ul>
        </li>
        <li>By E-mail at
            <ul>
                <li>thisIsARealPlace@thisRink.com</li>
            </ul>
        </li>
    </ul>
</asp:Content>