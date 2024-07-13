(function (jQuery) {
  "use strict";
  
  var registerDependencies = function () {
    if (null != PluginJsConfig && null != PluginJsConfig.js_dependencies) {
      var js_dependencies = PluginJsConfig.js_dependencies;
      for (var dependency in js_dependencies) {
        asyncloader.register(js_dependencies[dependency], dependency);
      }
    }
    console.log(PluginJsConfig.js_dependencies);
  },
  timer = function () {
    jQuery('.timer').countTo();
  },

  owl_carousel = function () {
    jQuery('.owl-carousel').each(function () {
      var app_slider = jQuery(this);
      var rtl = false;
      var prev = 'ion-ios-arrow-left';
      var next = 'ion-ios-arrow-right';
      var prev_text = 'Prev';
      var next_text = 'Next';
      if (jQuery('body').hasClass('pt-is-rtl')) {
        rtl = true;
        prev = 'ion-ios-arrow-left';
        next = 'ion-ios-arrow-right';
      }
      if (app_slider.data('prev_text') && app_slider.data('prev_text') != '') {
        prev_text = app_slider.data('prev_text');
      }
      if (app_slider.data('next_text') && app_slider.data('next_text') != '') {
        next_text = app_slider.data('next_text');
      }
      app_slider.owlCarousel({
        rtl: rtl,
        items: app_slider.data("desk_num"),
        loop: app_slider.data("loop"),
        margin: app_slider.data("margin"),
        nav: app_slider.data("nav"),
        dots: app_slider.data("dots"),
        loop: app_slider.data("loop"),
        autoplay: app_slider.data("autoplay"),
        autoplayHoverPause: true,
        autoplayTimeout: app_slider.data("autoplay-timeout"),
        navText: ["<i class='" + prev + "'></i>", "<i class='" + next + "'></i>"],
        responsiveClass: true,
        responsive: {
            // breakpoint from 0 up
            0: {
              items: app_slider.data("mob_sm"),
              nav: true,
              dots: false
            },
            // breakpoint from 480 up
            480: {
              items: app_slider.data("mob_num"),
              nav: true,
              dots: false
            },
            // breakpoint from 786 up
            786: {
              items: app_slider.data("tab_num")
            },
            // breakpoint from 1023 up
            1023: {
              items: app_slider.data("lap_num")
            },
            1199: {
              items: app_slider.data("desk_num")
            }
          }
        });
    });
  },

  portfolio_owl_carousel = function() {
    jQuery('.pt-portfoliobox .owl-carousel').each(function() {
      var app_slider = jQuery(this);
      var rtl = false;
      var prev = 'ion-ios-arrow-left';
      var next = 'ion-ios-arrow-right';
      var prev_text = 'Prev';
      var next_text = 'Next';

      if (app_slider.data('prev_text') && app_slider.data('prev_text') != '') {
        prev_text = app_slider.data('prev_text');
      }
      if (app_slider.data('next_text') && app_slider.data('next_text') != '') {
        next_text = app_slider.data('next_text');
      }
      app_slider.owlCarousel({
        center: true,
        items: app_slider.data("desk_num"),
        loop: app_slider.data("loop"),
        nav: app_slider.data("nav"),
        dots: app_slider.data("dots"),        
        margin: app_slider.data("margin"),
        autoplay: app_slider.data("autoplay"),
        autoplayHoverPause: true,
        autoplayTimeout: app_slider.data("autoplay-timeout"),
        navText: ["<i class='" + prev + "'></i>", "<i class='" + next + "'></i>"],
        responsiveClass: true,
        responsive: {

          0: {
            items: app_slider.data("mob_sm"),
              // nav: true,
              dots: false
            },
            // breakpoint from 480 up
            480: {
              items: app_slider.data("mob_num"),
              // nav: true,
              dots: false
            },
            // breakpoint from 786 up
            786: {
              items: app_slider.data("tab_num")
            },
            // breakpoint from 1023 up
            1023: {
              items: app_slider.data("lap_num")
            },
            1199: {
              items: app_slider.data("desk_num")
            }
          }
        });
    });
  },
  portfololio_follow = function() {
    (function(selector, horizontalOffset, verticalOffset) {
      var items;

      selector = selector || '.pt-hover-follow';
      horizontalOffset = horizontalOffset || 5;
      verticalOffset = verticalOffset || 5;

      items = document.querySelectorAll(selector);
      items = Array.prototype.slice.call(items);

      items.forEach(function(item) {
        item.addEventListener('mousemove', function(e) {
          let countCssRules = document.styleSheets[0].cssRules.length;
          var rightSpace = document.body.clientWidth - e.pageX;
          if (rightSpace > 300) {
            var newRule = selector +
            ':hover .pt-portfolio-info {  ' +
            'right:auto; left: ' + (horizontalOffset + e.offsetX) + 'px; ' +
            'top: ' + (e.offsetY + verticalOffset) + 'px; }';
          } else {
            var newRule = selector +
            ':hover .pt-portfolio-info {  ' +
            'left:auto; right: ' + (item.offsetWidth - e.offsetX) + 'px; ' +
            'top: ' + (e.offsetY + verticalOffset) + 'px; }';
          }


          document.styleSheets[0].insertRule(newRule, countCssRules);
        });
      });
    })('.pt-hover-follow', 10, 5);
  },

  pop_video = function () {
    jQuery('.popup-youtube, .popup-vimeo, .popup-gmaps, .button-play').magnificPopup({
      type: 'iframe',
      mainClass: 'mfp-fade',
      preloader: true,
    });
  },
  circle_progress = function () {
    jQuery('.pt-circle-progress-bar').each(function () {
      var number = jQuery(this).data('skill-level');
      var empty_color = jQuery(this).data('empty-color');
      var fill_color = jQuery(this).data('fill-color');
      var size = jQuery(this).data('size');
      var thickness = jQuery(this).data('thickness');
      jQuery(this).circleProgress({
        value: '0.' + number,
        size: size,
        emptyFill: empty_color,
        fill: {
          color: fill_color
        }
      }).on('circle-animation-progress', function (event, progress) {
        jQuery(this).find('.pt-progress-count').html(Math.round(number * progress) + '%');
      });
    });
  },
  progress_bar = function() {
    jQuery('.pt-progress-bar > span').each(function() {
      var app_slider = jQuery('.pt-progressbar-box');
      jQuery(this).progressBar({
        shadow: false,
        animation: true,
        height: app_slider.data('h'),
        percentage: false,
        border: false,
        animateTarget: true,
      });
    });
  }, 
  accordion = function () {
    jQuery('.pt-accordion-block .pt-accordion-box .pt-accordion-details').hide();
    jQuery('.pt-accordion-block .pt-accordion-box:first').addClass('pt-active').children().slideDown('slow');
    jQuery('.pt-accordion-block .pt-accordion-box').on("click", function () {
      if (jQuery(this).children('div.pt-accordion-details').is(':hidden')) {
        jQuery('.pt-accordion-block .pt-accordion-box').removeClass('pt-active').children('div.pt-accordion-details').slideUp('slow');
        jQuery(this).toggleClass('pt-active').children('div.pt-accordion-details').slideDown('slow');
      }
    });
  };
  jQuery(document).ready(function () {

    registerDependencies();

    if (jQuery('.timer').length > 0) {
      asyncloader.require(['jquery.countTo'], function () {
        timer();
      });
    }
    if (jQuery('.pt-portfoliobox .owl-carousel').length > 0) {
      asyncloader.require(['owl.carousel'], function() {
        portfolio_owl_carousel();
      });
    }
    if (jQuery('.owl-carousel').length > 0) {
      asyncloader.require(['owl.carousel'], function () {
        owl_carousel();
      });
    }
    if (jQuery('.popup-youtube, .popup-vimeo, .popup-gmaps, .button-play').length > 0) {
      asyncloader.require(['jquery.magnific-popup'], function () {
        pop_video();
      });
    }
    if (jQuery('.pt-circle-progress-bar').length > 0) {
      asyncloader.require(['circle-progress'], function () {
        circle_progress();
      });
    }

    if (jQuery('.pt-progressbar-box').length > 0) {
      asyncloader.require(['progressbar.js'], function() {
        progress_bar();
      });
    }  

    jQuery('p:empty').remove();

  });
  jQuery(window).load(function() {
    if (jQuery('.pt-portfoliobox-1 .pt-hover-follow')) {
      portfololio_follow();
    }
  });
})(jQuery);