(function (angular) {
    "use strict";

    angular.module('myApp').directive("actionEdit",
    function() {
        return {
            scope: {
                state: "@",
                permissions: '@?'
            },
            controller: ["$scope", "$attrs", "$rootScope", function ($scope, $attrs, $rootScope) {
              $scope.isAccess = $rootScope.hasAccess($attrs.permissions);
            }],
            replace: true,
            templateUrl: "/core/components/list/body/actionEdit/action-edit.tpl.html"
        };
    });

}(window.angular));
