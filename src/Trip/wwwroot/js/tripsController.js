//tripsController.js

(function() {

    "use strict";

    //Creating controller

    angular.module("app-trips")
        .controller("tripsController", tripsController);

    function tripsController($http) {

        var vm = this;

        vm.trips = [];

        vm.newTrip = {};

        vm.errorMessage = "";

        vm.isBusy = true;

        $http.get("/api/trip")
            .then(function(response) {
                //Success
                angular.copy(response.data, vm.trips);
            }, function(error) {
                //Error
                vm.errorMessage = "Failed to load data - " + error;
            })
            //Finally will execute in either case
            .finally(function () {
                vm.isBusy = false;
            });

        vm.addTrip = function() {
            vm.isBusy = true;
            vm.errorMessage = "";

            $http.post("/api/trip", vm.newTrip)
                .then(function(response) {
                    //Success
                    vm.trips.push(response.data);
                    vm.newTrip = {};
                }, function() {
                    //Error
                    vm.errorMessage = "Failed to save new trip";
                })
                .finally(function() {
                    vm.isBusy = false;
                });

        };
    }
})();