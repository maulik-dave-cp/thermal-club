(function (angular) {
  "use strict";

  angular.module('myApp').component('changePassword',
    {
      templateUrl: '/account/views/change-password.tpl.html',
      controller: 'ChangePasswordController'
    });

  angular.module('myApp').controller('ChangePasswordController',
    [
      '$scope', '$http', 'accountService', 'flash',
      function ($scope, $http, accountService, flash) {

        $scope.aj = {};
        $scope.aj.dto = {};
        $scope.aj.result = {};

        $scope.submit = function () {
          accountService.changePassword($scope.aj.dto)
            .then(function (resp) {

              $scope.aj.result = resp.data;
              flash.show(resp.data.message, resp.data.messageType);

              if (resp.data.success) {
                $scope.aj.dto = {};
              }
            });
        };
      }
    ]);

}(window.angular));
