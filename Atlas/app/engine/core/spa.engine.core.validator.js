define(["ko"], function (ko) {
    var n = function (n, e, o) {
        var c = this;
        c.state = ko.observable(!1),
        c.clear = ko.observable(!1),
        c.validate = ko.observable(!1),
        c.fields = n,
        "function" == typeof e ? c.success = e : c.success = function () { },
        "function" == typeof o ? c.error = o : c.error = function () { }
    };
    return n
});