'use strict';

angular.module('kheper', ['ui.router', 'kheper.controllers'])
    .run([
        '$state', function($state) {
            $state.transitionTo('kheper.home');
        }
    ]);