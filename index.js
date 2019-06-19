var app = angular.module('indexApp', []);
app.config(['$sceDelegateProvider', function($sceDelegateProvider) {
    $sceDelegateProvider.resourceUrlWhitelist([
      'self',
      'http://localhost:53818/**'
    ]);
  }])
app.controller('indexCtrl', function($scope, $http, $sce, $timeout) {

  $scope.status= "start...";
  $scope.loading = false;

  $scope.myWelcome = "Welcome to foo";



  console.log("loading");
  $scope.loading = true;


});

