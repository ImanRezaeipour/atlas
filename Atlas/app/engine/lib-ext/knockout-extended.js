define([
    'knockout', 'komapping',
    'knockout-amd-helpers'
], function(ko, komapping) {

    ko.options.deferUpdates = true;


    ko.amdTemplateEngine.defaultPath = '/';
    ko.amdTemplateEngine.defaultSuffix = '.html';

    ko.components['getComponentNameForNode'] = function(node) {        
        var tagNameLower = node.tagName && node.tagName.toLowerCase(node);
        if (!node.hasAttribute('data-bind')) {
            if ((tagNameLower.indexOf('-') != -1) || (('' + node) == '[object HTMLUnknownElement]') ||
            ((ko.utils.ieVersion <= 8) && (node.tagName === tagNameLower))) {
                return tagNameLower;
            }
        }
    };

    ko.components.loaders.push({
        getConfig: function (name, callback) {            
            var path = name.replace("spa-app-", "").split('-').join("/");            
            callback({ require: 'component/' + path + '/index' });
        }
    });

    ko.observableArray.fn.find = function (prop, data) {        
        var valueToMatch = (typeof (data) == 'object') ? data[prop] : data;
        return ko.utils.arrayFirst(this(), function (item) {
            return item[prop] === valueToMatch;
        });
    };

    ko.mapping = komapping;
  return ko;
});
