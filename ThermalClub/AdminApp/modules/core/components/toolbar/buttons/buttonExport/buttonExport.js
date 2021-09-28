(function (angular) {
    "use strict";

    angular.module('myApp').component("buttonExport",
    {
        templateUrl: "/core/components/toolbar/buttons/buttonExport/button-export.tpl.html",
        transclude: true,
        bindings: {
          state: '@',
          permissions: '@?'
        },
        controller: ["$scope", "$attrs", "$rootScope", function ($scope, $attrs, $rootScope) {
          $scope.isAccess = $rootScope.hasAccess($attrs.permissions);
        }]
    });

}(window.angular));
