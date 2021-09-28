(function (angular) {
    "use strict";

    angular.module('myApp').component("status",
    {
        templateUrl: "/core/components/form/status/status.tpl.html",
        bindings: {
          service: '=',
          activetext: '@',
          inactivetext:'@'
        }
    });

}(window.angular));
