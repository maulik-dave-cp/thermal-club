(function (angular) {
  "use strict";

  angular.module('myApp').component("textboxNumeric", {
    templateUrl: '/core/components/form/textboxNumeric/textbox-numeric.tpl.html',
    transclude: true,
    bindings: {
      name: '@',
      label: '@?',
      required: '@?',
      type: '@',
      maxlength: '@',
      disabledIf: '<?',
      service: '='
    }
  });

}(window.angular));
