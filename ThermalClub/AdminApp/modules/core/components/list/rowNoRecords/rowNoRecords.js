(function (angular) {
    "use strict";

    angular.module('myApp').directive("rowNoRecords", function () {
        return {
            scope: {
                model: '='
            },
            replace: true,
            templateUrl: "/core/components/list/rowNoRecords/row-no-records.tpl.html"
        };
    });

}(window.angular));
