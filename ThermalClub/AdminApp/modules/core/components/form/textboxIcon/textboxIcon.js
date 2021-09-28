(function (angular) {
  "use strict";

  angular.module('myApp').component("textboxIcon", {
    templateUrl: '/core/components/form/textboxIcon/textbox-icon.tpl.html',
    transclude: true,
    bindings: {
      name: '@',
      label: '@?',
      required: '@?',
      type: '@',
      maxlength: '@',
      disabledIf: '<?',
      icon: '@',
      service: '='
    }
  });

}(window.angular));
