(function (angular) {
  "use strict";

  angular.module('myApp').directive("mainMenu", function () {
    return {
      templateUrl: '/home/views/menu.tpl.html',
      controller: ["$scope", "$attrs", "$rootScope",
        function ($scope, $attrs, $rootScope) {
          // $rootScope.$watch('permissions', function (newValue, oldValue, scope) {
          //   $scope.hideMenu = $rootScope.permissions === null;
          // });
        }]
    };
  });

}(window.angular));
