(function (angular) {
  "use strict";

  angular.module('myApp').component("selectBox", {
    templateUrl: "/core/components/form/selectBox/select-box.tpl.html",
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
