define(["ko"],
    function (t) {
        var e = function (e, a, n, o, r, i, u, s, l, d, v) {
            var p = this;
            p.options = {
                theme: "bootstrap",
                language: "fa",
                placeholder: e,
                tags: a,
                dir: "rtl",
                multiple: n,
                data: d,
                maximumSelectionLength: o,
                minimumInputLength: r,
                enableValidation: v,
                ajax: i && {
                    url: i,
                    dataType: "json",
                    delay: 250,
                    data: function (t) {
                        return {
                            q: t.term,
                            page: t.page,
                            size: u
                        }
                    },
                    processResults: function (t, e) {
                        e.page = e.page || 1;
                        var n = t.items;
                        if (a) {
                            var o = [];
                            n.forEach(function (t) {
                                o.push({
                                    id: t.text,
                                    text: t.text
                                })
                            }),
                            n = o
                        }
                        return {
                            results: n,
                            pagination: {
                                more: e.page * u < t.total_count
                            }
                        }
                    },
                    cache: !0,
                    templateResult: function (t) {
                        return t.text
                    }
                }
            },
            p.selectedItems = t.observableArray([]),
            "Function" == typeof s && (p.options.ajax.templateResult = s),
            l && p.selectedItems(l)
        }; return e
    });