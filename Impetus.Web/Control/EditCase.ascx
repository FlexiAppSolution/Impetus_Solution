<%@ Control Language="C#" AutoEventWireup="true" CodeFile="EditCase.ascx.cs" Inherits="CaseManagement_Control_EditCase" %>
 <div class="bg">
    <fieldset><legend>Update CASE</legend>
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
                    <asp:TextBox ID="txtName" runat="server" Width="350px"></asp:TextBox>                    
                </td>
                <td><asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                        ErrorMessage="Required" Font-Italic="True" Font-Names="Calibri" 
                        ControlToValidate="txtName" ForeColor="Red">Required</asp:RequiredFieldValidator>
                        <br />
                        </td>
            </tr>
             <tr>
                <td class="style1">Error Code</td>
                <td><asp:TextBox ID="txtErrorCode" runat="server" Width="350px"></asp:TextBox></td>
                <td>
                        <br />
                        </td>
            </tr>  
            <tr>
                <td class="style1">Description</td>
                <td><asp:TextBox ID="txtDescription" runat="server" Width="350px" Height="70px" 
                        TextMode="MultiLine"></asp:TextBox></td>
                <td>
                        <br />
                        </td>
            </tr>
             <tr>
                <td class="style1">Resolution</td>
                <td><asp:TextBox ID="txtResolution" runat="server" Width="350px" Height="70px" 
                        TextMode="MultiLine"></asp:TextBox></td>
                <td>
                        <br />
                        </td>
            </tr>                      
            <tr>
                <td class="style1"></td>
                <td colspan="2">
                    <asp:Button ID="btnUpdate" runat="server" Text="Update Case" 
                          BackColor="Maroon" BorderColor="Maroon"  ForeColor="White"
                        BorderWidth="2px" Font-Bold="True" Font-Names="Calibri" Font-Size="Small" 
                        onclick="btnUpdate_Click" />
                </td>
            </tr>          
        </table>
    </fieldset>
</div>
