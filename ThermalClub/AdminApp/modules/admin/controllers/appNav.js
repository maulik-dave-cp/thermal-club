(function (angular) {
  "use strict";

  angular.module('myApp').component('appNav', {
    templateUrl: '/admin/views/appNav.tpl.html',
    controller: [
      '$scope', '$rootScope',
      function ($scope, $rootScope) {
        $scope.user = $rootScope.user;

        initApp.buildNavigation(myapp_config.navHooks);
        initApp.listFilter(myapp_config.navHooks, myapp_config.navFilterInput, myapp_config.navAnchor);
        initApp.domReadyMisc();
      }
    ]
  });

}(window.angular));
