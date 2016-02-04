'use strict';

/**
 * @ngdoc function
 * @name ticketStoreAppApp.controller:CheckoutCtrl
 * @description
 * # CheckoutCtrl
 * Controller of the ticketStoreAppApp
 */
angular.module('ticketStoreAppApp')
  .controller('CheckoutCtrl', function ($stateParams, $state, util, toaster, waitingDialog, session, EventService, OrderService) {
    var that = this;

    this.loadEvent = function(){
      return EventService.get($stateParams.eventId)
        .then(function(response){
          that.event = response.data;
        });
    };

    this.purchase = function(){
      if (!isValidForm()){
        return false;
      }

      that.order.event = that.event;
      that.order.customer = session.user;

      waitingDialog.show('Aguarde...');
      OrderService.post(this.order)
        .then(function(response){
          $state.go('confirmation', response.data);
        })
        ['catch'](function(){
          toaster.pop('error', null, 'Ocorreu um erro ao finalizar seu pedido. ' +
            'Verifique se os dados estão corretos e tente novamente.');
        })
        ['finally'](function(){
          waitingDialog.hide();
        });
    };

    this.checkCreditCardNumber = function(){
      var number = that.order.paymentInfo.creditCardNumber;
      that.order.paymentInfo.creditCardBrand = util.detectCardType(number);
      that.frmCheckout.creditCardNumber.$invalid = !that.order.paymentInfo.creditCardBrand;
    };

    this.init = function(){
      waitingDialog.show('Aguarde...');
      this.loadEvent()
        .then()
        ['finally'](function(){
          waitingDialog.hide();
        });
    };

    var isValidForm = function(){
      if (that.frmCheckout.$invalid){
        toaster.pop('error', null, 'Você deve preencher os campos corretamente para finalizar seu pedido.');
        angular.forEach(that.frmCheckout.$error.required, function(field) {
          field.$setDirty();
        });
        return false;
      }

      if (!that.order.paymentInfo.creditCardBrand){
        toaster.pop('error', null, 'Você deve digitar o número de um cartão de crédito válido.');
        return false;
      }
      return true
    };

    this.init();

  });
