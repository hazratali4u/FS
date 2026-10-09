<%@ Page Title="" Language="C#" MasterPageFile="~/LoginMaster.master" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<style type="text/css">
<!--
#main {
	position:relative;
	width:565px;
}
#boxbody {
	float:left;
	width:565px;
	padding:69px 0px 130px 0px;	
	margin-left:80px;
	margin-top:30px;	
	background-image:url(images/login-blue.png);
	background-repeat:no-repeat;
	background-position:top;
}
#boxbody div {
	margin:5px 0px 0px 32px;/*background-color:#CCC;*/
}
#boxbody div input {
	width:235px;
	padding:9px 5px;
	border:0px;
	background-color:transparent;
	font-weight:bold;/*background-color:#CCC;*/
}
#Products_select input {
	cursor:pointer;
}
#boxbody div#selectCityDrop {
	display:none;
	position:absolute;
	z-index:3;
	width:243px;
	margin-top:0px;
	margin-left:0px;
	_margin-left:-1px;
	border-left:1px solid #dcdcdc;
	border-right:1px solid #dcdcdc;
	border-bottom:1px solid #dcdcdc;
	background-color: #FFF;
}
#selectCityDrop ul {
	margin:0px;
	padding:5px 0px;
}
#selectCityDrop ul li {
	margin:0px;
	padding:3px 5px;
	cursor:pointer;
	list-style:none;
}
#boxbody div input.tb_signin {
	width:100px;
	margin-left:144px;
	background-color:transparent;
	cursor:pointer;
	border:0px solid red;
}
-->
</style>
<!--Drop Down Menue-->
<script language="javascript1.2" type="text/javascript">
    function shHide(dvID, st) {
        document.getElementById(dvID).style.display = st;
    }
    function chgValue(v) {
        document.getElementById('<%=txtCity.ClientID%>').value = v;        
        document.getElementById('selectCityDrop').style.display = 'none';
    }
</script>
<!--Drop Down Menue-->
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cpChild" Runat="Server">
<div id="right_data">
    <div id="main">
    <asp:Label ID="lblErrorMsg" runat="server" ForeColor="Red" Visible="false"></asp:Label>
  <div id="boxbody">
    <div id="Products_select" onclick="javascript:shHide('selectCityDrop','block');" onmouseout="javascript:shHide('selectCityDrop','none');">
      <form name="frmCity" action="dropdown-1.html" method="post">
        <asp:TextBox ID="txtCity" runat="server" Text="Fast Services"></asp:TextBox>     
        <div id="selectCityDrop" onmouseover="javascript:shHide('selectCityDrop','block');" onmouseout="javascript:shHide('selectCityDrop','none');">
          <ul >
            <li onclick="chgValue ('Fast Services');" >Fast Services</li>
            <li onclick="chgValue ('Fast Intelligent Solutions');" >Fast Intelligent Solutions</li>
          </ul>
        </div>
      </form>
    </div>
    <div>          
      <asp:TextBox ID="txtLogin" runat="server" Text="Login" onfocus="if(this.value=='Login'){this.value='';}" onblur="if(this.value=='' || this.value==null) this.value='Login'"></asp:TextBox>
    </div>
    <div>
      <asp:TextBox ID="txtPassword" runat="server" Text="........." TextMode="Password" onfocus="if(this.value=='.........'){this.value='';}" onblur="if(this.value=='' || this.value==null) this.value='.........'"></asp:TextBox>
    </div>
    <div>
      <asp:Button ID="btnSignIn" runat="server" CssClass="tb_signin" OnClick="btnSignIn_Click" />
    </div>
    <div><br /><br />
      *This is a secure server. Your activity will be monitored. </div>
  </div>
</div>
</div>
</asp:Content>

