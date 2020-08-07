define(['jquery', 'sugar'], function ($) {    
    function createQueryString(obj) {

        if (obj == undefined) return '';
        var ret = "?";        
        $.each(obj, function (key, val) {
            ret += key + '=' + val + '&';
        });
        ret = ret.slice(ret.lenght, -1);
        return ret;
    };

    var utils = {
        createQueryString: createQueryString
    };
    return utils;
});
