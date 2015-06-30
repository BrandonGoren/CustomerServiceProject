(function () {
    'use strict';

    var ticketsApp = angular.module('ticketsApp');
    ticketsApp.controller("TicketEditController", function ($routeParams, $location, ticketService, websiteService, teamService) {
        var vm = this;
        ticketService.getTicket($routeParams.ticketId).then(function (tickets) {
            console.log(tickets);
            vm.ticket = tickets;
        });

        teamService.getAll().then(function (teams) {
            vm.teams = teams;
        });

        websiteService.getAll().then(function (websites) {
            vm.websites = websites;
        });

        vm.getOpen = function (ticket) {
            return ticket.open ? "open" : "closed";
        };

        vm.saveEdits = function () {
            ticketService.editTicket(vm.ticket).then(function (response) {
                $location.path('/tickets/' + response.id);
            });
        }

        vm.cancelEdits = function () {
            $location.path('/tickets/' + vm.ticket.id);
        }

        vm.deleteTicket = function () {
            ticketService.deleteTicket(vm.ticket).then(function (response) {
                $location.path('/see-tickets');
            });
        }
    }
    );
})();