    // Wait for the DOM content to load
document.addEventListener('DOMContentLoaded', function() {
    addOnMap(lon, lat); // Call the function with lon and lat
});

function addOnMap(lon, lat) {
        var mapOptions = {
    center: [lat, lon],
zoom: 15
        }

// Creating a map object
var map = new L.map('map', mapOptions); // Change ‘map’ to 'map'

// Creating a Layer object
var layer = new L.TileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png');

// Adding layer to the map
map.addLayer(layer);

var marker = L.marker([lat, lon]).addTo(map);

    }
