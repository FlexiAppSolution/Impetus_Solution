<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="CreateCase.aspx.cs" Inherits="CreateCase" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
<asp:ScriptManager runat="server" ID="Manager1"></asp:ScriptManager>
    <section id="bodySection">	
	<div class="container">	
	<div class="row">
		<div class="span3">
			<div class="well well-small">
				<h4>Case <span style="color:#39b9d6">Management</span></h4>
	            <p><a id="A1" href="CreateCase.aspx" runat="server" title="Create Case">Create Case</a></p>
                <p><a id="A2"  href="ViewLoggedCases.aspx" runat="server" title="Case List">View Case List</a></p>
			</div>												

		</div>
		<div class="span9">
		<div class="well well-small" style="text-align:left">
         	
		<ul class="media-list">
		  <li class="media well well-small">
				
				<div class="media-body">
				  <h4 class="media-heading">Create Case</h4>
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
                <td class="style1">Name</td>
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
                <td class="style1">Error Code</td>
                <td><asp:TextBox ID="txtErrorCode" runat="server" Width="350px"></asp:TextBox></td>
                <td>
                        <br />
                        </td>
            </tr>  
            
            <tr>
                <td class="style1"></td>
                <td colspan="2">
                    <asp:Button ID="btnCreate" runat="server" Text="Create Case"   
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

