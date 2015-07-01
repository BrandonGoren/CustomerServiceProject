(function () {
    'use strict';

    angular
        .module('ticketsApp')
        .factory('teamService', teamService);

    teamService.$inject = ['$http'];
    function teamService($http) {
        return {
            getAll: getAll,
            getTeam: getTeam,
            getTeamIssues: getTeamIssues
        }

        function getAll() {
            return $http.get('http://localhost:2001/teams').
                error(function (error) {
                    alert(error);
                    return error;
                }).then(function (response) {
                    return response.data;
                });
        }

        function getTeam(teamId) {
            return $http.get('http://localhost:2001/teams/' + teamId).
            error(function (error) {
                alert(error);
                return error;
            }).then(function (response) {
                return response.data;
            });
        }

        function getTeamIssues(teamId) {
            return $http.get('http://localhost:2001/teams/' + teamId + '/issues').
            error(function (error) {
                alert(error);
                return error;
            }).then(function (response) {
                return response.data;
            });
        }
    }
})();