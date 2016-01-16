<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="MyWebApp.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <table>
        <tr><td><asp:Label ID="Label1" runat="server" Text="Firstname:"></asp:Label></td><td><asp:TextBox ID="tbFirstName" runat="server"></asp:TextBox></td></tr>
        <tr><td><asp:Label ID="Label2" runat="server" Text="Lastname:"></asp:Label></td><td><asp:TextBox ID="tbLastName" runat="server"></asp:TextBox></td></tr>
        <tr><td><asp:Label ID="Label3" runat="server" Text="Username:"></asp:Label></td><td><asp:TextBox ID="tbUsername" runat="server"></asp:TextBox></td></tr>
    </table>
    
    <div>
        <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" />
    </div>
    <div>
        <asp:Label ID="lblResult" runat="server" ></asp:Label>
    </div>
    </form>
</body>
</html>
