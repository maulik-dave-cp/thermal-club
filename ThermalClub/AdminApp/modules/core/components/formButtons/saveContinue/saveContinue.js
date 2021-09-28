(function (angular) {
    "use strict";

    angular.module('myApp').component("saveContinue",
    {
        templateUrl: "/core/components/formButtons/saveContinue/save-continue.tpl.html",
        bindings: {
          click: '&',
          permissions: '@?'
        },
        controller: ["$scope", "$attrs", "$rootScope", function ($scope, $attrs, $rootScope) {
          $scope.isAccess = $rootScope.hasAccess($attrs.permissions);
        }]
    });

}(window.angular));
