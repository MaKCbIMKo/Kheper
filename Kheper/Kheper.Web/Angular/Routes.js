angular.module("kheper").config([
    '$stateProvider', function ($stateProvider) {
        $stateProvider.state("kheper", {
            templateUrl: "Angular/Views/Layout.html",
            abstract: true,
        }).state('kheper.home', {
            url: "/",
            views: {
                "mainMenu": {
                    templateUrl: 'Angular/Views/MainMenu.html',
                    controller: "MainMenuCtrl"
                },
                "": {
                    templateUrl: 'Angular/Views/Home.html',
                    controller: "HomeCtrl"
                },
                "footer": {
                    templateUrl: 'Angular/Views/Footer.html',
                }
            }
        }).state('kheper.retrospective', {
            abstract: true,
            views: {
                "mainMenu": {
                    templateUrl: 'Angular/Views/MainMenu.html',
                    controller: "MainMenuCtrl"
                },
                "": {
                    templateUrl: 'Angular/Retrospective/Views/Layout.html'
                },

                "footer": {
                    templateUrl: 'Angular/Views/Footer.html',
                }
            }
        }).state('kheper.retrospective.default', {
            url: "/Retro",
            views: {
                "users": {
                    templateUrl: 'Angular/Retrospective/Views/Users.html',
                    controller: "UserCtrl"
                },
                "retro": {
                    templateUrl: 'Angular/Retrospective/Views/Retro.html',
                    controller: "RetroCtrl"
                }
            }

        });

    }
]);
