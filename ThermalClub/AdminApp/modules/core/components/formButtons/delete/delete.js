(function (angular) {
  "use strict";

  angular.module('myApp').component("delete", {
    templateUrl: "/core/components/formButtons/delete/delete.tpl.html",
    bindings: {
      click: '&',
      permissions: '@?'
    },
    controller: ["$scope", "$attrs", "$rootScope", function ($scope, $attrs, $rootScope) {
      $scope.isAccess = $rootScope.hasAccess($attrs.permissions);
    }]
  });

}(window.angular));
