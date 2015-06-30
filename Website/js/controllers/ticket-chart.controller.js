(function () {
    'use strict';

    var ticketsApp = angular.module('ticketsApp');
    ticketsApp.controller("TicketChartController", function ($filter, ticketService, websiteService, teamService) {
        var vm = this;
        ticketService.getAll().then(function (tickets) {
            vm.tickets = tickets;
            vm.labels = ["Open", "Closed"];
            vm.openData = [$filter('filter')(vm.tickets, { open: true }, true).length, $filter('filter')(vm.tickets, { open: false }, true).length];
            vm.colors = ['#33CC33', '#f7464a'];

            for (let i = 0; i < vm.tickets.length; i++) {
                websiteService.getWebsite(vm.tickets[i].websiteId).then(function (website) {
                    vm.tickets[i].website = website;
                });
                teamService.getTeam(vm.tickets[i].assignedTeamId).then(function (team) {
                    vm.tickets[i].team = team;
                });
            }
        });
    });
})();