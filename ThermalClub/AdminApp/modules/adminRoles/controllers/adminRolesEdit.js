(function (angular) {
  "use strict";

  angular.module('myApp').component('adminRolesEdit', {
    templateUrl: '/adminRoles/views/edit.tpl.html',
    controller: 'adminRolesEditController'
  });

  angular.module('myApp').controller('adminRolesEditController',
    [
      '$scope', 'adminRolesService', 'adminPermissionsService', 'ajEdit', '$filter', 'storesService',
      function ($scope, adminRolesService, adminPermissionsService, ajEdit, $filter, storesService) {

        $scope.editMode = true;

        $scope.aj = new ajEdit();
        $scope.aj.service = adminRolesService;
        $scope.aj.listRoute = 'adminRoles';
        $scope.aj.load();

        adminPermissionsService.all().then(function (resp) {
          $scope.permissions = unflatten(resp.data);
          setTimeout(function () {
            checkUncheckRolePermission();
          },
            400);
        });

        storesService.all().then(function (resp) {
          $scope.stores = resp.data;
        });

        $scope.collapseAll = function () {
          $scope.$broadcast('angular-ui-tree:collapse-all');
        };

        $scope.expandAll = function () {
          $scope.$broadcast('angular-ui-tree:expand-all');
        };

        function checkUncheckRolePermission() {
          $('div.checkbox :checkbox').on('click',
            function () {
              var $chk = $(this),
                $li = $chk.closest('li');
              $li.find(':checkbox').prop('checked', this.checked);

              $scope.aj.dto.permissions = $('div.checkbox :checkbox:checked').map(function () {
                return $(this).attr('id');
              }).get();
            });
        }
      }
    ]);

}(window.angular));
