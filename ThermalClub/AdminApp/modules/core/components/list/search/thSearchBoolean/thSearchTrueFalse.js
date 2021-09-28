(function (angular) {
  "use strict";

  angular.module('myApp').directive("thSearchTrueFalse",
    function () {
      return {
        scope: {
          name: "@",
          model: '=',
          label: '@'
        },
        replace: true,
        templateUrl: "/core/components/list/search/thSearchBoolean/th-search-true-false.tpl.html"
      };
    });

}(window.angular));
