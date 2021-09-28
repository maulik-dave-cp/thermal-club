(function (angular) {
  "use strict";

  angular.module('myApp').component("controlBase", {
    templateUrl: '/core/components/form/controlBase/control-base.tpl.html',
    transclude: {
      'note': '?controlBaseNote'
    },
    bindToController: true,
    bindings: {
      name: '@',
      label: '@?',
      required: '@',
      service: '=',
      secondColumnSize: '@?',
      permissions: '@?'
    },
    controller: ["$scope", "$attrs", "$rootScope", function ($scope, $attrs, $rootScope) {
      $scope.isAccess = $rootScope.hasAccess($attrs.permissions);
    }]
  });

}(window.angular));
