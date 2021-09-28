(function (angular) {
  'use strict';

  angular.module('myApp').config([
    '$stateProvider',
    function ($stateProvider) {

      $stateProvider.state('emailTemplates', {
        parent: 'system',
        url: '/system/email-templates',
        component: 'emailTemplates',
        data: {
          permissions: ['emailTemplates']
        }
      })
      .state('emailTemplatesCreate', {
        parent: 'system',
        url: '/system/email-templates/create',
        component: 'emailTemplatesCreate',
        data: {
          permissions: ['emailTemplates.create']
        }
      })
      .state('emailTemplatesEdit', {
        parent: 'system',
        url: '/system/email-templates/edit/{id:int}',
        component: 'emailTemplatesEdit',
        data: {
          permissions: ['emailTemplates.edit']
        }
      });
    }
  ]);

})(angular);
