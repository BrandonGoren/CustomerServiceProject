(function () {
    'use strict';

    angular
        .module('ticketsApp')
        .factory('websiteService', ['$http', function ($http) {
            return {
                getAll: function () {
                    return $http.get('http://localhost:2001/websites').
                        error(function (error) {
                            alert(error);
                            return error;
                        }).then(function (response) {
                            return response.data;
                        });
                },

                getWebsite: function (websiteId) {
                    return $http.get('http://localhost:2001/websites/' + websiteId).
                    error(function (error) {
                        return error
                    }).then(function(response) {
                        return response.data;
                    });
                },

                getWebsiteIssues: function (websiteId) {
                    return $http.get('http://localhost:2001/websites/' + websiteId + '/issues').
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