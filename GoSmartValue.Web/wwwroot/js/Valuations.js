$.getScript({
    url: "https://maps.googleapis.com/maps/api/js?key=AIzaSyCeoY8cKFVbUSyddmZL6kGiU8p3QsCHs-0&libraries=places",
    success: function() {
        // console.log("got google places API JavaScripts")
    }
});

var ValuationRequest = function() {
    var vars = {
            LocationId: "public variable",
            WardId: "public variable",
            PlotNo: "public variable",
            LandUseID: "public variable",
            SizeUnitsTypeID: "public variable",
            Size: "public variable"
        },
        root = this;
    this.construct = function(options) { $.extend(vars, options); };
};

function formatCurrency(amount) {
    var neg = false;
    if (amount < 0) {
        neg = true;
        amount = Math.abs(amount);
    }
    return (neg ? "-BWP " : 'BWP ') +
        parseFloat(amount, 10).toFixed(2).replace(/(\d)(?=(\d{3})+\.)/g, "$1,").toString();
}

function ClearStreetName() {
    $('#StreetName').val("");
}

function SetLocalities(locationId, locationName) {
    $('#LocalityId').empty();
    ClearStreetName();
    $.get({
        url: "/api/Valuations/GetLocalities?locationId=" + locationId + "&locationName=" + locationName,
        success: function(result) {
            //console.log("Success : localities retrieved..Count: " + result.length);
            $.each(result,
                function() {
                    let locality = this;
                    $('#LocalityId')
                        .append('<option value="' +
                            locality.id +
                            '" data-tokens="' +
                            locality.name +
                            '">' +
                            locality.name +
                            '</option>');
                });
        }
    });
}


function SetStreets(localityId, localityName) {
    
    $('#street-list').empty();
    ClearStreetName();
    $('#street-list').append(`<option>No Streets found in "${localityName}"</option>`);
    $.get({
        url: '/api/Valuations/GetStreets?localityId=' + localityId + '&localityName=' + localityName,
        success: function (result) {
            $('#street-list').empty();
           $.each(result,
                    function() {
                        $('#street-list')
                            .append('<option value="' +
                                this.streetName +
                                '" data-tokens="' +
                                this.streetName +
                                '">' +
                                this.streetName +
                                '</option>');
                    });
        }
    });
}

$(document).ready(function() {

    var map, infowindow, mapOptions = { zoom: 8, center: { lat: -24.6282, long: 25.9231 } };
    console.log("Document has loaded and ready...");

    document.getElementById("ownership").checked
        ? $("#ownershipQuestions").show()
        : $("#ownershipQuestions").hide();

    $.ajax({
        url: "/api/Valuations/GetLocations",
        success: function(result) {
            var locations = $("#Location");
            $.each(result, function() {
                locations.append(new Option(this.name, this.id));
            });
        }
    });

    for (let i = (new Date).getFullYear(); i > 1970; i--)
        $("#PurchaseYear")
            .append('<option value="' + i + '" data-tokens="' + i + '">' + i + '</option>');

    $("#ownership").on("change",
        function() {
            document.getElementById("ownership").checked
                ? $('#ownershipQuestions').show()
                : $('#ownershipQuestions').hide();
        });

    $('#street-list').on('click',
        function () {
            console.log('street clicked..' + $('#street-list').val());
            $('#StreetName').val($('#street-list').val());
        });

    $('#GetValuation').on('click',
        function() {
            console.log('Get Valuation button clicked..');
            $.ajax({
                type: 'post',
                contentType: 'application/json;',
                dataType: 'json',
                url: '/api/Valuations/GetValuation',
                data: JSON.stringify({
                    LocationId: $("#Location :selected").val(),
                    LocalityId: $("#Localities :selected").val(),
                    StreetName: $("#StreetName").val(),
                    PlotNo: $("#PlotNo").val(),
                    PropertyType: $("#PropertyType :selected").val(),
                    LandUse: $("#LandUse :selected").val(),
                    Size: $("#Size").val(),
                    PurchasePrice: $("#PurchasePrice").val() || 0,
                    PurchaseDate: new Date($("#PurchasePrice").val(), 1)
                }),
                success: function(result) {
                    console.log("successful call to GetValuations..");
                    $("#ValuationReference").text(result.referenceNumber);
                    $("#ValuationEstimatedValue").text(formatCurrency(result.estimatedValue));
                    $("#ValuationRequest").hide();
                    $("#ValuationResultfieldSet").show();
                }
            });
        });

    $("#GetFullReport").on("click",
        function() {
            const valuationReference = $("#ValuationReference").Text;
            console.log("Get Full Report button clicked..Reference: " + valuationReference);

            $.ajax({
                type: "Post",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                url: '/user/Valuations/GetFullReport',
                data: JSON.stringify({
                    Reference: valuationReference,
                    PropertyDetailsId: $("#PropertyDetailsId").Text,
                    EstimatedValue: $("#EstimatedValue").Text
                })
            });
        });
});