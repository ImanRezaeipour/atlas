define(["ko"], function (e) {
    var t = function (obj) {
        var t = obj.placeholder || 'انتخاب کنید',
            n = obj.tags || false,
            o = obj.multiple || false,
            a = obj.maximumSelectionLength || null,
            i = obj.minimumInputLength || null,
            s = obj.minimumResultsForSearch || -1,
            l = obj.url,
            r = obj.size || null,
            p = obj.templateResult || null,
            opt = obj.cascade || null,
            type = obj.requestType || 'GET',
			item = obj.selectItem || null,
            d = obj.Data || null,
            v = obj.enableValidation || false
        if (opt != null && opt !== undefined) {
            opt.subscribe(function (value) {
                var url = (value == null) ? null : l(value);
                if (url != null) {
                    u.options.ajax.url = url;
                    u.disabled(false);
                } else {
                    url = l;
                    u.disabled(true);
                }
                u.selectedItems(null);
                u.current.empty();
                u.current.select2(u.options);
            });
        }
        var u = this;
        u.current = null,
        u.element = null,
        u.options = {
            theme: "bootstrap",
            language: "fa",
            placeholder: t,
            tags: n,
            dir: "rtl",
            multiple: o,
            maximumSelectionLength: a,
            minimumInputLength: i,
            minimumResultsForSearch: s,
            data: d,
            enableValidation: v,
            ajax: {
                url: l,
                type: type,
                dataType: "json",
                delay: 250,
                quietMillis: 250,
                data: function (e) {
                    return {
                        searchTerm: e.term,
                        pageNum: e.page,
                        pageSize: r
                    }
                },
                processResults: function (e, t) {
                    t.page = t.page || 1;
                    var o = e.items;
                    if (n) {
                        var a = [];
                        o.forEach(function (e) {
                            a.push({
                                id: e.text,
                                text: e.text
                            })
                        }), o = a
                    }
                    return {
                        results: o,
                        pagination: {
                            more: obj.pagination === true ? t.page * r < e.total_count : null
                        }
                    }
                },
                cache: !0,
                templateResult: function (e) {
                    return e.text
                }
            }
        };

        u.selectedItems = e.observableArray([]),
        u.selectedItemsText = e.observableArray([]),
        u.selectItems = e.observableArray([]),
        "Function" == typeof p && (u.options.ajax.templateResult = p),
		item && u.selectItems(item)
    };
    return t
});
 