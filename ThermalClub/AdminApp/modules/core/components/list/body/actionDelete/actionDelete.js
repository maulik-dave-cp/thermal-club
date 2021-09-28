(function (angular) {
    "use strict";

    angular.module('myApp').directive("actionDelete",
    function() {
        return {
            scope: {
                click: "&",
                permissions: '@?'
            },
            controller: ["$scope", "$attrs", "$rootScope", function ($scope, $attrs, $rootScope) {
              $scope.isAccess = $rootScope.hasAccess($attrs.permissions);
            }],
            replace: true,
            templateUrl: "/core/components/list/body/actionDelete/action-delete.tpl.html"
        };
    });

}(window.angular));
