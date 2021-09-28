(function (angular) {
  'use strict';

  angular.module('myApp').config([
    '$stateProvider',
    function ($stateProvider) {

      $stateProvider.state('home', {
        parent: 'admin',
        url: '/home',
        component: 'home',
        // data: {
        //   permissions: ['dashboard']
        // }
      });

      $stateProvider.state('add', {
        parent: 'admin',
        url: '/add',
        component: 'add',
        // data: {
        //   permissions: ['dashboard']
        // }
      });
    }
  ]);

})(angular);
