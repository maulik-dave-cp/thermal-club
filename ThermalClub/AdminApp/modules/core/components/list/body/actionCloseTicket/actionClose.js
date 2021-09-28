(function (angular) {
    "use strict";

    angular.module('myApp').directive("actionClose",
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
            templateUrl: "/core/components/list/body/actionCloseTicket/action-closeTicket.tpl.html"
        };
    });

}(window.angular));
