(function (angular) {
    "use strict";

    angular.module('myApp').directive("cellStatus",
    function () {
        return {
            scope: {
                item: "=",
                hide: "="
            },
            replace: true,
            templateUrl: "/core/components/list/body/cellStatus/cell-status.tpl.html"
        };
    });

}(window.angular));
