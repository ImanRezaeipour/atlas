define(['ko',
        'spa.engine.infrastructure.htmlLoader!/DesktopModules/Atlas/app/component/common/Menu/index.html', 'spa.engine.core.data'],
function (ko, template, dataService) {
    function viewModel(params) {
        debugger;
        var self = this;
        self.items = params.items;
        self.FullName = ko.observable('');
        self.UserEmail = ko.observable('');
        self.ImageSrc = ko.observable('');

        self.ImageSrc('http://additionalservices.tehran.iri:8051/image.ashx?t=150&u=' + window['currentUser'].ActiveName);
        self.FullName(window['currentUser'].FirstName + ' ' + window['currentUser'].LastName);
        self.UserEmail(window['currentUser'].ActiveName + '@tehran.ir');
    }

    return {
        viewModel: viewModel,
        template: template
    };
});