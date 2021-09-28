(function (angular) {
    "use strict";

    angular.module('myApp').component("layoutHeader",
    {
        templateUrl: "/core/components/layoutHeader/layout-header.tpl.html",
        transclude: true,
        bindings: { icon: '@', breadcrumb: '=' }
    });
            
}(window.angular));