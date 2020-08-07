define(['ko',
        'spa.engine.infrastructure.htmlLoader!/home/home'],
function (ko, template) {
    function viewModel(params) {
        var self = this;

        //Hide Parent Loader
        params.loading(false);
    }

    return {
        viewModel: viewModel,
        template: template
    };
});