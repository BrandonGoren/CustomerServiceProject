(function () {
    'use strict';

    angular.module('ticketsApp')
        .controller("TeamDetailController", TeamDetailController);

    TeamDetailController.$inject = ['$location', '$routeParams', 'ticketService', 'websiteService', 'teamService'];
    function TeamDetailController($location, $routeParams, ticketService, websiteService, teamService) {
        var vm = this;
        vm.team = {};
        vm.tickets = [];
        vm.agents = [];
        vm.goToTicket = goToTicket;
        vm.deleteTeam = deleteTeam;

        activate();

        function activate() {
            teamService.getTeam($routeParams.teamId).then(function (team) {
                vm.team = team;
            });
            teamService.getTeamTickets($routeParams.teamId).then(function (tickets) {
                vm.tickets = tickets;
            });
            teamService.getTeamAgents($routeParams.teamId).then(function (agents) {
                vm.agents = agents;
            });
        }

        function goToTicket(ticketId) {
            $location.path('/tickets/' + ticketId);
        }

        function deleteTeam() {
            teamService.deleteTeam(vm.team).then(function () {
                $location.path('/teams');
            });
        }
    }
})();