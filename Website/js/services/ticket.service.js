(function () {
    'use strict';

    angular
        .module('ticketsApp')
        .factory('ticketService', ['$http', function ($http) {
            return {

                getAll: function () {
                    return $http.get('http://localhost:2001/issues').
                        error(function (error) {
                            alert(error);
                            return error;
                        }).then(function (response) {
                            return response.data;
                        })
                },

                getTicket: function (ticketId) {
                    return $http.get('http://localhost:2001/issues/' + ticketId).
                    error(function (error) {
                        return error
                    }).then(function (response) {
                        return response.data;
                    });
                },

                saveTicket: function (ticket) {
                    return $http.post('http://localhost:2001/issues', ticket).
                        success(function (response) {
                            console.log(response);
                        }).error(function (error) {
                            alert(error)
                        });
                },

                addNote: function (ticket, note) {
                    return $http.put('http://localhost:2001/issues/' + ticket.id + '/add-note', note).
                    error(function (error) {
                        alert(error)
                    }).then(function (response) {
                        return response.data;
                    });
                },

                editTicket: function (ticket) {
                    return $http.put('http://localhost:2001/issues/' + ticket.id, ticket).
                        success(function (response) {
                            console.log(response);
                        }).error(function (error) {
                            alert(error)
                        }).then(function (response) {
                            return response.data;
                        });
                },

                deleteTicket: function (ticket) {
                    return $http.delete('http://localhost:2001/issues/' + ticket.id, ticket).
                        success(function (response) {
                            console.log(response);
                        }).error(function (error) {
                            alert(error)
                        }).then(function (response) {
                            return response.data;
                        });
                },

                closeTicket: function (ticketId) {
                    return $http.put('http://localhost:2001/issues/' + ticketId + '/close-issue').success(function (response) {
                        console.log('ticket closed');
                    }).error(function (error) {
                        alert(error)
                    }).then(function (response) {
                        return response.data;
                    });
                }
            }
        }])
})();