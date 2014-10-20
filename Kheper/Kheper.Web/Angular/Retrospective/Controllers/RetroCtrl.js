"use strict";

angular.module("kheper.controllers").controller('RetroCtrl', function ($scope, $state, retroRepository) {

    $scope.items = retroRepository.get();

    $scope.newStartDoing = {
        title: "",
        description: "",
        anonymous: false,
        type: "Start",
    };
    $scope.addStartDoing = function() {
        retroRepository.add($scope.newStartDoing);
    };
    $scope.clearStartDoing = function() {
        $scope.newStartDoing.title = "";
        $scope.newStartDoing.description = "";
        $scope.newStartDoing.anonymous = false;
    };
    $scope.newStopDoing = {
        title: "",
        description: "",
        anonymous: false,
        type: "Stop",
    };
    $scope.addStopDoing = function() {
        retroRepository.add($scope.newStopDoing);
    };
    $scope.clearStopDoing = function() {
        $scope.newStopDoing.title = "";
        $scope.newStopDoing.description = "";
        $scope.newStopDoing.anonymous = false;
    };
    $scope.newContineDoing = {
        title: "",
        description: "",
        anonymous: false,
        type: "Continue",
    };
    $scope.addContineDoing = function() {
        retroRepository.add($scope.newContineDoing);
    };
    $scope.clearContineDoing = function() {
        $scope.newContineDoing.title = "";
        $scope.newContineDoing.description = "";
        $scope.newContineDoing.anonymous = false;
    };
});