(function (angular) {
    "use strict";

    angular.module('myApp').component("buttonBack",
    {
        templateUrl: "/core/components/toolbar/buttons/buttonBack/button-back.tpl.html",
        transclude: true,
      bindings: {
        state: '@',
        label: '@'
      }
    });

}(window.angular));
