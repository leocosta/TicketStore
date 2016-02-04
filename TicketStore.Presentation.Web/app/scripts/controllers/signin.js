'use strict';

/**
 * @ngdoc function
 * @name ticketStoreAppApp.controller:SigninCtrl
 * @description
 * # SigninCtrl
 * Controller of the ticketStoreAppApp
 */
angular.module('ticketStoreAppApp')
  .controller('SigninCtrl', function ($rootScope, $scope, $state, $location, session, toaster, waitingDialog, AuthService) {
    var that = this;

    this.credentials = {
      email: null,
      password: null
    };

    this.login = function(){
      if (!isValidForm()){
        return false;
      }

      waitingDialog.show('Aguarde...');
      AuthService.authenticate(this.credentials)
        .then(function(){
          $rootScope.userLoggedIn = true;
          var toState = decodeURIComponent($state.params.redirect || 'events');
          $location.path(toState);
        })
        ['catch'](function(){
          toaster.pop('error', null, 'Falha na autenticação. Verifique seus dados de acesso.');
        })
        ['finally'](function(){
          waitingDialog.hide();
        });
    };

    var isValidForm = function(){
      if (that.frmSignIn.$invalid){
        toaster.pop('error', null, 'Você deve preencher os campos para logar.');
        angular.forEach(that.frmSignIn.$error.required, function(field) {
            field.$setDirty();
        });
        return false;
      }
      return true
    };

  });
