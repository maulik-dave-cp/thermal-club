(function (angular) {
    "use strict";

    angular.module('myApp').directive("thStoreSearch",
    function () {
        return {
            scope: {
                name: "@",
                label: "@?",
                model: '=',
                permissions: '@?'
            },
            controller: ["$scope", "$attrs", "$rootScope", function ($scope, $attrs, $rootScope) {
              $scope.isAccess = $rootScope.hasAccess($attrs.permissions);
            }],
            replace: true,
            templateUrl: "/core/components/list/search/thStoreSearch/th-store-search.tpl.html"
        };
    });

}(window.angular));
