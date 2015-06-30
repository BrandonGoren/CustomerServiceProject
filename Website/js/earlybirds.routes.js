(function () {
    'use strict';

    angular.module('ticketsApp')
    .config(config);
    
    config.$inject = ['$routeProvider', '$locationProvider'];

    function config($routeProvider, $locationProvider) {
        $routeProvider.when('/see-tickets',
            {
                templateUrl: 'templates/see-tickets.html',
                //controller: 'TicketController',
                title: 'See Tickets'
            });
        $routeProvider.when('/', {
            templateUrl: 'templates/see-tickets.html',
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
    }
})();