(function () {
    'use strict';

    angular.module('ticketsApp')
    .controller("TicketDetailController", TicketDetailController)

    TicketDetailController.$inject = ['$routeParams', '$location', 'ticketService', 'websiteService', 'teamService'];
    function TicketDetailController($routeParams, $location, ticketService, websiteService, teamService) {

        var vm = this;
        vm.editingTicket = false;
        vm.addingNote = false;
        vm.ticket = {};
        vm.newNote;
        vm.getOpen = getOpen;
        vm.close = close;
        vm.saveNote = saveNote;
        vm.editTicket = editTicket;
        vm.showBaseButtons = showBaseButtons;

        activate();

        function activate() {
            ticketService.getTicket($routeParams.ticketId).then(function (ticket) {
                vm.ticket = ticket;
                websiteService.getWebsite(vm.ticket.websiteId).then(function (website) {
                    vm.ticket.website = website;
                });
                teamService.getTeam(vm.ticket.assignedTeamId).then(function (team) {
                    vm.ticket.team = team;
                });
            });
        }

        function getOpen(ticket) {
            return ticket.open ? "Open" : "Closed";
        }

        function close() {
            ticketService.closeTicket(vm.ticket.id).then(function (ticket) {
                vm.ticket.open = ticket.open;
                vm.ticket.dateClosed = ticket.dateClosed;
            });
        }

        function saveNote() {
            ticketService.addNote(vm.ticket, vm.newNote).then(function (response) {
                vm.ticket.notes = response.notes;
            });
            vm.addingNote = false;
            vm.newNote = "";
        }

        function editTicket() {
            $location.path('/edit-ticket/' + vm.ticket.id);
        }

        function showBaseButtons() {
            return vm.ticket.open && !vm.editingTicket && !vm.addingNote;
        }
    }
})();