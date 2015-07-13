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
            getWebsiteIssues: getWebsiteIssues,
            addWebsite: addWebsite,
            deleteWebsite: deleteWebsite
        };

        function getAll() {
            return $http.get('http://localhost:2001/websites')
                .error(function (error) {
                    alert(error);
                    return error;
                }).then(function (response) {
                    return response.data;
                });
        }

        function getWebsite(websiteId) {
            return $http.get('http://localhost:2001/websites/' + websiteId)
                .error(function (error) {
                    return error
                }).then(function (response) {
                    return response.data;
                });
        }

        function getWebsiteIssues(websiteId) {
            return $http.get('http://localhost:2001/websites/' + websiteId + '/issues').
                error(function (error) {
                    console.log(error);
                    return error;
                }).then(function (response) {
                    return response.data;
                });
        }

        function addWebsite(website) {
            return $http.post('http://localhost:2001/websites', website).
                success(function (response) {
                    console.log(response);
                }).error(function (error) {
                    console.log(error);
                });
        }

        function deleteWebsite(website) {
            return $http.delete('http://localhost:2001/websites/' + website.id, website)
                .then(function (response) {
                    return response.data;
                })
        }
    }
})();