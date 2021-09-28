(function (angular) {
  "use strict";

  angular.module('myApp').component('adminUsers', {
    templateUrl: '/adminUsers/views/index.tpl.html',
    controller: [
      '$scope', 'ajList', 'adminRolesService', '$rootScope',
      function ($scope, ajList, adminRolesService, $rootScope) {

        $scope.list = new ajList();
        $scope.list.url = 'api/admin/admin-users/';
        $scope.list.module = 'adminUsers';
        $scope.list.actions = {
          "active": "adminUsers.edit",
          "inactive": "adminUsers.edit",
          "delete": "adminUsers.delete"
        };
        $scope.list.load();

        $scope.roles = {};
        adminRolesService.all().then(function (resp) {
          $scope.roles = resp.data;
        });

        $scope.deleteAlert = function () {
          bootbox.confirm(
            {
                title: "Alert",
                message: "The User's data is associated with this application.<br>So, You can't delete it but You can always disable it.",
                buttons:
                {
                    cancel:
                    {
                        label: '<i class="fal fa-times"></i> Cancel'
                    },                    
                },
                callback: function(result)
                {
                    if (!result) return;
                }              
            });
        };
      }
    ]
  });

}(window.angular));
