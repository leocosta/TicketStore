'use strict';

/**
 * @ngdoc service
 * @name ticketStoreAppApp.interceptor
 * @description
 * # interceptor
 * Factory in the ticketStoreAppApp.
 */
angular.module('ticketStoreAppApp')
  .factory('interceptor', function ($q, session) {
    return {
      request: function (config) {
          config.headers = config.headers || {};

          var securityToken = session.getSecurityToken();
          if (securityToken) {
              config.headers.SecurityToken = securityToken;
          }
          return config;
      },
      response: function (response) {
          if (response.headers('securitytoken')) {
              session.setSecurityToken(response.headers('securitytoken'));
          }
          return response || $q.when(response);
      },
      responseError: function(error) {
          return $q.reject(error);
      }
    };
  });
