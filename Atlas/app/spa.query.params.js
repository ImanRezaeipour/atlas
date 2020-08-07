define(['ko',
        'spa.engine.core.data',
        'jquery',
        'spa.engine.widget.message',
        "spa.engine.util.cache"],
function (ko, dataService, $, message, cache) {
    var e = function (query, blockMessage) {
        var t = this;
        try {
           
            var USER_CACHE = new cache(2000, 'query.params');
            var obj = USER_CACHE.get(window.personId);
            //if (!obj) {
               
                var ret = $.ajax({
                    async: false,
                    type: 'GET',
                    url: '/DesktopModules/Atlas/api/user/GetCurrentUsername/',
                    dataType: 'json',
                    contentType: 'application/json'
                });
                if (ret.status != 200) throw ret.status;
               
                obj = ret.responseJSON;
                USER_CACHE.set(window.personId, obj);
            //}
            t.user = obj.FullName;
            t.access = obj.Access;
        } catch (err) {
            switch (err) {
                case 500:
                    t.message = "کاربر مورد نظر یافت نشد یا کد کاربری ارسالی اشتباه می باشد.";
                    break;
                case 401:
                    t.message = "نشست شما با سرور منقضی شده، لطفا یک بار دیگر وارد شوید.";
                    break;
                default:
                    t.message = "در ارتباط با سرور خطایی رخ داده";
            }
            t.user = null;
            t.access = null;
            blockMessage(t.message);
            message.warning(t.message);
            t.message = "در بارگذاری اطلاعات کاربر خطایی رخ داده";

        }

    };
    return e
});