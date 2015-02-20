<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="UploadedEXEFile.aspx.cs" Inherits="UploadedEXEFile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
 <asp:ScriptManager runat="server" ID="Manager1" EnablePageMethods="true"></asp:ScriptManager>
    <script type="text/javascript">
          
        function DeleteFile(ID) {

            if (confirm('Are you sure you want to delete this file?'))
                PageMethods.DeleteItem(ID, OnDeleteFileSuccess, OnDeleteFileFailure);
            else
                alert('File deletion cancelled.');
        }

        function OnDeleteFileSuccess(result, userContext, methodName) {
            window.location.reload();           
            alert(result);
        }

        function OnDeleteFileFailure(error, userContext, methodName) {
            alert(error.get_message());
        }
     </script>   
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
				  <h4 class="media-heading">View Uploaded EXE File</h4>
                 <div class="bg">
    <fieldset>
     
       <h5>Search Panel</h5>
       <hr style="border-color:Black; border-style:inset; border-width:thin;"/>

        <table cellpadding="10px" cellspacing="10px">                             
            <tr>
                <td class="style4">
            <asp:Label ID="institution" runat="server" Font-Names="Calibri" Font-Size="Medium">Institution:</asp:Label></td>
                <td class="style5">                                  
                <asp:DropDownList ID="comboInstitution" runat="server" Width="218px" 
                         DataSourceID="objInstitutions" DataTextField="InstitutionName" 
                        DataValueField="ID" AppendDataBoundItems="True" >
                     <asp:ListItem Enabled="true" Value="0">--select one--</asp:ListItem>  
                    </asp:DropDownList>
                
                </td>                                    
            </tr>                
            <tr>
                <td class="style1"></td>
                <td>
                    <asp:Button ID="btnSearch" runat="server" Text="Search"    
                    BackColor="Maroon" BorderColor="Maroon" ForeColor="White"                    
                        BorderWidth="2px" Font-Bold="True" Font-Names="Calibri" Font-Size="Small" onclick="btnSearch_Click"  />
                </td>
            </tr>
        </table>
        <hr style="border-color:Black; border-style:inset; border-width:thin;"/>
       
            <asp:GridView ID="GridView1" runat="server" AllowPaging="True" onpageindexchanging="GridView1_PageIndexChanging" 
                        CssClass="table table-hover"  AutoGenerateColumns="False"  PageSize="10" 
                        PagerSettings-Mode="NextPrevious" Width="100%" EmptyDataText="There are no <b>Result(s)</b> matching search criteria">
                           <Columns>                                                                        
                        <asp:TemplateField HeaderText="Institution"  HeaderStyle-BackColor="Maroon" HeaderStyle-BorderStyle="Outset" HeaderStyle-ForeColor="White" HeaderStyle-BorderColor="Black"  >
                            <ItemTemplate>
                                <asp:Label ID="lblRequestingInstitution" runat="server" Text='<%# Eval("TheInstitution.InstitutionName") %>'></asp:Label>
                            </ItemTemplate>                          
                            <ItemStyle ForeColor="Black"  />
                            <HeaderStyle  HorizontalAlign="Left" />
                        </asp:TemplateField>   
                        
                        <asp:TemplateField HeaderText="File Name"  HeaderStyle-BackColor="Maroon" HeaderStyle-BorderStyle="Outset" HeaderStyle-ForeColor="White" HeaderStyle-BorderColor="Black">
                            <ItemTemplate>
                                <asp:Label ID="lblFileName" runat="server" Text='<%# Eval("FileName") %>'></asp:Label>
                            </ItemTemplate>                        
                            <ItemStyle  ForeColor="Black"  />
                            <HeaderStyle  HorizontalAlign="Left" />
                        </asp:TemplateField>   

                        <asp:TemplateField   HeaderStyle-BackColor="Maroon" HeaderStyle-BorderStyle="Outset" HeaderStyle-ForeColor="White" HeaderStyle-BorderColor="Black">
                           <ItemTemplate>
                              <asp:LinkButton ID="lbLink" runat="server" ForeColor="Maroon" Text="Delete" Font-Underline="true" OnClientClick ='<%# "DeleteFile(" + Eval("ID") + ");return false;" %>'  ></asp:LinkButton>
                            </ItemTemplate>                        
                            <ItemStyle  ForeColor="Black"  />
                            <HeaderStyle  HorizontalAlign="Left" />
                        </asp:TemplateField>                                                                                                                                                                                                              
                    </Columns>

<PagerSettings Mode="NextPrevious"></PagerSettings>
                        </asp:GridView>             
            
    </fieldset>
</div>
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

