'use strict';

/**
 * @ngdoc service
 * @name ticketStoreAppApp.UserService
 * @description
 * # UserService
 * Service in the ticketStoreAppApp.
 */
angular.module('ticketStoreAppApp')
  .service('UserService', function ($http, urlBuilder) {
    return {
      get: function(id){
        var uri = urlBuilder('users/{{id}}', id);
        return $http.get(uri);
      },
      post: function(user){
        var uri = urlBuilder('users');
        return $http.post(uri, user);
      },
      getCreditCards: function(id){
        var uri = urlBuilder('users/{{id}}/creditcards', id);
        return $http.get(uri);
      }
    };
  });
