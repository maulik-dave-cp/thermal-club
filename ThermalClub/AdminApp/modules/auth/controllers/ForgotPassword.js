(function (angular) {
    "use strict";

    angular.module('myApp').component('forgotPassword',
        {
            templateUrl: '/auth/views/forgot-password.tpl.html',
            controller: ['$scope', '$http', 'authService',
            function ($scope, $http, authService) {

                    $scope.dto = {};
                    $scope.result = {};

                    $scope.submit = function () {
                        authService.forgotPassword($scope.dto)
                            .then(function (resp) {

                                $scope.result = resp.data;
                                if (resp.data.success) {
                                    $scope.dto = {};
                                }

                            });
                    };
                }
            ]
        });

}(window.angular));
