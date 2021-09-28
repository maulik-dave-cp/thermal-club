(function (angular) {
    "use strict";

    angular.module('myApp').directive("thSearchDecimal",
    function () {
        return {
            scope: {
                name: "@",
                model: '='
            },
            replace: true,
          templateUrl: "/core/components/list/search/thSearchDecimal/th-search-decimal.tpl.html"
        };
    });

}(window.angular));
