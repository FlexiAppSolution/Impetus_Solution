<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ViewLoggedCase.aspx.cs" Inherits="ViewLoggedCase" %>
<%@ Register src="Control/ViewCase.ascx" tagname="ViewCase" tagprefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
     <style type="text/css">
    .bg
{
	width:auto;
	height:auto;		
	line-height:1.5;
	position:relative;	
	float:left;
	left:15px;
}
.hide
{
	display:none;
}
  .panel
{	
	top:50px;
	height:auto;
}
fieldset
{
	border:2px inset Maroon;
	width:410px;
	height:auto;
	padding:15px;	
}
legend
{
	margin-left:5%;
	background-color:Maroon;
	color:White;	
}       
 
    .style1
    {
        width: 169px;
        font-weight:bold;
        font-family:Calibri;
        color:Maroon;
    }
    .style2
    {
        width: 200px;
    }
    
    .style3
    {
        width: 87px;
    }
    
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <uc1:ViewCase ID="ViewCaseAscx" runat="server" />
    </div>
    </form>
</body>
</html>
