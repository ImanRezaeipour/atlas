<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="spa.aspx.cs" Inherits="spa" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script>    
    !function (e, n, t) { function s(e, n) { return typeof e === n } function a() { var e, n, t, a, o, i, c; for (var f in l) if (l.hasOwnProperty(f)) { if (e = [], n = l[f], n.name && (e.push(n.name.toLowerCase()), n.options && n.options.aliases && n.options.aliases.length)) for (t = 0; t < n.options.aliases.length; t++) e.push(n.options.aliases[t].toLowerCase()); for (a = s(n.fn, "function") ? n.fn() : n.fn, o = 0; o < e.length; o++) i = e[o], c = i.split("."), 1 === c.length ? Modernizr[c[0]] = a : (!Modernizr[c[0]] || Modernizr[c[0]] instanceof Boolean || (Modernizr[c[0]] = new Boolean(Modernizr[c[0]])), Modernizr[c[0]][c[1]] = a), r.push((a ? "" : "no-") + c.join("-")) } } function o(e) { var n = f.className, t = Modernizr._config.classPrefix || ""; if (u && (n = n.baseVal), Modernizr._config.enableJSClass) { var s = new RegExp("(^|\\s)" + t + "no-js(\\s|$)"); n = n.replace(s, "$1" + t + "js$2") } Modernizr._config.enableClasses && (n += " " + t + e.join(" " + t), u ? f.className.baseVal = n : f.className = n) } function i() { return "function" != typeof n.createElement ? n.createElement(arguments[0]) : u ? n.createElementNS.call(n, "http://www.w3.org/2000/svg", arguments[0]) : n.createElement.apply(n, arguments) } var r = [], l = [], c = { _version: "3.3.1", _config: { classPrefix: "", enableClasses: !0, enableJSClass: !0, usePrefixes: !0 }, _q: [], on: function (e, n) { var t = this; setTimeout(function () { n(t[e]) }, 0) }, addTest: function (e, n, t) { l.push({ name: e, fn: n, options: t }) }, addAsyncTest: function (e) { l.push({ name: null, fn: e }) } }, Modernizr = function () { }; Modernizr.prototype = c, Modernizr = new Modernizr, Modernizr.addTest("ie8compat", !e.addEventListener && !!n.documentMode && 7 === n.documentMode); var f = n.documentElement, u = "svg" === f.nodeName.toLowerCase(); Modernizr.addTest("canvas", function () { var e = i("canvas"); return !(!e.getContext || !e.getContext("2d")) }), a(), o(r), delete c.addTest, delete c.addAsyncTest; for (var d = 0; d < Modernizr._q.length; d++) Modernizr._q[d](); e.Modernizr = Modernizr }(window, document);
    if (!Modernizr.canvas) {
        $(".container_class").html('<div style="text-align: center;"><h1 style="color:red;font-size:20px">ورژن مرورگر شما خیلی قدیمی است ، لطفا مرورگر خود را بروز نمایید.</h1><br/><a href="ftp://ftp.tehran.iri/Internet/Browsers/Mozilla%20Firefox/Mozilla.Firefox.44.0.x86.zip">دریافت مرورگر FireFox</a> | <a href="https://support.mozilla.org/fa/kb/%D9%86%D8%B5%D8%A8-%D9%81%D8%A7%DB%8C%D8%B1%D9%81%D8%A7%DA%A9%D8%B3-%D9%88%DB%8C%D9%86%D8%AF%D9%88%D8%B2">آموزش نصب</a><br/><a href="ftp://ftp.tehran.iri/Internet/Browsers/Google%20Chrome/Google.Chrome.47.0.2526.106.x86.zip">دریافت مرورگر Google Chrome</a> | <a href="https://support.google.com/chrome/answer/95346?hl=fa">آموزش نصب</a></div>');
    }
</script>
</head>
<body>
    <form id="form1" runat="server">
          
    </form>
    <spa-widget-manager style="z-index: 0;"></spa-widget-manager>
</body>

<script data-main="app/main.js?ver=4" src="app/engine/lib/require.js?ver=4"></script>
</html>
