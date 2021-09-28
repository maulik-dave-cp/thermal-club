(function (angular) {
    "use strict";

    angular.module('myApp').component("save",
    {
        templateUrl: "/core/components/formButtons/save/save.tpl.html",
        bindings: { click: '&' }
    });

}(window.angular));
