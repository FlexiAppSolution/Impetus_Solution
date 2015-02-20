<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ViewUser.ascx.cs" Inherits="CaseManagement_Control_ViewUser" %>
<div class="bg">
    <fieldset><legend>&nbsp;User Details</legend>                           
             <table cellpadding="5px" cellspacing="5px">
             <tr>
        <td colspan="3">
            <asp:Label ID="lblMsg" runat="server" Font-Names="Calibri" Font-Size="Medium" 
                ForeColor="#CC3300"></asp:Label>
            </td>
        </tr>
            <tr>
                <td class="style1">Name</td>
                <td>
                    <asp:Label ID="lblFullName" runat="server" Font-Names="Calibri"></asp:Label>                    
                </td>                
            </tr>
            <tr>
                <td class="style1">Employee Number</td>
                <td>
                    <asp:Label ID="lblEmployeeNum" runat="server" Font-Names="Calibri"></asp:Label>                    
                </td>                
            </tr>
            <tr>
                <td class="style1">Phone Number</td>
                <td><asp:Label ID="lblPhoneNum" runat="server" Font-Names="Calibri"></asp:Label></td>               
            </tr>
            <tr>
                 <td class="style1">Email</td>
                <td><asp:Label ID="lblEmail" runat="server" Font-Names="Calibri"></asp:Label></td>  
            </tr>
             <tr>
                 <td class="style1">Status</td>
                <td><asp:Label ID="lblstatus" runat="server" Font-Names="Calibri"></asp:Label></td>  
            </tr>      
             <tr>
                 <td class="style1">Address</td>
                <td><asp:Label ID="lblAddress" runat="server" Font-Names="Calibri"></asp:Label></td>  
            </tr>                                  
             <tr>
                <td class="style1">Institution</td>
                <td><asp:Label ID="lblInstitution" runat="server" Font-Names="Calibri"></asp:Label></td>  
            </tr>
             <tr>
                 <td class="style1">Role</td>
                <td><asp:Label ID="lblRole" runat="server" Font-Names="Calibri"></asp:Label></td>  
            </tr>                                                              
        </table>           
    </fieldset>
</div>

