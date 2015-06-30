(function () {
    'use strict';

    angular
        .module('ticketsApp')
        .factory('teamService', ['$http', function ($http) {
            return {
                getAll: function () {
                    return $http.get('http://localhost:2001/teams').
                        error(function (error) {
                            alert(error);
                            return error;
                        }).then(function (response) {
                            return response.data;
                        });
                },

                getTeam: function (teamId) {
                    return $http.get('http://localhost:2001/teams/' + teamId).
                    error(function (error) {
                        alert(error);
                        return error;
                    }).then(function (response) {
                        return response.data;
                    });
                },

                getTeamIssues: function (teamId) {
                    return $http.get('http://localhost:2001/teams/' + teamId + '/issues').
                    error(function (error) {
                        alert(error);
                        return error;
                    }).then(function (response) {
                        return response.data;
                    });
                }
            }
        }])
})();