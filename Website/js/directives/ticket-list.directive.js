(function () {
    'use strict';
    angular.module('ticketsApp')
        .directive('ticketList', ticketList);

    function ticketList() {
        return {
            restrict: 'E',
            replace: 'true',
            templateUrl: '/js/directives/templates/ticket-list.html',
            controller: TicketController,
            controllerAs: 'vm',
            scope: {}
        };
    }

    TicketController.$inject = ['$filter', '$location', 'ticketService', 'websiteService', 'teamService'];
    function TicketController($filter, $location, ticketService, websiteService, teamService) {
        var vm = this;

        vm.title = "Open Tickets";
        vm.reverse = false;
        vm.showOpenOnly = false;
        vm.showClosedOnly = false;
        vm.tickets = [];
        vm.ticketsToShow = vm.tickets;

        vm.orderBy = $filter('orderBy');
        vm.order = order;
        vm.toggleOpenFilter = toggleOpenFilter;
        vm.toggleClosedFilter = toggleClosedFilter;
        vm.goToTicket = goToTicket;

        activate();

        function activate() {
            ticketService.getAll().then(function (tickets) {
                vm.tickets = tickets;
                vm.ticketsToShow = vm.tickets;
                for (let i = 0; i < vm.tickets.length; i++) {

                    websiteService.getWebsite(vm.tickets[i].websiteId).then(function (website) {
                        vm.tickets[i].website = website;
                    });

                    teamService.getTeam(vm.tickets[i].assignedTeamId).then(function (team) {
                        vm.tickets[i].team = team;
                    });
                }
                vm.toggleOpenFilter();
            });
        }

        function order(predicate, reverse) {
            vm.ticketsToShow = vm.orderBy(vm.ticketsToShow, predicate, reverse);
        }

        function goToTicket(ticketId) {
            $location.path('tickets/' + ticketId);
        }

        function toggleOpenFilter() {
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
        }

        function toggleClosedFilter() {
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
            }
        }
    }
)();