define(['ko'], function (ko) {
    ko.components.register('spa-app-home', { require: './app/component/_Home/index.js' });


    ko.bindingHandlers.menu = {
        init: function (element) {
            $('li', element).click(function() {
                $('li', element).removeClass('active');
                $(this).addClass('active');
            });
        }
    }

    ko.bindingHandlers.ResponsiveSlides = {        
        init: function (element, valueAccessor, allBindings, viewModel, bindingContext) {
            
            requirejs(["ResponsiveSlides"], function (r) {
                
                $(element).responsiveSlides({
                    auto: true,
                    pager: false,
                    nav: false,
                    speed: 3000,
                    namespace: "callbacks",
                    before: function () {
                        $('.events').append("<li>before event fired.</li>");
                    },
                    after: function () {
                        $('.events').append("<li>after event fired.</li>");
                    }
                });

            });
        }
    }
    
    ko.bindingHandlers.jDate = {
        init: function (element, valueAccessor, allBindings, viewModel, bindingContext) {
            requirejs(["jquery", "spa.engine.infrastructure.moment.jalali"], function ($, i) {                
                i.loadPersian();                
                if (valueAccessor()!=null)
                $(element).text(i(valueAccessor(), "YYYY/MM/DD").calendar());
            });
        }
    }

    ko.bindingHandlers.digits = {
        init: function (element, valueAccessor, allBindings, viewModel, bindingContext) {
            requirejs(["jquery"], function ($) {
                $(element).text(valueAccessor().toString().replace(/(\d)(?=(\d\d\d)+(?!\d))/g, "$1,"));
            });
        }
    }

    ko.bindingHandlers.bitText = {
        init: function (element, valueAccessor, allBindings, viewModel, bindingContext) {
            requirejs(["jquery"], function ($) {
                $(element).text(valueAccessor()==1?"بلی":"خیر");
            });
        }
    }

    ko.bindingHandlers.animateContent = {
        init: function (element, valueAccessor, allBindings, viewModel, bindingContext) {            
            //transition.slideUpIn
            requirejs(["jquery", 'velocity'], function ($) {
                if ($.fn.velocity) {
                    $(element)
                    .css('visibility', 'visible')
                    .velocity(valueAccessor(), { stagger: 50 });
                }
            });
        }
    }
    
    ko.bindingHandlers.loading = {
        update: function (element, valueAccessor, allBindings) {
                        
            var value = valueAccessor();            
            var valueUnwrapped = ko.unwrap(value);
            requirejs(["jquery"], function ($) {
                var type = (allBindings.hasOwnProperty("type")) ? allBindings.type :"circular";
                if (valueUnwrapped) {
                    $(element).append('<div class="panel-loading"><div class="panel-loader-' + type + '"></div></div>');
                } else {
                    $(element).find('.panel-loading').remove();
                }
            });
        }        
    }

    ko.bindingHandlers.slideVisible = {
        update: function (element, valueAccessor, allBindings) {
            var value = valueAccessor();
            var valueUnwrapped = ko.unwrap(value);
            var duration = allBindings.get('slideDuration') || 400;
            if (valueUnwrapped == true)
                $(element).slideDown(duration);
            else
                $(element).slideUp(duration);
        }
    };

    ko.applyBindings({});
})