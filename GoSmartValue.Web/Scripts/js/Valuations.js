$.getScript({
    url: "https://maps.googleapis.com/maps/api/js?key=AIzaSyCeoY8cKFVbUSyddmZL6kGiU8p3QsCHs-0&libraries=places",
    success: function () {
        // console.log("got google places API JavaScripts")
    }
});

var ValuationRequest = function () {
    var vars = {
        LocationId: "public variable",
        WardId: "public variable",
        PlotNo: "public variable",
        LandUseID: "public variable",
        SizeUnitsTypeID: "public variable",
        Size: "public variable"
    },
        root = this;
    this.construct = function (options) { $.extend(vars, options); };
};

function formatCurrency(amount) {
    var neg = false;
    if (amount < 0) {
        neg = true;
        amount = Math.abs(amount);
    }
    return (neg ? "-BWP " : "BWP ") +
        parseFloat(amount, 10).toFixed(2).replace(/(\d)(?=(\d{3})+\.)/g, "$1,").toString();
}

var locationsArray = new Array();
var localitiesArray = new Array();
var districtsArray = new Array();

function SetLocalities() {
    var locationName = $("#LocationName").val();
    var locationId = locationsArray.find(l => l.name === locationName).id;
    //console.log("LocationId: " + locationId);
    var localities = new Array();
    $.get({
        url: `/api/locations/GetLocalities?locationId=${locationId}&locationName=${locationName}`,
        success: function (result) {
            //console.log("Success : localities retrieved..Count: " + result.length);
            localitiesArray = result;
            $.each(result,
                function () {
                    let locality = this;
                    localities.push(locality.name);
                });
            //console.log("Localities: " + localities);
            autocomplete(document.getElementById("LocalityName"), localities);
        }
    });
}

function SetStreets() {
    var localityName = $("#LocalityName").val();
    var localityId = localitiesArray.find(l => l.name === localityName).id;
    var streets = new Array();
    $.get({
        url: `/api/locations/GetStreets?localityId=${localityId}&localityName=${localityName}`,
        success: function (result) {
            $.each(result,
                function () {
                    streets.push(this.name);
                });
            autocomplete(document.getElementById("StreetName"), streets);
        }
    });
}

function autocomplete(targetTextboxElement, dataArray) {
    /*the autocomplete function takes two arguments,
    the text field element and an array of possible autocompleted values:*/
    var currentFocus;
    /*execute a function when someone writes in the text field:*/
    targetTextboxElement.addEventListener("input", function (e) {
        var a, b, i, val = this.value;
        /*close any already open lists of autocompleted values*/
        closeAllLists();
        if (!val) { return false; }
        currentFocus = -1;
        /*create a DIV element that will contain the items (values):*/
        a = document.createElement("DIV");
        a.setAttribute("id", this.id + "autocomplete-list");
        a.setAttribute("class", "autocomplete-items");
        /*append the DIV element as a child of the autocomplete container:*/
        this.parentNode.appendChild(a);
        /*for each item in the array...*/
        for (i = 0; i < dataArray.length; i++) {
            /*check if the item starts with the same letters as the text field value:*/
            if (dataArray[i].substr(0, val.length).toUpperCase() === val.toUpperCase()) {
                /*create a DIV element for each matching element:*/
                b = document.createElement("DIV");
                /*make the matching letters bold:*/
                b.innerHTML = "<strong>" + dataArray[i].substr(0, val.length) + "</strong>";
                b.innerHTML += dataArray[i].substr(val.length);
                /*insert a input field that will hold the current array item's value:*/
                b.innerHTML += "<input type='hidden' value='" + dataArray[i] + "'>";
                /*execute a function when someone clicks on the item value (DIV element):*/
                b.addEventListener("click", function (e) {
                    /*insert the value for the autocomplete text field:*/
                    targetTextboxElement.value = this.getElementsByTagName("input")[0].value;
                    /*close the list of autocompleted values,
                    (or any other open lists of autocompleted values:*/
                    closeAllLists();
                });
                a.appendChild(b);
            }
        }
    });
    /*execute a function presses a key on the keyboard:*/
    targetTextboxElement.addEventListener("keydown", function (e) {
        var x = document.getElementById(this.id + "autocomplete-list");
        if (x) x = x.getElementsByTagName("div");
        if (e.keyCode === 40) {
            /*If the arrow DOWN key is pressed,
            increase the currentFocus variable:*/
            currentFocus++;
            /*and and make the current item more visible:*/
            addActive(x);
        } else if (e.keyCode === 38) { //up
            /*If the arrow UP key is pressed,
            decrease the currentFocus variable:*/
            currentFocus--;
            /*and and make the current item more visible:*/
            addActive(x);
        } else if (e.keyCode === 13) {
            /*If the ENTER key is pressed, prevent the form from being submitted,*/
            e.preventDefault();
            if (currentFocus > -1) {
                /*and simulate a click on the "active" item:*/
                if (x) x[currentFocus].click();
            }
        }
    });
    function addActive(x) {
        /*a function to classify an item as "active":*/
        if (!x) return false;
        /*start by removing the "active" class on all items:*/
        removeActive(x);
        if (currentFocus >= x.length) currentFocus = 0;
        if (currentFocus < 0) currentFocus = (x.length - 1);
        /*add class "autocomplete-active":*/
        x[currentFocus].classList.add("autocomplete-active");
    }
    function removeActive(x) {
        /*a function to remove the "active" class from all autocomplete items:*/
        for (var i = 0; i < x.length; i++) {
            x[i].classList.remove("autocomplete-active");
        }
    }
    function closeAllLists(elmnt) {
        /*close all autocomplete lists in the document,
        except the one passed as an argument:*/
        var x = document.getElementsByClassName("autocomplete-items");
        for (var i = 0; i < x.length; i++) {
            if (elmnt !== x[i] && elmnt !== targetTextboxElement) {
                x[i].parentNode.removeChild(x[i]);
            }
        }
    }
    /*execute a function when someone clicks in the document:*/
    document.addEventListener("click", function (e) {
        closeAllLists(e.target);
    });
}

