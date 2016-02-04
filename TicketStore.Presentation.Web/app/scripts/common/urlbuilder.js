'use strict';

/**
 * @ngdoc service
 * @name ticketStoreAppApp.urlBuilder
 * @description
 * # urlBuilder
 * Factory in the ticketStoreAppApp.
 */
angular.module('ticketStoreAppApp')
  .factory('urlBuilder', function ($interpolate, $httpParamSerializer, settings) {
    return function(url, params){
      var newurl = settings.api + (url.charAt(0) === '/' ? url : ('/' + url));
      var newparams = angular.isObject(params) ? params : {id: params};
      var hasodataopts = 'query' in newparams;

      if (newurl.indexOf('?') === -1 && hasodataopts) {
        newurl += '?';
      }
      if ('query' in newparams) {
        if (angular.isObject(newparams.query)){
          newparams.query = $httpParamSerializer(newparams.query);
        }
        newurl += '{{query}}';
      }
      return $interpolate(newurl)(newparams);
    };
  });
