(function (angular) {
  "use strict";

  angular.module('myApp').component("textboxArea", {
    templateUrl: '/core/components/form/textboxArea/textbox-area.tpl.html',
    transclude: true,
    bindings: {
      name: '@',
      label: '@?',
      required: '@',
      maxlength: '@',
      service: '='
    },
    controller: function () {

      angular.element(document).ready(function() {
        $('[data-maxlength]').maxlength({
          alwaysShow: true,
          warningClass: "label label-info",
          limitReachedClass: "label label-danger",
          separator: ' of ',
          preText: 'You have ',
          postText: ' chars remaining.',
          validate: true
        });
      });

    }
  });

}(window.angular));
