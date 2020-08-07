define(['ko',
        'spa.engine.infrastructure.htmlLoader!./spa.engine.widget.manager.html',
        'spa.engine.core.router',
        'spa.engine.infrastructure.cssLoader'],
function (ko, template, router, cssLoader) {

    cssLoader.load('/app/engine/css/spa.engine.css');

    function viewModel() {
        var self = this;

        self.loading = ko.observable(true);

        self.page = ko.observable({ name: 'spa-app-home', params: { loading: self.loading } });

        router.addRoute('{page}', function (page) {
            switch (page) {
                case "home":
                    self.loading(true);
                    self.page({ name: 'spa-app-home', params: { loading: self.loading } });
                    break;
                case "hrms":
                    self.loading(true);
                    self.page({ name: 'spa-app-hrmsindex', params: { loading: self.loading } });
                    break;
                case "kartable":
                    self.loading(true);
                    self.page({ name: 'spa-app-kartablelist', params: { loading: self.loading } });
                    break;
                case "overtime":
                    self.loading(true);
                    self.page({ name: 'spa-app-overtimelist', params: { loading: self.loading } });
                    break;
                case "overtimePersonlist":
                    self.loading(true);
                    self.page({ name: 'spa-app-overtimePersonlist', params: { loading: self.loading } });
                    break;
                case "overtimeEdarilist":
                    self.loading(true);
                    self.page({ name: 'spa-app-overtimeEdarilist', params: { loading: self.loading } });
                    break;
                case "approvalSchedulelist":
                    self.loading(true);
                    self.page({ name: 'spa-app-approvalSchedulelist', params: { loading: self.loading } });
                    break;
            }
        });

        router.start();
    }

    return {
        viewModel: viewModel,
        template: template
    };
});