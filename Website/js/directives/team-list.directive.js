(function () {
    'use strict';
    var ticketsApp = angular.module('ticketsApp')
        .directive('teamList', teamList);

    function teamList() {
        return {
            restrict: 'E',
            replace: 'true',
            templateUrl: '/js/directives/templates/team-list.html',
            controller: TeamController,
            controllerAs: 'vm',
            scope: {}
        }
    }

    TeamController.$inject = ['$location', 'teamService'];
    function TeamController($location, teamService) {
        var vm = this;
        vm.title = "Teams";
        vm.teams = [];
        vm.newTeam = {};
        vm.goToTeam = goToTeam;
        vm.createTeam = createTeam;
        activate();

        function activate() {
            vm.newTeam.name = "";
            teamService.getAll().then(function (teams) {
                vm.teams = teams;
            });
        }

        function goToTeam(teamId) {
            $location.path('teams/' + teamId);
        }

        function createTeam() {
            console.log(vm.newTeam);
            teamService.createTeam(vm.newTeam).then(activate);
        }
    }
})();