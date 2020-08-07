define(['jquery',
        'Q'],
function ($, Q) {
    function get(id) {
        var deffer = Q.defer();
        var url = '/api/overtime/' + (typeof (id) == 'undefined' ? '' : id);
        $.ajax({
            type: 'GET',
            url: url,
            dataType: 'json',
            success: function (data) {
                deffer.resolve(data);
            },
            error: function (e) {
                deffer.reject(e);
            }
        });

        return deffer.promise;
    }
    function post(data) {
        var deffer = Q.defer();

        $.ajax({
            type: 'POST',
            url: '/api/overtime',
            dataType: 'json',
            data: data,
            success: function (data) {
                deffer.resolve(data);
            },
            error: function (e) {
                deffer.reject(e);
            }
        });

        return deffer.promise;
    }
    function put(id, data) {
        var deffer = Q.defer();

        $.ajax({
            type: 'PUT',
            url: '/api/overtime/' + id,
            dataType: 'json',
            data: data,
            success: function (data) {
                deffer.resolve(data);
            },
            error: function (e) {
                deffer.reject(e);
            }
        });

        return deffer.promise;
    }
    function remove(id) {
        var deffer = Q.defer();

        $.ajax({
            type: 'DELETE',
            url: '/api/overtime/' + id,
            dataType: 'json',
            success: function (data) {
                deffer.resolve(data);
            },
            error: function (e) {
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