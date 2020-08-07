define(['jquery', 'Q', 'spa.engine.widget.message'],
    function ($, Q, message) {

        function get(url, id) {
            var deffer = Q.defer();
            var url = url + (typeof (id) == 'undefined' ? '' : id);
            $.ajax({
                type: 'GET',
                url: url,
                dataType: 'json',
                success: function (data) {
                    deffer.resolve(data);
                },
                error: function (e) {
                    switch (e.status) {
                        case 500:
                            var exceptionMessage = jQuery.parseJSON(e.responseText).ExceptionMessage;
                            if (exceptionMessage != undefined && exceptionMessage != "")
                                message.error(exceptionMessage);
                            else
                                message.error(e.statusText);
                            break;
                        case 404:
                            message.error("خطا در بازیابی اطلاعات");
                            break;
                        case 401:
                            message.error("Unauthorized Request");
                            break;
                    }
                    deffer.reject(e);
                }
            });
            return deffer.promise;
        }

        function post(url, data) {
            var deffer = Q.defer();
            $.ajax({
                type: 'POST',
                url: url,
                dataType: 'json',
                data: data,
                success: function (data) {
                    deffer.resolve(data);
                },
                error: function (e) {
                    switch (e.status) {
                        case 500:
                            var exceptionMessage = jQuery.parseJSON(e.responseText).ExceptionMessage;
                            if (exceptionMessage != undefined && exceptionMessage != "")
                                message.error(exceptionMessage);
                            else
                                message.error(e.statusText);
                            break;
                        case 404:
                            var exceptionMessage = jQuery.parseJSON(e.responseText).ExceptionMessage;
                            if (exceptionMessage != undefined && exceptionMessage != "")
                                message.error(exceptionMessage);
                            else
                                message.error(e.responseText);
                            break;
                        case 401:
                            message.error("Unauthorized Request");
                            break;
                    }
                    deffer.reject(e);
                }
            });
            return deffer.promise;
        }

        function put(url, id, data)  {
            var deffer = Q.defer();
            $.ajax({
                type: 'POST',
                url: url + id,
                dataType: 'json',
                data: data,
                success: function (data) {
                    deffer.resolve(data);
                },
                error: function (e) {
                    switch (e.status) {
                        case 500:
                            var exceptionMessage = jQuery.parseJSON(e.responseText).ExceptionMessage;
                            if (exceptionMessage != undefined && exceptionMessage != "")
                                message.error(exceptionMessage);
                            else
                                message.error(e.responseText);
                            break;
                        case 404:
                            var exceptionMessage = jQuery.parseJSON(e.responseText).ExceptionMessage;
                            if (exceptionMessage != undefined && exceptionMessage != "")
                                message.error(exceptionMessage);
                            else
                                message.error(e.responseText);
                            break;
                        case 401:
                            message.error("Unauthorized Request");
                            break;
                    }
                    deffer.reject(e);
                }
            });
            return deffer.promise;
        }

        function remove(url, id) {
            var deffer = Q.defer();
            $.ajax({
                type: 'DELETE',
                url: url + id, dataType: 'json',
                success: function (data) {
                    deffer.resolve(data);
                },
                error: function (e) {
                    switch (e.status) {
                        case 500:
                            var exceptionMessage = jQuery.parseJSON(e.responseText).ExceptionMessage;
                            if (exceptionMessage != undefined && exceptionMessage != "")
                                message.error(exceptionMessage);
                            else
                                message.error(e.statusText);
                            break;
                        case 404:
                            message.error("خطا در بازیابی اطلاعات");
                            break;
                        case 401:
                            message.error("Unauthorized Request");
                            break;
                    }
                    deffer.reject(e);
                }
            });
            return deffer.promise;
        }

        return {
            get: get,
            post: post,
            put: put,
            remove: remove
        };
    });