ko.validation.configure({
    decorateElement: true,
    messagesOnModified: true,
    registerExtenders: true,
    insertMessages: true,
    parseInputAttributes: true,
    messageTemplate: null
});

ko.observableArray.fn.find = function (prop, data) {
    var valueToMatch = data;
    return ko.utils.arrayFirst(this(), function (item) {
        return item[prop] === valueToMatch;
    });
};

var patterns = {
    email: /^([\d\w-\.]+@([\d\w-]+\.)+[\w]{2,4})?$/,
    password: /^(?:[0-9]+[a-z]|[a-z]+[0-9])[a-z0-9]*$/i
};
