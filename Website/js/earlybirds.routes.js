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
                controller: 'TicketCreatorController',
                controllerAs: 'vm',
                title: 'Open Ticket'
            })
        .when('/tickets/:ticketId',
            {
                templateUrl: 'templates/ticket-details.html',
                controller: 'TicketDetailController',
                controllerAs: 'vm',
                title: 'Ticket Detail'
            })
        .when('/edit-ticket/:ticketId',
            {
                templateUrl: 'templates/edit-ticket.html',
                controller: 'TicketEditController',
                controllerAs: 'vm',
                title: 'Edit Ticket'
            })
        .when('/teams',
            {
                templateUrl: 'templates/see-teams.html',
                title: 'See Teams'
            })
        .when('/teams/:teamId', 
            {
                templateUrl: 'templates/team-details.html',
                controller: 'TeamDetailController',
                controllerAs: 'vm'
            })
        .when('/websites',
            {
                templateUrl: 'templates/see-websites.html',
            })
        .when('/chat',
        {
            templateUrl: 'templates/chat.html',
            controller: 'ChatController',
            controllerAs: 'vm'
        });

        $locationProvider.html5Mode(true)
    }
})();