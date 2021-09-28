CKEDITOR.plugins.addExternal( 'codemirror', assetsAdminPath + 'js/ckeditor/codemirror/', 'plugin.js' );

function setCkConfig(config) {

    config.extraPlugins = "autogrow,codemirror";
    config.autoGrow_onStartup = true;

    config.allowedContent = true;
    config.FormatSource = false;
    config.fillEmptyBlocks = false;
    config.enterMode = CKEDITOR.ENTER_P;

    config.baseHref = virtualDir;

    config.contentsCss =
    [
      assetsAdminPath + 'css/ckstyle.css',
      //assetsPath + "css/app.css"
    ];

    config.filebrowserBrowseUrl = virtualDir + 'filemanager/dialog.php?type=2&editor=ckeditor&fldr=';
    config.filebrowserUploadUrl = virtualDir + 'filemanager/dialog.php?type=2&editor=ckeditor&fldr=';
    config.filebrowserImageBrowseUrl = virtualDir + 'filemanager/dialog.php?type=1&editor=ckeditor&fldr=';
    config.filebrowserImageUploadUrl = virtualDir + 'filemanager/dialog.php?type=1&editor=ckeditor&fldr=';

    // config.filebrowserBrowseUrl = virtualDir + 'ckfinder/ckfinder.html';
    // config.filebrowserImageBrowseUrl = virtualDir + 'ckfinder/ckfinder.html?type=Images';
    // config.filebrowserUploadUrl = virtualDir + 'ckfinder/connector?command=QuickUpload&type=Files';
    // config.filebrowserImageUploadUrl = virtualDir + 'ckfinder/connector?command=QuickUpload&type=Images';

    $.each(CKEDITOR.dtd.$removeEmpty, function (i) {
        CKEDITOR.dtd.$removeEmpty[i] = false;
    });
    CKEDITOR.dtd.$removeEmpty['a'] = false;

    config.codemirror = {
        autoFormatOnStart: true
    };

    // Toolbar setup
    config.toolbarGroups = [
      { name: 'document', groups: [ 'mode', 'document', 'doctools' ] },
      { name: 'clipboard', groups: [ 'clipboard', 'undo' ] },
      { name: 'editing', groups: [ 'find', 'selection', 'spellchecker', 'editing' ] },
      { name: 'insert', groups: [ 'insert' ] },
      { name: 'links', groups: [ 'links' ] },
      { name: 'tools', groups: [ 'tools' ] },
      '/',
      { name: 'forms', groups: [ 'forms' ] },
      { name: 'basicstyles', groups: [ 'basicstyles', 'cleanup' ] },
      { name: 'paragraph', groups: [ 'list', 'indent', 'blocks', 'align', 'bidi', 'paragraph' ] },
      '/',
      { name: 'styles', groups: [ 'styles' ] },
      { name: 'colors', groups: [ 'colors' ] },
      { name: 'others', groups: [ 'others' ] },
      { name: 'about', groups: [ 'about' ] }
    ];
    config.removeButtons = 'Form,Checkbox,Radio,TextField,Textarea,Select,Button,ImageButton,HiddenField,SelectAll,Scayt,Flash,Smiley,About';

    return config;
};

//CKEDITOR.scriptLoader.load(["/Content/js/jquery.min.js", "/Content/js/easyResponsiveTabs.js", "/Content/js/custom.js"]);

(function (root, factory) {
  // AMD
  if (typeof define === 'function' && define.amd) define(['angular'], factory);
  // Global
  else factory(angular);
}(this, function (angular) {

  angular
  .module('ckeditor', [])
  .directive('ckeditor', ['$parse', ckeditorDirective]);

  // Polyfill setImmediate function.
  var setImmediate = window && window.setImmediate ? window.setImmediate : function (fn) {
    setTimeout(fn, 0);
  };

  /**
   * CKEditor directive.
   *
   * @example
   * <div ckeditor="options" ng-model="content" ready="onReady()"></div>
   */

  function ckeditorDirective($parse) {
    return {
      restrict: 'A',
      require: ['ckeditor', 'ngModel'],
      controller: [
        '$scope',
        '$element',
        '$attrs',
        '$parse',
        '$q',
        ckeditorController
      ],
      link: function (scope, element, attrs, ctrls) {
        // get needed controllers
        var controller = ctrls[0]; // our own, see below
        var ngModelController = ctrls[1];

        // Initialize the editor content when it is ready.
        controller.ready().then(function initialize() {
          // Sync view on specific events.
          ['dataReady', 'change', 'blur', 'saveSnapshot'].forEach(function (event) {
            controller.onCKEvent(event, function syncView() {
              ngModelController.$setViewValue(controller.instance.getData() || '');
            });
          });

          controller.instance.setReadOnly(!! attrs.readonly);
          attrs.$observe('readonly', function (readonly) {
            controller.instance.setReadOnly(!! readonly);
          });

          // Defer the ready handler calling to ensure that the editor is
          // completely ready and populated with data.
          setImmediate(function () {
            $parse(attrs.ready)(scope);
          });
        });

        // Set editor data when view data change.
        ngModelController.$render = function syncEditor() {
          controller.ready().then(function () {
            // "noSnapshot" prevent recording an undo snapshot
            controller.instance.setData(ngModelController.$viewValue || '', {
              noSnapshot: true,
              callback: function () {
                // Amends the top of the undo stack with the current DOM changes
                // ie: merge snapshot with the first empty one
                // http://docs.ckeditor.com/#!/api/CKEDITOR.editor-event-updateSnapshot
                controller.instance.fire('updateSnapshot');
              }
            });
          });
        };
      }
    };
  }

  /**
   * CKEditor controller.
   */

  function ckeditorController($scope, $element, $attrs, $parse, $q) {
    var config = $parse($attrs.ckeditor)($scope) || {};
    var editorElement = $element[0];
    var instance;
    var readyDeferred = $q.defer(); // a deferred to be resolved when the editor is ready

    config = jQuery.extend(config, setCkConfig(config));

    // Create editor instance.
    if (editorElement.hasAttribute('contenteditable') &&
        editorElement.getAttribute('contenteditable').toLowerCase() == 'true') {
      instance = this.instance = CKEDITOR.inline(editorElement, config);
    }
    else {
      instance = this.instance = CKEDITOR.replace(editorElement, config);
    }

    /**
     * Listen on events of a given type.
     * This make all event asynchronous and wrapped in $scope.$apply.
     *
     * @param {String} event
     * @param {Function} listener
     * @returns {Function} Deregistration function for this listener.
     */

    this.onCKEvent = function (event, listener) {
      instance.on(event, asyncListener);

      function asyncListener() {
        var args = arguments;
        setImmediate(function () {
          applyListener.apply(null, args);
        });
      }

      function applyListener() {
        var args = arguments;
        $scope.$apply(function () {
          listener.apply(null, args);
        });
      }

      // Return the deregistration function
      return function $off() {
        instance.removeListener(event, applyListener);
      };
    };

    this.onCKEvent('instanceReady', function() {
      readyDeferred.resolve(true);
    });

    /**
     * Check if the editor if ready.
     *
     * @returns {Promise}
     */
    this.ready = function ready() {
      return readyDeferred.promise;
    };

    // Destroy editor when the scope is destroyed.
    $scope.$on('$destroy', function onDestroy() {
      // do not delete too fast or pending events will throw errors
      readyDeferred.promise.then(function() {
        instance.destroy(false);
      });
    });
  }
}));
