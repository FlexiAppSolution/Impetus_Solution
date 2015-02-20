<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="home.aspx.cs" Inherits="home" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">

<!-- Page banner end -->
<section id="bodySection">		
	<div id="wrapper">
		<div class="container">	
		<div class="row">
			<div class="span4">
				<div class="thumbnail">
					<h4>Institution Management</h4>
					<h5>Create, View and Update Institution</h5>
					 
					<p><a href="CreateInstitution.aspx" runat="server" title="Create Institution">Create Institution</a></p>
                    <p><a  href="InstitutionList.aspx" runat="server" title="View Institution List">View Institution List</a></p>

				</div>
				<br/>
				<div class="thumbnail">
				     <h4>License Management</h4>
					<h5>Uploaded EXE File, View Uploaded EXE File and View License Request</h5>
					 
					<p><a id="A5" href="UploadEXEFile.aspx" runat="server" title="Upload EXE File">Upload EXE File</a></p>
                    <p><a id="A6"  href="UploadedEXEFile.aspx" runat="server" title="View Uploaded EXE File">View Uploaded EXE File</a></p>
                    <p><a id="A7"  href="LicenseInstitutionList.aspx" runat="server" title="View License Request">View License Request</a></p>       
				</div>
				<br/>
				
			</div>
			<div class="span4">
			<div class="thumbnail">
			<h4>User Management</h4>
					<h5>Create, View and Update User</h5>
					 
					<p><a id="A9" href="CreateUser.aspx" runat="server" title="Create User">Create User</a></p>
                    <p><a id="A10"  href="UserList.aspx" runat="server" title="User List">View User List</a></p>	
			</div>
			<br/>
			<div class="thumbnail">
				<h4>Password Management</h4>
					<h5>Change Password</h5>
					 
					<p><a id="A8" href="ChangeAdminPassword.aspx" runat="server" title="Change Password">Change Password</a></p>   
			</div>
			<br/>			
			</div>
			<div class="span4">
			<div class="thumbnail">
				<h4>Case Management</h4>
					<h5>Create, View and Update Case</h5>
					 
					<p><a id="A3" href="CreateCase.aspx" runat="server" title="Create Case">Create Case</a></p>
                    <p><a id="A4"  href="ViewLoggedCases.aspx" runat="server" title="View Case List">View Case List</a></p>
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

