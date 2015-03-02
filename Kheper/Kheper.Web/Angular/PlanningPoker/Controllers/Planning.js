"use strict";

angular.module("kheper.controllers").controller('PlanningCtrl', function ($scope, $state, planningRepository) {

    $scope.items = planningRepository.get();

    $scope.newVotingSession = function () {
        planningRepository.newSession().success(function(data) {
        });
    };
});