define(['ko',
        'spa.engine.infrastructure.htmlLoader!./spa.engine.widget.manager.html',
        'spa.engine.core.router',
        'spa.engine.infrastructure.cssLoader',
        'spa.engine.core.data',
        'jquery',
        'spa.query.params',
        "spa.engine.util.utils"],
function (ko, template, router, cssLoader, dataService, $, Queryparams, utils) {
    cssLoader.load('/DesktopModules/Atlas/app/engine/css/spa.engine.css');
    window.loader = false;
    function viewModel() {
 
        var self = this;
        self.loading = ko.observable(true);
        self.message = ko.observable(false);
        self.page = ko.observable({ name: 'spa-app-home', params: { loading: self.loading, } });
        self.Bootstrapper = function (page, query, queryString, data) {
            queryString = utils.createQueryString(query);
            page = (page && page.slice(-1) === '/') ? page.slice(page.lenght, -1) : page;
            var sections = [];
            if (page && page.indexOf('/') > -1) {
                sections = page.indexOf('/') > -1 ? page.split('/').reverse() : [page];
            }
            var sectionsStack = sections.slice(0);
            var sectionCurrent = null;

            if (window.loader) { window.loader = false; return; };

            self.tracedRoute = ko.observableArray([]);
            self.loading(true);
            //var queryString = (request.indexOf("?") != -1) ? request.substring(request.indexOf("?")) : "";
            self.page({
                name: 'spa-app-home',
                params: {
                    loading: self.loading,
                    message: self.message,
                    query: query,
                    queryString: queryString,
                    path: page,
                    page: sections,
                    getPage: function (index) {
                        if (!page) { return null; };
                        if (typeof (index) != 'number') {
                            sectionCurrent = sectionsStack.pop();
                            if (sectionCurrent) { self.tracedRoute.push(sectionCurrent); }
                        } else {
                            sectionCurrent = page.split('/')[index];
                            if (sectionCurrent) { self.tracedRoute.push(sectionCurrent); }
                        }
                        return sectionCurrent;
                    },
                    tracedRoute: self.tracedRoute,
                    getComponentName: function () {
                   
                        var currentComponent = page.substring(0, page.lastIndexOf(sectionCurrent) + sectionCurrent.length);
                        if (!currentComponent) return null;
                        var ccArray = currentComponent.indexOf('/') > -1 ? currentComponent.split('/') : [currentComponent];
                        return 'spa-app-' + ccArray.join('-');
                    },
                    loader: function (obj) {
                
                        var z = obj.name.replace('spa-app-', '').split('-');
                        var b = z.join('/') + queryString;
                        self.tracedRoute(z);
                        router.setHash(b);
                        return obj;
                    },
                    setHash: function (obj) {
          
                        //var obj = this.loader(obj);
                        var z = obj.name.replace('spa-app-', '').split('-');
                        var b = z.join('/') + queryString;
                        window.loader = true;
                        self.tracedRoute(z);
                        router.setHash(b);
                        return obj;
                    },
                    PreviousComponent: { name: '', params: {} },
                    root: (query) ? new Queryparams(query, self.message) : null
                }
            });
        }
        router.addRoute('/{?query}', function (query) {
            self.Bootstrapper(null, query);
        });
        router.addRoute('/{page*}/{?query}', function (page, query) {
            self.Bootstrapper(page, query);
        });
        router.addRoute('/{page*}', function (page, query) {
            self.Bootstrapper(page, query);
        });
        router.addRoute('/', function (page, query) {
            self.Bootstrapper(page, query);
        });
        router.rules({ page: '{page*}', query: '?query' });
        router.start();
    }

    return {
        viewModel: viewModel,
        template: template
    };
});