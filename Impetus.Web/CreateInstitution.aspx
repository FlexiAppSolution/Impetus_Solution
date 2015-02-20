<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="CreateInstitution.aspx.cs" Inherits="CreateInstitution" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
<asp:ScriptManager runat="server" ID="Manager1"></asp:ScriptManager>
    <section id="bodySection">	
	<div class="container">	
	<div class="row">
		<div class="span3">
			<div class="well well-small">
				<h4>Institution <span style="color:#39b9d6">Management</span></h4>
	            <p><a id="A1" href="CreateInstitution.aspx" runat="server" title="Create Institution">Create Institution</a></p>
                <p><a id="A2"  href="InstitutionList.aspx" runat="server" title="View Institution List">View Institution List</a></p>
			</div>												

		</div>
		<div class="span9">
		<div class="well well-small" style="text-align:left">
         
						
		<ul class="media-list">
		  <li class="media well well-small">
				
				<div class="media-body">
				  <h4 class="media-heading">Create Institution</h4>
                  <fieldset> 
                  <asp:UpdatePanel ID="updPanl" runat="server" RenderMode="Block" UpdateMode="Conditional" >
            <ContentTemplate>                 
        <table cellpadding="5px" cellspacing="5px">
        <tr>
        <td colspan="3">
            <asp:Label ID="lblMsg" runat="server" Font-Names="Calibri" Font-Size="Medium" 
                ForeColor="#CC3300"></asp:Label>
            </td>
        </tr>
         <tr style="border-bottom-color:Black; border-bottom-style:inset; border-bottom-width:medium; margin-bottom:15px;">
            <td colspan="2"><h5>Institution Details</h5></td>           
            </tr>
            <tr>            
                <td style="width:100px;">Name</td>
                <td>                
                    <asp:TextBox ID="txtName" runat="server" Width="350px"></asp:TextBox>                    
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
                <td style="width:100px;" >Address</td>
                <td><asp:TextBox ID="txtAddress" runat="server" Width="350px" Height="70px" 
                        TextMode="MultiLine"></asp:TextBox></td>
                <td>
                        <br />
                        </td>
            </tr>
            <tr>
                <td style="width:100px;">Website</td>
                <td><asp:TextBox ID="txtWebsite" runat="server" Width="350px"></asp:TextBox></td>
                <td>
                        <br />
                        </td>
            </tr>
            <tr><td colspan="2"></td></tr>
            <tr style="border-bottom-color:Black; border-bottom-style:inset; border-bottom-width:medium;">
            <td colspan="2"><h5>Contact Person Details</h5></td>           
            </tr>
            <tr>
                <td style="width:100px;">Name</td>
                <td><asp:TextBox ID="txtContactName" runat="server" Width="350px" ></asp:TextBox></td>
                <td><asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" 
                        ErrorMessage="Required" Font-Italic="True" Font-Names="Calibri" 
                        ControlToValidate="txtContactName" ForeColor="Red">Required</asp:RequiredFieldValidator><br />
                           <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" 
                        ControlToValidate="txtContactName" ErrorMessage="Invalid Name" Font-Bold="False" 
                        Font-Italic="True" Font-Names="calibri" Font-Size="Small" 
                        ValidationExpression="\D*" ForeColor="Red"></asp:RegularExpressionValidator>
                        
                </td>
            </tr>
            <tr>
                <td style="width:120px;">Phone</td>
                <td>
                    <asp:TextBox ID="txtPhone" runat="server" Width="350px"></asp:TextBox>
                   
                </td>
                 <td><asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" 
                        ErrorMessage="Required" Font-Italic="True" Font-Names="Calibri" 
                            ControlToValidate="txtPhone" ForeColor="Red">Required</asp:RequiredFieldValidator><br />
                               <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" 
                        ControlToValidate="txtPhone" ErrorMessage="Invalid Number" Font-Bold="False" 
                        Font-Italic="True" Font-Names="calibri" Font-Size="Small" 
                        ValidationExpression="\d*" ForeColor="Red"></asp:RegularExpressionValidator>
                            </td>
            </tr>            
            <tr>
                <td style="width:100px;">Email</td>
                <td><asp:TextBox ID="txtEmail" runat="server" Width="350px"></asp:TextBox></td>
                <td><asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" 
                        ErrorMessage="Required" Font-Italic="True" Font-Names="Calibri" 
                        ControlToValidate="txtEmail" ForeColor="Red">Required</asp:RequiredFieldValidator><br />
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
                        ControlToValidate="txtEmail" ErrorMessage="Invalid Email" Font-Italic="True" 
                        Font-Names="Calibri" 
                        ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" 
                        ForeColor="Red"></asp:RegularExpressionValidator>
                </td>
            </tr>                                                
            <tr><td colspan="2"></td></tr>
            <tr>
                <td class="style1"></td>
                <td colspan="2">
                    <asp:Button ID="btnCreate" runat="server" Text="Create Institution"    
                    BackColor="Maroon" BorderColor="Maroon" ForeColor="White"                      
                        Font-Bold="True" Font-Names="Calibri" Font-Size="Small" onclick="btnCreate_Click" />
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

