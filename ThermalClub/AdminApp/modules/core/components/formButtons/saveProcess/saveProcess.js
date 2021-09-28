(function (angular) {
    "use strict";

    angular.module('myApp').component("saveProcess",
    {
        templateUrl: "/core/components/formButtons/saveProcess/save-process.tpl.html",
        bindings: { click: '&' }
    });

}(window.angular));
