// Javascript for taking the location of the user

function geoLocation() {
    if (navigator.geolocation) {
        navigator.geolocation.getCurrentPosition(function (position) {
            var userLatitude = position.coords.latitude;
            var userLongitude = position.coords.longitude;

            $('#userLatitude').val(userLatitude);
            $('#userLongitude').val(userLongitude);

            // Set the action type
            $('form').submit();

            $('#loader').hide();

        });
    } else {
        $('#loader').hide();
        alert('Geolocation is not supported by this browser.');
    }
}