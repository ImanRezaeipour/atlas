/// <reference path="../lib/jqxdate.js" />
var date = Date.now();

define([], function () {
    return {
        paths: {
            "jquery": "/app/engine/lib/jquery-2.1.4.min",
            ko: "/app/engine/lib/knockout-3.3.0",
            Q: "/app/engine/lib/q.min",
            bootstrap: "/app/engine/lib/bootstrap.min",

            "jqx-all": "/app/engine/lib/jqx-all",
            "jqxcheckbox": "./engine/infrastructure/checkbox/spa.engine.infrastructure.checkbox.js?" + date,
            "jqxtree": "./engine/infrastructure/tree/spa.engine.infrastructure.tree.js?" + date,
            "jqxDropDownList": "./engine/infrastructure/dropdownlist/spa.engine.infrastructure.dropdownlist.js?" + date,
            "jqxDropDownButton": "./engine/infrastructure/dropdownbutton/spa.engine.infrastructure.dropdownbutton.js?" + date,

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
            "spa.engine.handler.validator": "./engine/handler/spa.engine.handler.validator",
            "spa.engine.handler.datepicker": "./engine/handler/spa.engine.handler.datepicker",
            "spa.engine.handler.dataTable": "./engine/handler/spa.engine.handler.dataTable",
            "spa.engine.core.router": "./engine/core/spa.engine.core.router",
            "spa.engine.core.modal": "./engine/core/spa.engine.core.modal",
            "spa.engine.core.validator": "./engine/core/spa.engine.core.validator",
            "spa.engine.core.data": "./engine/core/spa.engine.core.data",
            "spa.engine.core.dataTable": "./engine/core/spa.engine.core.dataTable",
            "spa.engine.widget.manager": "./engine/widget/manager/spa.engine.widget.manager",
            "spa.engine.widget.message": "./engine/widget/message/spa.engine.widget.message",
            "spa.engine.widget.register": "./engine/widget/spa.engine.widget.register",
            "spa.engine.infrastructure.select": "./engine/infrastructure/select/spa.engine.infrastructure.select",
            "spa.engine.infrastructure.selectfa": "./engine/infrastructure/select/spa.engine.infrastructure.selectfa",
            "spa.engine.handler.select": "./engine/handler/spa.engine.handler.select",
            "spa.engine.core.select": "./engine/core/spa.engine.core.select",
            "spa.controllers.register": "./controllers/spa.controllers.register"

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
            "spa.engine.infrastructure.dataTableBS": { deps: ["spa.engine.infrastructure.dataTable"] }
        }
    }
});