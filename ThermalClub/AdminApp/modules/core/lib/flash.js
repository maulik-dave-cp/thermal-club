(function (angular) {
    "use strict";

    var app = angular.module('myApp');

    app.filter('messageType', [function () { return function (input) { return getMessageType(input); }; }]);
    app.filter('messageTypeOnBoard', [function () { return function (input) { return getMessageTypeOnBoard(input); }; }]);
    app.filter('messageIcon', [function () { return function (input) { return getMessageIcon(input); }; }]);
    function getMessageType(messageType) { switch (messageType) { case 0: return "error"; case 1: return "success"; case 3: return "warning"; default: return 'info'; } }
    function getMessageTypeOnBoard(messageType) { switch (messageType) { case 0: return "danger"; case 1: return "success"; case 3: return "warning"; default: return 'info'; } }
    function getMessageIcon(messageType) { switch (messageType) { case 0: return "times"; case 1: return "check"; case 3: return "warning"; default: return 'info'; } }
    function getMessageColor(messageType) { switch (messageType) { case 0: return "C46A69"; case 1: return "739E73"; case 3: return "C79121"; default: return '296191'; } }
    function getMessageTitle(messageType) { switch (messageType) { case 0: return "Error"; case 1: return "Success"; case 3: return "Warning"; default: return 'Info'; } }

    toastr.options = {
      "closeButton": true,
      "debug": false,
      "newestOnTop": true,
      "progressBar": true,
      "positionClass": "toast-top-right",
      "preventDuplicates": true,
      "onclick": null,
      "showDuration": 300,
      "hideDuration": 100,
      "timeOut": 5000,
      "extendedTimeOut": 1000,
      "showEasing": "swing",
      "hideEasing": "linear",
      "showMethod": "fadeIn",
      "hideMethod": "fadeOut"
    }

    app.service('flash', function() {

        this.show = function(message, messageType, close) {
            if (message === '' || message === null || typeof message === 'undefined') return;

            messageType = messageType;
            if (messageType === '' || messageType === null || typeof messageType === 'undefined')
                messageType=1;
            //messageType = messageType || 1;
            //close = close || 5000;

            toastr[getMessageType(messageType)](message)

            // $.smallBox({
            //     title : "<strong>"+getMessageTitle(messageType)+"</strong>",
            //     content : message,
            //     color : "#" + getMessageColor(messageType),
            //     iconSmall : "fa fa-" + getMessageIcon(messageType),
            //     timeout : close !== false ? close : null
            // });
        };
    });

}(window.angular));
