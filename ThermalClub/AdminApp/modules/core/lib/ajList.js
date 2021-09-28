(function (angular) {
  "use strict";

  angular.module('myApp').factory('ajList', ["$http", "$cookies", "flash", function ($http, $cookies, flash) {

    var ajList = function () {

      // api url for list page
      this.url = null;

      // module name
      this.module = null;

      // from api result result
      this.result = {};

      // data
      this.data = {};

      // show/hide paging
      this.showPaging = true;

      this.paging = {};

      // for search filters send to api
      this.filters = {};

      // default page index
      this.filters.page = 1;

      // default page size
      this.filters.size = 10;

      this.filters.sortColumn = null;
      this.filters.sortType = null;

      // default page sizes
      this.pageSizes = [10, 25, 50, 100, 200];

       // Geenrate unique Number for selectall
      this.uniqueNumber = new Date().getTime();

      // select all checkbox. use for select other checkboxes
      this.selectAll = false;

      //callback function
      this.onAfterLoad = null;
      this.afterFireIdChange = null;

      this.idsResetAfterLoad = true;
      this.ids = [];
      this.action = '';
      this.actions = {
        "active": "edit",
        "inactive": "edit",
        "delete": "delete"
      };
    };

    ajList.prototype.load = function (clearPage) {

      if (typeof argument2 !== 'undefined' || clearPage === true)
        this.filters.page = 1;

      $http({
        url: this.url,
        method: "GET",
        params: this.filters
      }).then(function (resp) {

        var result = resp.data;

        flash.show(result.message, result.messageType);

        if (!result.success)
          return;



        this.result = result;
        this.paging = result.paging;
        this.data = result.data;
        this.filters.page = this.paging.page;
        this.filters.size = this.paging.size;

        // if second page all record delete than automatic first page record display
        if(this.filters.page > 1 && this.data.length == 0 && this.paging.total > 0)
          this.load(true);

        if (typeof this.onAfterLoad === "function")
          this.onAfterLoad();

        this.filters.action = null;
        this.filters.ids = null;

        this.action = '';
        if(this.idsResetAfterLoad)
           this.ids = [];

        this.selectAll = false;

      }.bind(this));
    };

    ajList.prototype.storeload = function (value,name,clearPage) {

      if (typeof argument2 !== 'undefined' || clearPage === true)
        this.filters.page = 1;
      
      this.filters.fromStore = value.fromVal;
      this.filters.toStore = value.toVal;
      this.filters.store = name;
      
      $http({
        url: this.url,
        method: "GET",
        params: this.filters
      }).then(function (resp) {
      
        var result = resp.data;
      
        flash.show(result.message, result.messageType);
      
        if (!result.success)
          return;           
      
        this.result = result;
        this.paging = result.paging;
        this.data = result.data;
        this.filters.page = this.paging.page;
        this.filters.size = this.paging.size;
      
        // if second page all record delete than automatic first page record display
        if(this.filters.page > 1 && this.data.length == 0 && this.paging.total > 0)
          this.load(true);
      
        if (typeof this.onAfterLoad === "function")
          this.onAfterLoad();
      
        this.filters.action = null;
        this.filters.ids = null;
      
        this.action = '';
        if(this.idsResetAfterLoad)
           this.ids = [];
      
        this.selectAll = false;
      
      }.bind(this));
    };

    ajList.prototype.sort = function ($event, name) {
      
      if (this.loading) return;

      var elmt = angular.element($event.target);
      var sortType = 'asc';

      elmt.removeClass('sorting');

      if (elmt.hasClass('sorting_asc'))
        sortType = 'desc';

      this.filters.sortColumn = name;
      this.filters.sortType = sortType;
      angular.element('.sorting_asc, .sorting_desc').removeClass('sorting_asc sorting_desc')
        .addClass('sorting');
      elmt.addClass(sortType === 'asc' ? 'sorting_asc' : 'sorting_desc')
        .removeClass(sortType === 'asc' ? 'sorting_desc' : 'sorting_asc');

      this.load(true);
    };

    ajList.prototype.previousPage = function () {
      if (this.result.paging.Page === 1) return;

      this.filters.page = this.result.paging.page - 1;

      this.load();
    };

    ajList.prototype.nextPage = function () {
      if (this.result.paging.lastPage === this.result.paging.page) return;

      this.filters.page = this.result.paging.page + 1;

      this.load();
    };

    ajList.prototype.goToPage = function (page) {
      this.filters.page = page;

      this.load();
    };

    ajList.prototype.deleteRow = function (id) {

      var $that = this;
      var ids = Array.isArray(id) ? id : [id];

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

              $http({
                url: $that.url + 'delete',
                method: "POST",
                data: {
                  ids: ids
                }
              }).then(function (resp) {

                var result = resp.data;

                flash.show(result.message, result.messageType);

                if (result.success)
                  $that.load();
              });
            }
      });
    };

    ajList.prototype.closeTicket = function (id) {
      
      var $that = this;
      var ids = Array.isArray(id) ? id : [id];

      bootbox.confirm(
      {
          title: "Confirmation required",
          message: "Do you want to close ticket?",
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

              $http({
                url: $that.url + 'close-ticket/' + id,
                method: "GET",
               
              }).then(function (resp) {

                var result = resp.data;

                flash.show(result.message, result.messageType);

                if (result.success)
                  $that.load();
              });
            }
      });
    };
    ajList.prototype.fireAction = function () {

      this.message = null;
      this.messageType = null;

      if (this.action !== '' && this.ids.length === 0) {
        flash.show('Select at least one item from action list.', 2);
        return;
      }

      var $that = this;

      if (this.action === 'delete') {

        $that.deleteRow(this.ids);

      }
      else if (this.action === 'closeTicket') {

        $that.closeTicket(this.ids);

      } else {

        $http({
          url: $that.url + this.action,
          method: "POST",
          data: {
            ids: this.ids
          }
        }).then(function (resp) {

          var result = resp.data;

          flash.show(result.message, result.messageType);

          if (result.success)
            $that.load();
        });
      }
    };

    ajList.prototype.checkAll = function () {

      var $that = this;

      if ($that.selectAll)
        $that.ids.splice(0, $that.ids.length);
      else {
        angular.forEach($that.data.map(function (item) {
            return item.id;
          }),
          function (value) {
            $that.ids.push(value);
          });
      }
    };

    ajList.prototype.fireIdChange = function () {
      if (this.ids.length !== this.result.data.length)
        this.selectAll = false;

        if (typeof this.afterFireIdChange === "function")
        this.afterFireIdChange();

    };

    /**
     *  Set sorting class on sort columns
     *
     * @param {any} model same class. If not defined then no sorting.
     * @param {any} name column name
     */
    ajList.prototype.setSortingClass = function (model, name) {

      if (model === null)
        return '';

      if (model.paging.sortColumn === name) {
        return model.paging.sortType === 'asc' ? 'sorting_asc' : 'sorting_desc';
      }

      return 'sorting';
    };

    /* Extra for amazon project */
    ajList.prototype.languageAvailable = function (languages, language) {

      var result = false;

      angular.forEach(languages,
        function (value) {
          if (value === language)
            result = true;
        });

      return result;
    };

    return ajList;
  }]);

}(window.angular));
