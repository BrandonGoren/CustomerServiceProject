(function () {
    'use strict';
    angular.module('ticketsApp')
        .directive('ticketCharts', ticketCharts);
    
    function ticketCharts () {
            return {
                restrict: 'E',
                replace: 'true',
                templateUrl: '/js/directives/templates/ticket-charts.html',
                controller: TicketChartController,
                controllerAs: 'vm',
                scope: {}
            };
        }

    TicketChartController.$inject = ['$filter', 'ticketService', 'websiteService', 'teamService'];
    function TicketChartController($filter, ticketService, websiteService, teamService) {

        var vm = this;

        vm.colors = ['#33CC33', '#f7464a'];
        vm.openLabels = ["Open", "Closed"];
        vm.teamFilter = false;
        vm.websiteFilter = false;
        vm.tickets = [];
        vm.openData = [];
        vm.teams = [];
        vm.teamLabels = [];
        vm.teamData = [];
        vm.websites = [];
        vm.websiteLabels = [];
        vm.websiteData = [];

        vm.filterTeam = filterTeam;
        vm.filterWebsite = filterWebsite;

        activate();

        function activate() {
            ticketService.getAll().then(function (tickets) {
                vm.tickets = tickets;
                vm.openData = [$filter('filter')(vm.tickets, { open: true }, true).length, $filter('filter')(vm.tickets, { open: false }, true).length];

                teamService.getAll().then(function (teams) {
                    vm.teams = teams;
                    vm.teams.forEach(function (team) {
                        team.issues = $filter('filter')(tickets, { assignedTeamId: team.id }, true);
                        vm.teamData.push(team.issues.length);
                        vm.teamLabels.push(team.name);
                    });
                });

                websiteService.getAll().then(function (websites) {
                    vm.websites = websites;
                    websites.forEach(function (website) {
                        website.issues = $filter('filter')(tickets, { websiteId: website.id }, true);
                        vm.websiteData.push(website.issues.length);
                        vm.websiteLabels.push(website.name);
                    });
                });
            });
        }

        function filterTeam () {
            vm.teamFilter = !vm.teamFilter;
            vm.teamData = [];
            vm.teams.forEach(function (team) {
                if (vm.teamFilter) {
                    vm.teamData.push($filter('filter')(team.issues, { open: true }, true).length);
                } else {
                    vm.teamData.push(team.issues.length);
                }
            });
        }

        function filterWebsite() {
            vm.websiteFilter = !vm.websiteFilter;
            vm.websiteData = [];
            vm.websites.forEach(function (website) {
                if (vm.websiteFilter) {
                    vm.websiteData.push($filter('filter')(website.issues, { open: true }, true).length);
                } else {
                    vm.websiteData.push(website.issues.length);
                }
            });
        }
    }
})();