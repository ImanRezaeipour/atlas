define(['spa.engine.core.validator'],
function (validator) {
    function factory(success, error) {
        var self = this;

        var validators = {
            DateFrom: {
                validators: {
                    notEmpty: {
                    },
                    jDate: {
                    }
                }
            },
            DateTo: {
                validators: {
                    notEmpty: {
                    },
                    jDate: {
                    }
                }
            } 
        };

        return new validator(validators, success, error);
    }

    return factory;
});