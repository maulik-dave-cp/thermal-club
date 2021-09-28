(function (angular) {
  "use strict";

  angular.module("myApp").component("errorLog", {
    templateUrl: "/errorLogs/views/errorLog.tpl.html",
    controller: [
      '$scope', 'ajList',
      function ($scope, ajList) 
      {
        $scope.list = new ajList();
        $scope.list.url = 'api/admin/error-log/';
        $scope.list.module = 'errorLogs';
        $scope.list.actions = null;
        $scope.list.load();
      }
    ]
  });
})(window.angular);