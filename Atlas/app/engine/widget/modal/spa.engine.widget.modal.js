define(["jquery", "bootstrap", "ko", "spa.engine.infrastructure.htmlLoader!./spa.engine.widget.modal.html"], function (i, o, a, e) {
    function t(o) {
        var e = this;
        e.id = o.modal.id,
        e.title = o.modal.title,
        e.cssClass = o.modal.cssClass,
        e.body = o.modal.body,
        e.body.subscribe(function (i) {
            i.params.context = e,
            i.params.loading = e.loading,
            e.loading(!0)
        }),
        e.visible = o.modal.visible,
        e.visible.subscribe(function (o) {
            o ? i("#" + e.id).modal({ backdrop: "static", show: !0 }) : i("#" + e.id).modal("hide")
        }),
        e.callback = o.modal.callback,
        e.buttons = a.observableArray([]),
        e.loading = a.observable(!0),
        e.close = function () {
            e.visible(!1)
        },
        e.registerButtons = function (i) {
            e.buttons(i)
        }
    } return { viewModel: t, template: e }
});