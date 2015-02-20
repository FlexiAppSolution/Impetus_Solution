<%@ Control Language="C#" AutoEventWireup="true" CodeFile="EditUser.ascx.cs" Inherits="CaseManagement_Control_EditUser" %>
<style type="text/css">
    .style1
    {
        width: 260px;
        font-family:Calibri;
        color:Maroon;
        font-weight:bold;        
    }
</style>
<div class="bg">
    <fieldset><legend>&nbsp;Edit User</legend>                           
              <table cellpadding="3px" cellspacing="3px">
                <tr>
                <td class="style1" >Last Name</td>
                <td>
                    <asp:TextBox ID="txtLastName" runat="server" Width="300px"></asp:TextBox>                    
                </td>
                <td><asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                        ErrorMessage="Required" Font-Italic="True" Font-Names="Calibri" 
                        ControlToValidate="txtLastName" ForeColor="Red">Required</asp:RequiredFieldValidator>                                        
                        </td>
            </tr>
            <tr>
                <td class="style1" >First Name</td>
                <td><asp:TextBox ID="txtFirstName" runat="server" Width="300px"></asp:TextBox></td>
                <td><asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                        ErrorMessage="Required" Font-Italic="True" Font-Names="Calibri" 
                        ControlToValidate="txtFirstName" ForeColor="Red">Required</asp:RequiredFieldValidator>                                            
                        </td>
            </tr>
            <tr>
                <td class="style1" >Employee Number</td>
                <td><asp:TextBox ID="txtEmployeeNum" runat="server" Width="300px"></asp:TextBox></td>
                <td>
                    &nbsp;</td>
            </tr>            
            <tr>
                <td class="style1" >Phone Number</td>
                <td><asp:TextBox ID="txtPhoneNum" runat="server" Width="300px"></asp:TextBox></td>
                <td><asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
                        ErrorMessage="Required" Font-Italic="True" Font-Names="Calibri" 
                        ControlToValidate="txtPhoneNum" ForeColor="Red">Required</asp:RequiredFieldValidator>                      
                        </td>
            </tr>
            <tr>
                <td class="style1" >Email</td>
                <td><asp:TextBox ID="txtEmail" runat="server" Width="300px" ></asp:TextBox></td>
                <td><asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" 
                        ErrorMessage="Required" Font-Italic="True" Font-Names="Calibri" 
                       ControlToValidate="txtEmail" ForeColor="Red">Required</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="style1" >Address</td>
                
                   
                        <td>
                            <asp:TextBox ID="txtAddress" runat="server" Height="45px" TextMode="MultiLine" 
                                Width="300px"></asp:TextBox>
                        </td>
                        <td>
                            &nbsp;</td>
                   
                
            </tr>
            <tr>
                <td class="style1" >Institution</td>
                <td>
                    <asp:DropDownList ID="comboInstitution" runat="server" Width="300px" 
                         DataSourceID="objInstitutions" DataTextField="InstitutionName" 
                        DataValueField="ID" AutoPostBack="true" AppendDataBoundItems="True" >
                     <asp:ListItem Enabled="true" Value="0">--select one--</asp:ListItem>  
                    </asp:DropDownList>
                   
                </td>
                 <td><asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" 
                        ErrorMessage="Required" Font-Italic="True" Font-Names="Calibri" 
                            ControlToValidate="comboInstitution" ForeColor="Red" InitialValue="0">Required</asp:RequiredFieldValidator></td>
            </tr>   
            <tr>
                <td class="style1" >Role</td>
                <td>
                    <asp:DropDownList ID="comboRole" runat="server" Width="300px" 
                        DataSourceID="objRoles" DataTextField="Name" DataValueField="ID" AutoPostBack="true"
                        AppendDataBoundItems="True">
                    <asp:ListItem Enabled="true" Value="0">--select one--</asp:ListItem>  
                    </asp:DropDownList>
                </td>
                 <td><asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                        ErrorMessage="Required" Font-Italic="True" Font-Names="Calibri" 
                            ControlToValidate="comboRole" ForeColor="Red" InitialValue="0">Required</asp:RequiredFieldValidator></td>
            </tr>
            <tr>
                <td class="style1" >Status</td>
                <td>
                    <asp:DropDownList ID="comboStatus" runat="server" Width="300px" AutoPostBack="true" >
                     <asp:ListItem Enabled="true" Value="0">--Select--</asp:ListItem>
                    <asp:ListItem  Value="1">InActive</asp:ListItem>  
                    <asp:ListItem  Value="2">Active</asp:ListItem> 
                    </asp:DropDownList>
                   
                </td>
                 <td><asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" 
                        ErrorMessage="Required" Font-Italic="True" Font-Names="Calibri" 
                            ControlToValidate="comboStatus" ForeColor="Red" InitialValue="0">Required</asp:RequiredFieldValidator></td>
            </tr>               
            <tr>
                <td class="style1" >Username</td>
                <td><asp:TextBox ID="txtUser" runat="server" Width="300px"></asp:TextBox></td>
                <td><asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" 
                        ErrorMessage="Required" Font-Italic="True" Font-Names="Calibri" 
                        ControlToValidate="txtUser" ForeColor="Red">Required</asp:RequiredFieldValidator></td>
            </tr>            
            <tr>
                <td class="style1" >
                    &nbsp;</td>
                <td colspan="2">
                    <asp:Button ID="btnUpdate" runat="server" Text="Update User" 
                        BackColor="Maroon" BorderColor="Maroon" ForeColor="White"
                        BorderWidth="2px" Font-Bold="True" Font-Names="Calibri" Font-Size="Small" onclick="btnUpdate_Click"  />
                </td>
            </tr>
        </table>
    </fieldset>
</div>
<asp:ObjectDataSource ID="objInstitutions" runat="server" SelectMethod="GetInstitutions"
                    TypeName="EvoPaj.Logic.InstitutionSystem" >  
                                   
</asp:ObjectDataSource>
<asp:ObjectDataSource ID="objRoles" runat="server" SelectMethod="GetRoles"
                    TypeName="EvoPaj.Logic.RoleSystem">   
               
</asp:ObjectDataSource>
