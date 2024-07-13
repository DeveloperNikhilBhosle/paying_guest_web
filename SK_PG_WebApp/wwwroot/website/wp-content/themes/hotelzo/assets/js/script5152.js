/*
Template: Hotelzo - Luxury Hotel WordPress Theme 
Author: Peacefulqode.com
Version: 1.1
Design and Developed by: Peacefulqode
NOTE: This is main javasctipt file of template.
*/
/*====================================
[  Table of contents  ]
======================================
==> Page Loader
==> Search Button
==> Sidebar Toggle
==> Sticky Header
==> Back To Top
======================================
[ End table content ]
======================================
*/
(function(jQuery) {
    "use strict";
    jQuery(window).on('load', function(e) {
        /*------------------------
                Page Loader
        --------------------------*/
        jQuery("#pt-loading").fadeOut();
        jQuery("#pt-loading").delay(0).fadeOut("slow");
        /*------------------------
                Search Button
        --------------------------*/
        jQuery('#pt-seacrh-btn').on('click', function() {
            jQuery('.pt-search-form').slideToggle();
            jQuery('.pt-search-form').toggleClass('pt-form-show');
            if (jQuery('.pt-search-form').hasClass("pt-form-show")) {
                jQuery(this).html('<i class="ti-close"></i>');
            } else {
                jQuery(this).html('<i class="ti-search"></i>');
            }
        });
        /*------------------------
                Sidebar Toggle
        --------------------------*/
        jQuery("#pt-toggle-btn").on('click', function() {
            jQuery('#pt-sidebar-menu-contain').toggleClass("active");
        });
        jQuery('.pt-toggle-btn').click(function() {
            jQuery('body').addClass('pt-siderbar-open');
        });
        jQuery('.pt-close').click(function() {
            jQuery('body').removeClass('pt-siderbar-open');
        });
        /*------------------------
                Sticky Header
        --------------------------*/
        var view_width = jQuery(window).width();
        if (!jQuery('header').hasClass('pt-header-default') && view_width >= 1023)
        {
            var height = jQuery('header').height();
            jQuery('.pt-breadcrumb').css('padding-top', height * 1.3);
        }
        if (jQuery('header').hasClass('pt-header-default'))
        {
            jQuery(window).scroll(function() {
                var scrollTop = jQuery(window).scrollTop();
                if (scrollTop > 300) {
                    jQuery('.pt-bottom-header').addClass('pt-header-sticky animated fadeInDown animate__faster');
                } else {
                    jQuery('.pt-bottom-header').removeClass('pt-header-sticky animated fadeInDown animate__faster');
                }
            });
        }
        if (jQuery('.pt-bottom-header').hasClass('pt-has-sticky')) {
            jQuery(window).scroll(function() {
                var scrollTop = jQuery(window).scrollTop();
                if (scrollTop > 300) {
                    jQuery('.pt-bottom-header').addClass('pt-header-sticky animated fadeInDown animate__faster');
                } else {
                    jQuery('.pt-bottom-header').removeClass('pt-header-sticky animated fadeInDown animate__faster');
                }
            });
        }
        /*------------------------
                Back To Top
        --------------------------*/
        jQuery('#back-to-top').fadeOut();
        jQuery(window).on("scroll", function() {
            if (jQuery(this).scrollTop() > 250) {
                jQuery('#back-to-top').fadeIn(1400);
            } else {
                jQuery('#back-to-top').fadeOut(400);
            }
        });
        jQuery('#top').on('click', function() {
            jQuery('top').tooltip('hide');
            jQuery('body,html').animate({
                scrollTop: 0
            }, 800);
            return false;
        });
    });
})(jQuery);