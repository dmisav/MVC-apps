var app = angular.module('PackagesModule', []);

app.controller('PackageController', function ($http) {
    var PkgCtrl = this;
    Init();

    function Init() {
        PkgCtrl.items = [];
        PkgCtrl.packages = [];
        PkgCtrl.selectedPkg = {};
        GetAllItems();
        GetAllPackages();
    }

    PkgCtrl.GetItemClass = function (item) {
        var index = PkgCtrl.items.indexOf(item);
        var enabled = IsItemEnabled(item.DisplayName);
        return "item" + index + " " + enabled;
    }

    PkgCtrl.SetSelectedPkg = function (packageParam) {
        PkgCtrl.selectedPkg = packageParam;
    }

    PkgCtrl.IsCurrent = function (packageParam) {
        return packageParam == PkgCtrl.selectedPkg;
    }

    function IsItemEnabled(itemName) {
        if (!isObjectEmpty(PkgCtrl.selectedPkg)) {
            for (var i = PkgCtrl.selectedPkg.items.length - 1; i > -1; i--) {
                if (PkgCtrl.selectedPkg.items[i].DisplayName === itemName)
                    return "circleEnabled";
            }
        }
        return "circleDisabled";
    }

    function GetAllItems() {
        var result = $http.get("../Packages/GetListOfItems");
        result.success(function (data) {
            PkgCtrl.items = data;
        });
    };

    function GetAllPackages() {
        var result = $http.get("../Packages/GetListOfPackages");
        result.success(function (data) {
            PkgCtrl.packages = data;
        });
    };

    function isObjectEmpty(object) {
        var isEmpty = true;
        for (keys in object) {
            isEmpty = false;
            break;
        }
        return isEmpty;
    }
});