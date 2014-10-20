"use strict";

angular.module("kheper.controllers").controller('UserCtrl', function($scope, $state) {
    $scope.retro = {
        id: "evbyminsd2245"
    };

    $scope.users = [
        {
            name: "Alex"
        },
        {
            name: "Anna"
        },
        {
            name: "Devid"
        },
        {
            name: "Stive"
        }
    ];
});
