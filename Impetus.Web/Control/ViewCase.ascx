<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ViewCase.ascx.cs" Inherits="CaseManagement_Control_ViewCase" %>
<div class="bg">
    <fieldset><legend>&nbsp;Case Details</legend>                           
             <table cellpadding="5px" cellspacing="5px">
             <tr>
        <td colspan="3">
            <asp:Label ID="lblMsg" runat="server" Font-Names="Calibri" Font-Size="Medium" 
                ForeColor="#CC3300"></asp:Label>
            </td>
        </tr>
         <tr>
               <td class="style1">Incident Number</td>
                <td>
                    <asp:Label ID="lblIncidentNumber" runat="server" Font-Names="Calibri"></asp:Label>                    
                </td>                
            </tr>
            <tr>
                <td class="style1">Name</td>
                <td>
                    <asp:Label ID="lblIssue" runat="server" Font-Names="Calibri"></asp:Label>                    
                </td>                
            </tr>
            <tr>
                <td class="style1">Error Code</td>
                <td>
                    <asp:Label ID="lblErrorCode" runat="server" Font-Names="Calibri"></asp:Label>                    
                </td>                
            </tr>
            <tr>
                <td class="style1">Institution</td>
                <td>
                    <asp:Label ID="lblInstitution" runat="server" Font-Names="Calibri"></asp:Label>                    
                </td>                
            </tr>
            <tr>
                 <td class="style1">Description</td>
                <td><asp:Label ID="lblDescription" runat="server" Font-Names="Calibri"></asp:Label></td>  
            </tr>      
            <tr>
                <td class="style1">Logged By</td>
                <td><asp:Label ID="lblLoggedBy" runat="server" Font-Names="Calibri"></asp:Label></td>               
            </tr>
            <tr>
                 <td class="style1">Date Logged</td>
                <td><asp:Label ID="lblDateLogged" runat="server" Font-Names="Calibri"></asp:Label></td>  
            </tr>

             <tr>
                 <td class="style1">Resolution</td>
                <td><asp:Label ID="lblResolution" runat="server" Font-Names="Calibri"></asp:Label></td>  
            </tr>                                                                                                                
        </table>           
    </fieldset>
</div>