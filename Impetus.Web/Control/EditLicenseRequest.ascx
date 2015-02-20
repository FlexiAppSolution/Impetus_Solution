<%@ Control Language="C#" AutoEventWireup="true" CodeFile="EditLicenseRequest.ascx.cs" Inherits="CaseManagement_Control_EditLicenseRequest" %>
<style type="text/css">
    .style1
    {        
        width: 180px;
        font-family:Calibri;
        color:Maroon;
        font-weight:bold;     
    }
</style>
<div class="bg">
    <fieldset><legend>Update License Request</legend>
    <table cellpadding="3px" cellspacing="3px">        
            <tr>
                <td class="style1">Quantity Requested</td>
                <td>
                    <asp:Label ID="lblNoOfLicense" runat="server" Font-Name="Calibri"></asp:Label>                    
                </td>
               
            </tr>  
            <tr>
                <td class="style1">License Type</td>
                <td>
                    <asp:Label ID="lblLicenseType" runat="server" Font-Name="Calibri"></asp:Label>                    
                </td>
               
            </tr>  
            <tr>
                <td class="style1">Requesting User</td>
                <td>
                    <asp:Label ID="lblRequestingUser" runat="server"  Font-Name="Calibri"></asp:Label>                    
                </td>
               
            </tr>  
            <tr>
                <td class="style1">Requesting Institution</td>
                <td>
                    <asp:Label ID="lblInstitution" runat="server"  Font-Name="Calibri"></asp:Label>                    
                </td>
               
            </tr>  
            <tr>
                <td class="style1">Status</td>
                <td>
                    <asp:Label ID="lblStatus" runat="server" Font-Name="Calibri"></asp:Label>                    
                </td>
               
            </tr>   
            <tr>
                <td  class="style1">Date Requested</td>
                <td>
                    <asp:Label ID="lblDateRequested" runat="server"  Font-Name="Calibri"></asp:Label>                    
                </td>
               
            </tr> 
            <tr>
                <td  class="style1">License Used</td>
                <td>
                    <asp:Label ID="lblLicenseUsed" runat="server" Font-Name="Calibri"></asp:Label>                    
                </td>
               
            </tr>    
            <tr>
                <td class="style1">Quantity Approved</td>
                <td>
                    <asp:TextBox ID="txtQtyApproved" runat="server" Width="300px" Font-Bold="True" 
                        ForeColor="Black"></asp:TextBox>                    
                </td>               
            </tr>          
                <tr>
                <td class="style1">Duration (Weeks)</td>
                <td>
                    <asp:DropDownList ID="ddlDuration" runat="server" Width="300px">
                        <asp:ListItem Value="None">--Select--</asp:ListItem>
                        <asp:ListItem Value="Two">2 (1/2 Month)</asp:ListItem>
                        <asp:ListItem Value="Four">4 (1 Month)</asp:ListItem>
                        <asp:ListItem Value="Twelve">12 (3 Months)</asp:ListItem>
                        <asp:ListItem Value="TwentyFour">24 (6 Months)</asp:ListItem>
                        <asp:ListItem Value="FiftyTwo">52 ( 1 year)</asp:ListItem>
                        <asp:ListItem Value="LifeTime">LifeTime</asp:ListItem>
                    </asp:DropDownList>
                </td>
               
            </tr>         
            <tr>
              <td></td>
                <td >
                     <asp:Button ID="btnApprove" runat="server" Text="Approve" 
                        BackColor="Maroon" BorderColor="Maroon"  ForeColor="White"
                        BorderWidth="2px" Font-Bold="True" Font-Names="Calibri" Font-Size="Small" onclick="btnUpdate_Click" />
                        &nbsp;&nbsp;                        
                        <asp:Button ID="btnDecline" runat="server" Text="Decline" 
                        BackColor="Maroon" BorderColor="Maroon" ForeColor="White"
                        BorderWidth="2px" Font-Bold="True" Font-Names="Calibri" Font-Size="Small" onclick="btnDecline_Click" />
                </td>
                
                
            </tr>
        </table>        
    </fieldset>
</div>

