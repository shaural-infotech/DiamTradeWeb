$(document).ready(function() {
	// Navigationbar Menu JS
	$(".hamburger-menu").click(function() {
		$(".navbar-menu").slideToggle(700);
	});

  // Dropdown Menu Responsive JS
  // $(".menu-listing > li ").click(function() {
  //   $(".dropdown-products").slideToggle();
  // });

	// Client Slider JS
	$('.client-Slides').slick({
	  slidesToShow: 4,
	  slidesToScroll: 1,
	  autoplay: true,
	  autoplaySpeed: 1000,
	  
  responsive: [
    {
      breakpoint: 1024,
      settings: {
        slidesToShow: 3,
        slidesToScroll: 3,
        infinite: true,
        dots: true
      }
    },
    {
      breakpoint: 600,
      settings: {
        slidesToShow: 2,
        slidesToScroll: 2
      }
    },
    {
      breakpoint: 480,
      settings: {
        slidesToShow: 1,
        slidesToScroll: 1
      }
    }
    // You can unslick at a given breakpoint now by adding:
    // settings: "unslick"
    // instead of a settings object
  ]
	});

  
	// Product Screenshot Slider JS

	$('.product-ss-Slides').slick({
		centerMode: true,
		centerPadding: '60px',
	  slidesToShow: 3,
	  slidesToScroll: 1,
	  // autoplay: true,
	  // autoplaySpeed: 1000,
	});


	// Switch JS

	$("input[name='switch']").click(function() {
		if($("#switch").is(':checked')) {
			$(".check-cloud").hide();
			$(".check-desktop").show();
		} else {
			$(".check-cloud").show();
			$(".check-desktop").hide();
		}
	});


	// Features Slider JS

	$('.feature-Slides').slick({
	  slidesToShow: 4,
	  slidesToScroll: 1,
	  autoplay: true,
	  autoplaySpeed: 1000,
	});


	// Product JS

	$(".product-listing").on("click", "li", function() {
    var tabsId = $(this).attr("id");
    $(this).addClass("active").siblings().removeClass("active");
    $("#" + tabsId + "-content-box").addClass("active").siblings().removeClass("active");
  });
  $(".service-listing").on("click", "li", function() {
    var tabsId = $(this).attr("id");
    $(this).addClass("active").siblings().removeClass("active");
    $("#" + tabsId + "-content-box").addClass("active").siblings().removeClass("active");
  });
  $(".product-listing > li > a").click(function(e) {
    e.preventDefault();
    $(".product-listing > li > a").parent().removeClass("active");
    $(this).parent().addClass("active");
  });
  $(".service-listing > li > a").click(function(e) {
    e.preventDefault();
    $(".service-listing > li > a").parent().removeClass("active");
    $(this).parent().addClass("active");
  });

  // Menu Active JS
 	// $(".menu-listing > li > a").click(function() {
  // 	$(".menu-listing > li > a").parent().removeClass("active");
  // 	$(this).parent().addClass("active");
  // });

  // Dropdown JS
  // $(".menu-listing > li:nth-child(2) > a").click(function() {
  // 	$(".dropdown-products").slideToggle(700);
  // });
  // $(".menu-listing > li:nth-child(3) > a").click(function() {
  // 	$(".dropdown-services").slideToggle(700);
  // });
  /*$(".navbar-menu > ul > li > a").hover(function(e) {
		e.preventDefault();
		$(this).parent().parent().find('div.dropdown-products').slideUp(700);
		if(!$(this).next().is(":visible")) {
			$(this).next().slideDown(700);
		}
	});*/
  // Prevent Right Click
	// $(document).bind("contextmenu",function(e){
	// 	return false;
	// });
	// Prevent F12
	// document.onkeydown = function (event) {
	// 	event = (event || window.event);
	// 	if (event.keyCode == 123) {
	// 	//alert(‘No F-keys’);
	// 	return false;
	// 	}
	// }
	// window.oncontextmenu = function () {
	// 	return false;
	// }
	// $(document).keydown(function (event) {
	// 	if (event.keyCode == 123) {
	// 		return false;
	// 	}
	// 	else if ((event.ctrlKey && event.shiftKey && event.keyCode == 73) || (event.ctrlKey && event.shiftKey && event.keyCode == 74)) {
	// 		return false;
	// 	}
	// });

	// Gallery JS

	$(".filter-button").click(function(){
        var value = $(this).attr('data-filter');
        
        if(value == "all")
        {
          //$('.filter').removeClass('hidden');
          $('.filter').show('1000');
        }
        else
        {
//            $('.filter[filter-item="'+value+'"]').removeClass('hidden');
//            $(".filter").not('.filter[filter-item="'+value+'"]').addClass('hidden');
	        $(".filter").not('.'+value).hide('3000');
	        $('.filter').filter('.'+value).show('3000');
            
        }
       if ($(".filter-button").removeClass("active")) {
            $(this).removeClass("active");
            }
            $(this).addClass("active");
            
            });

    $('[data-toggle="popover"]').popover(); 

    $(document).keydown(function (e) {
        if (e.keyCode === 37) {
            // Previous
            $(".carousel-control.left").click();
            return false;
        }
        if (e.keyCode === 39) {
            // Next
            $(".carousel-control.right").click();
            return false;
        }
    });

});