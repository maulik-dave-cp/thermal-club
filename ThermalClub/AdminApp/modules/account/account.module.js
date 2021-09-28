(function (angular) {
  'use strict';

  angular.module('myApp').config([
    '$stateProvider',
    function ($stateProvider) {

      $stateProvider
        .state('myAccount',
          {
            parent: 'system',
            templateUrl: '/account/views/layout.tpl.html'
          })
        .state('editProfile',
          {
            parent: 'myAccount',
            url: '/admin/edit-profile',
            component: 'editProfile'
          })
        .state('changePassword',
          {
            parent: 'myAccount',
            url: '/admin/change-password',
            component: 'changePassword'
          });
    }]);

})(angular);
