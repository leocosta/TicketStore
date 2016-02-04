'use strict';

/**
 * @ngdoc service
 * @name ticketStoreAppApp.EventService
 * @description
 * # EventService
 * Service in the ticketStoreAppApp.
 */
angular.module('ticketStoreAppApp')
  .service('EventService', function ($http, urlBuilder) {
    return {
      get: function(id){
        var uri = urlBuilder('events/{{id}}', id);
        return $http.get(uri);
      },
      getAll: function(){
        var uri = urlBuilder('events');
        return $http.get(uri);
      }
    }
  });
