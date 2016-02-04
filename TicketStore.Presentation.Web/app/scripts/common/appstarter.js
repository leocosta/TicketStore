'use strict';

/**
 * @ngdoc service
 * @name ticketStoreAppApp.appStarter
 * @description
 * # appStarter
 * Service in the ticketStoreAppApp.
 */
angular.module('ticketStoreAppApp')
  .service('appStarter', function ($rootScope, $q, session, UserService) {

  $rootScope.user = null;

  var loadUser = function(){
    if (session.user){
      return $q.when();
    }

    if (!session.isValid()){
      return $q.reject();
    }

    return UserService.get(session.getUserId())
      .then(function(response){
        session.setUser(response.data);
      });
  };
  return loadUser();
});
