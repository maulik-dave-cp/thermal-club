(function (angular) {
    "use strict";

    angular.module('myApp').component('add', {
        templateUrl: '/home/views/add.tpl.html',
        controller: ['$scope', '$state',
            function ($scope, $state) {

                $scope.aj = [];
                $scope.aj.dto = [];
                $scope.aj.dto['file'] = '';

                $scope.faqTypes = [{
                    id: 1,
                    name: "Hello"
                }, {
                    id: 2,
                    name: "Bello"
                }];
            }
        ]
    });

}(window.angular));