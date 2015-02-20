<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OurCompany.aspx.cs" Inherits="OurCompany" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
   <meta charset="utf-8"/>
    <title>Our Company</title>
    <meta name="viewport" content="width=device-width, initial-scale=1.0"/>
    <meta name="description" content=""/>
    <meta name="author" content=""/>
    <!-- Le HTML5 shim, for IE6-8 support of HTML5 elements -->
    <!--[if lt IE 9]>
    <script src="http://html5shim.googlecode.com/svn/trunk/html5.js"></script>
    <![endif]-->
	<link id="callCss" rel="stylesheet" href="themes/current/bootstrap.min.css" type="text/css" media="screen"/>
	<link href="themes/css/bootstrap-responsive.min.css" rel="stylesheet" type="text/css"/>
	<link href="themes/css/font-awesome.css" rel="stylesheet" type="text/css"/>
	<link href="themes/css/base.css" rel="stylesheet" type="text/css"/>
	<style type="text/css" id="enject"></style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <section id="headerSection">
	<div class="container">
		<div class="navbar">
			<div class="container">
				<button type="button" class="btn btn-navbar" data-toggle="collapse" data-target=".nav-collapse">
				<span class="icon-bar"></span>
				<span class="icon-bar"></span>
				<span class="icon-bar"></span>
				</button>
				<h1><a class="brand" href="Index.aspx"> PAJUNO <small> Development Company Ltd.</small></a></h1>
				<div class="nav-collapse collapse">
					<ul class="nav pull-right">
						<li class=""><a href="Index.aspx">Home	</a></li>
						<li class="active"><a href="OurCompany.aspx">Our Company</a></li>  
						<li class=""><a href="OurServices.aspx">Our Services</a></li>						
						<li class=""><a href="OurSupport.aspx">Our Support</a></li>
						<li class=""><a href="Contact.aspx">Contact</a></li>
                        <li class=""><a href="AppLinks.aspx">App Links</a></li>
					</ul>
				</div>
			</div>
		</div>
	</div>
</section>
<!--Header Ends================================================ -->
<!-- Page banner -->
<section id="bannerSection" style="background:url(themes/images/banner/aboutus.png) no-repeat center center #000;">
	<div class="container" >	
		<h1 id="pageTitle">Our Company
		<span class="pull-right toolTipgroup">
			</span>
		</h1>
	</div>
</section> 
<!-- Page banner end -->
<section id="bodySection">	
	<div class="container">	
	<div class="row">
		<div class="span3">
			<div class="well well-small">
				<h3>Card Issuance<span style="color:#39b9d6"> Services</span></h3>
	            <figure class="p2"><img class="img-border" src="themes/images/banner/company.jpg" alt="" /></figure><br /><br />
			</div>						
			<br/>
			<h4>Other Services</h4>
			<ul class="media-list">
			   <li class="media well well-small">
					<a class="pull-left" href="OurServices.aspx">					  
					</a>
					<div class="media-body">
					  <a class="pull-right" href="OurServices.aspx"><strong>TOP-NOTCH SUPPORT SERVICES</strong></a>
					</div>
			  </li>
			   <li class="media well well-small">
					<a class="pull-left" href="themes/images/banner/PajunoCorporateProfile.pdf">					  
					</a>
					<div class="media-body">
					 <a class="pull-right" href="themes/images/banner/PajunoCorporateProfile.pdf"><strong>CORPORATE PROFILE DOWNLOAD</strong></a>
					</div>
			  </li>
			  
			   <li class="media well well-small">
					<a class="pull-left" href="OurSupport.aspx">					  
					</a>
					<div class="media-body">
					 <a class="pull-right" href="#"><strong>OUR SUPPORT</strong></a>
					</div>
			  </li>			  
			</ul>

		</div>
		<div class="span9">
		<div class="well well-small" style="text-align:left">
        
			<h3>Pajuno Development Company Ltd.</h3>
			<p><img src="themes/images/carousel/business_website_templates_3.jpg" alt="Image" /></p>
			<p> 
				Pajuno Development Company Limited is a card based information technology solution provider. The business, which evolved in the year 1998, has systematically developed into a silent icon providing effective distribution and support (technical and consumables) of various card issuance machines (centralized and decentralized systems). Pajuno Development Company Limited is deeply focused on application developments designed for added functionality to our hardware range.
			   <br/>
			</p>
			<p>We sell digital technologies, with solutions for secure and cost effective card issuance, specially packaged for Banking and Financial Institutions. Our organization shares a synergy with several highly effective and reputable organizations to provide hardware distribution, integration and deployment. With this partnership arrangement our clients are assured of a formidable and cost effective technical team, proffering complete and valuable card solutions.
            <br />
            </p>
			<div class="btn-toolbar">
			  <div class="btn-group toolTipgroup">
				<a class="btn" href="Contact.aspx" data-placement="top" data-original-title="Contact Us"><i class="icon-envelope"></i></a>				
				<a class="btn" href="OurSupport.aspx" data-placement="top" data-original-title="Our Support"><i class="icon-link"></i></a>
				<a class="btn" href="OurServices.aspx" data-placement="top" data-original-title="Our Services"><i class="icon-print"></i></a>
			  </div>
			</div>

		<br/>			
		<ul class="media-list">
		  <li class="media well well-small">
				
				<div class="media-body" id="Mission">
				  <h4 class="media-heading">Our Mission</h4>
					To always seek our clients satisfaction with a service culture that keeps them happy and through the formulation and implementation of our policies to recruit quality personnel, train them well, and to adhere strictly to job specifications, ensuring near zero down time and keeping to a schedule of work at all our hardware and solution deployment locations.
					<br/>
				</div>
		  </li>
		  <li class="media well well-small">
				
				<div class="media-body" id="Vision">
				  <h4 class="media-heading">Our Vision</h4>
				Pajuno Development Company Limited aims to become a model of excellence and a key player in the card issuance industry with a view to becoming a hub in Africa for card issuance technology distribution through focus and practices that earn us the confidence of our clients and customers.
				</div>
		  </li>
		 
		</ul>			
		</div>
		</div>
		</div>
	</div>
