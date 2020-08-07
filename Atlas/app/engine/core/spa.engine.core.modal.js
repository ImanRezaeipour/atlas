define(["ko", "spa.engine.infrastructure.guid"], function (e, i) {
    function n(n) {
        var a = this;
        a.id = i.newGuid(),
        a.title = e.observable(""),
        a.cssClass = e.observable("modal-dialog"),
        a.body = e.observable("spa-widget-empty"),
        a.visible = e.observable(!1), n ? a.callback = n : a.callback = function () { }
    } return n
});