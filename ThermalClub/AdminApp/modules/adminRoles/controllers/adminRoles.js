(function (angular) {
  "use strict";

  angular.module('myApp').component('adminRoles', {
    templateUrl: '/adminRoles/views/index.tpl.html',
    controller: [
      '$scope', 'ajList',
      function ($scope, ajList) {

        $scope.list = new ajList();
        $scope.list.url = 'api/admin/admin-roles/';
        $scope.list.module = 'adminRoles';
        $scope.list.actions = {
          "delete": "adminRoles.delete"
        };
        $scope.list.load();
      }
    ]
  });

}(window.angular));
