'use strict';

/**
 * @ngdoc function
 * @name ticketStoreAppApp.controller:EventsCtrl
 * @description
 * # EventsCtrl
 * Controller of the ticketStoreAppApp
 */
angular.module('ticketStoreAppApp')
  .controller('EventsCtrl', function (EventService, waitingDialog) {

    var that = this;

    this.loadEvents = function(){
      return EventService.getAll()
        .then(function(response){
          that.events = response.data;
        });
    };

    this.init = function(){
      waitingDialog.show('Aguarde...');
      this.loadEvents()
        .then()
        ['finally'](function(){
          waitingDialog.hide();
        });
    };

    this.init();

  });
