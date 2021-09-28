(function (angular) {
    "use strict";

    angular.module('myApp').directive("thSortDate",
    function () {
        return {
            scope: {
                model: '=',
                name: '@'
            },
            replace: true,
            transclude: true,
            templateUrl: "/core/components/list/header/thSortDate/th-sort-date.tpl.html"
        };
    });

}(window.angular));
