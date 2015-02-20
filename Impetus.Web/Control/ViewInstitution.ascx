<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ViewInstitution.ascx.cs" Inherits="CaseManagement_Control_ViewInstitution" %>
<div class="bg">
    <fieldset><legend>&nbsp;Institution Details</legend>                           
             <table cellpadding="5px" cellspacing="5px">
             <tr>
        <td colspan="3">
            <asp:Label ID="lblMsg" runat="server" Font-Names="Calibri" Font-Size="Medium" 
                ForeColor="Black"></asp:Label>
            </td>
        </tr>
            <tr>
                <td class="style1">Name</td>
                <td>
                    <asp:Label ID="lblName" runat="server" Font-Names="Calibri"></asp:Label>                    
                </td>                
            </tr>
            <tr>
                <td class="style1">Code</td>
                <td>
                    <asp:Label ID="lblCode" runat="server" Font-Names="Calibri"></asp:Label>                    
                </td>                
            </tr>
            <tr>
                <td class="style1">Address</td>
                <td><asp:Label ID="lblAddress" runat="server" Font-Names="Calibri"></asp:Label></td>               
            </tr>
            <tr>
                 <td class="style1">Website</td>
                <td><asp:Label ID="lblWebsite" runat="server" Font-Names="Calibri"></asp:Label></td>  
            </tr>

             <tr>
                 <td class="style1">Status</td>
                <td><asp:Label ID="lblStatus" runat="server" Font-Names="Calibri"></asp:Label></td>  
            </tr>            
            <tr>
            <td colspan="2" style="font-family:Calibri; font-weight:bold; font-style:italic">Contact Person Details</td>
            </tr>            
             <tr>
                <td class="style1">Name</td>
                <td><asp:Label ID="lblContactName" runat="server" Font-Names="Calibri"></asp:Label></td>  
            </tr>
             <tr>
                 <td class="style1">Phone</td>
                <td><asp:Label ID="lblPhone" runat="server" Font-Names="Calibri"></asp:Label></td>  
            </tr>    
            <tr>
                 <td class="style1">Email</td>
                <td><asp:Label ID="lblEmail" runat="server" Font-Names="Calibri"></asp:Label></td>  
            </tr>                                   
        </table>           
    </fieldset>
</div>
