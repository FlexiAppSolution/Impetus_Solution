<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="ChangeAdminPassword.aspx.cs" Inherits="ChangeAdminPassword" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
<asp:ScriptManager runat="server" ID="Manager1"></asp:ScriptManager>
    <section id="bodySection">	
	<div class="container">	
	<div class="row">
		<div class="span3">
			<div class="well well-small">
				<h4>Password <span style="color:#39b9d6">Management</span></h4>
	            <p><a id="A1" href="ChangeAdminPassword.aspx" runat="server" title="Change Password">Change Password</a></p>               
			</div>												

		</div>
		<div class="span9">
		<div class="well well-small" style="text-align:left">
         	
		<ul class="media-list">
		  <li class="media well well-small">
				
				<div class="media-body">
				  <h4 class="media-heading">Change Password</h4>
                  <fieldset> 
                  <asp:UpdatePanel ID="updPanl" runat="server" RenderMode="Block" UpdateMode="Conditional" >
            <ContentTemplate>                 
       <table cellpadding="10px" cellspacing="10px">        
            <tr>
        <td colspan="3">
            <asp:Label ID="lblMsg" runat="server" Font-Names="Calibri" Font-Size="Medium" 
                ForeColor="#CC3300"></asp:Label>
            </td>
        </tr>
            <tr>
                <td class="style1">Current Password</td>
                <td>
                    <asp:TextBox ID="txtCurrentPassword" runat="server" Width="350px" 
                        TextMode="Password"></asp:TextBox>                    
                </td>
                <td><asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                        ErrorMessage="Required" Font-Italic="True" Font-Names="Calibri" 
                        ControlToValidate="txtCurrentPassword" ForeColor="Red">Required</asp:RequiredFieldValidator>
                        <br />
                        </td>
            </tr>           
            <tr>
                <td class="style1">New Password</td>
                <td><asp:TextBox ID="txtNewPassword" runat="server" Width="350px" 
                        TextMode="Password"></asp:TextBox></td>
                <td>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                        ErrorMessage="Required" Font-Italic="True" Font-Names="Calibri" 
                        ControlToValidate="txtNewPassword" ForeColor="Red">Required</asp:RequiredFieldValidator>
                        <br />
                        </td>
            </tr>              
             <tr>
                <td class="style1">Confirm Password</td>                
                <td><asp:TextBox ID="txtConfirmPassword" runat="server" Width="350px" 
                        TextMode="Password"></asp:TextBox></td>
                <td>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                        ErrorMessage="Required" Font-Italic="True" Font-Names="Calibri" 
                        ControlToValidate="txtConfirmPassword" ForeColor="Red">Required</asp:RequiredFieldValidator>
                        <br />
                        </td>
            </tr> 
            <tr>
                <td class="style1"></td>
                <td colspan="2">
                    <asp:Button ID="btnCreate" runat="server" Text="Change Password"   
                    BackColor="Maroon" BorderColor="Maroon" ForeColor="White"                     
                        BorderWidth="2px" Font-Bold="True" Font-Names="Calibri" Font-Size="Small" onclick="btnChangePassword_Click" />
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

</asp:Content>

