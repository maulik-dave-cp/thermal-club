(function (angular) {
    "use strict";

    angular.module('myApp').component("buttonImport",
    {
        templateUrl: "/core/components/toolbar/buttons/buttonImport/button-import.tpl.html",
        bindings: { state: '@' }
    });

}(window.angular));
