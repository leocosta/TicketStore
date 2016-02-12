'use strict';

/**
 * @ngdoc service
 * @name ticketStoreAppApp.OrderService
 * @description
 * # OrderService
 * Service in the ticketStoreAppApp.
 */
angular.module('ticketStoreAppApp')
  .service('OrderService', function ($http, urlBuilder) {
    return {
      post: function(order){
        var uri = urlBuilder('orders');
        return $http.post(uri, order);
      }
    };
  });
