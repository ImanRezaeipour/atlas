define(['ko'], function (ko) {
    ko.components.register('spa-app-home', { require: './controllers/home/home' });
    ko.components.register('spa-app-menu', { require: './controllers/home/menu' });

    ko.components.register('spa-app-hrmsindex', { require: './controllers/hrms/index' });
    ko.components.register('spa-app-kartablelist', { require: './controllers/kartable/list' });

    ko.components.register('spa-app-overtimelist', { require: './controllers/overtime/list' });
    ko.components.register('spa-app-overtimeCreate', { require: './controllers/overtime/create' });
    ko.components.register('spa-app-overtimeEdit', { require: './controllers/overtime/edit' });

    ko.components.register('spa-app-overtimePersonlist', { require: './controllers/overtimePerson/list' });
    ko.components.register('spa-app-overtimePersonEdit', { require: './controllers/overtimePerson/edit' });

    ko.components.register('spa-app-overtimeEdarilist', { require: './controllers/overtimeEdari/list' });
    ko.components.register('spa-app-overtimeEdariEdit', { require: './controllers/overtimeEdari/edit' });

    ko.components.register('spa-app-approvalSchedulelist', { require: './controllers/approvalSchedule/list' });
    ko.components.register('spa-app-approvalScheduleEdit', { require: './controllers/approvalSchedule/edit' });

    ko.bindingHandlers.menu = {
        init: function (element) {
            $('li', element).click(function () {
                $('li', element).removeClass('active');
                $(this).addClass('active');
            })
        }
    };

    
    ko.applyBindings({});
})