(function (angular) {
  "use strict";

  angular.module('myApp').directive("thSearchStatus",
    function () {
      return {
        scope: {
          model: '='
        },
        replace: true,
        templateUrl: "/core/components/list/search/thSearchStatus/th-search-status.tpl.html"
      };
    });

}(window.angular));
