'use strict';

/**
 * @ngdoc function
 * @name ticketStoreAppApp.controller:SignupCtrl
 * @description
 * # SignupCtrl
 * Controller of the ticketStoreAppApp
 */
angular.module('ticketStoreAppApp')
  .controller('SignupCtrl', function ($rootScope, $location, $state, waitingDialog, toaster, UserService, AuthService) {
    var that = this;

    this.signUp = function(){
      if (!isValidForm()){
        return false;
      }

      waitingDialog.show('Aguarde...');
      UserService.post(this.user)
        .then(function(){
          var credentials = {
            email: that.user.email,
            password: that.user.password
          };

          return AuthService.authenticate(credentials)
            .then(function(){
              $rootScope.userLoggedIn = true;
              var toState = decodeURIComponent($state.params.redirect || 'events');
              $location.path(toState);
            });
        })
        ['catch'](function(){
          toaster.pop('error', null, 'Ocorreu um erro ao efetuar cadastro. ' +
            'Verifique se os dados estão corretos e tente novamente.');
        })
        ['finally'](function(){
          waitingDialog.hide();
        });
    };

    var isValidForm = function(){
      if (that.frmSignUp.$invalid){
        toaster.pop('error', null, 'Você deve preencher os campos corretamente para se cadastrar.');
        angular.forEach(that.frmSignUp.$error.required, function(field) {
            field.$setDirty();
        });
        return false;
      }
      return true
    };

  });
