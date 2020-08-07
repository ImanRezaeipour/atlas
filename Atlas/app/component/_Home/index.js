define(['ko',
        'spa.engine.infrastructure.htmlLoader!/DesktopModules/Atlas/app/component/_Home/index.html',
        'spa.engine.core.data', 'jquery'],
    function (ko, template, dataService) {        
        function viewModel(params) {            
           
            var self = this;
            self.params=params;
            //self.HomeContainer = null;
            var page = params.getPage(0);
            if(page)
            {
                var componentName=params.getComponentName();
                self.HomeContainer={ name: componentName, params: params };
            }
            else
                self.HomeContainer={ name: 'spa-app-common-landingpage', params: params };
        }
        return {
            viewModel: viewModel,
            template: template
        };
    });
