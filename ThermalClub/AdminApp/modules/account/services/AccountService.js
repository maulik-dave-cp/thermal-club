(function (angular) {
  "use strict";

  angular.module('myApp')
    .service('accountService',
      [
        '$http', function ($http) {

          var urlBase = 'api/admin/account/';

          this.getEditProfile = function () {
            return $http({
              url: urlBase + 'edit-profile',
              method: 'GET'
            });
          };

          this.editProfile = function (dto) {
            return $http({
              url: urlBase + 'edit-profile',
              method: 'POST',
              data: dto
            });
          };

          this.changePassword = function (dto) {
            return $http({
              url: urlBase + 'change-password',
              method: 'POST',
              data: dto
            });
          };
        }
      ]);

}(window.angular));
