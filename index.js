var module = angular.module('module', []);
module.service('moduleService', function($sce, $http) {
    this.getone = function(id) {
       var url2 = "http://localhost:53818/api/module/" + id

        var trustedUrl2 = $sce.trustAsResourceUrl(url2);
   
        return $http.get(trustedUrl2, {jsonpCallbackParam: 'callback'})
            .then(function(rsp){
                console.log(">>>>   " + trustedUrl2);
                console.log(">>>>   " + "status:" + rsp.status + "statusText: " + rsp.statusText + "data: " + rsp.data);
                return rsp.data;
                
            }, function (error) {
                console.log("####   " + trustedUrl2);
                console.log("####   " +error);
            });
    
    }
});
module.controller('moduleCtrl',  function($scope, $http, $sce, $timeout, moduleService) {

  $scope.status= "";
  $scope.loading = false;


  $scope.getone =  function (id) {
      console.log("start:" + id);
      moduleService.getone(id).then(function(data) {
            $scope.Module = data;
            console.log("module:" + $scope.Module);
       });;
  }

  console.log("loading");
  $scope.loading = true;


});
var app = angular.module('app', ['module']);
app.controller('indexCtrl', function($scope) {


  $scope.myWelcome = "Welcome to web-builder!";



});

