(function (angular) {
  "use strict";

  angular.module('myApp').factory('ajCreate', [
    "flash", "$state", "$window",
    function (flash, $state, $window) {

      var ajCreate = function () {

        this.listRoute = null;
        this.editRoute = null;

        this.service = null;

        this.result = {};
        this.dto = {};

        this.isSubModule = false;
      };

      ajCreate.prototype.save = function () {
        var $that = this;
        $that.service.create($that.dto)
          .then(function (resp) {

            $that.result = resp.data;
            flash.show(resp.data.message, resp.data.messageType);

            if (resp.data.success) {
              $state.go($that.listRoute);
            }

            $window.scrollTo(0, 0);
          });
      };

      ajCreate.prototype.saveContinue = function () {
        var $that = this;
        this.service.create(this.dto)
          .then(function (resp) {

            $that.result = resp.data;
            flash.show(resp.data.message, resp.data.messageType);

            if (resp.data.success) {
              if ($that.isSubModule) {
                $state.go($that.editRoute, {
                  childId: resp.data.id
                });
              } else {
                $state.go($that.editRoute, {
                  id: resp.data.id
                });
              }
            }

            $window.scrollTo(0, 0);
          });
      };

      ajCreate.prototype.saveNew = function () {
        var $that = this;
        this.service.create(this.dto)
          .then(function (resp) {

            $that.result = resp.data;
            flash.show(resp.data.message, resp.data.messageType);

            if (resp.data.success) {
              $that.dto = {};
              $that.dto.isActive = true;
              $state.reload();
            }

            $window.scrollTo(0, 0);
          });
      };

      ajCreate.prototype.saveProcess = function () {
        
        var $that = this;
        this.service.create(this.dto)
          .then(function (resp) {

            $that.result = resp.data;
            flash.show(resp.data.message, resp.data.messageType);

            if (resp.data.success) {
              $that.dto = {};
              $that.dto.isActive = true;
              $that.dto.fileText = 'Download result file';
              $that.dto.fileResultPath =  $that.result.data.fileResultPath;
              //$state.reload();
            }

            $window.scrollTo(0, 0);
          });
      };

      return ajCreate;
    }
  ]);

}(window.angular));
