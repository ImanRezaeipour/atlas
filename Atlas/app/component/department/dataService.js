define(['jquery',
        'Q'],
function ($, Q) {
    function getAllManagerDepartmentTree() {
        var deffer = Q.defer();
        var url = '/api/Department/GetAllManagerDepartmentTree';
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


    return {
        GetAllManagerDepartmentTree: getAllManagerDepartmentTree,

    };
});