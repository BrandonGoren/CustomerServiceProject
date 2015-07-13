(function () {
    'use strict';

    angular
        .module('ticketsApp')
        .factory('ticketService', ticketService);

    ticketService.$inject = ['$http'];
            
    function ticketService($http) {
        return {
            getAll: getAll,
            getTicket: getTicket, 
            saveTicket: saveTicket,
            addNote: addNote,
            editTicket: editTicket,
            deleteTicket: deleteTicket,
            closeTicket: closeTicket
        };

        function getAll () {
            return $http.get('http://localhost:2001/issues').
                error(function (error) {
                    alert(error);
                    return error;
                }).then(function (response) {
                    return response.data;
                })
        }

        function getTicket (ticketId) {
            return $http.get('http://localhost:2001/issues/' + ticketId).
            error(function (error) {
                return error
            }).then(function (response) {
                return response.data;
            });
        }

        function saveTicket (ticket) {
            return $http.post('http://localhost:2001/issues', ticket).
                success(function (response) {
                    console.log(response);
                }).error(function (error) {
                    console.log(error)
                });
        }

        function addNote (ticket, note) {
            return $http.put('http://localhost:2001/issues/' + ticket.id + '/add-note', note).
            error(function (error) {
                console.log(error)
            }).then(function (response) {
                return response.data;
            });
        }

        function editTicket (ticket) {
            return $http.put('http://localhost:2001/issues/' + ticket.id, ticket).
                success(function (response) {
                    console.log(response);
                }).error(function (error) {
                    alert(error)
                }).then(function (response) {
                    return response.data;
                });
        }

        function deleteTicket (ticket) {
            return $http.delete('http://localhost:2001/issues/' + ticket.id, ticket).
                success(function (response) {
                    console.log(response);
                }).error(function (error) {
                    console.log(error)
                }).then(function (response) {
                    return response.data;
                });
        }

        function closeTicket (ticketId) {
            return $http.put('http://localhost:2001/issues/' + ticketId + '/close-issue').success(function (response) {
                console.log('ticket closed');
            }).error(function (error) {
                console.log(error)
            }).then(function (response) {
                return response.data;
            });
        }
    }
})();