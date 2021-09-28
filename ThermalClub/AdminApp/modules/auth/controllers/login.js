(function (angular) {
  "use strict";

  angular.module('myApp').component('login',
    {
      templateUrl: '/auth/views/login.tpl.html',
      controller: [
        '$scope', '$http', 'authService', '$rootScope', 'localStorageService', '$state', '$cookies',
        function ($scope, $http, authService, $rootScope, localStorageService, $state, $cookies) {

          $scope.dto = {};
          $scope.result = {};

          if ($rootScope.result) {
            $scope.result = $rootScope.result;
            $rootScope.result = null;
          }

          $scope.submit = function () {
            authService.login($scope.dto)
              .then(function (resp) {

                $scope.result = resp.data;

                if (resp.data.success) {
                  var token = resp.data.data;

                  localStorageService.set(tokenName, token);
                  $cookies.put('auth', token);

                  var ref = localStorageService.get('ref');
                  if (ref) {
                    localStorageService.remove('ref');
                    $state.go(ref ?? 'home');
                  } else {
                    $state.go('home');
                  }
                }

              });
          };

        }
      ]
    });

}(window.angular));
