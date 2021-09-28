(function (angular) {
    "use strict";

    angular.module('myApp').component("buttonAdd",
    {
        templateUrl: "/core/components/toolbar/buttons/buttonAdd/button-add.tpl.html",
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
