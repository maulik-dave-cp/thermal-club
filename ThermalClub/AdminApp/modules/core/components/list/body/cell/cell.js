(function (angular) {
    "use strict";

    angular.module('myApp').directive("cell",
    function () {
        return {
            scope: {
                model: '=',
                item: "=",
                gravatar: '=',
                align: '@',
                permissions: '@?'
            },
            controller: ["$scope", "$attrs", "$rootScope", function ($scope, $attrs, $rootScope) {
              $scope.isAccess = $rootScope.hasAccess($attrs.permissions);
            }],
            replace: true,
            transclude: true,
            templateUrl: "/core/components/list/body/cell/cell.tpl.html"
        };
    });

}(window.angular));
