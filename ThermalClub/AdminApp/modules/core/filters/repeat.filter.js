(function (angular) {
  "use strict";

  angular.module('myApp').filter('repeat', function () {
    return function (text, times) {
      if (text) {
        return text.repeat(times);
      }
    };
  });

}(window.angular));
