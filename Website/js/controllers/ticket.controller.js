(function () {
    'use strict';

    var ticketsApp = angular.module('ticketsApp');
    ticketsApp.controller("TicketController", function ($filter, ticketService, websiteService, teamService) {
        var vm = this;
        ticketService.getAll().then(function (tickets) {
            vm.orderBy = $filter('orderBy');
            vm.reverse = false;
            vm.tickets = tickets;
            vm.ticketsToShow = vm.tickets;
            console.log(vm.ticketsToShow);
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
                    vm.ticketsToShow = $filter('filter')(vm.tickets, { open: true }, true);
                } else {
                    vm.ticketsToShow = vm.tickets;
                }
            }


            vm.toggleClosedFilter = function () {
                vm.showClosedOnly = !vm.showClosedOnly;
                vm.showOpenOnly = false;
                if (vm.showClosedOnly) {
                    vm.ticketsToShow = $filter('filter')(vm.tickets, { open: false }, true);
                } else {
                    vm.ticketsToShow = vm.tickets;
                }
            }

            vm.toggleOpenFilter();

        });
    });
})();