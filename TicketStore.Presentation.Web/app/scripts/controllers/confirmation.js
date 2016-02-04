'use strict';

/**
 * @ngdoc function
 * @name ticketStoreAppApp.controller:ConfirmationCtrl
 * @description
 * # ConfirmationCtrl
 * Controller of the ticketStoreAppApp
 */
angular.module('ticketStoreAppApp')
  .controller('ConfirmationCtrl', function ($stateParams) {
    this.orderId = $stateParams.orderId;
  });
