'use strict';

/**
 * @ngdoc overview
 * @name ticketStoreAppApp
 * @description
 * # ticketStoreAppApp
 *
 * Main module of the application.
 */
angular
  .module('ticketStoreAppApp', [
    'ngSanitize',
    'ngTouch',
    'ui.router',
    'ui.bootstrap',
    'toaster',
    'ipCookie'
  ])
  .config(function ($stateProvider, $urlRouterProvider) {

    var defaultResolver = {
      start: ['appStarter', function (appStarter) {
        return appStarter;
      }]
    };

    $urlRouterProvider.otherwise('/');

    $stateProvider
      .state('main', {
        url: '/'
      })
      .state('events', {
        url: '/events',
        templateUrl: 'views/events.html',
        controller: 'EventsCtrl',
        controllerAs: 'events'
      })
      .state('signin', {
        url: '/signin?redirect',
        templateUrl: 'views/signin.html',
        controller: 'SigninCtrl',
        controllerAs: 'signin'
      })
      .state('signup', {
        url: '/signup',
        templateUrl: 'views/signup.html',
        controller: 'SignupCtrl',
        controllerAs: 'signup'
      })
      .state('checkout', {
        url: '/checkout/:eventId?',
        templateUrl: 'views/checkout.html',
        controller: 'CheckoutCtrl',
        controllerAs: 'checkout',
        resolve: defaultResolver
      })
      .state('confirmation', {
        url: '/confirmation/:orderId',
        templateUrl: 'views/confirmation.html',
        controller: 'ConfirmationCtrl',
        controllerAs: 'confirmation',
        resolve: defaultResolver
      });
  })
  .config(function ($httpProvider) {
    $httpProvider.interceptors.push('interceptor');
  })
  .run(function ($rootScope, $state, toaster, session) {

    $rootScope.logout = function(){
      session.destroy();
      $state.go('main');
      $rootScope.userLoggedIn = false;
    };

    $rootScope.$on('$stateChangeStart', function (event, toState, params) {
      var protectedPage = ['checkout'].indexOf(toState.name) > -1;
      var userLoggedIn = session.isValid();
      $rootScope.userLoggedIn = userLoggedIn;
      $rootScope.showCoverImage = toState.name === 'main';

      if (protectedPage && !userLoggedIn){
        event.preventDefault();

        var url = $state.href(toState.name, params).replace('#/','');
        var redirect = encodeURIComponent(url);
        $state.go('signin', { redirect: redirect});
      }
    });
  });
