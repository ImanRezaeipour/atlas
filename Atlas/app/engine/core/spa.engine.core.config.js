/// <reference path="../lib/jqxdate.js" />
var date = Date.now();

define([], function () {
    return {
        baseUrl: "/DesktopModules/Atlas/app/",
        waitSeconds: 0,
        urlArgs: 'ver=' + (new Date()).getTime(),
        paths: {
            "jquery": "./engine/lib/jquery-2.1.4.min",
            knockout: "./engine/lib/knockout-3.4.0",
            komapping: "./engine/lib/knockout.mapping-latest",
           'knockout-amd-helpers': './engine/lib/knockout-amd-helpers.min',
            ko: "./engine/lib-ext/knockout-extended",
            Q: "./engine/lib/q.min",
            bootstrap: "./engine/lib/bootstrap.min",
            'sugar': "./engine/infrastructure/sugar/sugar-full",
            "jqx-all": "./engine/lib/jqx-all",
            "jqxcheckbox": "./engine/infrastructure/checkbox/spa.engine.infrastructure.checkbox",
            "jqxtree": "./engine/infrastructure/tree/spa.engine.infrastructure.tree",
            "jqxDropDownList": "./engine/infrastructure/dropdownlist/spa.engine.infrastructure.dropdownlist",
            "jqxDropDownButton": "./engine/infrastructure/dropdownbutton/spa.engine.infrastructure.dropdownbutton",
            "keyboardJS": "./engine/lib/keyboard.min",

            //infrastructure
            "spa.engine.infrastructure.domReady": "./engine/infrastructure/loader/spa.engine.infrastructure.domReady",
            "spa.engine.infrastructure.htmlLoader": "./engine/infrastructure/loader/spa.engine.infrastructure.htmlLoader",
            "spa.engine.infrastructure.cssLoader": "./engine/infrastructure/loader/spa.engine.infrastructure.cssLoader",
            "spa.engine.infrastructure.easing": "./engine/infrastructure/easing/spa.engine.infrastructure.easing",
            "spa.engine.infrastructure.message": "./engine/infrastructure/message/spa.engine.infrastructure.message",
            "spa.engine.infrastructure.router": "./engine/infrastructure/router/spa.engine.infrastructure.router",
            "spa.engine.infrastructure.animate": "./engine/infrastructure/animate/spa.engine.infrastructure.animate",
            "spa.engine.infrastructure.guid": "./engine/infrastructure/guid/spa.engine.infrastructure.guid",
            "spa.engine.infrastructure.validator": "./engine/infrastructure/validator/spa.engine.infrastructure.validator",
            "spa.engine.infrastructure.validator.custom.jDate": "./engine/infrastructure/validator/custom/spa.engine.infrastructure.validator.custom.jDate",
            "spa.engine.infrastructure.datepicker": "./engine/infrastructure/datepicker/spa.engine.infrastructure.datepicker",
            "spa.engine.infrastructure.moment": "./engine/infrastructure/moment/spa.engine.infrastructure.moment",
            "spa.engine.infrastructure.moment.jalali": "./engine/infrastructure/moment/spa.engine.infrastructure.moment.jalali",
            "spa.engine.infrastructure.dataTable": "./engine/infrastructure/dataTable/spa.engine.infrastructure.dataTable",
            "spa.engine.infrastructure.dataTableBS": "./engine/infrastructure/dataTable/spa.engine.infrastructure.dataTableBS",
            "spa.engine.infrastructure.jsface": "./engine/infrastructure/jsface/jsface",
            "spa.engine.infrastructure.select": "./engine/infrastructure/select/spa.engine.infrastructure.select",
            "spa.engine.infrastructure.selectfa": "./engine/infrastructure/select/spa.engine.infrastructure.selectfa",
            "velocity": "./engine/infrastructure/animate/spa.engine.infrastructure.velocity.min",
            "velocity-ui": "./engine/infrastructure/animate/spa.engine.infrastructure.velocity.ui.min",
            //"sugar": "./engine/infrastructure/sugar/sugar",

            //handler
            "spa.engine.handler.validator": "./engine/handler/spa.engine.handler.validator",
            "spa.engine.handler.datepicker": "./engine/handler/spa.engine.handler.datepicker",
            "spa.engine.handler.dataTable": "./engine/handler/spa.engine.handler.dataTable",
            "spa.engine.handler.select": "./engine/handler/spa.engine.handler.select",

            //core
            "spa.engine.core.router": "./engine/core/spa.engine.core.router",
            "spa.engine.core.modal": "./engine/core/spa.engine.core.modal",
            "spa.engine.core.validator": "./engine/core/spa.engine.core.validator",
            "spa.engine.core.data": "./engine/core/spa.engine.core.data",
            "spa.engine.core.dataTable": "./engine/core/spa.engine.core.dataTable",
            "spa.engine.core.select": "./engine/core/spa.engine.core.select",

            //util
            "spa.engine.util.utils": "./engine/util/utils",
            "spa.engine.util.cache": "./engine/util/cache",
            "spa.engine.util.storage": "./engine/util/storage",

            //widget
            "spa.engine.widget.manager": "./engine/widget/manager/spa.engine.widget.manager",
            "spa.engine.widget.message": "./engine/widget/message/spa.engine.widget.message",
            "spa.engine.widget.register": "./engine/widget/spa.engine.widget.register",

            "spa.controllers.register": "./spa.controllers.register",
            "spa.query.params": "./spa.query.params"

            //theme                
        //        'jquery-ui': '../theme/assets/js/jqueryui-1.10.3.min',
        //'enquire': '../theme/assets/js/enquire.min',
        //'velocity': '../theme/assets/plugins/velocityjs/velocity.min',
        //'velocity-ui': '../theme/assets/plugins/velocityjs/velocity.ui.min',
        //'wijets': '../theme/assets/plugins/wijets/wijets',
        //'prettify': '../theme/assets/plugins/codeprettifier/prettify',
        //'bootstrap-switch': '../theme/assets/plugins/bootstrap-switch/bootstrap-switch',
        //'bootstrap-tabdrop': '../theme/assets/plugins/bootstrap-tabdrop/js/bootstrap-tabdrop',
        //'icheck': "../theme/assets/plugins/iCheck/icheck.min",
        //'nanoscroller': "../theme/assets/plugins/nanoScroller/js/jquery.nanoscroller.min",
        //'application': "../theme/assets/js/application",
        //'demo': "../theme/assets/demo/demo",
        //"ResponsiveSlides": "../theme/assets/plugins/ResponsiveSlides/responsiveslides.min"

        }, shim: {
            bootstrap: { deps: ["jquery"] },
            "jqx-all": { deps: ["jquery", "ko"] },
            "jqxcheckbox": { deps: ["jqx-all"] },
            "jqxtree": { deps: ["jqx-all"] },
            "jqxDropDownList": { deps: ["jqx-all"] },
            "jqxDropDownButton": { deps: ["jqx-all"] },

            "spa.engine.infrastructure.easing": { deps: ["jquery"] },
            "spa.engine.widget.message": { deps: ["jquery", "spa.engine.infrastructure.easing", "spa.engine.infrastructure.message"] },
            "spa.controllers.register": { deps: ["spa.engine.widget.register"] },
            "spa.engine.handler.validator": { deps: ["spa.engine.infrastructure.validator"] },
            "spa.engine.core.validator": { deps: ["spa.engine.handler.validator"] },
            "spa.engine.core.select": { deps: ["spa.engine.handler.select"] },
            "spa.engine.handler.select": { deps: ["spa.engine.infrastructure.selectfa"] },
            "spa.engine.infrastructure.selectfa": { deps: ["spa.engine.infrastructure.select"] },
            "spa.engine.handler.datepicker": { deps: ["spa.engine.infrastructure.datepicker"] },
            "spa.engine.core.dataTable": { deps: ["spa.engine.handler.dataTable"] },
            "spa.engine.handler.dataTable": { deps: ["spa.engine.infrastructure.dataTableBS"] },
            "spa.engine.infrastructure.dataTableBS": { deps: ["spa.engine.infrastructure.dataTable"] },
            'spa.engine.infrastructure.jsface': { exports: 'Class' }
            //theme
            //'jquery-ui': { deps: ['jquery'] },
            //'icheck': { deps: ['jquery'] },
            //'bootstrap-tabdrop': { deps: ['jquery'] },
            //'bootstrap-switch': { deps: ['jquery'] },
            //'wijets': { deps: ['jquery', 'jquery-ui'] },
            //'application': { deps: ['jquery', 'wijets', 'velocity'] },
            //'demo': { deps: ['jquery', 'wijets'] },
            ////'demo-switcher': { deps: ['jquery'] }
            //'ResponsiveSlides': { deps: ['jquery'] },
            //'velocity': { deps: ['jquery'] }
        }
    }
});