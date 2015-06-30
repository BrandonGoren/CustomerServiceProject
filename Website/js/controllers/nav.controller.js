(function () {
    'use strict';

    var ticketsApp = angular.module('ticketsApp');
    ticketsApp.controller("NavController", function ($location) {
        var vm = this;
        vm.isActive = function (viewLocation) {
            return viewLocation === $location.path();
        }
    });
})();