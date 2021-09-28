(function (angular) {
  "use strict";

  angular.module('myApp').component('emailTemplatesEdit', {
    templateUrl: '/emailTemplates/views/edit.tpl.html',
    controller: [
      '$scope', 'emailTemplatesService', 'ajEdit',
      function ($scope, emailTemplatesService, ajEdit) {

        $scope.aj = new ajEdit();
        $scope.aj.service = emailTemplatesService;
        $scope.aj.listRoute = 'emailTemplates';
        $scope.aj.load();

        $scope.deleteRow = function (array, index) {
          array.splice(index, 1);
        };
        $scope.addToEmail = function () {
            $scope.aj.dto.toEmails.push({email: ''});
        };
        $scope.addCcEmail = function () {
          $scope.aj.dto.ccEmails.push({email: ''});
        };
        $scope.addBccEmail = function () {
            $scope.aj.dto.bccEmails.push({email: ''});
        };

        $scope.sortableOptions = {
            handle: '> .drag'
        };
      }
    ]
  });

}(window.angular));
