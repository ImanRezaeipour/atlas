define(['ko',
        'spa.engine.infrastructure.htmlLoader!/DesktopModules/Atlas/app/component/common/Breadcrumb/index.html',
    'spa.engine.core.data', 'jquery'],
function (ko, template, dataService) {
    function viewModel(params) {
        var self = this;
        self.visibleBreadcrumb = params.tracedRoute().length;
        self.dictionary = {
            "home": "سامانه جامع حضور و غیاب شهرداری تهران",
            "create": "درج",
            "edit": "ویرایش",
            "delete": "حذف",
            "overtimeperson": "تایید اضافه کار تشویقی - مدیران و معاونین",
            "overtimeedari": "تایید اضافه کار تشویقی - اداری",
            "approvalschedule": "زمان بندی تایید کارتابل اضافه کار تشویقی",
            "overtime": "بودجه بندی اضافه کاری تشویقی"
        };
        self.items = ko.observableArray([]);
        self.createBreadcrumbItems = function () {
            self.items([{ href: "../../" + params.queryString, name: "سامانه جامع حضور و غیاب شهرداری تهران" }]);
            var temp = [];
            for (var i = 0 ; i < params.tracedRoute().length; i++) {
                var item = params.tracedRoute()[i];
                item = item.toLowerCase();

                var queryString = "";
                if (params.queryString != null)
                    queryString = params.queryString;

                var obj = {
                    href: (temp.length) ? '#' + temp.join('/') + '/' + item + params.queryString : '#' + item + queryString,
                    name: (self.dictionary[item]) ? self.dictionary[item] : item
                }
                temp.push(item);
                self.items.push(obj);
            };
        };
        self.createBreadcrumbItems();
        params.tracedRoute.subscribe(function (value) {
            self.createBreadcrumbItems();
        })
    }
    return {
        viewModel: viewModel,
        template: template
    };
});