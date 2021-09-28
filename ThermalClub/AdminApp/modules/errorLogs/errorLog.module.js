(function (angular) {
  'use strict';

  angular.module('myApp').config([
    '$stateProvider',
    function ($stateProvider) {

      $stateProvider.state('errorLog', {
        parent: 'system',
        url: '/error-log',
        component: 'errorLog',
        data: {
          permissions: ['errorLog']
        }
      });
    }
  ]);

})(angular);