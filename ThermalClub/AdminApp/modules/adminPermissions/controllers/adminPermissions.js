(function (angular) {
  "use strict";

  angular.module('myApp').component('adminPermissions', {
    templateUrl: '/adminPermissions/views/index.tpl.html',
    controller: 'AdminPermissionsController'
  });

  angular.module('myApp').controller('AdminPermissionsController',
    [
      '$scope', 'ajList',
      function ($scope, ajList) {

        $scope.list = new ajList();
        $scope.list.url = 'api/admin/admin-permissions/';
        $scope.list.module = 'adminPermissions';
        $scope.list.actions = {
          "delete": "adminPermissions.delete"
        };
        $scope.list.load();
      }
    ]);

}(window.angular));
