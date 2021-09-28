(function (angular) {
    "use strict";

    angular.module('myApp').directive("thSearchSelect",
    function () {
        return {
            scope: {
                name: "@",
                model: '=',
                data: '=',
                label: '@'
            },
            replace: true,
            templateUrl: "/core/components/list/search/thSearchSelect/th-search-select.tpl.html"
        };
    });

}(window.angular));
