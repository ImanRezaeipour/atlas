define(["spa.engine.infrastructure.router"], function () {
    function r(r) {
        function e(r, e) {
            crossroads.parse(r);
        }
        hasher.changed.add(e),
        hasher.initialized.add(e),
        hasher.init(),
        r && hasher.setHash(r)
    }

    function e(r, e) {
        return crossroads.addRoute(r, e);
    }

    function d(r) {
        crossroads.routed.add(r);
    }

    function c(r) {
        return crossroads.rules = r;
    }
    function f(r) {
        return hasher.setHash(r);
    }

    return {
        addRoute: e,
        rules: c,
        start: r,
        routed: d,
        setHash: f
    }
});