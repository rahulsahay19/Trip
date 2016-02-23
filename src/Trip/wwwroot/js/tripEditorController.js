// tripEditorController.js

(function() {

    "use strict";

    angular.module("app-trips").controller("tripEditorController", tripEditorController);

    function tripEditorController($routeParams, $http) {
        var vm = this;

        vm.tripName = $routeParams.tripName;

        vm.stops = [];
        vm.errorMessage = "";
        vm.isBusy = true;
        vm.newStop = {};

        $http.get("/api/trip/" + vm.tripName + "/stop")
            .then(function(response) {
                //Success
                angular.copy(response.data, vm.stops);
                _showMaps(vm.stops);
            }, function(err) {
                //Error
                vm.errorMessage = "Failed to load stops - " + err;
            }).finally(function() {
                vm.isBusy = false;
            });

        vm.addStop = function() {
            vm.isBusy = true;

            $http.post("/api/trip/" + vm.tripName + "/stop", vm.newStop)
                .then(function(response) {
                    //Success
                    vm.stops.push(response.data);
                    _showMaps(vm.stops);
                    vm.newStop = {};
                }, function() {
                    //Error
                    vm.errorMessage = "Failed to add new stop";
                }).finally(function() {
                    vm.isBusy = false;
                });
        }
    }

    function _showMaps(stops) {
        if (stops && stops.length > 0) {
            
            //Mapping via underscore lib
            var mapStops = _.map(stops, function (item) {
                return {
                    lat: item.latitude,
                    long: item.longitude,
                    info: item.name
                };
            });

            // Show Map
            travelMap.createMap({
                stops: mapStops,
                selector: "#map",
                currentStop: 1,
                initialZoom: 3
            });
        }
    }
})();