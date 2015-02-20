<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ViewLicenseRequest.ascx.cs" Inherits="CaseManagement_Control_ViewLicenseRequest" %>
<div class="bg">
    <fieldset><legend>License Request Details</legend>
    <table cellpadding="5px" cellspacing="5px">   
    <tr>
        <td colspan="3">
            <asp:Label ID="lblMsg" runat="server" Font-Names="Calibri" Font-Size="Medium" 
                ForeColor="#CC3300"></asp:Label>
            </td>
        </tr>     
           <tr>
                <td  class="style1">
                   Quantity Requested
                   </td>
                <td>
                    <asp:Label ID="lblNoOfLicense" runat="server" Font-Names="Calibri"></asp:Label>                    
                </td>
               
            </tr>  
            <tr>
                <td class="style1">
            Requesting User</td>
                <td>
                    <asp:Label ID="lblRequestingUser" runat="server" Font-Names="Calibri"></asp:Label>                    
                </td>
               
            </tr>  
            <tr>
                <td class="style1">Requesting Institution</td>
                <td>
                    <asp:Label ID="lblInstitution" runat="server" Font-Names="Calibri" ></asp:Label>                    
                </td>
               
            </tr>  
            <tr>
                <td class="style1">Status</td>
                <td>
                    <asp:Label ID="lblStatus" runat="server" Font-Names="Calibri"></asp:Label>                    
                </td>
               
            </tr>   
            <tr>
                <td  class="style1">Date Requested</td>
                <td>
                    <asp:Label ID="lblDateRequested" runat="server" Font-Names="Calibri" ></asp:Label>                    
                </td>
               
            </tr> 
            <tr>
                <td  class="style1">License Used</td>
                <td>
                    <asp:Label ID="lblLicenseUsed" runat="server" Font-Names="Calibri"></asp:Label>                    
                </td>
               
            </tr>    
              <tr>
                <td class="style1">Quantity Approved</td>
                <td>
                    <asp:Label ID="lblQuantityApproved" runat="server" Font-Names="Calibri"></asp:Label>                    
                </td>
               
            </tr>                      
                <tr>
                <td class="style1">Duration (Weeks)</td>
                <td>
                    <asp:Label ID="lblDuration" runat="server" Font-Names="Calibri" ></asp:Label>                    
                </td>
               
            </tr>                    
        </table>        
    </fieldset>
</div>