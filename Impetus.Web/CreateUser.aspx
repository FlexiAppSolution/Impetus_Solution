<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="CreateUser.aspx.cs" Inherits="CreateUser" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
<asp:ScriptManager runat="server" ID="Manager1"></asp:ScriptManager>
    <section id="bodySection">	
	<div class="container">	
	<div class="row">
		<div class="span3">
			<div class="well well-small">
				<h4>User <span style="color:#39b9d6">Management</span></h4>
	            <p><a id="A1" href="CreateUser.aspx" runat="server" title="Create User">Create User</a></p>
                <p><a id="A2"  href="UserList.aspx" runat="server" title="User List">View User List</a></p>
			</div>												

		</div>
		<div class="span9">
		<div class="well well-small" style="text-align:left">
         	
		<ul class="media-list">
		  <li class="media well well-small">
				
				<div class="media-body">
				  <h4 class="media-heading">Create User</h4>
                  <fieldset> 
                  <asp:UpdatePanel ID="updPanl" runat="server" RenderMode="Block" UpdateMode="Conditional" >
            <ContentTemplate>                 
       <table cellpadding="10px" cellspacing="10px">        
            <tr>
                <td class="style1">Last Name</td>
                <td>
                    <asp:TextBox ID="txtLastName" runat="server" Width="350px"></asp:TextBox>                    
                </td>
                <td><asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                        ErrorMessage="Required" Font-Italic="True" Font-Names="Calibri" 
                        ControlToValidate="txtLastName" ForeColor="Red">Required</asp:RequiredFieldValidator>
                        <br />
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" 
                        ControlToValidate="txtLastName" ErrorMessage="Invalid Name" Font-Bold="False" 
                        Font-Italic="True" Font-Names="calibri" Font-Size="Small" 
                        ValidationExpression="\D*" ForeColor="Red"></asp:RegularExpressionValidator>
                        </td>
            </tr>
            <tr>
                <td class="style1">First Name</td>
                <td><asp:TextBox ID="txtFirstName" runat="server" Width="350px"></asp:TextBox></td>
                <td><asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                        ErrorMessage="Required" Font-Italic="True" Font-Names="Calibri" 
                        ControlToValidate="txtFirstName" ForeColor="Red">Required</asp:RequiredFieldValidator>
                        <br />
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" 
                        ControlToValidate="txtFirstName" ErrorMessage="Invalid Name" Font-Bold="False" 
                        Font-Italic="True" Font-Names="calibri" Font-Size="Small" 
                        ValidationExpression="\D*" ForeColor="Red"></asp:RegularExpressionValidator>
                        </td>
            </tr>
            <tr>
                <td class="style1">Employee Number</td>
                <td><asp:TextBox ID="txtEmployeeNum" runat="server" Width="350px"></asp:TextBox></td>
                <td>
                    &nbsp;</td>
            </tr>            
            <tr>
                <td class="style1">Phone Number</td>
                <td><asp:TextBox ID="txtPhoneNum" runat="server" Width="350px"></asp:TextBox></td>
                <td><asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
                        ErrorMessage="Required" Font-Italic="True" Font-Names="Calibri" 
                        ControlToValidate="txtPhoneNum" ForeColor="Red">Required</asp:RequiredFieldValidator>
                        <br />
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" 
                        ControlToValidate="txtPhoneNum" ErrorMessage="Invalid Number" Font-Bold="False" 
                        Font-Italic="True" Font-Names="calibri" Font-Size="Small" 
                        ValidationExpression="\d*" ForeColor="Red"></asp:RegularExpressionValidator>
                        </td>
            </tr>
            <tr>
                <td class="style1">Email</td>
                <td><asp:TextBox ID="txtEmail" runat="server" Width="350px" ></asp:TextBox></td>
                <td><asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" 
                        ErrorMessage="Required" Font-Italic="True" Font-Names="Calibri" 
                        ControlToValidate="txtEmail" ForeColor="Red">Required</asp:RequiredFieldValidator><br />
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
                        ControlToValidate="txtEmail" ErrorMessage="Invalid Email" Font-Italic="True" 
                        Font-Names="Calibri" 
                        ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" 
                        ForeColor="Red"></asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr>
                <td class="style1">Address</td>
                <td>
                    <asp:TextBox ID="txtAddress" runat="server" Width="350px" Height="45px" 
                        TextMode="MultiLine"></asp:TextBox></td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style1">Institution</td>
                <td>
                    <asp:DropDownList ID="comboInstitution" runat="server" Width="365px" 
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
                <td class="style1">Role</td>
                <td>
                    <asp:DropDownList ID="comboRole" runat="server" Width="365px" 
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
                <td class="style1">Username</td>
                <td><asp:TextBox ID="txtUsername" runat="server" Width="350px"></asp:TextBox></td>
                <td><asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" 
                        ErrorMessage="Required" Font-Italic="True" Font-Names="Calibri" 
                        ControlToValidate="txtUsername" ForeColor="Red">Required</asp:RequiredFieldValidator></td>
            </tr>  
                    
            <tr>
                <td class="style1"></td>
                <td colspan="2">
                    <asp:Button ID="btnCreate" runat="server" Text="Create User"   
                    BackColor="Maroon" BorderColor="Maroon" ForeColor="White"                     
                        BorderWidth="2px" Font-Bold="True" Font-Names="Calibri" Font-Size="Small" onclick="btnCreate_Click" />
                </td>
            </tr>       
             <tr>                   
                   <td colspan="3"> <asp:UpdateProgress ID="UPG1" runat="server" AssociatedUpdatePanelID="updPanl"  Visible="true"  >
                        <ProgressTemplate >
                            <p style="color:Maroon">Processing... Please wait.</p>
                        </ProgressTemplate>
                    </asp:UpdateProgress>  </td>
                   </tr>
        </table>
        </ContentTemplate>
        </asp:UpdatePanel>  
                  </fieldset>				
					<br/>
				</div>
		  </li>		  
		</ul>	
           
		
		</div>
		</div>
		</div>
	</div>
</section>
<asp:ObjectDataSource ID="objRoles" runat="server" SelectMethod="GetRoles"
                    TypeName="EvoPaj.Logic.RoleSystem">   
               
</asp:ObjectDataSource>
<asp:ObjectDataSource ID="objInstitutions" runat="server" SelectMethod="GetInstitutions"
                    TypeName="EvoPaj.Logic.InstitutionSystem" >  
                                   
</asp:ObjectDataSource>
</asp:Content>

