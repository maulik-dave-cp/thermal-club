(function (angular) {
    "use strict";

    angular.module('myApp').component("message",
    {
        templateUrl: "/core/components/message/message.tpl.html",
        bindings: { result: '=' },
        controller: ['$scope', function ($scope) {
            var $that = this;

            $scope.close = function () {
                $that.ngResult.message = null;
            };
        }]
    });
            
}(window.angular));