(function (angular) {
    "use strict";

    angular.module('myApp').directive("cellCheckboxId",
    function () {
        return {
            scope: {
                model: '=',
                item: "=",
                permissions: '@?'
            },
            controller: ["$scope", "$attrs", "$rootScope", function ($scope, $attrs, $rootScope) {
              $scope.isAccess = $rootScope.hasAccess($attrs.permissions);
            }],
            replace: true,
            templateUrl: "/core/components/list/body/cellCheckboxId/cell-checkbox-id.tpl.html"
        };
    });

}(window.angular));
