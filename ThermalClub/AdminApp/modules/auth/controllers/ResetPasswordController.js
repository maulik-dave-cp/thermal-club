(function (angular) {
    "use strict";

    angular.module('myApp').component('resetPassword',
        {
            templateUrl: '/auth/views/reset-password.tpl.html',
            controller: 'ResetPasswordController'
        });

    angular.module('myApp').controller('ResetPasswordController',
        [
          '$scope', '$http', 'authService', '$stateParams', '$state', '$rootScope',
          function ($scope, $http, authService, $stateParams, $state, $rootScope) {

                if ($stateParams.token === undefined || $stateParams.token === '') {
                    $state.go("login");
                }

                $scope.dto = {};
                $scope.result = {};

                $scope.submit = function () {
                    $scope.result = {};

                    authService.resetPassword($stateParams.token, $scope.dto)
                        .then(function (resp) {

                            if (resp.data.success) {
                                $rootScope.result = resp.data;
                                $state.go("login");
                            }
                            $scope.result = resp.data;

                        });
                };
                setTimeout(function () {
                    funInputPlaceholder();
                }, 20);
            }
        ]);

}(window.angular));