</section>
 <!-- Footer
  ================================================== -->
<section id="footerSection">
<div class="container">
    <footer class="footer well well-small">
	<div class="row-fluid">
	<div class="span4" >
			<h4>Pajuno Development Company Ltd.</h4>
			<h5>Our Mission</h5>
			<em>
			"To always seek our clients satisfaction with a service culture that keeps them happy and through the formulation and 
            implementation of our policies to recruit quality personnel, train them well, and to adhere strictly to job specifications, 
            ensuring near zero down time and keeping to a schedule of work at all our hardware and solution deployment locations." <br/><br/>
			</em>				
		</div>
		<div class="span5">
        <h4>&nbsp;</h4>
			<h5>Our Vision</h5>
			<em>
			"Pajuno Development Company Limited aims to become a model of excellence and a key player in the card issuance industry with a 
            view to becoming a hub in Africa for card issuance technology distribution through focus and practices that earn us the 
            confidence of our clients and customers." <br/><br/>
			</em>	
        </div>
	
	<div class="span3">
			<h4>Contact us</h4>
			<address style="margin-bottom:15px;">
			<strong><a href="index.aspx" title="business"><i class=" icon-home"></i> Pajuno Development Company Ltd. </a></strong><br/>
				1A Madam Cellular Street, Ajiran, <br/>
				Lekki - Lagos, Nigeria<br/>
			</address>
			Phone: <i class="icon-phone-sign"></i> &nbsp; +234 (0)1 735 8272,<br /> +234 (0)1 735 8273 <br/>
			Email: <i class="icon-envelope-alt"></i> &nbsp; info@pajuno.com <br/><br/>
			<h5>Quick Links</h5>	
			<a href="OurCompany.aspx" title=""><i class="icon-cogs"></i> Our Company </a><br/>
			<a href="OurServices.aspx" title=""><i class="icon-info-sign"></i> Our Services </a><br/>
			<a href="OurSupport.aspx" title=""><i class="icon-question-sign"></i> Our Support </a><br/>	
	</div>
    </div>
	<p style="padding:18px 0 44px">&copy; 2014, allright reserved </p>
	</footer>
    </div><!-- /container -->
</section>

<a href="#" class="btn" style="position: fixed; bottom: 38px; right: 10px; display: none; " id="toTop"> <i class="icon-arrow-up"></i> Go to top</a>
<!-- Javascript
    ================================================== -->
<!-- Placed at the end of the document so the pages load faster -->
    <script type="text/javascript" src="http://platform.twitter.com/widgets.js"></script>
	<script src="themes/js/jquery-1.8.3.min.js"></script>
	<script src="themes/js/bootstrap.min.js"></script>
	<script src="themes/js/bootstrap-tooltip.js"></script>
    <script src="themes/js/bootstrap-popover.js"></script>
	<script src="themes/js/business_ltd_1.0.js"></script>
