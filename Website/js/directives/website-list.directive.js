(function () {
    'use strict';
    var ticketsApp = angular.module('ticketsApp')
        .directive('websiteList', websiteList);

    function websiteList() {
        return {
            restrict: 'E',
            replace: 'true',
            templateUrl: '/js/directives/templates/website-list.html',
            controller: WebsiteController,
            controllerAs: 'vm',
            scope: {}
        }
    }

    WebsiteController.$inject = ['$location', 'websiteService'];
    function WebsiteController($location, websiteService) {
        var vm = this;
        vm.title = "Websites";
        vm.websites = [];
        vm.goToWebsite = goToWebsite;
        activate();

        function activate() {
            websiteService.getAll().then(function (websites) {
                vm.websites = websites;
                vm.websites.forEach(function (website) {
                    websiteService.getWebsiteIssues(website.id).then(function (issues) {
                        website.issues = issues;
                    });
                })
            });
        }

        function goToWebsite(websiteId) {
            //$location.path('websites/' + websiteId);
        }
    }
})();