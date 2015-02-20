<%@ Page Language="C#" AutoEventWireup="true" CodeFile="adminLogin.aspx.cs" Inherits="adminLogin" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Login Page</title>
	<meta name="keywords" content="" />
	<meta name="description" content="" />
	<meta name="robots" content="index, follow" />
	<meta charset="utf-8" />
	<!-- // General meta information -->
		
	<!-- Load Javascript -->
	<script type="text/javascript" src="js/jquery.js"></script>
	<script type="text/javascript" src="js/jquery.query-2.1.7.js"></script>
	<script type="text/javascript" src="js/rainbows.js"></script>
	<!-- // Load Javascipt -->

	<!-- Load stylesheets -->
	<link type="text/css" rel="stylesheet" href="css/style.css" media="screen" />
	<!-- // Load stylesheets -->
	
<script type="text/javascript">


    $(document).ready(function () {

        $("#submit1").hover(
	function () {
	    $(this).animate({ "opacity": "0" }, "slow");
	},
	function () {
	    $(this).animate({ "opacity": "1" }, "slow");
	});
    });


</script>
<style type="text/css">
table
{
    padding:10px;
    margin-left:20px;    
}
#body {
	height: 100%;
	position: relative;
	font-family: Arial, Helvetica, sans-serif;
	color: #888;
	font-size: 13px;
	line-height: 20px;
	min-width: 998px;
	border-top:3px solid #919191;
	background:url(themes/BG.jpg) repeat;
}
#wrapper {	
	padding: 70px 0 0 0px;
	height: 100%;
}
#wrapper {
	width: 350px;
	margin:auto;
	position: relative;
}

#wrappertop {
	background:url(themes/wrapper_top.png) no-repeat;
	height:22px;
}

#wrappermiddle {
	background:url(themes/wrapper_middle.png) repeat-y;
	height:240px;
}

#wrapperbottom {
	background:url(themes/wrapper_bottom.png) no-repeat;
	height:22px;
}

#wrapper h2 {
	margin-left:20px;
	font-size:20px;
	font-weight:bold;
	font-family:Myriad Pro;
	text-transform:uppercase;
	position:absolute;
	text-shadow: #fff 2px 2px 2px;
}

#username_input {
	margin-left:25px;
	position:absolute;
	width:300;
	height:50px;
	margin-top:40px;
}

</style>
</head>
<body id="body">
     <form id="form1" runat="server">   
    <asp:ScriptManager runat="server" ID="Manager1"></asp:ScriptManager> 
    <div id="wrapper">
		<div id="wrappertop"></div>

		<div id="wrappermiddle">

			<h2 style="font-family:Calibri;font-size:14px;text-transform:uppercase; color:Maroon;">Impetus Solutions</h2>            
             <asp:UpdatePanel ID="updPanl" runat="server" RenderMode="Block" UpdateMode="Conditional" >
            <ContentTemplate>
			<table cellpadding="5px" cellspacing="8px">
             <tr><td>
                       &nbsp;&nbsp; &nbsp;</td></tr>
                   <tr>                  
                   <td><asp:Label runat="server" ID="lblmsg" ForeColor="Black" Font-Italic="true"></asp:Label><br />                   
                   </td>
                   </tr>                  
                   <tr>
                   <td style="color:Maroon">Username:</td>                   
                   </tr                               
                   <tr>
                   <td><asp:TextBox runat="server" ID="txtUsername" Width="280px" Height="30px"></asp:TextBox></td>
                   </tr>
                   <tr><td> &nbsp;&nbsp; &nbsp;</td></tr>
                   <tr>
                   <td style="color:Maroon">Password:</td>                   
                   </tr>                
                   <tr>
                   <td><asp:TextBox runat="server" ID="txtPassword" Width="280px" Height="30px" TextMode="Password"></asp:TextBox></td>
                   </tr>
                   <tr><td> &nbsp;&nbsp; &nbsp;</td></tr>
                   <tr>                  
                   <td style="text-align:right">                                   
                   <asp:Button runat="server" ID="btnLogin" Text="Login" Width="120px" Height="30px" 
                           BackColor="Maroon" ForeColor="White" CssClass="btn btn-primary" OnClick="btnLogin_Click" 
                             /></td>
                   </tr>
                   <tr>                   
                   <td> <asp:UpdateProgress ID="UPG1" runat="server" AssociatedUpdatePanelID="updPanl"  Visible="true"  >
                        <ProgressTemplate >
                            <p style="color:Maroon">Processing... Please wait.</p>
                        </ProgressTemplate>
                    </asp:UpdateProgress>  </td>
                   </tr>
                   </table>
                   <br /><br />
                   <a href="Index.aspx">Go to Home Page</a>
            </ContentTemplate>
        </asp:UpdatePanel>    

			<div id="links_left">
			

			</div>

			<div id="links_right">
            </div>

		</div>

		<div id="wrapperbottom"></div>
		
		<div id="powered">
		<p>Powered by <a href="http://www.pajuno.com">Pajuno Development Company Ltd.</a></p>
		</div>
	</div>

    </form>
</body>
</html>
