<%@ Page Language="C#" MasterPageFile="~/CMS.master" AutoEventWireup="true" CodeFile="TestControl.aspx.cs" Inherits="TestControl" Title="Untitled Page" %>

<%@ Register src="Control/DailyCardBalancel.ascx" tagname="DailyCardBalancel" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="contentTitle" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentPrint" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:DailyCardBalancel ID="DailyCardBalancel1" runat="server" />
</asp:Content>

