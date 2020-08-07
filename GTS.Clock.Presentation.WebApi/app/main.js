(function () {

    require(['./engine/core/spa.engine.core.config'], function (config) {

        require.config(config);

        require(['spa.controllers.register',
                 'spa.engine.infrastructure.cssLoader',
                 'spa.engine.widget.message'], function (r, cssLoader, message) {
                     cssLoader.load('/app/engine/css/bootstrap.min.css');
                     cssLoader.load('/app/engine/css/jqx.base.css');
                     cssLoader.load('/app/engine/css/jqx.bootstrap.css');
                     cssLoader.load('/app/engine/css/bootstrap-rtl.min.css');
                     cssLoader.load('/app/engine/css/Site.css');
                     message.info('برنامه با موفقیت بارگذاری گردید.');
                 });
    });
})();