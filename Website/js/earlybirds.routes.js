(function () {
    'use strict';

    angular.module('ticketsApp')
    .config(config);

    config.$inject = ['$routeProvider', '$locationProvider'];

    function config($routeProvider, $locationProvider) {
        $routeProvider.when('/',
            {
            templateUrl: 'templates/home.html',
            title: 'Home'
            })
        .when('/see-tickets',
            {
                templateUrl: 'templates/see-tickets.html',
                title: 'See Tickets'
            })
        .when('/open-ticket',
            {
                templateUrl: 'templates/open-ticket.html',
                title: 'Open Ticket'
            })
        .when('/tickets/:ticketId',
            {
                templateUrl: 'templates/ticket-details.html',
                controller: 'TicketDetailController',
                title: 'Ticket Detail'
            })
        .when('/edit-ticket/:ticketId',
            {
                templateUrl: 'templates/edit-ticket.html',
                controller: 'TicketEditController',
                title: 'Edit Ticket'
            })
        .when('/see-teams',
            {
                templateUrl: 'templates/see-teams.html',
                title: 'See Teams'
            });

        $locationProvider.html5Mode(true)
    }
})();