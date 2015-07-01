(function () {
    'use strict';

    angular.module('ticketsApp')
        .controller("TicketCreatorController", TicketCreatorController);

    TicketCreatorController.$inject = ['$location', 'ticketService', 'websiteService', 'teamService'];
    function TicketCreatorController($location, ticketService, websiteService, teamService) {
        var vm = this;
        vm.ticket = {};
        vm.websites = [];
        vm.teams = [];
        vm.save = save;
        vm.validTicket = validTicket;

        activate();

        function activate() {
            websiteService.getAll().then(function (websites) {
                console.log(websites);
                vm.websites = websites;
            });

            teamService.getAll().then(function (teams) {
                console.log(teams);
                vm.teams = teams;
            });
        }

        function save() {
            console.log("saving");
            console.log(vm.ticket);
            ticketService.saveTicket(vm.ticket).then(function () {
                $location.path("/see-tickets");
            });
        }

        function validTicket() {
            var invalidName = vm.ticket.name === undefined || vm.ticket.name.length === 0;
            var invalidWebsite = vm.ticket.websiteId === undefined;
            var invalidTeam = vm.ticket.assignedTeamId === undefined;
            var invalidDescription = vm.ticket.description === undefined || vm.ticket.description.length === 0;
            var invalidType = vm.ticket.issueType === undefined;
            return invalidName || invalidWebsite || invalidTeam || invalidDescription || invalidType;
        }
    }
})();