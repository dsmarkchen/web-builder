var page = angular.module('app',['ngSanitize'])
.filter('markdown', function() {
    var converter = new Showdown.converter();
    return converter.makeHtml;
})
.factory('PageFactory', function ($http, $sce) {
    var factory = {};
    var apiurl = "http://localhost:53818/api/module";

    factory.fetch = function() {
        var url2 = "http://localhost:53818/api/module";
        var trustedUrl2 = $sce.trustAsResourceUrl(url2);
 
        return $http.get(trustedUrl2, {jsonpCallbackParam: 'callback'})
            .then(function(rsp){
                console.log(">>>>   " + trustedUrl2);
                console.log(">>>>   " + "status:" + rsp.status + "statusText: " + rsp.statusText + "data: " + rsp.data);
                return rsp.data;
            }, function (error) {
                console.log("####   " + trustedUrl2);
                console.log("####   " + error);
            });
    }
    factory.getone = function(id) {
        var url2 = "http://192.168.1.106:53818/api/module" + "/" + id;
        var trustedUrl2 = $sce.trustAsResourceUrl(url2);
 
        return $http.get(trustedUrl2, {jsonpCallbackParam: 'callback'})
            .then(function(rsp){
                console.log(">>>>   " + trustedUrl2);
                console.log(">>>>   " + "status:" + rsp.status + "statusText: " + rsp.statusText + "data: " + rsp.data);
                return rsp.data;
            }, function (error) {
                console.log("####   " + trustedUrl2);
                console.log("####   " + error);
            });
    }

   factory.update =  function(id, page) {
        var  content = page.p1; //$sce.trustAsHtml(markdown.toHTML(page.p1)); 
        //
        // <div ng-bind-html="article.content"></div>
        var data = {title: page.title, p1: content};
        var url2 = "http://192.168.1.106:5000/api/module" + "/" + id;

        var urlput = $sce.trustAsResourceUrl(url2);
        $http.put(urlput, JSON.stringify(data))
            .then(function (response) {
                console.log( ">>>> " + response);
            }, function (response) {
                console.log( "#### " + response);
           }); 
     }

     return factory; 
})
.service('PageService', ['$http', '$sce', 'PageFactory', function($http, $sce, PageFactory) {
   this.fetch = function() {
        return PageFactory.fetch();
    }
   this.getone = function(id) {
        return PageFactory.getone(id);
    }
   this.update = function(id, page) {
        PageFactory.update(id, page);
    }
}])
.controller("pageCtrl",  function($scope, $http, $sce, PageService){
    $scope.title = "a page" ;


    $scope.apiurl = "http://192.168.1.106:5000/api/page"
    console.log("pageCtrl begin");

    $scope.fetch = function() {

        $scope.status= "fetching pages";
        
        $scope.loading = true;
 
        PageService.fetch()
            .then(function(data){
                $scope.pages = data;
                $scope.loading = false;
                $scope.status = "done";
            });
     };

     $scope.update = function() {
   
        var data = {title: $scope.updateTitle, p1: $scope.input};
        PageService.update($scope.page.id, data); 
     } 

     $scope.select = function(x) {
        $scope.page = x ;
        $scope.title = x.title;

        $scope.updateTitle = x.title;
        $scope.input = x.p1;
     }

    $scope.fetch();
    console.log("pageCtrl end");
})
.controller('homePageCtrl', ['$scope', '$http', '$sce', 'PageService', function($scope, $http, $sce, PageService) {

    PageService.getone(1)
            .then(function(data){
                $scope.page = data;
                $scope.loading = false;
                $scope.status = "done";
            });
 

}]);

