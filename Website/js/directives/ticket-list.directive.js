(function () {
    'use strict';
    var ticketsApp = angular.module('ticketsApp');

    ticketsApp.directive('ticketList', function () {
        return {
            restrict: 'E',
            replace: 'true',
            templateUrl: '/js/directives/templates/ticket-list.html',
            controller: TicketController,
            controllerAs: 'vm',
            scope: {}
        };
    });

    function TicketController($filter, $location, ticketService, websiteService, teamService) {
        var vm = this;

        vm.title = "Open Tickets";

        vm.goToTicket = function (ticketId) {
            $location.path('tickets/' + ticketId);
        };

        ticketService.getAll().then(function (tickets) {
            vm.orderBy = $filter('orderBy');
            vm.reverse = false;
            vm.tickets = tickets;
            vm.ticketsToShow = vm.tickets;
            vm.showOpenOnly = false;
            vm.showClosedOnly = false;

            for (let i = 0; i < vm.tickets.length; i++) {
                websiteService.getWebsite(vm.tickets[i].websiteId).then(function (website) {
                    vm.tickets[i].website = website;
                });
                teamService.getTeam(vm.tickets[i].assignedTeamId).then(function (team) {
                    vm.tickets[i].team = team;
                });
            }

            vm.order = function (predicate, reverse) {
                vm.ticketsToShow = vm.orderBy(vm.ticketsToShow, predicate, reverse);
            };

            vm.toggleOpenFilter = function () {
                vm.showOpenOnly = !vm.showOpenOnly;
                vm.showClosedOnly = false;
                if (vm.showOpenOnly) {
                    vm.title = "Open Tickets";
                    vm.ticketsToShow = $filter('filter')(vm.tickets, { open: true }, true);
                } else {
                    vm.title = "All Tickets";
                    vm.ticketsToShow = vm.tickets;
                    vm.order('open', true);
                }
            };


            vm.toggleClosedFilter = function () {
                vm.showClosedOnly = !vm.showClosedOnly;
                vm.showOpenOnly = false;
                if (vm.showClosedOnly) {
                    vm.title = "Closed Tickets";
                    vm.ticketsToShow = $filter('filter')(vm.tickets, { open: false }, true);
                } else {
                    vm.title = "All Tickets";
                    vm.ticketsToShow = vm.tickets;
                    vm.order('open', true);
                }
            };

            vm.toggleOpenFilter();

        });
    }
})();