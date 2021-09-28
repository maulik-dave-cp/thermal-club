(function (angular) {
  "use strict";

  angular.module('myApp').factory('ajEdit', [
    "flash", "$state", "$window",
    function (flash, $state, $window) {

      var ajEdit = function () {

        this.listRoute = null;
        this.editRoute = null;

        this.service = null;

        this.id = $state.params.id;

        this.result = {};
        this.dto = {};

        //callback function
        this.onAfterLoad = null;

      };

      ajEdit.prototype.load = function () {
        var $that = this;
        this.service.byId(this.id).then(function (resp) {
          $that.dto = resp.data;
          if (typeof $that.onAfterLoad === "function")
            $that.onAfterLoad();

        }, function (error) {
          if (error.status === 404) {
            $state.go('404');
          }
        });
      };

      ajEdit.prototype.save = function () {
        var $that = this;
        this.service.edit(this.id, this.dto)
          .then(function (resp) {

            $that.result = resp.data;
            flash.show(resp.data.message, resp.data.messageType);

            if (resp.data.success) {
              $state.go($that.listRoute);
            }

            $window.scrollTo(0, 0);
          });
      };

      ajEdit.prototype.saveContinue = function () {
        var $that = this;
        this.service.edit(this.id, this.dto)
          .then(function (resp) {

            $that.result = resp.data;
            flash.show(resp.data.message, resp.data.messageType);

            if (typeof $that.onAfterLoad === "function")
              $that.onAfterLoad();

            $window.scrollTo(0, 0);
          });
      };

      ajEdit.prototype.deleteExecute = function () {
        var $that = this;
        this.service.delete([this.id])
          .then(function (resp) {

            $that.result = resp.data;
            flash.show(resp.data.message, resp.data.messageType);

            if (resp.data.success) {
              $state.go($that.listRoute);
            }

            $window.scrollTo(0, 0);
          });
      };

      ajEdit.prototype.delete = function () {

        bootbox.confirm(
        {
            title: "Confirmation required",
            message: "Do you want to delete record?",
            buttons:
            {
                cancel:
                {
                    label: '<i class="fal fa-times"></i> Cancel'
                },
                confirm:
                {
                    label: '<i class="far fa-trash-alt"></i> Confirm',
                    className: 'btn-danger'
                }
            },
            callback: function(result)
            {
                if (!result) return;
                this.deleteExecute
              }
        });
      };

      return ajEdit;
    }
  ]);

}(window.angular));
