(function () {
    'use strict';

    var ticketsApp = angular.module('ticketsApp');
    ticketsApp.controller("TicketCreatorController", function ($location, ticketService, websiteService, teamService) {
        var vm = this;
        vm.ticket = { };

        websiteService.getAll().then(function (websites) {
            console.log(websites);
            vm.websites = websites;
        });

        teamService.getAll().then(function (teams) {
            console.log(teams);
            vm.teams = teams;
        });

        vm.save = function () {
            console.log("saving");
            console.log(vm.ticket);
            ticketService.saveTicket(vm.ticket).then(function (){
                $location.path("/see-tickets");
            });
        };

        vm.validTicket = function () {
            var invalidName = vm.ticket.name === undefined || vm.ticket.name.length === 0;
            var invalidWebsite = vm.ticket.websiteId === undefined;
            var invalidTeam = vm.ticket.assignedTeamId === undefined;
            var invalidDescription = vm.ticket.description === undefined || vm.ticket.description.length === 0;
            var invalidType = vm.ticket.issueType === undefined;
            return invalidName || invalidWebsite || invalidTeam || invalidDescription || invalidType;
        };
    });
})();