$(document).ready(function () {
    var countryId = GetCurrentCountryId();
    var countriesArray = GetCountries();
    var provincesArray = GetProvinces(countryId);
    var provinceId = GetCurrentProvinceId();
    var model = new LocationViewModel(countriesArray, countryId, provincesArray, provinceId);
    SetUserId(model);
    ko.applyBindings(model);
});

var Country = function (id, name) {
    this.id = id;
    this.name = name;
};

var Province = function (id, name, countryId) {
    this.id = id;
    this.name = name;
    this.countryId = countryId;
};

var LocationViewModel = function (countriesArray, countryId, provincesArray, provinceId) {
    var self = this;
    self.availableCountries = ko.observableArray(countriesArray);
    self.currentUser = ko.observable();

    if (countryId) {
        self.selectedCountry = ko.observable(self.availableCountries.find("id", countryId)).extend({
            required: { message: "Country Must Be Selected" }
        });
    }
    else {
        self.selectedCountry = ko.observable().extend({
            required: { message: "Country Must Be Selected" }
        });
    }

        self.availableProvinces = ko.observableArray(provincesArray);
    if (provinceId) {
        self.selectedProvince = ko.observable(self.availableProvinces.find("id", provinceId)).extend({
            required: { message: "Province Must Be Selected" }
        });
    }
    else {
        self.selectedProvince = ko.observable().extend({
            required: { message: "Province Must Be Selected" }
        });
    }

    self.selectedCountry.subscribe(function (newCountry) {
        if (newCountry) {
            $.getJSON("/Wizard/GetProvinces/" + newCountry.id, null, function (data) {
                self.availableProvinces(data);
            });
        } else self.availableProvinces(null);
        }.bind(this));

    self.displayAlert = ko.observable(false);
    self.save = function () {
        if (self.errors().length == 0) {
            SaveUser(self.currentUser, self.selectedCountry().id, self.selectedProvince().id);
            self.displayAlert = ko.observable(false);
        } else {
            self.displayAlert(true);
            self.errors.showAllMessages();
        }
    };
    self.errors = ko.validation.group(this);
}

function GetCountries() {
        var countries="";
        $.ajax({
            async: false,
            dataType : 'json',
            url: "/Wizard/GetCountries",
            type : 'GET',
            success: function(data) {
                countries = data;
                }
            }
        );
    return countries;
}

function GetProvinces(countryId) {
    var provinces = "";
    $.ajax({
        async: false,
        dataType: 'json',
        url: "/Wizard/GetProvinces/" + countryId,
        type: 'GET',
        success: function (data) {
            provinces = data;
        }
    }
    );
    return provinces;
}

function SetUserId(model) {
    var userId = $('#UserId').val()
    model.currentUser(userId);
}

function GetCurrentCountryId() {
    return $('#CountryId').val()
}

function GetCurrentProvinceId() {
    return $('#ProvinceId').val()
}

function SaveUser(userId, countryId, provinceId) {
    var jsonObj = ko.toJSON({
        id: userId,
        provinceId: provinceId,
        countryId: countryId
    });
    $.ajax("/Wizard/SaveLocation", {
        type: 'POST',
        dataType: 'json',
        data: jsonObj,
        contentType: 'application/json; charset=utf-8',
        success: function (response) {
            $("#ResultMessage").text(response.resultMessage);
        }
    });
}
