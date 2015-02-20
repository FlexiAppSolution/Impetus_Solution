<%@ Page Title="" Language="C#" MasterPageFile="~/RegularSite.master" AutoEventWireup="true" CodeFile="regularhome.aspx.cs" Inherits="regularhome" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
<section id="bodySection">		
	<div id="wrapper">
		<div class="container">	
		<div class="row">
			<div class="span4">
				<div class="thumbnail">
					<h4>Case Management</h4>
					<h5>Log and View Cases</h5>
					 
					<p><a href="LogCase.aspx">Log Case</a></p>
                    <p><a href="InstitutionCaseList.aspx">View Case List</a></p>

				</div>
				<br/>
				<div class="thumbnail">
				     <h4>Printer Utility</h4>
					<h5>Download EXE File, Get License, Enter Error Code and Get Service Code</h5>
					 
					<p><a href="DownloadEXEFile.aspx">Download Exe File</a></p>
                    <p><a href="GetLicense.aspx">Get License</a></p>
                    <p><a href="GetRightCode.aspx">Enter Error Code</a></p>     
                    <p><a href="GetServiceCode.aspx">Get Service Code</a></p>       
				</div>
				<br/>
				
			</div>
			<div class="span4">
			<div class="thumbnail">
			<h4>License Management</h4>
					<h5>Request for License and View Requested License</h5>
					 
					<p><a href="RequestForLicense.aspx">Request For License</a></p>
                    <p><a href="LicenseRequestList.aspx">License Request List</a></p>	
			</div>						
			<br/>			
			</div>
			<div class="span4">
			<div class="thumbnail">
				<h4>Password Management</h4>
					<h5>Change Password</h5>				 
					<p><a id="A8" href="ChangeUserPassword.aspx" runat="server" title="Change Password">Change Password</a></p>
                    <p>&nbsp;</p>   
			</div>
			<br/>
			
			<br/>			
			</div>
		</div>
		<br/>
		</div>
	</div>
</section>
</asp:Content>

