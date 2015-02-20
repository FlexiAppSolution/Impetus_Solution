<%@ Page Title="" Language="C#" MasterPageFile="~/RegularSite.master" AutoEventWireup="true" CodeFile="RequestForLicense.aspx.cs" Inherits="RequestForLicense" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
<asp:ScriptManager runat="server" ID="Manager1"></asp:ScriptManager>
    <section id="bodySection">	
	<div class="container">	
	<div class="row">
		<div class="span3">
			<div class="well well-small">
				<h4>License <span style="color:#39b9d6">Management</span></h4>
	            <p><a id="A1" href="RequestForLicense.aspx" runat="server" title="Request For License">Request For License</a></p>
                <p><a id="A2"  href="LicenseRequestList.aspx" runat="server" title="View License Request List">View License Request List</a></p>
			</div>												

		</div>
		<div class="span9">
		<div class="well well-small" style="text-align:left">
         	
		<ul class="media-list">
		  <li class="media well well-small">
				
				<div class="media-body">
				  <h4 class="media-heading">Request For License</h4>
                  <fieldset> 
                  <asp:UpdatePanel ID="updPanl" runat="server" RenderMode="Block" UpdateMode="Conditional" >
            <ContentTemplate>                 
       <table cellpadding="10px" cellspacing="10px">        
             <tr>
                <td class="style1">No Of License</td>
                <td>
                    <asp:TextBox ID="txtLicenseNo" runat="server" Width="350px"></asp:TextBox>                    
                </td>
                <td><asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                        ErrorMessage="Required" Font-Italic="True" Font-Names="Calibri" 
                        ControlToValidate="txtLicenseNo" ForeColor="Red">Required</asp:RequiredFieldValidator>
                        <br />
                     <asp:CompareValidator ID="CompareValidator4" runat="server" 
                    ControlToValidate="txtLicenseNo" Display="Dynamic" 
                    ErrorMessage="Number of License(s) field should be a number" 
                    Operator="DataTypeCheck" Type="Integer">*</asp:CompareValidator>
                        </td>
            </tr>
           
            <tr>
                <td class="style1"> License Type</td>
                
                <td>
                    <asp:DropDownList ID="DropDownLicensetype" runat="server"  
                                DataTextField="Name" DataValueField="ID"  
                                TabIndex="5">
                                <asp:ListItem Enabled="true" Value="0">--select one--</asp:ListItem>  
                                <asp:ListItem Value="1">PC</asp:ListItem>
                                <asp:ListItem Value="2">Printer</asp:ListItem>                           
                                 
                            </asp:DropDownList>
                   
                </td>
                 <td><asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" 
                        ErrorMessage="Required" Font-Italic="True" Font-Names="Calibri" 
                            ControlToValidate="DropDownLicensetype" ForeColor="Red" InitialValue="0">Required</asp:RequiredFieldValidator></td>
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
