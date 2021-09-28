(function (angular) {
  "use strict";

  angular.module('myApp').component("textboxImage", {
    templateUrl: "/core/components/form/textboxImage/textbox-image.tpl.html",
    transclude: true,
    bindings: {
      name: '@',
      required: '@',
      service: '=',
      noteWidth: '@?',
      noteHeight: '@?',
      label: '@?'
    },
    controller: ["$scope", function ($scope) {
      var $that = this;

      $(function () {
        $("#" + $that.name).observe_field(1, function () {
          $that.service.dto[$that.name] = this.value;
          $scope.$apply();
        });
      });

      this.browseFiles = function () {

        $.fancybox.open({
          src: virtualDir + 'filemanager/dialog.php?type=0&field_id=' + $that.name,
          'width'		: 900,
          'height'	: 600,
          'type'		: 'iframe',
          'autoScale'    	: false,
          iframe: {preload: false}
        });

        // var finderImage;
        // if (window.CKFinder)
        //     finderImage = new CKFinder();

        // finderImage.selectActionFunction = function (fileUrl) {

        //     var fileUrlWithoutVirtualDir = fileUrl.replace(virtualDir, "");
        //     $that.service.dto[$that.name] = fileUrlWithoutVirtualDir;
        //     $scope.$apply();
        // };
        // finderImage.popup();
      };

      this.removeFile = function () {

        $that.service.dto[$that.name] = null;
      };
    }]
  });

}(window.angular));
