define(["spa.engine.infrastructure.message", "spa.engine.infrastructure.cssLoader"],
    function (e, n) {
        function s(n, s) {
            e.error(n, s)
        }
        function t(n, s) { e.info(n, s) }
        function i(n, s) { e.success(n, s) }
        function r(n, s) { e.warning(n, s) }
        return n.load("/app/engine/infrastructure/message/spa.engine.infrastructure.message.css"),
            n.load("/app/engine/infrastructure/message/spa.engine.infrastructure.message-rtl.css"),
            e.options = {
                direction: "rtl",
                closeButton: !0,
                debug: !1,
                newestOnTop: !0,
                progressBar: !0,
                positionClass: "toast-bottom-full-width",
                preventDuplicates: !1,
                onclick: null,
                showDuration: "300",
                hideDuration: "1000",
                timeOut: "2000",
                extendedTimeOut: "1000",
                showEasing: "easeInBounce",
                hideEasing: "easeInBack",
                showMethod: "fadeIn",
                hideMethod: "fadeOut"
            }, {
                error: s,
                info: t,
                success: i,
                warning: r
            }
    });