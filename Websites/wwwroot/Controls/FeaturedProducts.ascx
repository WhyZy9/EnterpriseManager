﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FeaturedProducts.ascx.cs" Inherits="SieraDelta.Website.Controls.FeaturedProducts" %>
				
<div class="featuredProduct">
		
	<h5><%=Languages.LanguageStrings.FeaturedProducts %>...</h5>
	
	<ul class="mycarousel fixed">
        <%=GetCarouselProducts() %>
	</ul>
				
</div><!-- end of #productScroller -->
