define(['ko',
        'spa.engine.infrastructure.htmlLoader!/DesktopModules/Atlas/app/component/common/Header/index.html',
        'spa.engine.core.data', 'jquery'],
function (ko, template, dataService) {
    function viewModel(params) {
        var self = this;
        self.items = params.items;
        self.username = ko.observable();

        GetCurrentUsername();
        function GetCurrentUsername() {
            dataService.get('/DesktopModules/Atlas/api/user/GetCurrentUsername/', null).done(function (data) {
                self.username(data);
            }, function (error) {

            });
        }

        $('#trigger-fullscreen').click(function (e) {
            e.preventDefault();
            Utility.toggle_fullscreen(document.documentElement);
        });
        $('#trigger-close').click(function (e) {
            e.preventDefault();
            window.top.close();
        });
        $('#trigger-sidebar>a').click(function () {
            Utility.toggle_leftbar();
        });
        $(".switch-header-footer").on("click", function () {
            $(".skin_footer").toggle();
            $(".skin_header").toggle();
            if ($(".skin_footer").css("display") == "none") {
                localStorage.setItem("hideHeaderFooter", true);
            } else {
                localStorage.setItem("hideHeaderFooter", false);
            }
        });
    }

    return {
        viewModel: viewModel,
        template: template
    };
});