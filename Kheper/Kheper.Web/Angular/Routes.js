'use strict';

angular.module('app', ['ui.router'])
    .config([
        '$stateProvider', function($stateProvider) {
            var home = {
                    name: 'home',
                    url: '/',
                    templateUrl: 'Angular/Layout.html'
                },
                retrospective = {
                    name: 'retrospective',
                    url: '/retrospective',
                    parent: home,
                    templateUrl: 'Angular/Retrospective/Layout.html'
                },
                planningPocker = {
                    name: 'planningPocker',
                    url: '/planningPocker',
                    parent: home,
                    templateUrl: 'Angular/PlanningPocker/Layout.html'
                };

            $stateProvider.state(home);
            $stateProvider.state(retrospective);
            $stateProvider.state(planningPocker);
        }
    ])
    .run([
        '$state', function($state) {
            $state.transitionTo('home');
        }
    ])
    .controller('SidebarCtrl', function($scope, $state) {

        $scope.content = ['red', 'green', 'blue'];

        $scope.setPage = function(page) {
            $state.transitionTo(page);
        };
    });

