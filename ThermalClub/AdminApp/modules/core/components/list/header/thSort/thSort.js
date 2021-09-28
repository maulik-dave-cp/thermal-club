(function (angular) {
    "use strict";

    angular.module('myApp').directive("thSort",
    function () {
        return {
            scope: {
                model: '=',
                name: '@',
                width: "@",
                align: '@',
                permissions: '@?'
            },
            controller: ["$scope", "$attrs", "$rootScope", function ($scope, $attrs, $rootScope) {
              $scope.isAccess = $rootScope.hasAccess($attrs.permissions);
            }],
            replace: true,
            transclude: true,
            templateUrl: "/core/components/list/header/thSort/th-sort.tpl.html"
        };
    });

}(window.angular));
