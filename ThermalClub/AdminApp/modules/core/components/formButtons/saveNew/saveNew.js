(function (angular) {
    "use strict";

    angular.module('myApp').component("saveNew",
    {
        templateUrl: "/core/components/formButtons/saveNew/save-new.tpl.html",
        bindings: { click: '&' }
    });

}(window.angular));
