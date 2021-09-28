(function (angular) {
  "use strict";

  angular.module('myApp').directive("thSortAction",
    function () {
      return {
        scope: {
          permissions: '@?'
        },
        replace: true,
        templateUrl: "/core/components/list/header/thSortAction/th-sort-action.tpl.html",
        controller: ["$scope", "$attrs", "$rootScope", function ($scope, $attrs, $rootScope) {
          $scope.isAccess = $rootScope.hasAccess($attrs.permissions);
        }]
      };
    });

}(window.angular));
