(function (angular) {
  "use strict";

  angular.module('myApp').directive("thSearchYesNo",
    function () {
      return {
        scope: {
          name: "@",
          model: '=',
          label: '@'
        },
        replace: true,
        templateUrl: "/core/components/list/search/thSearchYesNo/th-search-yes-no.tpl.html"
      };
    });

}(window.angular));
