(function (angular) {
  "use strict";

  angular.module('myApp').component('adminRolesCreate', {
    templateUrl: '/adminRoles/views/create.tpl.html',
    controller: 'adminRolesCreateController'
  });

  angular.module('myApp').controller('adminRolesCreateController',
    [
      '$scope', 'adminRolesService', 'adminPermissionsService', 'ajCreate', 'storesService',
      function ($scope, adminRolesService, adminPermissionsService, ajCreate, storesService) {

        $scope.aj = new ajCreate();
        $scope.aj.service = adminRolesService;

        $scope.aj.listRoute = 'adminRoles';
        $scope.aj.editRoute = 'adminRolesEdit';

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
          $('.tree-fix-boxed').find('div.checkbox :checkbox').on('click',
            function () {
              var $chk = $(this),
                $li = $chk.closest('li');
              $li.find(':checkbox').prop('checked', this.checked);

              $scope.aj.dto.permissions = $('.tree-fix-boxed').find('div.checkbox :checkbox:checked').map(function () {
                return $(this).attr('id');
              }).get();
            });
        }
      }
    ]);

}(window.angular));
