define(['spa.engine.core.validator'],
function (validator) {
    function factory(success, error) {
        var self = this;

        var validators = {
            MaxOverTime: {
                validators: {
                    notEmpty: {},
                    numeric: {},
                    greaterThan: { value: 0, message: 'مقدار سرانه اضافه کاری تشویقی از صفر کمتر نمی تواند باشد' },
                    lessThan: { value: 270, message: 'مقدار سرانه اضافه کاری تشویقی از 270 بیشتر نمی تواند باشد' }
                }
            },
            MaxNightWork: {
                validators: {
                    notEmpty: {},
                    numeric: {},
                    greaterThan: { value: 0, message: 'مقدار سرانه شب کاری تشویقی از صفر کمتر نمی تواند باشد' },
                    lessThan: { value: 100, message: 'مقدار سرانه شب کاری تشویقی از 100 بیشتر نمی تواند باشد' }
                }
            },
            MaxHolidayWork: {
                validators: {
                    notEmpty: {},
                    numeric: {},
                    greaterThan: { value: 0, message: 'درصد سرانه تعطیل کاری تشویقی از صفر کمتر نمی تواند باشد' },
                    lessThan: { value: 100, message: 'درصد سرانه تعطیل کاری تشویقی از 100 بیشتر نمی تواند باشد' }
                }
            }
        };

        return new validator(validators, success, error);
    }

    return factory;
});