<%@ Page Title="" Language="C#" MasterPageFile="~/RegularSite.master" AutoEventWireup="true" CodeFile="DownloadEXEFile.aspx.cs" Inherits="DownloadEXEFile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <asp:ScriptManager runat="server" ID="Manager1" EnablePageMethods="true"></asp:ScriptManager>
    <script type="text/javascript">

        function Download(ID) {
            PageMethods.DownloadFile(ID, OnDownloadSuccess, OnDownloadFailure);
        }
        function OnDownloadSuccess(result, userContext, methodName) {
            alert(result);
        }
        function OnDownloadFailure(error, userContext, methodName) {
            alert(error.get_message());
        }
     </script>   
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
				  <h4 class="media-heading">Download EXE File</h4>
                 <div class="bg">
    <fieldset>
     
       
       
        <hr style="border-color:Black; border-style:inset; border-width:thin;"/>
       
            <asp:GridView ID="GridView1" runat="server" AllowPaging="True" onpageindexchanging="GridView1_PageIndexChanging" 
            onselectedindexchanged="GridView1_SelectedIndexChanged" 
                        CssClass="table table-hover"  AutoGenerateColumns="False"  PageSize="10" 
                        PagerSettings-Mode="NextPrevious" Width="100%" EmptyDataText="There are no <b>uploaded files(s) for your institution.</b>">
                           <Columns>                                                                        
                        <asp:BoundField HeaderStyle-BackColor="Maroon" HeaderStyle-BorderStyle="Outset" HeaderStyle-ForeColor="White" HeaderStyle-BorderColor="Black" DataField="FileName" 
                        HeaderText="File Name">
                    <HeaderStyle HorizontalAlign="Left" Width="200px" />
                    <ItemStyle Width="250px" Height="20px" />
                    </asp:BoundField>                    
                        <asp:CommandField HeaderStyle-BackColor="Maroon" HeaderStyle-BorderStyle="Outset" HeaderStyle-ForeColor="White" HeaderStyle-BorderColor="Black" ShowSelectButton="True" SelectText="Download">
                    <ItemStyle Font-Underline="True" 
                        HorizontalAlign="Center" ForeColor="Maroon" Width="120px" />
                    </asp:CommandField>                                                                                                                                                                                                            
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




