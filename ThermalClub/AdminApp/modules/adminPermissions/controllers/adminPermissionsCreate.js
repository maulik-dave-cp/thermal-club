(function (angular) {
  "use strict";

  angular.module('myApp').component('adminPermissionsCreate', {
    templateUrl: '/adminPermissions/views/create.tpl.html',
    controller: 'adminPermissionsCreateController'
  });

  angular.module('myApp').controller('adminPermissionsCreateController',
    [
      '$scope', 'adminPermissionsService', 'ajCreate',
      function ($scope, adminPermissionsService, ajCreate) {

        $scope.aj = new ajCreate();
        $scope.aj.service = adminPermissionsService;

        $scope.aj.listRoute = 'adminPermission';
        $scope.aj.editRoute = 'adminPermissionEdit';
        $scope.aj.dto.isParentSelected = "false";

        adminPermissionsService.all().then(function (resp) {
          $scope.permissionsData = resp.data;
          $('#parentId').attr('disabled', true);
        });

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
