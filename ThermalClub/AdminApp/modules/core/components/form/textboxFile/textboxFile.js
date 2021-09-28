(function (angular) {
  "use strict";

  angular.module('myApp').component("textboxFile", {
    templateUrl: "/core/components/form/textboxFile/textbox-file.tpl.html",
    transclude: true,
    bindings: {
      name: '@',
      label: '@?',
      required: '@',
      service: '='
    },
    controller: ["$scope", function ($scope) {
      var $that = this;

      $(function() {
        $("#" + $that.name).observe_field(1, function( ) {
          $that.service.dto[$that.name] = this.value;
          $scope.$apply();
        });
      });

      this.browseFiles = function () {

        console.log('hi');

        $.fancybox.open({
          src       : virtualDir + 'filemanager/dialog.php?type=0&field_id=' + $that.name,
          'width'		: 900,
          'height'	: 600,
          'type'		: 'iframe',
          'autoScale'    	: false,
          iframe: {preload: false}
        });

        // CKFinder.popup( {
        //   chooseFiles: true,
        //   width: 1024,
        //   height: 800,
        //   onInit: function( finder ) {
        //     finder.on( 'files:choose', function( evt ) {
        //       var file = evt.data.files.first();
        //       $that.service.dto[$that.name] = file.getUrl();

        //       $scope.$apply();
        //     });

        //     finder.on( 'file:choose:resizedImage', function( evt ) {

        //       $that.service.dto[$that.name] = evt.data.resizedUrl;

        //       $scope.$apply();
        //     });
        //   }
        // });
      };

      this.removeFile = function () {

        $that.service.dto[$that.name] = null;
      };
    }]
  });

}(window.angular));
