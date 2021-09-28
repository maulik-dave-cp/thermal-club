(function (angular) {
  "use strict";

  angular.module('myApp').component('emailTemplates', {
    templateUrl: '/emailTemplates/views/index.tpl.html',
    controller: [
      '$scope', 'ajList',
      function ($scope, ajList) {

        $scope.list = new ajList();
        $scope.list.url = 'api/admin/email-templates/';
        $scope.list.module = 'emailTemplates';
        $scope.list.actions = null;
        $scope.list.load();
      }
    ]
  });

}(window.angular));
