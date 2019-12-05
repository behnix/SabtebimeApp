(function ($) {
	"use strict";
	
	$(window).on('scroll resize load', function () {
		
		var scroll = $(window).scrollTop();
		
		if( scroll < $('.wrapper header').outerHeight() ) {

			if( sticky ) {

				if( $('.wrapper').hasClass('home-2') ){
					$('.main-menu-area').css('padding-top', 0);
				}
				else{
					$('.header-area').css('padding-bottom', 0);
				}

				$(".sticky-header").removeClass("sticky");
				sticky = false;
			}
		}
		else{
			
			if( !sticky ){

				if( $('.wrapper').hasClass('home-2') ){
					$('.main-menu-area').css('padding-top', $(".sticky-header").outerHeight());
				}
				else{
					$('.header-area').css('padding-bottom', $(".sticky-header").outerHeight());
				}

				$(".sticky-header").addClass("sticky");
				sticky = true;
			}
		}
	});
    
	$('.slider-active').owlCarousel({
		loop: true,
		items: 1,
		autoplay: true,
		dots: true,
		nav: false,
		responsive: {
			0: {
				items: 1
			},
			600: {
				items: 1
			},
			1000: {
				items: 1
			},
			1200: {
				items: 1
			}
		},
		rtl: true
	});


})(jQuery);
