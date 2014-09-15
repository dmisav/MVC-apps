$(document).ready(function () {
    ko.applyBindings(new LoginViewModel());
});

var patterns = {
    email: /^([\d\w-\.]+@([\d\w-]+\.)+[\w]{2,4})?$/,
    password: /^(?:[0-9]+[a-z]|[a-z]+[0-9])[a-z0-9]*$/i
};

ko.validation.configure({
    decorateElement: true,
    messagesOnModified: true,
    registerExtenders: true,
    insertMessages: true,
    parseInputAttributes: true,
    messageTemplate: null
});

var LoginViewModel = function()
{
    var self = this;
    self.inputEmail = ko.observable().extend({
       required: true,
       pattern: {
           message: 'Must be valid e-mail',
           params: patterns.email
       }
    });

    self.inputPassword = ko.observable().extend({
        required: true,
        pattern: {
           message: 'Must contain at least one digit and one letter',
           params: patterns.password
        }
    });

    self.confirmInputPassword = ko.observable().extend({ areSame: self.inputPassword });

    self.isAccepted = ko.observable(false).extend({
        checked: { message: 'Must be selected' }
    });

    self.displayAlert = ko.observable(false);
    self.submit = function () {
        if (self.errors().length == 0) {
            ValidateUser(self.inputEmail, self.inputPassword);
            self.displayAlert = ko.observable(false);
        } else {
            self.displayAlert(true);
            self.errors.showAllMessages();
        }
    };
    self.errors = ko.validation.group(this);
}

ko.validation.rules['areSame'] = {
    getValue: function (o) {
        return (typeof o === 'function' ? o() : o);
    },
    validator: function (val, otherField) {
        return val === this.getValue(otherField);
    },
    message: 'The fields must have the same value'
};
ko.validation.registerExtenders();

ko.validation.rules['checked'] = {
    validator: function (value) {
        if (!value) {
            return false;
        }
        return true;
    }
};
ko.validation.registerExtenders();

function ValidateUser(email, password) {
    var jsonObj = ko.toJSON({
        email: email,
        password: password
    });
    $.ajax("/Wizard/ValidateUser", {
    type: 'POST',
    dataType: 'json',
    data: jsonObj,
    contentType: 'application/json; charset=utf-8',
    success: function (response) {
        if (response.isValidUser) {
            window.location.href = response.url;
        }
        else {
            $("#ResultMessage").text(response.validationResultMessage);
        }
    }
});
}