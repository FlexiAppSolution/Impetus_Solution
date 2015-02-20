<%@ Control Language="C#" AutoEventWireup="true" CodeFile="EditInstitution.ascx.cs" Inherits="CaseManagement_Control_EditInstitution" %>
<div class="bg">
    <fieldset><legend>&nbsp;Institution Details</legend>                           
             <table cellpadding="3px" cellspacing="3px">
          <tr>
        <td colspan="3">
            <asp:Label ID="lblMsg" runat="server" Font-Names="Calibri" Font-Size="Medium" 
                ForeColor="Black"></asp:Label>
            </td>
        </tr>
            <tr>            
                <td class="style1">Name</td>
                <td>                
                    <asp:TextBox ID="txtName" runat="server" Width="300px"></asp:TextBox>                    
                </td>
                <td><asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                        ErrorMessage="Required" Font-Italic="True" Font-Names="Calibri" 
                        ControlToValidate="txtName" ForeColor="Red">Required</asp:RequiredFieldValidator>
                        <br />
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" 
                        ControlToValidate="txtName" ErrorMessage="Invalid Name" Font-Bold="False" 
                        Font-Italic="True" Font-Names="calibri" Font-Size="Small" 
                        ValidationExpression="\D*" ForeColor="Red"></asp:RegularExpressionValidator>
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
                <td><asp:TextBox ID="txtAddress" runat="server" Width="300px" Height="70px" 
                        TextMode="MultiLine"></asp:TextBox></td>
                <td>                        
                        </td>
            </tr>
            <tr>
                <td class="style1">Website</td>
                <td><asp:TextBox ID="txtWebsite" runat="server" Width="300px"></asp:TextBox></td>
                <td>
                        <br />
                        </td>
            </tr>
            <tr>
                <td class="style1">Status</td>
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
            <td colspan="2"><asp:Label ID="lblTitle" runat="server" Text="Contact Person" Font-Names="Calibri" 
            Font-Underline="true" Font-Bold="true" Font-Size="Large" Font-Italic="true"></asp:Label></td>
            </tr>
            <tr>
                <td class="style1">Name</td>
                <td><asp:TextBox ID="txtContactName" runat="server" Width="300px" ></asp:TextBox></td>
                <td><asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" 
                        ErrorMessage="Required" Font-Italic="True" Font-Names="Calibri" 
                        ControlToValidate="txtContactName" ForeColor="Red">Required</asp:RequiredFieldValidator>                           
                        
                </td>
            </tr>
            <tr>
                <td class="style1">Phone</td>
                <td>
                    <asp:TextBox ID="txtPhone" runat="server" Width="300px"></asp:TextBox>
                   
                </td>
                 <td><asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" 
                        ErrorMessage="Required" Font-Italic="True" Font-Names="Calibri" 
                            ControlToValidate="txtPhone" ForeColor="Red">Required</asp:RequiredFieldValidator>                              
            </tr>            
            <tr>
                <td class="style1">Email</td>
                <td><asp:TextBox ID="txtEmail" runat="server" Width="300px"></asp:TextBox></td>
                <td><asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" 
                        ErrorMessage="Required" Font-Italic="True" Font-Names="Calibri" 
                        ControlToValidate="txtEmail" ForeColor="Red">Required</asp:RequiredFieldValidator>               
                </td>
            </tr>                                                            
            <tr>
                <td class="style1"></td>
                <td colspan="2">
                    <asp:Button ID="btnUpdate" runat="server" Text="Update Institution" 
                          BackColor="Maroon" BorderColor="Maroon" ForeColor="White"
                        BorderWidth="2px" Font-Bold="True" Font-Names="Calibri" Font-Size="Small" 
                        onclick="btnUpdate_Click" />
                </td>
            </tr>                                 
        </table>           
    </fieldset>
</div>

