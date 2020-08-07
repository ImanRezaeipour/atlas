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
                    greaterThan: { value: 0, message: 'سرانه تعطیل کاری تشویقی از صفر کمتر نمی تواند باشد' },
                    lessThan: { value: 100, message: 'سرانه تعطیل کاری تشویقی از 100 بیشتر نمی تواند باشد' }
                }
            },
            P1: {
                validators: {
                    notEmpty: {},
                    numeric: {},
                    greaterThan: { value: 0, message: 'ناهار از صفر کمتر نمی تواند باشد' }
                    //lessThan: { value: 100, message: 'ناهار از 31 بیشتر نمی تواند باشد' }
                }
            },
            P2: {
                validators: {
                    notEmpty: {},
                    numeric: {},
                    greaterThan: { value: 0, message: 'تعطیل ناهار از صفر کمتر نمی تواند باشد' }
                    //lessThan: { value: 100, message: 'تعطیل ناهار از 31 بیشتر نمی تواند باشد' }
                }
            },
            P3: {
                validators: {
                    notEmpty: {},
                    regexp: { regexp: '([0-9]+):([0-5][0-9])', message: 'فرمت اضافه کاری صحیح نمی باشد' }//,
                    //greaterThan: { value: 0, message: 'اضافه کاری از صفر کمتر نمی تواند باشد' },
                    //lessThan: { value: 100, message: 'اضافه کاری از 100 بیشتر نمی تواند باشد' }
                }
            },
            P4: {
                validators: {
                    notEmpty: {},
                    regexp: { regexp: '([0-9]+):([0-5][0-9])', message: 'فرمت تعطیل کاری صحیح نمی باشد' }
                }
            },
            P5: {
                validators: {
                    notEmpty: {},
                    regexp: { regexp: '([0-9]+):([0-5][0-9])', message: 'فرمت شب کاری صحیح نمی باشد' }
                }
            },
            P6: {
                validators: {
                    notEmpty: {},
                    numeric: {},
                    greaterThan: { value: 0, message: 'مرخصی بی حقوق از صفر کمتر نمی تواند باشد' }
                    //lessThan: { value: 100, message: 'مرخصی بی حقوق از 31 بیشتر نمی تواند باشد' }
                }
            },
            P7: {
                validators: {
                    notEmpty: {},
                    numeric: {},
                    greaterThan: { value: 0, message: 'مرخصی استحقاقی از صفر کمتر نمی تواند باشد' }
                    //lessThan: { value: 100, message: 'مرخصی استحقاقی از 31 بیشتر نمی تواند باشد' }
                }
            },
            P8: {
                validators: {
                    notEmpty: {},
                    numeric: {},
                    greaterThan: { value: 0, message: 'مرخصی استعلاجی از صفر کمتر نمی تواند باشد' }
                    //lessThan: { value: 100, message: 'مرخصی استعلاجی از 31 بیشتر نمی تواند باشد' }
                }
            },
            P9: {
                validators: {
                    notEmpty: {},
                    numeric: {},
                    greaterThan: { value: 0, message: 'غیبت از صفر کمتر نمی تواند باشد' }
                    //lessThan: { value: 100, message: 'غیبت از 31 بیشتر نمی تواند باشد' }
                }
            },
            P10: {
                validators: {
                    notEmpty: {},
                    regexp: { regexp: '([0-9]+):([0-5][0-9])', message: 'فرمت کسر کار صحیح نمی باشد' }//,
                    //numeric: {},
                    //greaterThan: { value: 0, message: 'کسر کار از صفر کمتر نمی تواند باشد' },
                    //lessThan: { value: 100, message: 'کسر کار از 100 بیشتر نمی تواند باشد' }
                }
            },
            P11: {
                validators: {
                    notEmpty: {},
                    numeric: {},
                    greaterThan: { value: 0, message: 'کارکرد از صفر کمتر نمی تواند باشد' }
                    //lessThan: { value: 100, message: 'کارکرد از 31 بیشتر نمی تواند باشد' }
                }
            },
            P12: {
                validators: {
                    notEmpty: {},
                    regexp: { regexp: '([0-9]+):([0-5][0-9])', message: 'فرمت کارکرد ساعتی صحیح نمی باشد' }//,
                    //numeric: {},
                    //greaterThan: { value: 0, message: 'کارکرد ساعتی از صفر کمتر نمی تواند باشد' },
                    //lessThan: { value: 100, message: 'کارکرد ساعتی از 100 بیشتر نمی تواند باشد' }
                }
            }

        };

        return new validator(validators, success, error);
    }

    return factory;
});