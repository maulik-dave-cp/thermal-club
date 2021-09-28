(function (angular) {
  "use strict";

  angular.module('myApp').component("textbox", {
    templateUrl: '/core/components/form/textbox/textbox.tpl.html',
    transclude: true,
    bindings: {
      name: '@',
      label: '@?',
      required: '@?',
      type: '@',
      maxlength: '@',
      disabledIf: '<?',
      service: '=',
	  placeholder: '@',
    }
  });

}(window.angular));
