﻿/**
* This is a simple JavaScript demonstration of how to call MapBox API to load the maps.
* I have set the default configuration to enable the geocoder and the navigation control.
* https://www.mapbox.com/mapbox-gl-js/example/popup-on-click/
*
* @author Jian Liew <jian.liew@monash.edu>
*/
const TOKEN = "pk.eyJ1IjoicG9vcm5paSIsImEiOiJjazFvZ3FydGgwbTN3M2xsdmJrMXk0OTdlIn0.WsvBou-iMQdPLidVQkvFrw";
var locations = [];
// The first step is obtain all the latitude and longitude from the HTML
// The below is a simple jQuery selector
$(".coordinates").each(function () {

   var longitude = $(".longitude", this).text().trim();
   var latitude = $(".latitude", this).text().trim();
    var description = $(".description", this).text().trim();
    // Create a point data structure to hold the values.
    var point = {
        "latitude": latitude,
        "longitude": longitude,
        "description": description
    };
    // Push them all into an array.
    locations.push(point);
});
var data = [];
for (i = 0; i < locations.length; i++) {
    var feature = {
        "type": "Feature",
        "properties": {
            "description": locations[i].description,
            "icon": "lodging-15"
        },
        "geometry": {
            "type": "Point",
            "coordinates": [locations[i].longitude, locations[i].latitude]
        }
    };
    data.push(feature)
}
function forwardGeocoder(query) {
    var matchingFeatures = [];
    for (var i = 0; i < data.length; i++) {
        var feature = data[i];
        // handle queries with different capitalization than the source data by calling toLowerCase()
        if (feature.properties.description.toLowerCase().search(query.toLowerCase()) !== -1) {
            // add a tree emoji as a prefix for custom data results
            // using carmen geojson format: https://github.com/mapbox/carmen/blob/master/carmen-geojson.md
            feature['place_name'] =  feature.properties.description;
            feature['center'] = feature.geometry.coordinates;
            feature['place_type'] = ['lodging'];
            matchingFeatures.push(feature);
        }
    }
    return matchingFeatures;
}
mapboxgl.accessToken = TOKEN;
var map = new mapboxgl.Map({
    container: 'map',
    style: 'mapbox://styles/mapbox/streets-v10',
    zoom: 11,
    marker: true,
    center: [locations[0].longitude, locations[0].latitude]
});
map.addControl(new MapboxGeocoder({
    accessToken: mapboxgl.accessToken,
    localGeocoder: forwardGeocoder,
    zoom: 14,
    placeholder: "Enter search",
    mapboxgl: mapboxgl
}));

// Add geolocate control to the map.
map.addControl(new mapboxgl.GeolocateControl({
    positionOptions: {
        enableHighAccuracy: true
    },
    trackUserLocation: true
}));


map.on('load', function () {
    // Add a layer showing the places.
    map.addLayer({
        "id": "places",
        "type": "symbol",
        "source": {
            "type": "geojson",
            "data": {
                "type": "FeatureCollection",
                "features": data
            }
        },
        "layout": {
            "icon-image": "{icon}",
            "icon-allow-overlap": true
        }
    });

    //map.addControl(new MapboxGeocoder({
    //    accessToken: mapboxgl.accessToken
    //}));;
    map.addControl(new mapboxgl.NavigationControl());
    // When a click event occurs on a feature in the places layer, open a popup at the
    // location of the feature, with description HTML from its properties.
    map.on('click', 'places', function (e) {
        var coordinates = e.features[0].geometry.coordinates.slice();
        var description = e.features[0].properties.description;
        // Ensure that if the map is zoomed out such that multiple
        // copies of the feature are visible, the popup appears
        // over the copy being pointed to.
        while (Math.abs(e.lngLat.lng - coordinates[0]) > 180) {
            coordinates[0] += e.lngLat.lng > coordinates[0] ? 360 : -360;
        }
        new mapboxgl.Popup()
            .setLngLat(coordinates)
            .setHTML(description)
            .addTo(map);
    });
    // Change the cursor to a pointer when the mouse is over the places layer.
    map.on('mouseenter', 'places', function () {
        map.getCanvas().style.cursor = 'pointer';
    });
    // Change it back to a pointer when it leaves.
    map.on('mouseleave', 'places', function () {
        map.getCanvas().style.cursor = '';
    });
});