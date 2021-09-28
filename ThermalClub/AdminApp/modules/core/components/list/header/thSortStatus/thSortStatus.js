(function (angular) {
    "use strict";

    angular.module('myApp').directive("thSortStatus",
    function () {
        return {
            scope: {
                model: '='
            },
            replace: true,
            templateUrl: "/core/components/list/header/thSortStatus/th-sort-status.tpl.html"
        };
    });

}(window.angular));
