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
            },
            DateRangeOrder: {
                validators: {
                    notEmpty: {},
                    numeric: {},
                    greaterThan: { value: 1, message: 'کد دوره از 1 کمتر نمی تواند باشد' },
                    lessThan: { value: 12, message: 'کد دوره از 12 بیشتر نمی تواند باشد' }
                }
            }
        };

        return new validator(validators, success, error);
    }

    return factory;
});