(function (angular) {
    "use strict";

    angular.module('myApp').component("formActions",
    {
        templateUrl: "/core/components/formActions/form-actions.tpl.html",
        transclude: true
    });
            
}(window.angular));