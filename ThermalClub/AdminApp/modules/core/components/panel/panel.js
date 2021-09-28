(function (angular) {
    "use strict";

    angular.module('myApp').component("panel",
    {
        templateUrl: "/core/components/panel/panel.tpl.html",
        transclude: true
    });

}(window.angular));
