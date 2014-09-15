$(document).ready(function () {
    var model = new LocationViewModel();
    SetUserId(model);
    LoadCountries(model);
    ko.applyBindings(model);
});


ko.validation.configure({
    decorateElement: true,
    messagesOnModified: true,
    registerExtenders: true,
    insertMessages: true,
    parseInputAttributes: true,
    messageTemplate: null
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

var LocationViewModel = function () {
    var self = this;
    self.availableCountries = ko.observableArray();
    self.currentUser = ko.observable();
    self.selectedCountry = ko.observable().extend({
        required: { message: "Country Must Be Selected" }
    });

    self.availableProvinces = ko.observableArray();

    self.selectedProvince = ko.observable().extend({
        required: { message: "Province Must Be Selected" }
    });

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
          //  ValidateUser(self.inputEmail, self.inputPassword);
            self.displayAlert = ko.observable(false);
        } else {
            self.displayAlert(true);
            self.errors.showAllMessages();
        }
    };
    self.errors = ko.validation.group(this);
}

function LoadCountries(model)
{
    $.getJSON("/Wizard/GetCountries", null, function (data) {
        model.availableCountries(data);
    });
}

function SetUserId(model) {
    var userId = $('#UserId').val()
    model.currentUser(userId);
}

//that.submit = function () {
//var json1 = ko.toJSON(that.Employee);
//$.ajax({
//    url: '/Employee/SaveEmployee',
//    type: 'POST',
//    dataType: 'json',
//    data: json1,
//    contentType: 'application/json; charset=utf-8',
//    success: function (data) {
//        var message = data.Message;
//    }
//});

//
//
//function ValidateUser(email, password) {
//    var jsonObj = ko.toJSON({
//        email: email,
//        password: password
//    });
//    $.ajax("/Authorize/ValidateUser", {
//        type: 'POST',
//        dataType: 'json',
//        data: jsonObj,
//        contentType: 'application/json; charset=utf-8',
//        success: function (response) {
//            if (response.isValidUser) {
//                window.location.href = response.url;
//            }
//            else {
//                $("#ResultMessage").text(response.validationResultMessage);
//            }
//        }
//    });
//}