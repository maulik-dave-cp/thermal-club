(function (angular) {
  "use strict";

  angular.module('myApp').directive("thSearchDate",
    function () {
      return {
        scope: {
          name: "@",
          model: '='
        },
        replace: true,
        templateUrl: "/core/components/list/search/thSearchDate/th-search-date.tpl.html",
        link: function () {
          setTimeout(function () {
            $('.datepicker').each(function () {

              var $that = $(this),
                dataDateFormat = $that.attr("data-dateformat") || "dd.mm.yy";

              $that.datepicker({
                dateFormat: dataDateFormat,
                prevText: '<i class="fa fa-chevron-left"></i>',
                nextText: '<i class="fa fa-chevron-right"></i>',
                onSelect: function (d, i) {
                  if (d !== i.lastVal) {
                    $(this).change();
                  }
                }
              });
            });
          });
        }
      };
    });

}(window.angular));