<!-- Themes switcher section ============================================================================================= -->
<div id="secectionBox">
<link rel="stylesheet" href="themes/switch/themeswitch.css" type="text/css" media="screen" />
<script src="themes/switch/theamswitcher.js" type="text/javascript" charset="utf-8"></script>
	<div id="themeContainer">
	<div id="hideme" class="themeTitle">Style Selector</div>
	<div class="themeName">Oregional Skin</div>
	<div class="images style">
	<a href="themes/css/#" name="current"><img src="themes/switch/images/clr/current.png" alt="bootstrap business templates" class="active"></a>
	</div>
	<div class="themeName">Bootswatch Skins (11)</div>
	<div class="images style">
		<a href="themes/css/#" name="amelia" title="Amelia"><img src="themes/switch/images/clr/amelia.png" alt="bootstrap business templates"></a>
		<a href="themes/css/#" name="spruce" title="Spruce"><img src="themes/switch/images/clr/spruce.png" alt="bootstrap business templates" ></a>
		<a href="themes/css/#" name="superhero" title="Superhero"><img src="themes/switch/images/clr/superhero.png" alt="bootstrap business templates"></a>
		<a href="themes/css/#" name="cyborg"><img src="themes/switch/images/clr/cyborg.png" alt="bootstrap business templates"></a>
		<a href="themes/css/#" name="cerulean"><img src="themes/switch/images/clr/cerulean.png" alt="bootstrap business templates"></a>
		<a href="themes/css/#" name="journal"><img src="themes/switch/images/clr/journal.png" alt="bootstrap business templates"></a>
		<a href="themes/css/#" name="readable"><img src="themes/switch/images/clr/readable.png" alt="bootstrap business templates"></a>	
		<a href="themes/css/#" name="simplex"><img src="themes/switch/images/clr/simplex.png" alt="bootstrap business templates"></a>
		<a href="themes/css/#" name="slate"><img src="themes/switch/images/clr/slate.png" alt="bootstrap business templates"></a>
		<a href="themes/css/#" name="spacelab"><img src="themes/switch/images/clr/spacelab.png" alt="bootstrap business templates"></a>
		<a href="themes/css/#" name="united"><img src="themes/switch/images/clr/united.png" alt="bootstrap business templates"></a>
		<p style="margin:0;line-height:normal;margin-left:-10px;display:none;"><small>These are just examples and you can build your own color scheme in the backend.</small></p>
	</div>
	<div class="themeName">Background Patterns </div>
	<div class="images patterns">
		<a href="themes/css/#" name="pattern1"><img src="themes/switch/images/pattern/pattern1.png" alt="bootstrap business templates" class="active"></a>
		<a href="themes/css/#" name="pattern2"><img src="themes/switch/images/pattern/pattern2.png" alt="bootstrap business templates"></a>
		<a href="themes/css/#" name="pattern3"><img src="themes/switch/images/pattern/pattern3.png" alt="bootstrap business templates"></a>
		<a href="themes/css/#" name="pattern4"><img src="themes/switch/images/pattern/pattern4.png" alt="bootstrap business templates"></a>
		<a href="themes/css/#" name="pattern5"><img src="themes/switch/images/pattern/pattern5.png" alt="bootstrap business templates"></a>
		<a href="themes/css/#" name="pattern6"><img src="themes/switch/images/pattern/pattern6.png" alt="bootstrap business templates"></a>
		<a href="themes/css/#" name="pattern7"><img src="themes/switch/images/pattern/pattern7.png" alt="bootstrap business templates"></a>
		<a href="themes/css/#" name="pattern8"><img src="themes/switch/images/pattern/pattern8.png" alt="bootstrap business templates"></a>
		<a href="themes/css/#" name="pattern9"><img src="themes/switch/images/pattern/pattern9.png" alt="bootstrap business templates"></a>
		<a href="themes/css/#" name="pattern10"><img src="themes/switch/images/pattern/pattern10.png" alt="bootstrap business templates"></a>
		
		<a href="themes/css/#" name="pattern11"><img src="themes/switch/images/pattern/pattern11.png" alt="bootstrap business templates"></a>
		<a href="themes/css/#" name="pattern12"><img src="themes/switch/images/pattern/pattern12.png" alt="bootstrap business templates"></a>
		<a href="themes/css/#" name="pattern13"><img src="themes/switch/images/pattern/pattern13.png" alt="bootstrap business templates"></a>
		<a href="themes/css/#" name="pattern14"><img src="themes/switch/images/pattern/pattern14.png" alt="bootstrap business templates"></a>
		<a href="themes/css/#" name="pattern15"><img src="themes/switch/images/pattern/pattern15.png" alt="bootstrap business templates"></a>
		
		<a href="themes/css/#" name="pattern16"><img src="themes/switch/images/pattern/pattern16.png" alt="bootstrap business templates"></a>
		<a href="themes/css/#" name="pattern17"><img src="themes/switch/images/pattern/pattern17.png" alt="bootstrap business templates"></a>
		<a href="themes/css/#" name="pattern18"><img src="themes/switch/images/pattern/pattern18.png" alt="bootstrap business templates"></a>
		<a href="themes/css/#" name="pattern19"><img src="themes/switch/images/pattern/pattern19.png" alt="bootstrap business templates"></a>
		<a href="themes/css/#" name="pattern20"><img src="themes/switch/images/pattern/pattern20.png" alt="bootstrap business templates"></a>
		 
	</div>
	</div>
</div>
<span id="themesBtn"></span>
    </div>
    </form>
</body>
</html>
