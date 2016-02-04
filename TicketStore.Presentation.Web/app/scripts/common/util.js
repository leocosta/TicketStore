'use strict';

/**
 * @ngdoc service
 * @name ticketStoreAppApp.util
 * @description
 * # util.model
 * Factory in the ticketStoreAppApp.
 */
angular.module('ticketStoreAppApp')
  .factory('util', function () {

    function detectCardType(number) {
      var re = {
        visa: /^4[0-9]{12}(?:[0-9]{3})?$/,
        mastercard: /^5[1-5][0-9]{14}$/,
        hipercard: /^(38|60)\d{11}(?:\d{3})?(?:\d{3})?$/,
        amex: /^3[47][0-9]{13}$/,
        diners: /^3(?:0[0-5]|[68][0-9])[0-9]{11}$/,
        electron: /^(4026|417500|4405|4508|4844|4913|4917)\d+$/
      };

      if (re.visa.test(number)) {
        return 'Visa';
      }
      if (re.mastercard.test(number)) {
        return 'Mastercard';
      }
      if (re.hipercard.test(number)) {
        return 'Hipercard';
      }
      if (re.amex.test(number)) {
        return 'Amex';
      }
      if (re.diners.test(number)) {
        return 'Diners';
      }
      return undefined;
    }

    return {
      detectCardType: detectCardType
    };
  });
