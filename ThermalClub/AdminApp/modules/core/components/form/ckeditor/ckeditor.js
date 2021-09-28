(function (angular) {
  "use strict";

  angular.module('myApp').component("ckeditor", {
    templateUrl: "/core/components/form/ckeditor/ckeditor.tpl.html",
    transclude: true,
    bindings: {
      name: '@',
      label: '@?',
      required: '@',
      options: '=',
      service: '='
    }
  });

}(window.angular));
