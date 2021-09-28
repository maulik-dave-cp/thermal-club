(function (angular) {
  'use strict';

  angular.module('myApp').config([
    '$stateProvider',
    function ($stateProvider) {

      $stateProvider.state('adminUsers', {
          parent: 'permission',
          url: '/admin/admin-users',
          component: 'adminUsers',
          data: {
            permissions: ['adminUsers']
          }
        })
        .state('adminUsersCreate', {
          parent: 'permission',
          url: '/admin/admin-users/create',
          component: 'adminUsersCreate',
          data: {
            permissions: ['adminUsers.create']
          }
        })
        .state('adminUsersEdit', {
          parent: 'permission',
          url: '/admin/admin-users/edit/{id:int}',
          component: 'adminUsersEdit',
          data: {
            permissions: ['adminUsers.edit']
          }
        });
    }
  ]);

})(angular);
