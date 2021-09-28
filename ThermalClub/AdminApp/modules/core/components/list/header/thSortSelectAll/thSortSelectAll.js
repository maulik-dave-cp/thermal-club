(function (angular) {
    "use strict";

    angular.module('myApp').directive("thSortSelectAll",
    function () {
        return {
            scope: {
                model: '=',
                permissions: '@?'
            },
            controller: ["$scope", "$attrs", "$rootScope", function ($scope, $attrs, $rootScope) {
              $scope.isAccess = $rootScope.hasAccess($attrs.permissions);
            }],
            replace: true,
            templateUrl: "/core/components/list/header/thSortSelectAll/th-sort-select-all.tpl.html"
        };
    });

}(window.angular));
