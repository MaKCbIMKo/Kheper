'use strict';

angular.module("kheper.services").factory("planningRepository", ['$http', function ($http) {
    var baseUrl = '/api/planning/sessions';
    return {
        newSession: function() {
            return $http.post(baseUrl);
        }
    };
}]);
