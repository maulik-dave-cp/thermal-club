(function (angular) {
  'use strict';

  angular.module('myApp').config([
    '$stateProvider',
    function ($stateProvider) {

      $stateProvider.state('auth', {
          url: '/admin/auth',
          component: 'auth',
          redirectTo: 'login',
        })
        .state('login', {
          parent: 'auth',
          url: '/login',
          component: 'login'
        })
        .state('forgotPassword', {
          parent: 'auth',
          url: '/forgot-password',
          component: 'forgotPassword'
        })
        .state('resetPassword', {
          parent: 'auth',
          url: '/reset-password/{token}',
          component: 'resetPassword'
        })
        .state('logout', {
          parent: 'auth',
          url: '/logout'
        });
    }
  ]);

})(angular);
