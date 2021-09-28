(function (angular) {
  "use strict";

  angular.module('myApp')
    .service('adminPermissionsService',
      [
        '$http',
        function ($http) {

          var urlBase = 'api/admin/admin-permissions/';

          this.all = function () {
            return $http({
              url: urlBase + 'all',
              method: 'GET'
            });
          };

          this.list = function (filters) {
            return $http({
              url: urlBase,
              params: filters,
              method: 'GET'
            });
          };

          this.create = function (dto) {
            return $http({
              url: urlBase,
              method: 'POST',
              data: dto
            });
          };

          this.byId = function (id) {
            return $http({
              url: urlBase + id,
              method: 'GET'
            });
          };

          this.edit = function (id, dto) {
            return $http({
              url: urlBase + id,
              method: 'PUT',
              data: dto
            });
          };

          this.delete = function (ids) {
            return $http({
              url: urlBase + 'delete',
              method: 'POST',
              data: ids
            });
          };

          this.sequence = function (ids) {
            return $http({
              url: urlBase + 'sequence',
              method: 'GET'
            });
          };

          this.saveSequence = function (data) {
            return $http({
              url: urlBase + 'save-sequence',
              method: 'POST',
              data: data
            });
          };
        }
      ]);

}(window.angular));
