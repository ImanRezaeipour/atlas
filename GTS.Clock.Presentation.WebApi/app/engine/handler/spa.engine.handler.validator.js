define(["jquery", "ko", "spa.engine.infrastructure.cssLoader"],
function (n, i, r) {
    r.load("/app/engine/infrastructure/validator/spa.engine.infrastructure.validator.css"),
    i.bindingHandlers.formValidator = {
        init: function (r, a) {
            var e = i.unwrap(a());
            n(r).formValidation({
                framework: "bootstrap",
                icon: { valid: "glyphicon glyphicon-ok", invalid: "glyphicon glyphicon-remove", validating: "glyphicon glyphicon-refresh" },
                locale: "fa_IR",
                fields: e.fields
            }).on("success.form.fv", function (n) {
                n.preventDefault(), e.success(n)
            }).on("err.form.fv", function (n) {
                e.error(n)
            })
        },
        update: function (r, a) {
            var e = i.unwrap(a());
            e.state() ? (n(r).trigger("submit"), e.state(!1)) : n(r).formValidation("resetForm", !0)
        }
    }
});