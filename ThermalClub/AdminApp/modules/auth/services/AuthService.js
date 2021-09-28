(function (angular) {
  "use strict";

  angular.module('myApp')
    .service('authService',
      [
        '$http', function ($http) {

          var urlBase = 'api/admin/auth/';

          this.login = function (adminLoginDto) {
            return $http({
              url: urlBase + 'login',
              skipAuthorization: true,
              method: 'POST',
              data: adminLoginDto
            });
          };

          this.forgotPassword = function (adminForgotPasswordDto) {
            return $http({
              url: urlBase + 'forgot-password',
              skipAuthorization: true,
              method: 'POST',
              data: adminForgotPasswordDto
            });
          };

          this.resetPassword = function (token, adminResetPasswordDto) {
            return $http({
              url: urlBase + 'reset-password/' + token,
              skipAuthorization: true,
              method: 'POST',
              data: adminResetPasswordDto
            });
          };
        }
      ]);

}(window.angular));
