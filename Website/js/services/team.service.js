﻿(function () {
    'use strict';

    angular
        .module('ticketsApp')
        .factory('teamService', teamService);

    teamService.$inject = ['$http'];
    function teamService($http) {
        return {
            getAll: getAll,
            getTeam: getTeam,
            getTeamTickets: getTeamTickets,
            getTeamAgents: getTeamAgents,
            createTeam: createTeam,
            deleteTeam: deleteTeam
        }

        function getAll() {
            return $http.get('http://localhost:2004/teams').
                error(function (error) {
                    console.log(error);
                    return error;
                }).then(function (response) {
                    return response.data;
                });
        }

        function getTeam(teamId) {
            return $http.get('http://localhost:2004/teams/' + teamId).
            error(function (error) {
                console.log(error);
                return error;
            }).then(function (response) {
                return response.data;
            });
        }

        function getTeamTickets(teamId) {
            return $http.get('http://localhost:2004/teams/' + teamId + '/issues').
            error(function (error) {
                console.log(error);
                return error;
            }).then(function (response) {
                return response.data;
            });
        }

        function getTeamAgents(teamId) {
            return $http.get('http://localhost:2004/teams/' + teamId + '/agents').
            error(function (error) {
                console.log(error);
                return error;
            }).then(function (response) {
                return response.data;
            });
        }

        function createTeam(team) {
            return $http.post('http://localhost:2004/teams', team).
            success(function (response) {
                console.log(response);
            }).error(function (error) {
                alert(error)
            });
        }

        function deleteTeam(team) {
            return $http.delete('http://localhost:2004/teams/' + team.id, team).
                success(function (response) {
                    console.log(response);
                }).error(function (error) {
                    console.log(error)
                }).then(function (response) {
                    return response.data;
                });
        }
    }
})();