(function () {
    'use strict';

    var ticketsApp = angular.module('ticketsApp');
    ticketsApp.controller("TicketDetailController", function ($routeParams, $location, ticketService, websiteService, teamService) {

        var vm = this;
        vm.editingTicket = false;
        vm.addingNote = false;
        vm.ticket = {};
        vm.newNote;

        ticketService.getTicket($routeParams.ticketId).then(function (ticket) {
            vm.ticket = ticket;
            websiteService.getWebsite(vm.ticket.websiteId).then(function (website) {
                vm.ticket.website = website;
            });
            teamService.getTeam(vm.ticket.assignedTeamId).then(function (team) {
                vm.ticket.team = team;
            });
        });

        vm.getOpen = function (ticket) {
            return ticket.open ? "Open" : "Closed";
        };

        vm.close = function () {
            ticketService.closeTicket(vm.ticket.id).then(function(ticket){
                vm.ticket.open = ticket.open;
                vm.ticket.dateClosed = ticket.dateClosed;
                });
        };

        vm.saveNote = function () {
            ticketService.addNote(vm.ticket, vm.newNote).then(function (response) {
                vm.ticket.notes = response.notes;
            });
            vm.addingNote = false;
            vm.newNote = "";
        };

        vm.editTicket = function () {
            $location.path('/edit-ticket/' + vm.ticket.id);
        };

        vm.showBaseButtons = function () {
            return vm.ticket.open && !vm.editingTicket && !vm.addingNote;
        };
      
    });
})();