define(["jquery"], function ($) {
    var e = function (e) {
        var n = this;
        n.client = e.client ? e.client : false;
        n.current = null,
        n.element = null,
        n.changePageLength = null,
        n.actionButtons = e.actionButtons || null,
        n.paging = e.paging == "undefined" ? true : e.paging,
        n.info = e.info == "undefined" ? true : e.info
        , "function" == typeof e.draw ? n.draw = e.draw : n.draw = function () { }
        , "function" == typeof e.page ? n.page = e.page : n.page = function () { }
        , "function" == typeof e.select ? n.select = e.select : n.select = function () { }
        , "function" == typeof e.selectData ? n.selectData = e.selectData : n.selectData = function () { }
        , "function" == typeof e.processing ? n.processing = e.processing : n.processing = function () { }

        // ایجاد دکمه های مربوط به عملیات هر سطر 
        if (n.actionButtons) {
            n.actionButtons.className = "actions"
            var listBtn = "";
            $(n.actionButtons.buttons).each(function (index, btn) {
                btn.name = btn.name || ""
                btn.icon = '<i class="' + btn.icon + '"></i>' || "";
                var a = $('<button>' + btn.icon + btn.name + '</button>');
                btn.attr = btn.attr || [];
                $.each(btn.attr, function (key, val) {
                    $(a).attr(key, val)
                });
                $(a).attr("data-index", index);
                listBtn += $(a)[0].outerHTML;
            })
            n.actionButtons.render = function (data) {
                return listBtn
            }
            e.columnDefs.push(n.actionButtons);
        }

        n.option = {
            dom: "itp",
            processing: !n.client,
            serverSide: !n.client,
            paging: n.paging,
            pagingType: "full_numbers",
            info: n.info,
            language: {
                processing: "درحال پردازش...",
                lengthMenu: "تعداد ردیف اطلاعات _MENU_",
                zeroRecords: "",
                emptyTable: "",
                info: "نمایش _START_ تا _END_ از مجموع _TOTAL_ مورد",
                infoEmpty: "",
                infoFiltered: "(فیلتر شده از مجموع _MAX_ مورد)",
                infoPostFix: "",
                search: "جستجو:",
                url: "",
                paginate: {
                    first: "ابتدا",
                    previous: "قبلی",
                    next: "بعدی",
                    last: "انتها"
                }
            },
            columnDefs: e.columnDefs,
            pageLength: e.pageLength || 10,
            order: [],
            ajax: {
                url: e.url || null,
                data: e.filter || null,
                type: e.type || "POST",
            },
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
        };
         
        if (n.client) {
            e.data.subscribe(function (value) {
                n.option.aaData = value;
                n.current.clear().draw();
                n.current.rows.add(value).draw(false);
            });
        }
        else {
            n.option.ajax = {
                url: e.url ? e.url : null,
                data: e.filter ? e.filter : null,
                type: e.type ? e.type : "POST"
            }
        }

        if (e.createdRow) {
            n.option.createdRow = function (row, data, index) {
                e.createdRow(n.element, row, data, index);
            }
        }

        n.selected = e.selected || false;
        n.keybord = e.keybord || false;
    };
    return e
}

);