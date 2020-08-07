define(["jquery",
    "ko",
    "spa.engine.infrastructure.cssLoader"],
    function (e, n, t) {
        t.load("/DesktopModules/Atlas/app/engine/infrastructure/select/spa.engine.infrastructure.select.css"),
            n.bindingHandlers.select = {
                init: function (t, a) {
                    var r = n.unwrap(a()),
                        s = e(t).select2(r.options);
                    r.disabled = function (value) { e(t).prop("disabled", value); };
                    if (typeof (r.options.ajax.url) == 'function') r.disabled(true);
                    e(".select2-container--bootstrap").removeAttr("style");
                    var a = r.options.enableValidation;
                    r.current = s,
                    r.element = t;
                    var z = function (e) {
                        "object" == typeof e && "undefined" == typeof e.length && ($("<option selected></option>").val(e.id).text(e.text).appendTo($(r.element)),
                        $(r.element).trigger("change")),
                        "object" == typeof e && "number" == typeof e.length && (e.forEach(function (e) {
                            $("<option selected></option>").val(e.id).text(e.text).appendTo($(r.element))
                        }), $(r.element).trigger("change"))
                    };
                   
                    if (r.selectItems().length !== 0) {
                        z(r.selectItems());
                    }
                    r.selectItems.subscribe(function (value) {
                        z(value);
                    })
                    r.selectedItems.subscribe(function (value) {
                        z(value);
                    })
                    e(t).change(function () {
                        r.selectedItems(s.val()),
                        r.selectedItemsText(s.children().last().text())
                        a && e(t).parents("form:first").length > 0 && (e(t).parents("form:first").formValidation("revalidateField", e(t).attr("name")))
                    }) 
                },
                update: function (t, a) {
                    n.unwrap(a());
                    e("span.select2").css("width", "100%")
                }
            }
    });

//define(["jquery", "ko", "spa.engine.infrastructure.cssLoader"],
//function (e, t, n) {
//    n.load("/DesktopModules/Atlas/app/engine/infrastructure/select/spa.engine.infrastructure.select.css"),
//    t.bindingHandlers.select = {
//        init: function (n, i) {
//            var r = t.unwrap(i()),
//                s = e(n).select2(r.options);
//            r.disabled = function (value) { e(n).prop("disabled", value); };
//            if (typeof (r.options.ajax.url) == 'function') r.disabled(true);
//            e(".select2-container--bootstrap").removeAttr("style");
//            var a = r.options.enableValidation;
//            r.current = s,
//            r.element = t,
//            e(n).change(function () {
//                a && (
//                        r.selectedItems(s.val()),
//                        e(n).parents("form:first").length > 0 && (e(n).parents("form:first").formValidation("revalidateField", e(n).attr("name")))
//                    )
//            }),
//            0 != r.selectedItems().length && (
//            r.selectedItems().forEach(function (t) {
//                e("<option selected></option>").val(t.id).text(t.text).appendTo(e(n))
//            }
//            ),
//            e(n).trigger("change")
//            ),
//            r.selectedItems.subscribe(function (t) {
//                (t instanceof Array) && t.forEach(function (t) {
//                    e("<option selected></option>").val(t.id).text(t.text).appendTo(e(n))
//                }),
//                a = !(t instanceof Array),
//                e(n).trigger("change")
//            })
//        }

//    }
//});