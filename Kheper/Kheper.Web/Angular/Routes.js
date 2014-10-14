
angular.module("kheper").config([
    '$stateProvider', function($stateProvider) {
        $stateProvider.state("kheper", {
            templateUrl: "Angular/Views/Layout.html",
            controller: "BaseCtrl"
        }).state('kheper.home', {
            url: "/",
            views: {
                "mainMenu": {
                    templateUrl: 'Angular/Views/MainMenu.html',
                    controller: "MainMenuCtrl"
                },
                "footer": {
                    templateUrl: 'Angular/Views/Footer.html',
                }
            }
        });

    }
]);
