define(["jquery", "ko", "spa.engine.infrastructure.cssLoader"], function ($, ko, r) {
    r.load("/DesktopModules/Atlas/app/engine/infrastructure/validator/spa.engine.infrastructure.validator.css"),
    ko.bindingHandlers.formValidator = {
        init: function (element, valueAccessor, allBindings, viewModel, bindingContext) {
            var e = ko.unwrap(valueAccessor());
            $(element).formValidation({
                framework: "bootstrap",
                icon: {
                    valid: "glyphicon glyphicon-ok",
                    invalid: "glyphicon glyphicon-remove",
                    validating: "glyphicon glyphicon-refresh"
                },
                locale: "fa_IR",
                fields: e.fields
            }).on("success.form.fv", function (n) {
                n.preventDefault(), e.success(n)
            }).on("err.form.fv", function (n) {
                e.error(n)
            });

            e.clear.subscribe(function () {
                $(element).formValidation("resetForm", !0)
            });
            e.validate.subscribe(function () {
                $(element).trigger("submit");
            });

        },
        update: function (r, valueAccessor) {
        }
    }
});