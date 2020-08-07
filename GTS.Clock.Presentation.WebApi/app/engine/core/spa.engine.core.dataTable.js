define(function () {
    var e = function (e, n, t, i, o, a, r) {
        var u = this;
        u.current = null,
        u.element = null,
        u.chagePageLength = null,
        "function" == typeof i ? u.draw = i : u.draw = function () { },
        "function" == typeof o ? u.page = o : u.page = function () { },
        "function" == typeof a ? u.select = a : u.select = function () { },
        u.option = {
            dom: "itp",
            processing: !0,
            serverSide: !0,
            paging: !0,
            pagingType: "full_numbers",
            language: {
                processing: "درحال پردازش...",
                lengthMenu: "تعداد ردیف اطلاعات _MENU_",
                zeroRecords: "", emptyTable: "",
                info: "نمایش _START_ تا _END_ از مجموع _TOTAL_ مورد",
                infoEmpty: "تهی",
                infoFiltered: "(فیلتر شده از مجموع _MAX_ مورد)",
                infoPostFix: "", search: "جستجو:",
                url: "",
                paginate: { first: "ابتدا", previous: "قبلی", next: "بعدی", last: "انتها" }
            },
            columnDefs: e,
            pageLength: n,
            order: [[1, "asc"]],
            ajax: { url: r, data: t },
            "footerCallback": function (row, data, start, end, display) {
                var api = this.api();
                api.columns('.sum', { page: 'current' }).every(function () {
                    var sum = api
                        .cells(null, this.index(), { page: 'current' })
                        .render('display')
                        .reduce(function (a, b) {
                            var x = parseFloat(a) || 0;
                            var y = parseFloat(b) || 0;
                            return x + y;
                        }, 0); 
                    $(this.footer()).html(sum);
                });
            }
        }
    };
    return e
});