var localStoragePrefix = 'aj/admin';
var tokenName = 'jwta';

(function (angular) {
  'use strict';

  var app = angular.module("myApp", [
    "ui.bootstrap",
    "ui.router",
    "ngCookies",
    "ngSanitize",
    "angular-jwt",
    "ui.gravatar",
    "checklist-model",
    "LocalStorageModule",
    "ui.select",
    "ui.sortable",
    "slugifier",
    "monospaced.elastic",
    "ui.codemirror",
    "ui.mask",
    "ngDialog",
    "ngFileSaver",
    "ckeditor",
    "ui.bootstrap.datetimepicker",
    "bw.paging",
    "ui.tree",
    "ng-nestable",
    "daterangepicker",
    "xeditable"
  ]);

  app.config([
    '$urlRouterProvider',
    "$locationProvider",
    "jwtOptionsProvider",
    "$httpProvider",
    "localStorageServiceProvider",
    "$compileProvider",
    '$stateProvider',
    function ($urlRouterProvider,
      $locationProvider,
      jwtOptionsProvider,
      $httpProvider,
      localStorageServiceProvider,
      $compileProvider,
      $stateProvider) {

      // unsafe
      $compileProvider.aHrefSanitizationWhitelist(/^\s*(https?|ftp|mailto|file|javascript):/);

      /**
       *  Set prefix for local storage data
       */
      localStorageServiceProvider.setPrefix(localStoragePrefix);

      /**
       * Added authentication barrier token for all request
       * - Also skip sending auth token for html templates
       */
      jwtOptionsProvider.config({
        whiteListedDomains: ['oq2js15kxl.execute-api.us-east-1.amazonaws.com'],
        tokenGetter: function (options) {
          // Skip authentication for any requests ending in .html
          if (options.url.substr(options.url.length - 5) === '.html') {
            return null;
          }

          if (window.localStorage[localStoragePrefix + '.' + tokenName])
            return window.localStorage[localStoragePrefix + '.' + tokenName].slice(1, -1);

          return "";
        }
      });
      $httpProvider.interceptors.push('jwtInterceptor');

      $httpProvider.interceptors.push([
        "$q", "$state", "localStorageService", "$rootScope", function ($q, $state, localStorageService, $rootScope) {
          return {
            'responseError': function (rejection) {

              if (rejection.status === 400 || rejection.status === 401 || rejection.status === 419) {
                localStorageService.remove(tokenName);
                $rootScope.loggedIn = false;
                $rootScope.auth = null;
                $state.go("login");

                return rejection;
              }

              return $q.reject(rejection);
            }
          };
        }
      ]);

      $urlRouterProvider.otherwise('/admin');

      $stateProvider.state('admin', {
        url: '/admin',
        component: 'admin',
        redirectTo: 'home',
        resolve: {
          greeting: ['$rootScope', '$http', '$q', '$timeout',
            function ($rootScope, $http, $q, $timeout) {
              var deferred = $q.defer();
              $timeout(function () {
                //if ($rootScope.permissions == null) {
                  $http.get('api/admin/me/permissions').then(function (resp) {
                    $rootScope.permissions = JSON.stringify(resp.data);

                    // // if ($rootScope.stores == null) {
                    // //   $http.get('api/admin/me/stores').then(function (resp) {
                    // //     $rootScope.stores = JSON.stringify(resp.data);
                    // //     deferred.resolve(resp.data);
                    // //   });
                    // // } else {
                    // //   deferred.resolve(resp.data);
                    // // }

                  });
                //} else if ($rootScope.stores == null) {
                  // // $http.get('api/admin/me/stores').then(function (resp) {
                  // //   $rootScope.stores = JSON.stringify(resp.data);
                  // //   deferred.resolve(resp.data);
                  // // });
                // } else {
                //   deferred.resolve($rootScope.permissions);
                // }
              }, 200);
              return deferred.promise;
            }]
        }
      });
      
      $stateProvider.state('system', { parent: 'admin' });
    //   $stateProvider.state('contents', { parent: 'admin' });
    //   $stateProvider.state('achOperations', { parent: 'admin' });
    //   $stateProvider.state('transactions', { parent: 'admin' });
    //   $stateProvider.state('thermalAchOperations', { parent: 'admin' });
    //   $stateProvider.state('tenant', { parent: 'admin' });
    //   $stateProvider.state('helpdesk', { parent: 'admin' });
    //   $stateProvider.state('permission', { parent: 'system' });
    //   $stateProvider.state('reports', { parent: 'admin' });
    //   $stateProvider.state('vprdata', { parent: 'reports' });
    //   $stateProvider.state('delifoodreports', { parent: 'reports' });
	  // $stateProvider.state('audit', { parent: 'admin' });
	  
      $locationProvider.html5Mode(true);
    }]);

  app.controller('BodyController', ['$rootScope', '$scope', function ($rootScope, $scope) {
    $scope.hasAccess = function (permissions) {
      return $rootScope.hasAccess(permissions);
    };
  }]);

  app.run([
    '$rootScope', '$state', '$transitions', 'auth', 'localStorageService', '$http', '$cookies', '$timeout', 'editableOptions', 'editableThemes',
    function ($rootScope, $state, $transitions, auth, localStorageService, $http, $cookies, $timeout, editableOptions, editableThemes) {

      editableThemes.bs4.inputClass = 'form-control-sm';
      editableOptions.theme = 'bs4';

      $rootScope.result = null;
      $rootScope.year = new Date().getFullYear();
      $rootScope.assetsAdminPath = assetsAdminPath;

      $rootScope.hasAccess = function (permissions) {
        if (permissions !== undefined && $rootScope.permissions != null) {

          var allow = false;

          angular.forEach(permissions.split(','), function (value) {
            if ($rootScope.permissions.indexOf(value) !== -1) {
              allow = true;
            }
          });

          return allow;
        }

        return true;
      };

      $rootScope.permissionsLoaded = function () {
        return $rootScope.permissions != null;
      };

      $rootScope
        .$on('$viewContentLoaded',
            function(){
              $timeout(function () {

                initApp.appForms('.input-group', 'has-length', 'has-disabled');

              }, 100);
        });

      $transitions.onBefore({}, function (trans) {

        if (trans.$to().name === 'logout') {

          localStorageService.remove(tokenName);
          $cookies.remove('auth');
          $rootScope.loggedIn = false;
          $rootScope.auth = null;
          return $state.target("login");
        }

        if (!auth.loggedIn()) {

          $rootScope.loggedIn = false;

          if (trans.to().name != 'login') {
            localStorageService.set('ref', trans.to().name)
          }

          if (typeof trans.$to().data !== 'undefined') {
            return $state.target("login");
          }
        }
        else {

          $rootScope.loggedIn = true;
          $cookies.put('auth', localStorageService.get(tokenName));

          var permissions = null;

          if (trans.$to().data !== undefined)
            permissions = trans.$to().data.permissions;

          if (permissions !== null) {

            var allow = false;

            if ($rootScope.permissions == null) {
              $http.get('api/admin/me/permissions').then(function (resp) {
                $rootScope.permissions = JSON.stringify(resp.data);
                angular.forEach(permissions, function (value) {
                  if ($rootScope.permissions.indexOf(value) !== -1) {
                    allow = true;
                  }
                });

                if (allow == false) {
                  // TODO: Permission failed redirect access denied
                  return $state.go("home");
                }
              });
            }
            else {

              angular.forEach(permissions, function (value) {
                if ($rootScope.permissions.indexOf(value) !== -1) {
                  allow = true;
                }
              });

              if (allow == false) {
                // TODO: Permission failed redirect access denied
                return $state.target("home");
              }
            }

            //if ($rootScope.stores == null) {
              // // $http.get('api/admin/me/stores').then(function (resp) {
              // //   $rootScope.stores = JSON.stringify(resp.data);
              // // });
            //}
          }
          // TODO: Permission check
        }
      });
    }
  ]);

  app.factory('auth',
    [
      '$http', 'jwtHelper', 'localStorageService', '$rootScope', function ($http, jwtHelper, localStorageService, $rootScope) {
        return {
          loggedIn: function () {

            var token = localStorageService.get(tokenName);

            if (token == null)
              return false;

            if (jwtHelper.isTokenExpired(token)) {

              localStorageService.remove(tokenName);
              return false;
            } else {

              var user = jwtHelper.decodeToken(token);
              $rootScope.user = {
                id: user.id,
                name: user.name,
                email: user.email
              };

              $rootScope.auth = jwtHelper.decodeToken(token);
            }

            return true;
          }
        };
      }
    ]);

})(angular);

Object.size = function (obj) {
  var size = 0, key;

  for (key in obj) {
    if (obj.hasOwnProperty(key)) size++;
  }

  return size;
};

function unflatten(arr) {
  var tree = [],
    mappedArr = {},
    arrElem,
    mappedElem;

  // First map the nodes of the array to an object -> create a hash table.
  for (var i = 0, len = arr.length; i < len; i++) {
    arrElem = arr[i];
    mappedArr[arrElem.id] = arrElem;
    mappedArr[arrElem.id].nodes = [];
  }
  for (var id in mappedArr) {

    if (mappedArr.hasOwnProperty(id)) {
      mappedElem = mappedArr[id];

      // If the element is not at the root level, add it to its parent array of children.
      if (mappedElem.parentId) {
        mappedArr[mappedElem.parentId].nodes.push(mappedElem);
      }

      // If the element is at the root level, add it to first level elements array.
      else {
        tree.push(mappedElem);
      }
    }
  }
  return tree;
}
