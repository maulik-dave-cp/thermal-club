(function (angular) {
    "use strict";

    angular.module('myApp').component("grid",
    {
        templateUrl: "/core/components/grid/grid.tpl.html",
        transclude: true
    });

}(window.angular));
