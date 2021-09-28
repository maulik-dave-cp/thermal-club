(function (angular) {
  "use strict";

  angular.module('myApp').component('adminPermissionsSequence', {
    templateUrl: '/adminPermissions/views/sequence.tpl.html',
    controller: [
      '$scope', 'adminPermissionsService',
      function ($scope, adminPermissionsService) {

        $scope.permissions = null;

        adminPermissionsService.sequence().then(function (resp) {
          $scope.permissions = unflattenForPerms(resp.data);
        });

        $scope.$watch('permissions', function (newValue, oldValue, scope) {

          if (oldValue == null) return;

          adminPermissionsService.saveSequence(newValue);
        });
      }
    ]
  });

}(window.angular));

function unflattenForPerms(arr) {
  var tree = [],
    mappedArr = {},
    arrElem,
    mappedElem;

  // First map the nodes of the array to an object -> create a hash table.
  for (var i = 0, len = arr.length; i < len; i++) {
    arrElem = arr[i];
    mappedArr[arrElem.id] = {
      id: arrElem.id,
      parentId: arrElem.parentId
    };
    mappedArr[arrElem.id].item = {
      id: arrElem.id,
      displayName: arrElem.displayName,
      name: arrElem.name,
      order: arrElem.left
    };
    mappedArr[arrElem.id].children = [];
  }

  for (var id in mappedArr) {
    if (mappedArr.hasOwnProperty(id)) {
      mappedElem = mappedArr[id];
      // If the element is not at the root level, add it to its parent array of children.
      if (mappedElem.parentId) {
        mappedArr[mappedElem.parentId].children.push(mappedElem);

        mappedArr[mappedElem.parentId].children.sort(function (a, b) {
          return parseFloat(a.item.order) - parseFloat(b.item.order);
        });
      }
      // If the element is at the root level, add it to first level elements array.
      else {
        tree.push(mappedElem);
      }
    }
  }

  // Level 0
  tree.sort(function (a, b) {
    return parseFloat(a.item.order) - parseFloat(b.item.order);
  });

  return tree;
}

var documentHeight = 0;
// call in jquery.nestable.js
//if (typeof window.onSeqMoveEvent === "function")
//  window.onSeqMoveEvent(e);
var isFirstTime = true;
var onSeqMoveEvent = function (e) {
  if (isFirstTime) {
    documentHeight = $(document).height();
    isFirstTime = false;
  }
  e.preventDefault();
  var scrollheightBottom = $(document).scrollTop() + $(window).height();
  var scrollheightTop = $(document).scrollTop() - 100;
  if (scrollheightBottom < e.offsetY && documentHeight > scrollheightBottom) {
    var diffrence = e.offsetY - scrollheightBottom;
    var scrollTop = $(document).scrollTop() + diffrence;
    $(document).scrollTop(scrollTop);
  }
  else if (scrollheightTop < e.offsetY) {
    var diffrence = e.offsetY - scrollheightTop;
    var scrollTop = $(document).scrollTop() - 25;
    $(document).scrollTop(scrollTop);
  }
};
