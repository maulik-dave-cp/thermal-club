(function (angular) {
  "use strict";

  angular.module('myApp').filter('humanize', function () {
    return function (text) {
      if (text) {
        text = text.split(/(?=[A-Z])/);

        // go through each word in the text and capitalize the first letter
        for (var i in text) {
          var word = text[i];
          word = word.toLowerCase();
          word = word.charAt(0).toUpperCase() + word.slice(1);
          text[i] = word;
        }

        return text.join(" ");
      }
    };
  });

}(window.angular));
