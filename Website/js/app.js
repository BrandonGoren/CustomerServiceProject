(function () {
    'use strict';

    var ticketsApp = angular.module('ticketsApp', ['ngRoute', 'chart.js'])
    .config(function ($routeProvider, $locationProvider) {
        $routeProvider.when('/see-tickets',
            {
                templateUrl: 'templates/see-tickets.html',
                //controller: 'TicketController',
                title: 'See Tickets'
            });
        $routeProvider.when('/', {
            templateUrl: 'templates/home.html',
            title: 'Home'
        });
        $routeProvider.when('/open-ticket',
            {
                templateUrl: 'templates/open-ticket.html',
                title: 'Open Ticket'
            });
        $routeProvider.when('/tickets/:ticketId',
            {
                templateUrl: 'templates/ticket-details.html',
                controller: 'TicketDetailController',
                title: 'Ticket Detail'
            });
        $routeProvider.when('/edit-ticket/:ticketId',
        {
            templateUrl: 'templates/edit-ticket.html',
            controller: 'TicketEditController',
            title: 'Edit Ticket'
        });

        $routeProvider.when('/see-teams',
        {
            templateUrl: 'templates/see-teams.html',
            title: 'See Teams'
        });

        $locationProvider.html5Mode(true)
    })
})();