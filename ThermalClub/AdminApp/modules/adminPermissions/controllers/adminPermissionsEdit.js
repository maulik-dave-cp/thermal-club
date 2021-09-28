(function (angular) {
  "use strict";

  angular.module('myApp').component('adminPermissionsEdit', {
    templateUrl: '/adminPermissions/views/edit.tpl.html',
    controller: 'adminPermissionsEditController'
  });

  angular.module('myApp').controller('adminPermissionsEditController',
    [
      '$scope', 'adminPermissionsService', 'ajEdit', '$timeout',
      function ($scope, adminPermissionsService, ajEdit, $timeout) {

        $scope.aj = new ajEdit();
        $scope.aj.service = adminPermissionsService;
        $scope.aj.listRoute = 'adminPermission';
        $scope.aj.load();

        adminPermissionsService.all().then(function (resp) {
          $scope.permissionsData = resp.data;
        });

        $scope.aj.onAfterLoad = function () {
          if ($scope.aj.dto.parentId != null && $scope.aj.dto.parentId != undefined) {
            $('#parentSelect').attr('checked', 'checked');
            $('#parentId').attr('disabled', false);
            $scope.aj.dto.isParentSelected = "true";
          } else {
            $('#parentId').attr('disabled', true);
            $scope.aj.dto.isParentSelected = "false";
          }
        };

        $scope.changeParentSelected = function (isParentSelected) {
          if (isParentSelected == "true") {
            $scope.aj.dto.parentId = null;
            $('#parentId').removeAttr('disabled');
          } else {
            $scope.aj.dto.parentId = null;
            $('#parentId').attr('disabled', true);
          }
        };
      }
    ]);

}(window.angular));
