(function () {
    'use strict';

    angular.module('ticketsApp')
        .controller("NavController", NavController);

    NavController.$inject = ['$location'];
    function NavController ($location) {
        var vm = this;
        vm.isActive = isActive;
        
        function isActive (viewLocation) {
            return viewLocation === $location.path();
        }
    }
})();