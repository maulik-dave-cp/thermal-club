(function (angular) {
  "use strict";

  angular.module('myApp').component('adminUsersEdit', {
    templateUrl: '/adminUsers/views/edit.tpl.html',
    controller: [
      '$scope', 'adminUsersService', 'adminRolesService', 'ajEdit',
      function ($scope, adminUsersService, adminRolesService, ajEdit) {

        $scope.editMode = true;

        $scope.aj = new ajEdit();
        $scope.aj.service = adminUsersService;
        $scope.aj.listRoute = 'adminUsers';
        $scope.aj.load();

        adminRolesService.all().then(function (resp) {
          $scope.roles = resp.data;
        });
      }
    ]
  });

}(window.angular));
