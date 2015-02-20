<%@ Page Title="" Language="C#" MasterPageFile="~/RegularSite.master" AutoEventWireup="true" CodeFile="GetRightCode.aspx.cs" Inherits="GetRightCode" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
<asp:ScriptManager runat="server" ID="Manager1"></asp:ScriptManager>
    <section id="bodySection">	
	<div class="container">	
	<div class="row">
		<div class="span3">
			<div class="well well-small">
				<h4>Printer <span style="color:#39b9d6">Utilities</span></h4>
	            <p><a href="DownloadEXEFile.aspx">Download Exe File</a></p>
		        <p><a href="GetLicense.aspx">Get License</a></p>
                <p><a href="GetRightCode.aspx">Enter Error Code</a></p>
                <p><a href="GetServiceCode.aspx">Get Service Code</a></p>
			</div>												

		</div>
		<div class="span9">
		<div class="well well-small" style="text-align:left">
         	
		<ul class="media-list">
		  <li class="media well well-small">
				
				<div class="media-body">
				  <h4 class="media-heading">Get Right Code</h4>
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
                <td class="style1">Error Code</td>
                <td>
                    
                    <asp:TextBox ID="txtErrorCode" runat="server" Width="336px"></asp:TextBox>
                    
                </td>
                 <td><asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" 
                        ErrorMessage="Required" Font-Italic="True" Font-Names="Calibri" 
                            ControlToValidate="txtErrorCode" ForeColor="Red" InitialValue="0">Required</asp:RequiredFieldValidator></td>
            </tr>  
            <tr>
                <td class="style1">Right Code</td>
                <td>
                    
                    <asp:TextBox ID="txtRightCode" runat="server" Width="336px"></asp:TextBox>
                    
                </td>
                <td>
                    <br />
                        </td>
            </tr>       
            <tr>
                <td class="style1">Admin Password</td>
                <td>
                    
                    <asp:TextBox ID="txtAdminPassword" runat="server" Width="336px"></asp:TextBox>
                    
                </td>
                <td>
                    <br />
                        </td>
            </tr>         
            <tr>
                <td class="style1"></td>
                <td colspan="2">
                    <asp:Button ID="btnCreate" runat="server" Text="Get Right Code"   
                    BackColor="Maroon" BorderColor="Maroon" ForeColor="White"                     
                        BorderWidth="2px" Font-Bold="True" Font-Names="Calibri" Font-Size="Small" onclick="btnGet_Click" />
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



