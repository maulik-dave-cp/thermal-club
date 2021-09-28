(function (angular) {
  "use strict";

  angular.module('myApp').component('admin', {
    templateUrl: '/admin/views/admin.tpl.html',
    controller: [
      '$scope', '$rootScope', '$state',
      function ($scope, $rootScope, $state) {
        $scope.user = $rootScope.user;

        $scope.logout = function() {

          bootbox.confirm(
          {
              title: "<i class='fal fa fa-sign-out text-danger mr-2'></i> Logout <strong class='ml-2 color-warning-100'>" + $scope.user.name + "</strong> ?",
              message: "You can improve your security further after logging out by closing this opened browser",
              centerVertical: true,
              swapButtonOrder: true,
              buttons:
              {
                  confirm:
                  {
                      label: 'Yes',
                      className: 'btn-danger shadow-0'
                  },
                  cancel:
                  {
                      label: 'No',
                      className: 'btn-default'
                  }
              },
              className: "modal-alert",
              closeButton: false,
              callback: function(result) {
                if (!result) return;

                $state.go('logout');
              }
          });

        };
      }
    ]
  });

}(window.angular));
