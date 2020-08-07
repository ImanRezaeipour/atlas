define(["jquery", "ko", "spa.engine.infrastructure.cssLoader"],
function (e, t, n) {
    n.load("/app/engine/infrastructure/select/spa.engine.infrastructure.select.css"),
    t.bindingHandlers.select = {
        init: function (n, i) {
            var r = t.unwrap(i()),
                s = e(n).select2(r.options);
            e(".select2-container--bootstrap").removeAttr("style");
            var a = r.options.enableValidation;
            e(n).change(function () {
                a && (
                        r.selectedItems(s.val()),
                        e(n).parents("form:first").length > 0 && (e(n).parents("form:first").formValidation("revalidateField", e(n).attr("name")))
                    )
            }),
            0 != r.selectedItems().length && (
            r.selectedItems().forEach(function (t) {
                e("<option selected></option>").val(t.id).text(t.text).appendTo(e(n))
            }
            ),
            e(n).trigger("change")
            ),
            r.selectedItems.subscribe(function (t) {
                (t instanceof Array) && t.forEach(function (t) {
                    e("<option selected></option>").val(t.id).text(t.text).appendTo(e(n))
                }),
                a = !(t instanceof Array),
                e(n).trigger("change")
            })
        }
    }
});