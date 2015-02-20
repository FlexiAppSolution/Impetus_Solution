<%@ Page Title="" Language="C#" MasterPageFile="~/RegularSite.master" AutoEventWireup="true" CodeFile="InstitutionCaseList.aspx.cs" Inherits="InstitutionCaseList" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    <link href="greybox/gb_styles.css" rel="stylesheet" type="text/css" /> 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </cc1:ToolkitScriptManager>
    <script type="text/javascript">

        var GB_ROOT_DIR = '<%= Utilities.GreyBoxFolderURL %>';

        function refreshPage() {
            window.location.reload();
        }

        function EditCase(CaseID) {

            var caption = "Case Management";
            var url = "../EditCase.aspx?ID=" + CaseID;
            return GB_showCenter(caption, url, 460, 600, refreshPage);
        }


        function ViewCase(CaseID) {
            var caption = "Case Management";
            var url = "../ViewLoggedCase.aspx?ID=" + CaseID;
            return GB_showCenter(caption, url, 540, 500, refreshPage);
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
				<h4>Case <span style="color:#39b9d6">Management</span></h4>
	            <p><a id="A1" href="LogCase.aspx" runat="server" title="Create Case">Create Case</a></p>
                <p><a id="A2"  href="InstitutionCaseList.aspx" runat="server" title="Case List">View Case List</a></p>
			</div>												

		</div>
		<div class="span9">
		<div class="well well-small" style="text-align:left">
         
					
		<ul class="media-list">
		  <li class="media well well-small">
				
				<div class="media-body">
				  <h4 class="media-heading">View Case List</h4>
                 <div class="bg">
    <fieldset>
     
       <h5>Search Panel</h5>
       <hr style="border-color:Black; border-style:inset; border-width:thin;"/>

        <table cellpadding="10px" cellspacing="10px">        
            <tr>
                        <td align="left" class="style4">                
            <asp:Label ID="institution1" runat="server" Font-Names="Calibri" Font-Size="Medium" 
                >Date From:</asp:Label>
                        </td>
                        <td align="left" class="style5">
                        <asp:TextBox ID="txtDateFrom" runat="server" Enabled="true" Width="200px"></asp:TextBox>                                                                         
                          <asp:Image ID="image2" runat="server" ImageUrl="assets/calendar-schedulehs.png" />
                           <cc1:CalendarExtender ID="CalendarExtender1" runat="server"
                      TargetControlID="txtDateFrom" Format="MM/dd/yyyy" PopupButtonID="image2" 
                                FirstDayOfWeek="Sunday" PopupPosition="BottomLeft">
                      </cc1:CalendarExtender> 
                        </td>
                        <td align="left" class="style3">                
            <asp:Label ID="institution2" runat="server" Font-Names="Calibri" Font-Size="Medium" 
                >Date To:</asp:Label>
                        </td>
                        <td align="left" class="style5">
                        <asp:TextBox ID="txtDateTo" runat="server" Enabled="true" Width="200px"></asp:TextBox>
                        <asp:Image ID="image1" runat="server" ImageUrl="assets/calendar-schedulehs.png" />
                           <cc1:CalendarExtender ID="CalendarExtender2" runat="server"
                      TargetControlID="txtDateTo" Format="MM/dd/yyyy" PopupButtonID="image1"
                      FirstDayOfWeek="Sunday" PopupPosition="BottomLeft">
                      </cc1:CalendarExtender>   
                        </td>
                    </tr>           
            <tr>                
                <td class="style3"><asp:Label ID="Label2" runat="server" Font-Names="Calibri" Font-Size="Medium" 
                ForeColor="Maroon">Issue:</asp:Label></td>
                <td class="style5"><asp:TextBox ID="txtIssue" runat="server" Width="133px"                         ></asp:TextBox></td>             
            </tr>
            <tr>
                <td class="style1"></td>
                <td>
                    <asp:Button ID="btnSearch" runat="server" Text="Search Case"    
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
                        <asp:TemplateField HeaderText="Incident Number"  HeaderStyle-BackColor="Maroon" HeaderStyle-BorderStyle="Outset" HeaderStyle-ForeColor="White" HeaderStyle-BorderColor="Black"  >
                             <ItemTemplate>
                                <asp:Label ID="lblIncidentNumber" runat="server" Text='<%# Eval("IncidentNumber") %>'></asp:Label> 
                            </ItemTemplate>                         
                            <ItemStyle ForeColor="Black"  />
                            <HeaderStyle  HorizontalAlign="Left" />
                        </asp:TemplateField>   
                        
                         <asp:TemplateField HeaderText="Name"  HeaderStyle-BackColor="Maroon" HeaderStyle-BorderStyle="Outset" HeaderStyle-ForeColor="White" HeaderStyle-BorderColor="Black"  >
                             <ItemTemplate>
                                <asp:Label ID="lblName" runat="server" Text='<%# Eval("Name") %>'></asp:Label> 
                            </ItemTemplate>                         
                            <ItemStyle ForeColor="Black"  />
                            <HeaderStyle  HorizontalAlign="Left" />
                        </asp:TemplateField>                           

                        <asp:TemplateField   HeaderStyle-BackColor="Maroon" HeaderStyle-BorderStyle="Outset" HeaderStyle-ForeColor="White" HeaderStyle-BorderColor="Black">
                           <ItemTemplate>
                              <asp:LinkButton ID="lblEdit" runat="server" Text="View" ForeColor="Maroon" Font-Underline="true"  
                             OnClientClick='<%# "return ViewCase(" + Eval("ID") + ");" %>'></asp:LinkButton>  
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



