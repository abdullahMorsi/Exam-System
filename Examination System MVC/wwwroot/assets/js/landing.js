(function ($) {
    "use strict";

        // PAGE LOADING
        $(window).on("load", function (e) {
            $("#global-loader").fadeOut("slow");
        })

        // CARD
        const DIV_CARD = 'div.card';
        
        // FUNCTIONS FOR COLLAPSED CARD
        $(document).on('click', '[data-bs-toggle="card-collapse"]', function (e) {
            let $card = $(this).closest(DIV_CARD);
            $card.toggleClass('card-collapsed');
            e.preventDefault();
            return false;
        });

        // BACK TO TOP BUTTON
        $(window).on("scroll", function (e) {
            if ($(this).scrollTop() > 0) {
                $('#back-to-top').fadeIn('slow');
            } else {
                $('#back-to-top').fadeOut('slow');
            }
        });
        $(document).on("click", "#back-to-top", function (e) {
            $("html, body").animate({
                scrollTop: 0
            }, 0);
            return false;
        });
        
        $('.testimonial-carousel').slick({
            slidesToShow: 1,
            slidesToScroll: 1,
            autoplay: true,
            autoplaySpeed: 1000,
            arrows: true,
            dots: false,
                pauseOnHover: false,
                responsive: [{
                breakpoint: 768,
                settings: {
                    slidesToShow: 1
                }
            }, {
                breakpoint: 520,
                settings: {
                    slidesToShow: 1
                }
            }]
        });

        $('.feature-logos').slick({
            slidesToShow: 6,
            slidesToScroll: 1,
            autoplay: true,
            autoplaySpeed: 1000,
            arrows: false,
            dots: false,
                pauseOnHover: false,
                responsive: [{
                breakpoint: 992,
                settings: {
                    slidesToShow: 4
                }
            }, {
                breakpoint: 520,
                settings: {
                    slidesToShow: 2
                }
            }]
        });
        let bodyhorizontal = $('body').hasClass('horizontal');
        if (bodyhorizontal) {
            ActiveSubmenu();
            if(window.innerWidth>=992){
                let li = document.querySelectorAll('.side-menu li')
                li.forEach((e, i) => {
                    e.classList.remove('is-expanded')
                })
                var animationSpeed = 300;
                // first level
                var parent = $("[data-bs-toggle='sub-slide']").parents('ul');
                var ul = parent.find('ul:visible').slideUp(animationSpeed);
                ul.removeClass('open');
                var parent1 = $("[data-bs-toggle='sub-slide2']").parents('ul');
                var ul1 = parent1.find('ul:visible').slideUp(animationSpeed);
                ul1.removeClass('open');
            }
            $('body').addClass('horizontal');
            $(".main-content").addClass("hor-content");
            $(".main-content").removeClass("app-content");
            $(".app-header").addClass("hor-header");
            $(".hor-header").removeClass("app-header");
            $(".app-sidebar").addClass("horizontal-main")
            $('body').removeClass('sidebar-mini');
            $('body').removeClass('sidenav-toggled');
            $('body').removeClass('horizontal-hover');
            $('body').removeClass('default-menu');
            $('body').removeClass('icontext-menu');
            $('body').removeClass('icon-overlay');
            $('body').removeClass('closed-leftmenu');
            $('body').removeClass('hover-submenu');
            $('body').removeClass('hover-submenu1');
            localStorage.setItem("horizantal", "True");
            // // To enable no-wrap horizontal style
            $('#slide-left').removeClass('d-none');
            $('#slide-right').removeClass('d-none');
            document.querySelector('.horizontal .side-menu').style.flexWrap = 'nowrap'
            // To enable wrap horizontal style
            // $('#slide-left').addClass('d-none');
            // $('#slide-right').addClass('d-none');
            // document.querySelector('.horizontal .side-menu').style.flexWrap = 'wrap'
            checkHoriMenu();
            responsive();
        } 


})(jQuery);

// FOOTER
document.getElementById("year").innerHTML = new Date().getFullYear();

window.addEventListener('scroll', reveal);

function reveal(){
    var reveals = document.querySelectorAll('.reveal');
  
    for(var i = 0; i < reveals.length; i++){
  
      var windowHeight = window.innerHeight;
      var cardTop = reveals[i].getBoundingClientRect().top;
      var cardRevealPoint = 150;
  
    //   console.log('condition', windowHeight - cardRevealPoint)
  
      if(cardTop < windowHeight - cardRevealPoint){
        reveals[i].classList.add('active');
      }
      else{
        reveals[i].classList.remove('active');
      }
    }
  }
  
reveal();

// ==== for menu scroll
const pageLink = document.querySelectorAll(".side-menu__item");

pageLink.forEach((elem) => {
    elem.addEventListener("click", (e) => {
        e.preventDefault();
        document.querySelector(elem.getAttribute("href")).scrollIntoView({
            behavior: "smooth",
            offsetTop: 1 - 60,
        });
    });
});

// section menu active
function onScroll(event) {
    const sections = document.querySelectorAll(".side-menu__item");
    const scrollPos =
        window.pageYOffset ||
        document.documentElement.scrollTop ||
        document.body.scrollTop;

        sections.forEach((elem)=> {
        const val = elem.getAttribute("href");
        const refElement = document.querySelector(val);
        const scrollTopMinus = scrollPos + 73;
        if (
            refElement.offsetTop <= scrollTopMinus &&
            refElement.offsetTop + refElement.offsetHeight > scrollTopMinus
        ) {
            elem.classList.add("active");
        } else {
            elem.classList.remove("active");
        }
    })
}
window.document.addEventListener("scroll", onScroll);

