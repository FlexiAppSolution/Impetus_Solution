<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="UploadEXEFile.aspx.cs" Inherits="UploadEXEFile" %>

<%@ Register assembly="EO.Web" namespace="EO.Web" tagprefix="eo" %>

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
	            <p><a id="A1" href="UploadEXEFile.aspx" runat="server" title="Upload EXE File">Upload EXE File</a></p>
                <p><a id="A2"  href="UploadedEXEFile.aspx" runat="server" title="View Uploaded EXE File">View Uploaded EXE File</a></p>
                <p><a id="A3"  href="LicenseInstitutionList.aspx" runat="server" title="View License Request">View License Request</a></p>
			</div>												

		</div>
		<div class="span9">
		<div class="well well-small" style="text-align:left">
         	
		<ul class="media-list">
		  <li class="media well well-small">
				
				<div class="media-body">
				  <h4 class="media-heading">Upload EXE File</h4>
                  <fieldset> 
                                 
       <table cellpadding="10px" cellspacing="10px">        
            <tr>
                <td class="style1">Institution</td>
                <td>
                    <asp:DropDownList ID="comboInstitution" runat="server" Width="266px" 
                         DataSourceID="objInstitutions" DataTextField="InstitutionName" 
                        DataValueField="ID" AppendDataBoundItems="True" >
                     <asp:ListItem Enabled="true" Value="0">--select one--</asp:ListItem>  
                    </asp:DropDownList>
                   
                </td>
                 <td><asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" 
                        ErrorMessage="Required" Font-Italic="True" Font-Names="Calibri" 
                            ControlToValidate="comboInstitution" ForeColor="Red" InitialValue="0">Required</asp:RequiredFieldValidator></td>
            </tr>                                                               
            <tr>
                <td class="style1"></td>
                <td>
                    
                    <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Italic="True" 
                        Text="File Format (.zip)"></asp:Label>                                    
                    
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style1">
                    Upload File</td>
                <td>
                    
                    <asp:FileUpload ID="FileUpload1" runat="server" />
                  
                    </td>
                <td>
                   </td>
            </tr>         
            <tr>
                <td class="style1"></td>
                <td colspan="2">
                    <asp:Button ID="btnCreate" runat="server" Text="Upload EXE File"   
                    BackColor="Maroon" BorderColor="Maroon" ForeColor="White"                     
                        BorderWidth="2px" Font-Bold="True" Font-Names="Calibri" Font-Size="Small" onclick="btnCreate_Click" />
                </td>
            </tr>       
             
        </table>
         
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

<asp:ObjectDataSource ID="objInstitutions" runat="server" SelectMethod="GetInstitutions"
                    TypeName="EvoPaj.Logic.InstitutionSystem" >  
                                   
</asp:ObjectDataSource>
</asp:Content>


