(function (angular) {
  "use strict";

  angular.module('myApp').filter('placeholder', [function () {
    return function (text, placeholder) {
        // If we're dealing with a function, get the value
        if (angular.isFunction(text)) text = text();
        // Trim any whitespace and show placeholder if no content
        return jQuery.trim(text) || placeholder;
    };
  }]);

}(window.angular));
