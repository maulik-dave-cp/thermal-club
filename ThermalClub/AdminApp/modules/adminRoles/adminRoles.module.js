(function (angular) {
  'use strict';

  angular.module('myApp').config([
    '$stateProvider',
    function ($stateProvider) {

      $stateProvider.state('adminRoles', {
          parent: 'permission',
          url: '/admin/admin-roles',
          component: 'adminRoles',
          data: {
            permissions: ['adminRoles']
          }
        })
        .state('adminRolesCreate', {
          parent: 'permission',
          url: '/admin/admin-roles/create',
          component: 'adminRolesCreate',
          data: {
            permissions: ['adminRoles.create']
          }
        })
        .state('adminRolesEdit', {
          parent: 'permission',
          url: '/admin/admin-roles/edit/{id:int}',
          component: 'adminRolesEdit',
          data: {
            permissions: ['adminRoles.edit']
          }
        });
    }
  ]);

})(angular);
