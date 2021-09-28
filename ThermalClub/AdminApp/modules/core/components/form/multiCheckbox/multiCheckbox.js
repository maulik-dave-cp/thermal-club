(function (angular) {
  "use strict";

  angular.module('myApp').component("multiCheckbox", {
    templateUrl: "/core/components/form/multiCheckbox/multi-checkbox.tpl.html",
    transclude: true,
    bindings: {
      name: '@',
      label: '@',
      required: '@',
      data: '=',
      service: '=',
      change: '&',
      isdisable: '@?',
      permissions: '@?',
      ishide: '@?',
      humanize: '@?',
    }
  });

}(window.angular));
