<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RegisterPage.aspx.cs" Inherits="NewReddit.Register" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <br />
            <h1>Register</h1><br />
            <table>
                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="RegisterUserValidation" Visible="false"/>
                <asp:Label ID="LabelError" runat="server" Text=""></asp:Label>
                <tr>
                    <td>Username: </td>
                    <td>
                        <asp:TextBox ID="txtUsername" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldUsername" runat="server" ErrorMessage="Username required" 
                            ValidationGroup="RegisterUserValidation" ControlToValidate="txtUsername"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>Name: </td>
                    <td>
                        <asp:TextBox ID="txtFirstName" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldFirstName" runat="server" ErrorMessage="First Name Required" 
                            ValidationGroup="RegisterUserValidation" ControlToValidate="txtFirstName"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>Last Name: </td>
                    <td>
                        <asp:TextBox ID="txtLastName" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldLastName" runat="server" ErrorMessage="Last Name required" 
                            ValidationGroup="RegisterUserValidation" ControlToValidate="txtLastName"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>Password: </td>
                    <td>
                        <asp:TextBox ID="txtPassword" runat="server" TextMode="Password"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldPassword" runat="server" ErrorMessage="Password required" 
                            ValidationGroup="RegisterUserValidation" ControlToValidate="txtPassword"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>Confirm Password: </td>
                    <td>
                        <asp:TextBox ID="txtConfirmPassword" runat="server" TextMode="Password"></asp:TextBox>
                        <asp:CompareValidator ID="RequiredFieldComparePassword" runat="server" ErrorMessage="Passwords do not match" ControlToCompare="txtPassword" 
                            ValidationGroup="RegisterUserValidation" ControlToValidate="txtConfirmPassword"></asp:CompareValidator>
                    </td>
                </tr>
            </table>
            
            <br />
            <asp:Button ID="ButtonSubmit" runat="server" Text="Button" ValidationGroup="RegisterUserValidation" OnClick="ButtonSubmit_Click" />
            <br />
            <p>Already have an account? <asp:Button ID="Button1" runat="server" Text="Login" OnClick="Button1_Click"/>  </p>
            
        </div>
    </form>
</body>
</html>
