(function (angular) {
    "use strict";

    angular.module('myApp').directive("thSearchNumber",
    function () {
        return {
            scope: {
                name: "@",
                model: '='
            },
            replace: true,
            templateUrl: "/core/components/list/search/thSearchNumber/th-search-number.tpl.html"
        };
    });

}(window.angular));
