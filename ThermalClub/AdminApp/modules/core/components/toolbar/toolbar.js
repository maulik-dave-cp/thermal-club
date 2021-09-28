(function (angular) {
  "use strict";

  angular.module('myApp').directive("toolbar",
    function () {
      return {
        scope: {
          model: '=',
          //transclude: true,
          paging: '@?',
          permissions: '@?'
        },
        transclude: true,
        templateUrl: "/core/components/toolbar/toolbar.tpl.html",
        controller: ["$scope", "$attrs", "$rootScope", function ($scope, $attrs, $rootScope) {
          $scope.isAccess = $rootScope.hasAccess($attrs.permissions);

          $scope.hasAccess = function (permissions) {
            return $rootScope.hasAccess(permissions);
          };
        }]
      };
    });

}(window.angular));
