<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="UserList.aspx.cs" Inherits="UserList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    <link href="greybox/gb_styles.css" rel="stylesheet" type="text/css" /> 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <script type="text/javascript">

   var GB_ROOT_DIR = '<%= Utilities.GreyBoxFolderURL %>';
   function refreshPage() {
       window.location.reload();
   }

   function EditUser(InsID) {

       var caption = "User Management";
       var url = "../UserEdit.aspx?ID=" + InsID;
       return GB_showCenter(caption, url, 460, 600, refreshPage);
   }


   function ViewUser(UserID) {
       var caption = "User Management";
       var url = "../UserDetails.aspx?ID=" + UserID;
       return GB_showCenter(caption, url, 410, 500, refreshPage);
   } 

     </script>
    <script type="text/javascript" src="greybox/AJS.js"></script>
    <script type="text/javascript" src="greybox/AJS_fx.js"></script>
    <script type="text/javascript" src="greybox/gb_scripts.js"></script>  
    <section id="bodySection">	
	<div class="container">	
	<div class="row">
		<div class="span3">
			<div class="well well-small">
				<h4>User <span style="color:#39b9d6">Management</span></h4>
	            <p><a id="A1" href="CreateUser.aspx" runat="server" title="Create User">Create User</a></p>
                <p><a id="A2"  href="UserList.aspx" runat="server" title="View User List">View User List</a></p>
			</div>												

		</div>
		<div class="span9">
		<div class="well well-small" style="text-align:left">
         
					
		<ul class="media-list">
		  <li class="media well well-small">
				
				<div class="media-body">
				  <h4 class="media-heading">View User List</h4>
                 <div class="bg">
    <fieldset>
     
       <h5>Search Panel</h5>
       <hr style="border-color:Black; border-style:inset; border-width:thin;"/>

        <table cellpadding="10px" cellspacing="10px">        
            <tr>
            
            <td>
                <asp:Label ID="institution" runat="server" Font-Names="Calibri" Font-Size="Medium" 
                ForeColor="Black">Institution:</asp:Label>&nbsp;                
                </td> 
                    <td class="style2">
            <asp:DropDownList ID="comboInstitution" runat="server" Width="282px" 
                         DataSourceID="objInstitutions" DataTextField="InstitutionName" 
                        DataValueField="ID" AutoPostBack="true" AppendDataBoundItems="True" >
                     <asp:ListItem Enabled="true" Value="0">--select one--</asp:ListItem>  
                    </asp:DropDownList>
                
                </td>               
            </tr>
            <tr>                
                <td class="style3" ><asp:Label ID="Name" runat="server" Font-Names="Calibri" Font-Size="Medium" 
                ForeColor="Black">LastName:</asp:Label>&nbsp;</td>   
                <td><asp:TextBox ID="txtLastName" runat="server" Width="270px"></asp:TextBox></td>             
            </tr>     
            <tr>
                <td class="style1"></td>
                <td>
                    <asp:Button ID="btnSearch" runat="server" Text="Search User"    
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
                        <asp:TemplateField HeaderText="Name"  HeaderStyle-BackColor="Maroon" HeaderStyle-BorderStyle="Outset" HeaderStyle-ForeColor="White" HeaderStyle-BorderColor="Black"  >
                            <ItemTemplate >                                                                                     
                             <asp:LinkButton ID="lblUsername" runat="server" Text='<%# Eval("FullName") %>' ForeColor="Maroon"  Font-Underline="true"
                             OnClientClick='<%# "return ViewUser(" + Eval("ID") + ");" %>'></asp:LinkButton>  
                            </ItemTemplate>                           
                            <ItemStyle ForeColor="Black"  />
                            <HeaderStyle  HorizontalAlign="Left" />
                        </asp:TemplateField>   
                        
                        <asp:TemplateField HeaderText="Institution"  HeaderStyle-BackColor="Maroon" HeaderStyle-BorderStyle="Outset" HeaderStyle-ForeColor="White" HeaderStyle-BorderColor="Black">
                           <ItemTemplate>
                                <asp:Label ID="lblInstitution" runat="server" Text='<%# Eval("TheInstitution.InstitutionName") %>'></asp:Label> 
                            </ItemTemplate>                           
                            <ItemStyle  ForeColor="Black"  />
                            <HeaderStyle  HorizontalAlign="Left" />
                        </asp:TemplateField>   

                        <asp:TemplateField HeaderText="Status"  HeaderStyle-BackColor="Maroon" HeaderStyle-BorderStyle="Outset" HeaderStyle-ForeColor="White" HeaderStyle-BorderColor="Black">
                             <ItemTemplate>
                                <asp:Label ID="lblStatus" runat="server" Text='<%# Eval("Status") %>'></asp:Label>
                            </ItemTemplate>                           
                            <ItemStyle  ForeColor="Black"  />
                            <HeaderStyle  HorizontalAlign="Left" />
                        </asp:TemplateField>  

                        <asp:TemplateField   HeaderStyle-BackColor="Maroon" HeaderStyle-BorderStyle="Outset" HeaderStyle-ForeColor="White" HeaderStyle-BorderColor="Black">
                            <ItemTemplate>
                              <asp:LinkButton ID="lblEdit" runat="server" Text="Edit" Font-Underline="true"  ForeColor="Maroon"  OnClientClick='<%# "return EditUser(" + Eval("ID") + ");" %>'></asp:LinkButton>  
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




