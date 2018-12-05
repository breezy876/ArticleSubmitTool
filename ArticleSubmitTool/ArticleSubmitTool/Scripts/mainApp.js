function initTinymce(elemId) {


    tinymce.init(
                    {
                        selector: !isNullOrEmpty(elemId) ? "textarea#"+elemId : "textarea",
                        toolbar: "styleselect | undo redo | removeformat | bold italic underline |  aligncenter alignjustify  | bullist numlist outdent indent | link",
                        statusbar: false,
                        resize: false,
                        plugins: "autoresize, textcolor, link",

                        setup: function (ed) {

                        },

                        menu: {
                            edit: { title: "Edit", items: "undo redo | cut copy paste pastetext | selectall" },
                            format: { title: "Format", items: "bold italic underline strikethrough superscript subscript | formats | removeformat" },
                        },

                        content_css: "/tinymce-editor.css"
                    });

    //var editor = tinymce.get('tinymceEditor');
    //var iframeDoc = $(editor.iframeElement).contents();
    //var headElem = iframeDoc.children('html').children('head');
    //headElem.append("<script>function test() { alert('testing'); }</script>");

    //headElem.append("<link type='text/css' rel='stylesheet' href='/Content/tinymce/tinymce-editor.css'>");
}


var mainApp = angular.module('mainApp', []);

mainApp.controller('MainController', ['$scope', '$http', '$timeout', '$window', function ($scope, $http, $timeout, $window) {

    $scope.trustAsHtml = function (html) {
        return $sce.trustAsHtml(html);
    }

    $scope.parent = $scope;

    $scope.init = function (model) {
        //init global scope
        //initScope($scope, model, $http, $timeout);

        //init module page scope

        var fbAppId = $('#fbAppId').val();

        fbInit($scope, $window, $http, fbAppId);

        $scope.Facebook.init();

        pageInit($scope, model, $http, $timeout);
    

    }
   
}]);

mainApp.filter('trustUrl', ['$sce', function ($sce) {
    return function (url) {
        return $sce.trustAsResourceUrl(url);
    };
}]);

mainApp.factory('RecursionHelper', ['$compile', function ($compile) {
    var RecursionHelper = {
        compile: function (element) {
            var contents = element.contents().remove();
            var compiledContents;
            return function (scope, element, attr) {
                if (!compiledContents) {
                    compiledContents = $compile(contents);
                }
                compiledContents(scope, function (clone, scope) {
                    element.append(clone);
                });
            };
        }
    };

    return RecursionHelper;
}]);

/////begin items
//mainApp.directive('itemReady', function($parse) {
//    return {
//        restrict: 'A',
//        link: function($scope, elem, attrs) {
//            elem.ready(function() {
//                console.log('done - ' + $scope.index);
//                //var $elem = $(elem);
//                //var elemId = $elem.attr('id');
//                ////initTinymce(elemId);
//                //console.log('done - ' + $elem.tagName + ' - ' + elemId);
//                //$scope.$apply(function() {
//                //    var func = $parse(attrs.elemReady);
//                //    func($scope);
//                //});
//            });
//        },

//    }
//});

mainApp.directive('instantArticleItem', function (RecursionHelper) {
    return {
        restrict: "E",
        transclude: true,
        scope: {
            model: '=',
            parent: '=',
            index: '='
        },
        templateUrl: '/Templates/Items/InstantArticleItem.html',
        link: function (scope, element, attrs) {
        },
        compile: function (element, attributes) {
            return RecursionHelper.compile(element);
        }
    }
});

mainApp.directive('videoItem', function (RecursionHelper) {
    return {
        restrict: "E",
        transclude: true,
        scope: {
            model: '=',
            parent: '='
        },
        templateUrl: '/Templates/Items/VideoItem.html',
        link: function (scope, element, attrs) {

        },
        compile: function (element, attributes) {
            return RecursionHelper.compile(element);
        }
    }
});

