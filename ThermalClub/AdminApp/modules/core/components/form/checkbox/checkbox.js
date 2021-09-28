(function (angular) {
  "use strict";

  angular.module('myApp').component("checkbox", {
    templateUrl: "/core/components/form/checkbox/checkbox.tpl.html",
    transclude: true,
    bindings: {
      name: '@',
      label: '@?',
      checkboxLabel: '@?',
      required: '@',
      service: '=',
      secondColumnSize: '@?',       
    }
  });

}(window.angular));
