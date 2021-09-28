(function (angular) {
    "use strict";

    angular.module('myApp').component("buttonSequence",
    {
        templateUrl: "/core/components/toolbar/buttons/buttonSequence/button-sequence.tpl.html",
        transclude: true,
        bindings: 
        { 
            state: '@', 
            permissions: '@?',
            label: '@'
        },
        controller: ["$scope", "$attrs", "$rootScope", function ($scope, $attrs, $rootScope) {
        $scope.isAccess = $rootScope.hasAccess($attrs.permissions);
        }]
    });

}(window.angular));