$(document).ready(function () {

    var locations = new Array();
    $.ajax({
        url: "/api/locations/GetLocations",
        success: function (result) {
            //console.log(result);
            $.each(result, function () {
                locationsArray.push(this);
                locations.push(this.name);
            });
            //console.log(locationsArray);
        }
    });
    autocomplete(document.getElementById("LocationName"), locations);

    var districts = new Array();
    $.ajax({
        url: "/api/locations/GetDistricts",
        success: function (result) {
            //console.log(result);
            $.each(result, function () {
                districts.push(this.name);
            });
            //console.log(locationsArray);
        }
    });
    autocomplete(document.getElementById("DistrictsName"), locations);

    $("#GetValuation").on("click",
        function () {
            console.log("Get Valuation button clicked..");
            $.ajax({
                type: "post",
                contentType: "application/json;",
                dataType: "json",
                url: "/api/locations/GetValuation",
                data: JSON.stringify({
                    LocationId: $("#Location :selected").val(),
                    LocationName: $("#LocationName :selected").val(),
                    LocalityId: $("#Localities :selected").val(),
                    LocalityName: $("#LocalityName :selected").val(),
                    StreetName: $("#StreetName").val(),
                    PlotNo: $("#PlotNo").val(),
                    PropertyType: $("#PropertyType :selected").val(),
                    LandUse: $("#LandUse :selected").val(),
                    Size: $("#Size").val(),
                    PurchasePrice: $("#PurchasePrice").val() || 0,
                    PurchaseDate: new Date($("#PurchasePrice").val(), 1)
                }),
                success: function (result) {
                    console.log("successful call to GetValuations..");
                    $("#ValuationReference").text(result.referenceNumber);
                    $("#ValuationEstimatedValue").text(formatCurrency(result.estimatedValue));
                    $("#ValuationRequest").hide();
                    $("#ValuationResultfieldSet").show();
                }
            });
        });

    $("#GetFullReport").on("click",
        function () {
            const valuationReference = $("#ValuationReference").Text;
            //console.log(`Get Full Report button clicked..Reference: ${valuationReference}`);

            $.ajax({
                type: "Post",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                url: "/user/locations/GetFullReport",
                data: JSON.stringify({
                    Reference: valuationReference,
                    PropertyDetailsId: $("#PropertyDetailsId").Text,
                    EstimatedValue: $("#EstimatedValue").Text
                })
            });
        });
});