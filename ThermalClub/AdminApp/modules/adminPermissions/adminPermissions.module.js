(function (angular) {
  'use strict';

  angular.module('myApp').config([
    '$stateProvider',
    function ($stateProvider) {

      $stateProvider.state('adminPermissions', {
          parent: 'permission',
          url: '/admin/admin-permissions',
          component: 'adminPermissions',
          //data: {
          //  permissions: ['adminPermissions']
          //}
        })
        .state('adminPermissionsCreate', {
          parent: 'permission',
          url: '/admin/admin-permissions/create',
          component: 'adminPermissionsCreate',
          //data: {
          //  permissions: ['adminPermissions.create']
          //}
        })
        .state('adminPermissionsEdit', {
          parent: 'permission',
          url: '/admin/admin-permissions/edit/{id:int}',
          component: 'adminPermissionsEdit',
          //data: {
          //  permissions: ['adminPermissions.edit']
          //}
        })
        .state('adminPermissionsSequence', {
          parent: 'permission',
          url: '/admin/admin-permissions/sequence',
          component: 'adminPermissionsSequence',
          data: {
            permissions: ['adminPermissions']
          }
        });
    }
  ]);

})(angular);
