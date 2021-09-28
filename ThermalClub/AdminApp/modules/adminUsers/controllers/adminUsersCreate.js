(function (angular) {
  "use strict";

  angular.module('myApp').component('adminUsersCreate', {
    templateUrl: '/adminUsers/views/create.tpl.html',
    controller: [
      '$scope', 'adminUsersService', 'adminRolesService', 'ajCreate',
      function ($scope, adminUsersService, adminRolesService, ajCreate) {

        $scope.aj = new ajCreate();
        $scope.aj.service = adminUsersService;

        $scope.aj.listRoute = 'adminUsers';
        $scope.aj.editRoute = 'adminUsersEdit';

        adminRolesService.all().then(function (resp) {
          $scope.roles = resp.data;
        });
      }
    ]
  });

}(window.angular));
