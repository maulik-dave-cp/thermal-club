(function (angular) {
    "use strict";

    angular.module('myApp').directive("cellDate",
    function () {
        return {
            scope: {
                item: "="
            },
            replace: true,
            templateUrl: "/core/components/list/body/cellDate/cell-date.tpl.html"
        };
    });

}(window.angular));
