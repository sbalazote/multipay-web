<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PushNotification.aspx.cs" Inherits="Multipay.PushNotification" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    
        Bienvenido<br />
        <br />
        <asp:Label ID="Label1" runat="server" Text="Message:  "></asp:Label>
        <asp:TextBox ID="Text1" runat="server" OnTextChanged="TextBox1_TextChanged"></asp:TextBox>
        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Enviar" />
        <br />
        <br />
        <asp:Label ID="Label3" runat="server" Text=""></asp:Label>
    </form>
</body>
</html>