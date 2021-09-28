(function (angular) {
    "use strict";

    angular.module('myApp').component("buttonReset",
    {
        templateUrl: "/core/components/toolbar/buttons/buttonReset/button-reset.tpl.html",
        transclude: true,
      bindings: {
        state: '@',
        label: '@'
      }
    });

}(window.angular));