mainApp.directive('imageItem', function (RecursionHelper) {
    return {
        restrict: "E",
        transclude: true,
        scope: {
            model: '=',
            parent: '='
        },
        templateUrl: '/Templates/Items/ImageItem.html',
        link: function (scope, element, attrs) {

        },
        compile: function (element, attributes) {
            return RecursionHelper.compile(element);
        }
    }
});

mainApp.directive('textItem', function (RecursionHelper) {
    return {
        restrict: "E",
        transclude: true,
        scope: {
            model: '=',
            parent: '='
        },
        templateUrl: '/Templates/Items/TextItem.html',
        link: function (scope, element, attrs) {

        },
        compile: function (element, attributes) {
            return RecursionHelper.compile(element);
        }
    }
});

mainApp.directive('blockQuoteItem', function (RecursionHelper) {
    return {
        restrict: "E",
        transclude: true,
        scope: {
            model: '=',
            parent: '='
        },
        templateUrl: '/Templates/Items/BlockQuoteItem.html',
        link: function (scope, element, attrs) {

        },
        compile: function (element, attributes) {
            return RecursionHelper.compile(element);
        }
    }
});

mainApp.directive('pullQuoteItem', function (RecursionHelper) {
    return {
        restrict: "E",
        transclude: true,
        scope: {
            model: '=',
            parent: '='
        },
        templateUrl: '/Templates/Items/PullQuoteItem.html',
        link: function (scope, element, attrs) {

        },
        compile: function (element, attributes) {
            return RecursionHelper.compile(element);
        }
    }
});

mainApp.directive('adItem', function (RecursionHelper) {
    return {
        restrict: "E",
        transclude: true,
        scope: {
            model: '=',
            parent: '='
        },
        templateUrl: '/Templates/Items/AdItem.html',
        link: function (scope, element, attrs) {

        },
        compile: function (element, attributes) {
            return RecursionHelper.compile(element);
        }
    }
});

mainApp.directive('mapItem', function (RecursionHelper) {
    return {
        restrict: "E",
        transclude: true,
        scope: {
            model: '=',
            parent: '='
        },
        templateUrl: '/Templates/Items/MapItem.html',
        link: function (scope, element, attrs) {

        },
        compile: function (element, attributes) {
            return RecursionHelper.compile(element);
        }
    }
});

mainApp.directive('embedItem', function (RecursionHelper) {
    return {
        restrict: "E",
        transclude: true,
        scope: {
            model: '=',
            parent: '='
        },
        templateUrl: '/Templates/Items/EmbedItem.html',
        link: function (scope, element, attrs) {

        },
        compile: function (element, attributes) {
            return RecursionHelper.compile(element);
        }
    }
});

mainApp.directive('slideShowItem', function (RecursionHelper) {
    return {
        restrict: "E",
        transclude: true,
        scope: {
            model: '=',
            parent: '='
        },
        templateUrl: '/Templates/Items/SlideShowItem.html',
        link: function (scope, element, attrs) {

        },
        compile: function (element, attributes) {
            return RecursionHelper.compile(element);
        }
    }
});

mainApp.directive('captionItem', function (RecursionHelper) {
    return {
        restrict: "E",
        transclude: true,
        scope: {
            model: '=',
            parent: '='
        },
        templateUrl: '/Templates/Items/CaptionItem.html',
        link: function (scope, element, attrs) {

        },
        compile: function (element, attributes) {
            return RecursionHelper.compile(element);
        }
    }
});

mainApp.directive('itemOptions', function (RecursionHelper) {
    return {
        restrict: "E",
        transclude: true,
        scope: {
            item: '=',
            parent: '='
        },
        templateUrl: '/Templates/ItemOptions.html',
        link: function (scope, element, attrs) {

        },
        compile: function (element, attributes) {
            return RecursionHelper.compile(element);
        }
    }
});
//end