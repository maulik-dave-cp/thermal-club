(function (angular) {
  "use strict";

  angular.module('myApp').component('editProfile',
    {
      templateUrl: '/account/views/edit-profile.tpl.html',
      controller: 'EditProfileController'
    });

  angular.module('myApp').controller('EditProfileController',
    [
      '$scope', '$http', 'accountService', 'flash',
      function ($scope, $http, accountService, flash) {
        $scope.aj = {};
        $scope.aj.dto = {};
        $scope.aj.result = {};

        accountService.getEditProfile().then(function (resp) {
          $scope.aj.dto = resp.data;
        });

        $scope.submit = function () {
          accountService.editProfile($scope.aj.dto)
            .then(function (resp) {

              $scope.aj.result = resp.data;
              flash.show(resp.data.message, resp.data.messageType);
            });
        };
      }
    ]);

}(window.angular));
