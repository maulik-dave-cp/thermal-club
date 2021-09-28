(function (angular) {
  "use strict";

  angular.module('myApp').component("textboxSlug", {
    templateUrl: "/core/components/form/textboxSlug/textbox-slug.tpl.html",
    bindings: {
      name: '@',
      label: '@?',
      required: '@',
      type: '@',
      maxlength: '@',
      disabledIf: '<?',
      from: '@',
      service: '='
    },
    controller: ["$scope", "Slug",
      function ($scope, Slug) {

        var self = this;

        $scope.generateSlug = function () {
          var name = self.service.dto[self.from];
          self.service.dto[self.name] = Slug.slugify(name);
        };
      }
    ]
  });

}(window.angular));
