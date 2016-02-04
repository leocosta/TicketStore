'use strict';

/**
 * @ngdoc service
 * @name ticketStoreAppApp.AuthService
 * @description
 * # AuthService
 * Service in the ticketStoreAppApp.
 */
angular.module('ticketStoreAppApp')
  .service('AuthService', function ($http, session, urlBuilder, UserService) {
    return {
      authenticate: function(credentials){
        var uri = urlBuilder('/access');
        return $http
         .post(uri, credentials)
         .then(function(data) {
            session.create(data.headers('securityToken'), data.headers('userId'));
            return UserService.get(data.headers('userId'))
              .then(function(response){
                  session.setUser(response.data);
                  return response;
              });
          });
      }
    };
  });
