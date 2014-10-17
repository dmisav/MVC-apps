var app = angular.module('PackagesModule',[]);

app.controller('PackageController', function ($http) {
    var PkgCtrl = this;
    PkgCtrl.items = [];
    PkgCtrl.packages = [];
    PkgCtrl.selectedPkg = {};
    Init();

    function Init() {
        GetAllItems();
        GetAllPackages();
    }

    PkgCtrl.SetSelectedPkg = function(packageParam) {
        PkgCtrl.selectedPkg = packageParam;
    }

    PkgCtrl.IsCurrent = function(packageParam)
    {
        return packageParam == PkgCtrl.selectedPkg;
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

});