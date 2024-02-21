<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LoginPage.aspx.cs" Inherits="NewReddit.LoginPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <br />
            <h1>Login</h1><br />
            <table>
                <tr>
                    <td>
                        <p>Username: </p>           
                    </td>
                    <td>
                        <asp:TextBox ID="txtUsername" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFielUsername" runat="server" ErrorMessage="Required Field" ControlToValidate="txtUsername" ValidationGroup="GroupLogin"></asp:RequiredFieldValidator>   
                    </td>
                </tr>
                <tr>
                    <td>
                        <p>Password: </p>
                    </td>
                    <td>
                        <asp:TextBox ID="txtPassword" runat="server" TextMode="Password"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Required Field" ControlToValidate="txtPassword" ValidationGroup="GroupLogin"></asp:RequiredFieldValidator>   

                    </td>
                </tr>
            </table>
            <asp:Button ID="btnSubmit" runat="server" Text="Login" OnClick="btnSubmit_Click" />

        </div>
        <div>
            <asp:Label ID="lblError" runat="server" Text=""></asp:Label>
        </div>
        <div>
            <p>Don't have an account? <asp:Button ID="Button1" runat="server" Text="Create Account" OnClick="Button1_Click"/>  </p>
        </div>
    </form>
</body>
</html>
