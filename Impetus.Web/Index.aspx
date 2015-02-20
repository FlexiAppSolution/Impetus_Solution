<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Index.aspx.cs" Inherits="Index" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
     <meta charset="utf-8"/>
    <title>Impetus Solution</title>
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
				<button type="button" class="btn btn-navbar active" data-toggle="collapse" data-target=".nav-collapse">
				<span class="icon-bar"></span>
				<span class="icon-bar"></span>
				<span class="icon-bar"></span>
				</button>
				<h1><a class="brand" href="Index.aspx"> PAJUNO <small> Development Company Ltd.</small></a></h1>
				<div class="nav-collapse collapse">
					<ul class="nav pull-right">
						<li class="active"><a href="Index.aspx">Home	</a></li>
						<li class=""><a href="OurCompany.aspx">Our Company</a></li>  
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
<section id="carouselSection" style="text-align:center">
	<div id="myCarousel" class="carousel slide">
			<div class="carousel-inner">
				<div  style="text-align:center"  class="item active">
					<div class="wrapper"><img src="themes/images/carousel/Bitmap2.png" alt="Image" />
					<div class="carousel-caption">
                      <h2>Our Company</h2>
                     <p>Pajuno Development Company Limited is a card based information technology solution provider. The business, which evolved in the year 1998, has systematically...</p>
					  <a href="OurCompany.aspx#OurCompany" class="btn btn-large btn-success">Read more</a>
                    </div>
					</div>
				</div>
				<div  style="text-align:center"  class="item">
					<div class="wrapper"><img src="themes/images/carousel/Bitmap3.png" alt="Image" />
					<div class="carousel-caption">
                      <h2>Our Mission</h2>
                      <p>To always seek our clients satisfaction with a service culture that keeps them happy and through the formulation and implementation of our policies...</p>
					  <a href="OurCompany.aspx#Mission" class="btn btn-large btn-success">Read more</a>
                    </div>
					</div>
				</div>
				<div  style="text-align:center"  class="item">
					<div class="wrapper"><img src="themes/images/carousel/Bitmap4.png" alt="Image"/>
					<div class="carousel-caption">
                       <h2>Our Vision</h2>
                     <p>Pajuno Development Company Limited aims to become a model of excellence and a key player in the card issuance industry with a view to becoming a hub...</p>
					  <a href="OurCompany.aspx#Mission" class="btn btn-large btn-success">Read More</a>
                    </div>
					</div>
				</div>
				<div  style="text-align:center"  class="item">
					<div class="wrapper"><img src="themes/images/carousel/Bitmap5.png" alt="Images" />
					<div class="carousel-caption">
                      <h2>Our Services</h2>
                     <p>We sell digital technologies, with solutions for secure and cost effective card issuance, specially packaged for Banking and Financial Institutions.</p>
					  <a href="OurServices.aspx" class="btn btn-large btn-success">Our Services</a>
                    </div>
					</div>
				</div>
				
			</div>
			<a class="left carousel-control" href="#myCarousel" data-slide="prev">&lsaquo;</a>
			<a class="right carousel-control" href="#myCarousel" data-slide="next">&rsaquo;</a>
		</div>
</section>
<!-- Sectionone ends ======================================== -->
<section id="middleSection">
<div class="container">
		<div class="row" style="text-align:center">
			<div class="span12">
			<div class="well well-small">
				<h4>PAJUNO Development Company</h4>
				<p>Pajuno Development Company Limited is a card based information technology solution provider. The business, which evolved in the year 1998, has systematically developed into a silent icon providing effective distribution and support (technical and consumables) of various card issuance machines (centralized and decentralized systems). Pajuno Development Company Limited is deeply focused on application developments designed for added functionality to our hardware range.</p>
<p>We sell digital technologies, with solutions for secure and cost effective card issuance, specially packaged for Banking and Financial Institutions. Our organization shares a synergy with several highly effective and reputable organizations to provide hardware distribution, integration and deployment. With this partnership arrangement our clients are assured of a formidable and cost effective technical team, proffering complete and valuable card solutions.<br/><br/></p>
			</div>
			</div>
			<div class="span2">
				<div class="well well-small">
					<h4>
					<a href="OurCompany.aspx" class="popOver" data-placement="top" data-content=" Pajuno Development Company Limited is a card based information technology solution provider." data-original-title="Pajuno Development Company Ltd." style="display:block; text-decoration:none">
					<i style="width:auto; font-size:2em; line-height:1em; height:auto" class="icon-magic"></i>
					<span><br/>Our Company</span>
					</a>
					</h4>
					<a href="OurCompany.aspx"><small>view details</small></a>
				</div>
			</div>
			<div class="span2">
				<div class="well well-small">
					<h4>
					<a href="#" class="popOver" data-placement="top" data-content="To always seek our clients satisfaction with a service culture that keeps them happy..." data-original-title="Our Mission" style="display:block; text-decoration:none">
					<i style="width:auto; font-size:2em; line-height:1em; height:auto" class="icon-link"></i>
					<span><br/>Our Mission</span>
					</a>
					</h4>
					<a href="OurCompany.aspx#Mission"><small>view details</small></a>
				</div>
			</div>
			<div class="span2">
				<div class="well well-small">
					<h4>
					<a href="OurCompany.aspx#Vision" class="popOver" data-placement="top" data-content="Pajuno Development Company Limited aims to become a model of excellence..." data-original-title="Our Vision" style="display:block; text-decoration:none">
					<i style="width:auto; font-size:2em; line-height:1em; height:auto" class="icon-cogs"></i>
					<span><br/>Our Vision</span>
					</a>
					</h4>
					<a href="OurCompany.aspx#Vision"><small>view details</small></a>
				</div>
			</div>
			<div class="span2">
				<div class="well well-small">
					<h4>
					<a href="#" class="popOver" data-placement="top" data-content="We sell digital technologies, with solutions for secure and cost effective card issuance,..." data-original-title="Our Services" style="display:block; text-decoration:none">
					<i style="width:auto; font-size:2em; line-height:1em; height:auto" class="icon-wrench"></i>
					<span><br/>Our Services</span>
					</a>
					</h4>
					<a href="OurServices.aspx"><small>view details</small></a>
				</div>
			</div>
			<div class="span2">
				<div class="well well-small">
					<h4>
					<a href="OurSupport.aspx" id="poverone" class="popOver" data-placement="top" data-content="We render great support for all our products ensuring that our customers are always happy." data-original-title="Our Support" style="display:block; text-decoration:none">
					<i style="width:auto; font-size:2em; line-height:1em; height:auto" class="icon-beaker"></i>
					<span><br/>Our Support</span>
					</a>
					</h4>
					<a href="OurSupport.aspx"><small>view details</small></a>
				</div>
			</div>
			<div class="span2">
				<div class="well well-small">
					<h4>
					<a href="Contact.aspx" class="popOver" data-placement="top" data-content="We appreciate your interest in Pajuno Development Company Limited. If you desire to contact us kindly click this link" data-original-title="Contact Us" style="display:block; text-decoration:none">
					<i style="width:auto; font-size:2em; line-height:1em; height:auto" class="icon-volume-up"></i>
					<span><br/>Contact Us</span>
					</a>
					</h4>
					<a href="Contact.aspx"><small>view details</small></a>
				</div>
			</div>	
		</div>
		</div>
</section>

<!-- body block end======================================== -->
 <!-- Footer
  ================================================== -->
<section id="footerSection">
<div class="container">
    <footer class="footer well well-small">
	<div class="row-fluid">
	<div class="span4">
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
