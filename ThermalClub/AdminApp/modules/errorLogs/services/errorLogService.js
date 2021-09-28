(function (angular) {
  "use strict";

  angular.module('myApp')
    .service('errorLogService', [
      '$http',
      function ($http) {

        var urlBase = 'api/admin/error-log/';

        this.list = function (filters) {
          return $http({
            url: urlBase,
            params: filters,
            method: "GET",
          });
        };

      }
    ]);

}(window.angular));
