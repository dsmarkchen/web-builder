var app = angular.module('indexApp', []);
app.config(['$sceDelegateProvider', function($sceDelegateProvider) {
    $sceDelegateProvider.resourceUrlWhitelist([
      'self',
      'http://localhost:53818/**'
    ]);
  }])
app.controller('indexCtrl', function($scope, $http, $sce, $timeout) {

  $scope.status= "";
  $scope.loading = false;

  $scope.myWelcome = "Welcome to web-builder!";

  $scope.getone =  function (id) {
        var url2 = "http://localhost:53818/api/module/" + id

        var trustedUrl2 = $sce.trustAsResourceUrl(url2);
 
   
        $http.get(trustedUrl2, {jsonpCallbackParam: 'callback'})
            .then(function(rsp){
                console.log(">>>>   " + trustedUrl2);
                console.log(">>>>   " + "status:" + rsp.status + "statusText: " + rsp.statusText + "data: " + rsp.data);
                $scope.Module = rsp.data;
                $scope.loading = false;
            }, function (error) {
                console.log("####   " + trustedUrl2);
                console.log("####   " +error);
            });
     }

  console.log("loading");
  $scope.loading = true;


});

