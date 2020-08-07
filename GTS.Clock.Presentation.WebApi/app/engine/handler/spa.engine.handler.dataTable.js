define(["jquery",
    "ko",
    "spa.engine.infrastructure.cssLoader"],
    function (e, t, n) {
        n.load("/app/engine/infrastructure/dataTable/spa.engine.infrastructure.dataTable.css"),
            t.bindingHandlers.dataTable = {
                init: function (n, a) {
                    var r = t.unwrap(a());
                    r.element = n,
                    r.current = e(n).DataTable(r.option),
                    r.current.on("draw.dt", function () {
                        r.draw(),
                        e("tbody", n).on("change", ".select", function () {
                            var t = e(this).prop("checked");
                            e("tbody .select", n).prop("checked", ""),
                            e(this).prop("checked", t),
                            e("tbody tr", n).removeClass("selected"),
                            t && e(this).parents("tr:first").addClass("selected"),
                            r.select(e(this).val())
                        }),
                         e("tbody", n).on('click', 'tr', function () {
                             e("tbody tr", n).removeClass("selected");
                             e("tbody .select", n).prop("checked", ""),
                             e(this).addClass('selected'),
                             e(this).children('td:first').children()[0].checked = true,
                             r.select(e(this).children('td:first').children()[0].value)
                         })
                    }),
                    r.current.on("page.dt", r.page),
                    r.chagePageLength = function (t) {
                        r.current.destroy(),
                        e(n).empty(),
                        r.option.pageLength = t,
                        r.current = e(n).DataTable(r.option),
                        r.current.on("draw.dt", r.draw)
                    }
                }
            }
    });