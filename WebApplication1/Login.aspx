<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="WebApplication1.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div style="background:#fff8dc">
            <h1 style="text-align:center">登入頁面</h1>    
            <p>&nbsp;&nbsp; </p>
              <p>&nbsp;&nbsp; </p>
              <p>&nbsp;&nbsp; </p>
            <h3 style="text-align:center;font-family:標楷體">帳號：<asp:TextBox ID="TextBox1" runat="server"></asp:TextBox></h3>
               <h3 style="text-align:center;font-family:標楷體">密碼：<asp:TextBox ID="TextBox2" runat="server" type="Password"></asp:TextBox></h3>
            
             <p style="text-align:center"><asp:Button ID="Button1" runat="server" Text="Login" BackColor="#FF0066" Font-Names="標楷體" Height="50px" OnClick="Button1_Click" Width="124px" /></p> 
        </div>
    </form>
</body>
</html>
