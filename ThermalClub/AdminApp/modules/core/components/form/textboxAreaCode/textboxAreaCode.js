(function (angular) {
  "use strict";

  angular.module('myApp').component("textboxAreaCode", {
    templateUrl: "/core/components/form/textboxAreaCode/textbox-area-code.tpl.html",
    transclude: true,
    bindings: {
      name: '@',
      label: '@?',
      required: '@',
      service: '='
    },
    controller: ["$scope",
      function ($scope) {

        $scope.editorOptions = {
          lineNumbers: true,
          mode: "text/html"
        };
      }
    ]
  });
}(window.angular));
