'use strict';

/**
 * @ngdoc service
 * @name ticketStoreAppApp.session
 * @description
 * # session
 * Service in the ticketStoreAppApp.
 */
angular.module('ticketStoreAppApp')
  .service('session', function ($window, ipCookie) {

    var SESSION_TIMEOUT = 60;
    var cookieSettings = {
      expires: SESSION_TIMEOUT,
      expirationUnit: 'minutes',
      path: '/'
    };

    this.create = function (securityToken, userId) {
      ipCookie('securityToken', securityToken, cookieSettings);
      ipCookie('userId', userId, cookieSettings);
    };

    this.destroy = function () {
      var options = {
        path: cookieSettings.path
      };

      this.user = null;
      ipCookie.remove('securityToken', options);
      ipCookie.remove('userId', options);
    };

    this.getSecurityToken = function() {
      var securityToken = ipCookie('securityToken');
      return securityToken;
    };

    this.getUserId = function() {
      return ipCookie('userId');
    };

    this.setSecurityToken = function(securityToken) {
      this.securityToken = securityToken;
      ipCookie('securityToken', securityToken, cookieSettings);
    };

    this.setUser = function(user) {
      this.user = user;
    };

    this.isValid = function(){
      return !!(this.getSecurityToken() && this.getUserId());
    };

    return this;
  });
