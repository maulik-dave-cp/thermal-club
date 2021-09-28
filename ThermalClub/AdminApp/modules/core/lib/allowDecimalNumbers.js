(function (angular) {
    "use strict";

    var app = angular.module('myApp');

    app.directive('allowDecimalNumbers', function () {
        return {
          restrict: 'A',
          link: function (scope, elm, attrs, ctrl) {
            elm.on('keydown',
              function (event) {

                var $input = $(this);
                var value = $input.val();
                value = value.replace(/[^0-9\.-]/g, '');
                $input.val(value);
      
                if (event.which == 64 || event.which == 16) {
                  // numbers  
                  return false;
                }
                if ([8, 9, 13, 27, 37, 38, 39, 40].indexOf(event.which) > -1) {
                  // backspace, Tab,enter, escape, arrows  
                  return true;
                } else if (event.which >= 48 && event.which <= 57) {
                  // numbers  
                  return true;
                } else if (event.which >= 96 && event.which <= 105) {
                  // numpad number  
                  return true;
                }
                else if ([46, 110, 190, 109, 189].indexOf(event.which) > -1) {
                 // dot and numpad dot - 109,189 for - 
                 return true;
                }
                else {
                  event.preventDefault();
                  return false;
                }
              });
          }
        };
      });


      app.directive('allowNumbersOnly', function () {
        return {
          require: 'ngModel',
          link: function (scope, element, attr, ngModelCtrl) {
            function fromUser(text) {
              if (text) {
                var transformedInput = text.replace(/[^0-9]/g, '');
      
                if (transformedInput !== text) {
                  ngModelCtrl.$setViewValue(transformedInput);
                  ngModelCtrl.$render();
                }
                return transformedInput;
              }
              return undefined;
            }
            ngModelCtrl.$parsers.push(fromUser);
          }
        };
      });

}(window.angular));
