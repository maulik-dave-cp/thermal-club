(function (angular) {
  "use strict";

  angular.module('myApp').component("textboxDecimal", {
    templateUrl: '/core/components/form/textboxDecimal/textbox-decimal.tpl.html',
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
