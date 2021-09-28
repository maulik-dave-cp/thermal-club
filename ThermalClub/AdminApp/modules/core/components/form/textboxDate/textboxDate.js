(function (angular) {
  "use strict";

  angular.module('myApp').component("textboxDate", {
    templateUrl: "/core/components/form/textboxDate/textbox-date.tpl.html",
    transclude: true,
    bindings: {
      name: '@',
      required: '@',
      label: '@?',
      service: '='
    },
    controller: ['$scope',
      function ($scope) {

        this.$onInit=function(){
          $scope.picker = {
            datepickerOptions: {
              showWeeks: false,
              startingDay: 1,
              timezone: 'America/Los_Angeles'
            }
          };
        };

        $scope.openCalendar = function() {
          $scope.picker .open = true;
        };
      }
    ]
  });

}(window.angular));
