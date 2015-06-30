(function () {
    'use strict';
    var ticketsApp = angular.module('ticketsApp');

    ticketsApp.directive('teamList', function () {
        return {
            restrict: 'E',
            replace: 'true',
            templateUrl: '/js/directives/templates/team-list.html',
            controller: TeamController,
            controllerAs: 'vm',
            scope: {}
        };
    });

    function TeamController($location, teamService) {
        var vm = this;

        vm.title = "Teams";

        vm.goToTeam = function (teamId) {
            $location.path('teams/' + teamId);
        };

        teamService.getAll().then(function (teams) {
            vm.teams = teams;
        });
    }

})();