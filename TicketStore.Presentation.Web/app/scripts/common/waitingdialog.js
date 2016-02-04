'use strict';

/**
 * @ngdoc service
 * @name ticketStoreAppApp.waitingDialog
 * @description
 * # waitingDialog
 * Factory in the ticketStoreAppApp.
 */
angular.module('ticketStoreAppApp')
  .factory('waitingDialog', function () {

  var $dialog = angular.element(
    '<div class="modal fade" data-backdrop="false" data-keyboard="false" tabindex="-1" role="dialog" aria-hidden="true" style="padding-top:15%; overflow-y:visible;z-index:9999">' +
    '<div class="modal-dialog modal-sm">' +
    '<div class="modal-content">' +
      '<div class="modal-header"><h3 style="margin:0;" class="title"></h3></div>' +
      '<div class="modal-body">' +
        '<div class="progress progress-striped active" style="margin-bottom:0;"><div class="progress-bar" style="width: 100%"></div></div>' +
      '</div>' +
    '</div></div></div>');

  return {
    show: function (message, options) {
      var settings = angular.extend({
        dialogSize: 'sm',
        progressType: 'danger'
      }, options);
      if (typeof message === 'undefined') {
        message = 'Carregando...';
      }
      if (typeof options === 'undefined') {
        options = {};
      }

      // Configuring dialog
      if (options.backdrop === true){
        $dialog.attr('data-backdrop', true);
      }

      $dialog.find('.modal-dialog').attr('class', 'modal-dialog').addClass('modal-' + settings.dialogSize);
      $dialog.find('.progress-bar').attr('class', 'progress-bar');
      if (settings.progressType) {
        $dialog.find('.progress-bar').addClass('progress-bar-' + settings.progressType);
      }
      $dialog.find('h3').text(message);
      $dialog.modal();
    },
    hide: function () {
      $dialog.modal('hide');
    }
  };
});
