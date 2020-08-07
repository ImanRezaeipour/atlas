define(['ko',
        'spa.engine.infrastructure.htmlLoader!/home/menu'],
function (ko, template) {
    function viewModel(params) {
        var self = this;
        self.items = params.items;
    }

    return {
        viewModel: viewModel,
        template: template
    };
});