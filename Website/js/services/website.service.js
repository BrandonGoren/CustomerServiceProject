(function () {
    'use strict';

    angular
        .module('ticketsApp')
        .factory('websiteService', websiteService);

    websiteService.$inject = ['$http'];

    function websiteService($http) {
        return {
            getAll: getAll,
            getWebsite: getWebsite,
            getWebsiteIssues: getWebsiteIssues
        };

        function getAll() {
            return $http.get('http://localhost:2001/websites').
                error(function (error) {
                    alert(error);
                    return error;
                }).then(function (response) {
                    return response.data;
                });
        }

        function getWebsite(websiteId) {
            return $http.get('http://localhost:2001/websites/' + websiteId).
                error(function (error) {
                    return error
                }).then(function (response) {
                    return response.data;
                });
        }

        function getWebsiteIssues(websiteId) {
            return $http.get('http://localhost:2001/websites/' + websiteId + '/issues').
                error(function (error) {
                    alert(error);
                    return error;
                }).then(function (response) {
                    return response.data;
                });
        }
    }
})();