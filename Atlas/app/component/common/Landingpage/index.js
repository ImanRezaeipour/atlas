
define(['ko',
        'spa.engine.infrastructure.htmlLoader!/DesktopModules/Atlas/app/component/common/Landingpage/index.html',
        'spa.engine.widget.message',
        'spa.engine.core.data',
        "spa.engine.infrastructure.moment.jalali",
        'spa.engine.infrastructure.cssLoader'],
    function (ko, template,message,dataService,moment,cssLoader) {
        function viewModel(params) {
            var self = this;
            
            
            params.loading(false);


        }

        return {
            viewModel: viewModel,
            template: template
        };
    });
