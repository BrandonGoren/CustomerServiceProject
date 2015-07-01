(function () {
    'use strict';

    angular.module('ticketsApp')
       .controller("TicketEditController", TicketEditController);

    TicketEditController.$inject = ['$routeParams', '$location', 'ticketService', 'websiteService', 'teamService'];
    function TicketEditController($routeParams, $location, ticketService, websiteService, teamService) {

        var vm = this;
        vm.ticket = [];
        vm.getOpen = getOpen;
        vm.saveEdits = saveEdits;
        vm.cancelEdits = cancelEdits;
        vm.deleteTicket = deleteTicket;

        activate();

        function activate() {
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
        }

        function getOpen(ticket) {
            return ticket.open ? "open" : "closed";
        }

        function saveEdits() {
            ticketService.editTicket(vm.ticket).then(function (response) {
                $location.path('/tickets/' + response.id);
            });
        }

        function cancelEdits() {
            $location.path('/tickets/' + vm.ticket.id);
        }

        function deleteTicket() {
            ticketService.deleteTicket(vm.ticket).then(function (response) {
                $location.path('/see-tickets');
            });
        }
    }
})();