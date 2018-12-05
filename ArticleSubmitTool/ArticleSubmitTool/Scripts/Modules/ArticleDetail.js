function dialogInit() {


    $('div#mediaTab').removeAttr('style');

    var tabs = $('#itemAttributesModal .modal-content ul.tabs');

    if (tabs.length > 0) {
        tabs.tabs();

        tabs.tabs('select_tab', 'mediaTab');
                 console.log($('#mediaTab').attr('style'));
  
    }

}


function pageInit($scope, model, $http, $timeout) {

    $scope.model = model;
      
    $scope.UI = {

        closeModal: function (id) {
            $('#' + id).closeModal();
        },

        Icons: {
            1: 'fa fa-image fa-2x',
            2: 'fa fa-video-camera fa-2x',
            3: 'fa fa-font fa-2x',
            4: 'fa fa-quote-right fa-2x',
            5: 'fa fa-quote-left fa-2x',
            6: 'fa fa-newspaper-o fa-2x',
            7: 'fa fa-map-marker fa-2x',
            8: 'fa fa-twitter fa-2x',
            9: 'fa fa-facebook fa-2x',
            10: 'fa fa-instagram fa-2x',
            11: 'fa fa-youtube-play fa-2x',
            12: 'fa fa-image fa-2x',
            13: 'fa fa-desktop fa-2x'
        },

        showItemEditorModal: function(articleItem, isBodyItem) {

            $scope.modalItem = articleItem;

            $('#itemAttributesModal').openModal({
                dismissible: false, // Modal can be dismissed by clicking outside of the modal
                opacity: .5, // Opacity of modal background
                in_duration: 200, // Transition in duration
                out_duration: 200, // Transition out duration
                //starting_top: '4%', // Starting top style attribute
                //ending_top: '10%', // Ending top style attribute
                ready: function (modal, trigger) { // Callback for Modal open. Modal and trigger parameters available.
                    globalInit();
                    dialogInit();
                },
                complete: function() {

                    if (!articleItem.Edit) {
                        Materialize.toast(articleItem.Name + ' Added.', 2000, 'success');
                    } else {
                        Materialize.toast(articleItem.Name + ' Updated.', 2000, 'success');
                    }

                } // Callback for Modal close
            });
        },

        showAuthorModal: function (article, isEdit) {

            var $modal = $('#authorModal');
            $modal.openModal({
                dismissible: false, // Modal can be dismissed by clicking outside of the modal
                opacity: .5, // Opacity of modal background
                in_duration: 200, // Transition in duration
                out_duration: 200, // Transition out duration
                //starting_top: '4%', // Starting top style attribute
                //ending_top: '10%', // Ending top style attribute
                ready: function (modal, trigger) { // Callback for Modal open. Modal and trigger parameters available.
                    dialogInit();
                },
                complete: function () {

                    var authorName = $modal.find('#name').val();
                    $scope.InstantArticle.addAuthor(article, authorName);


                } // Callback for Modal close
            });
        }
    };


    $scope.InstantArticle = {

        ItemType: {
            Image: 1,
            Video: 2,
            Text: 3,
            PullQuote: 4,
            BlockQuote: 5,
            Ad: 6,
            Map: 7,
            Twitter: 8,
            Facebook: 9,
            Instagram: 10,
            YouTube: 11,
            Caption: 12,
            SlideShow: 13
        },

        init: function (article) {

            var self = this;

            article.feedbackType = toStringArr(flagArr(article.FacebookFeedback, [1, 2]));

            article.Items.forEach(function (item) {
                self.initItem(item);
            });

            //build index
            article.Items.map(function (item, index) { item.Index = index });

            var itemEnum = Enumerable.From(article.Items);

            article.BodyItems = [];
            article.HeaderAds = [];
            article.Captions = [];

            if (itemEnum != null && itemEnum.Count() > 0) {
                article.HeaderMedia = itemEnum.Where(function(i) { return i.IsHeader && (i.Type === self.ItemType.Video || i.Type === self.ItemType.Image);  }).FirstOrDefault();

                article.BodyItems = itemEnum.Where(function (i) { return !i.IsHeader && !i.IsCaption }).ToArray();

                article.Captions =itemEnum.Where(function (i) { return i.IsCaption }).ToArray();

                article.HeaderAds = itemEnum.Where(function (i) { return i.IsHeader && i.Type === self.ItemType.Ad }).ToArray();

                $scope.$watchCollection(
                                "model.BodyItems",
                                function (newValue, oldValue) {
                                    //console.log(newValue);
                                    for (var i = 0; i < newValue.length; i++) {
                                        newValue[i].Index = i;
                                    }
                                }
                            );

                $scope.$watchCollection(
                            "model.HeaderAds",
                            function (newValue, oldValue) {
                                //console.log(newValue);
                                for (var i = 0; i < newValue.length; i++) {
                                    newValue[i].Index = i;
                                }
                            }
                        );
            }   

        },

        clean: function (article) {
            var self = this;
            delete article.HeaderMedia;
            if (article.Items != null && article.Items.length > 0) {
                article.Items.forEach(function(item) {
                    if (item.Caption != null) {
                        delete item.Caption;
                    }
                    self.deleteAttrs(item);
                    if (item.Children != null && item.Children.length > 0) {
                        item.Children.forEach(function(child) {
                            if (child.Caption != null) {
                                delete child.Caption;
                            }
                            self.deleteAttrs(child);
                        });
                    }
                });
            }
        },

        deleteAttrs: function (item) {
            if (item.Attributes !== null && item.Attributes.length > 0) {
                item.Attributes.forEach(function(attr) {
                    delete item[attr.Name];
                });
            }
        },

        addAuthor: function(article, authorName, isEdit) {
            //console.log(authorName);

            article.Authors.push(authorName);

            //console.log(article.Authors);

            $scope.model.Authors = article.Authors;
            $scope.$apply();

            if (!isEdit) {
                Materialize.toast('Author Added.', 2000, 'success');
            } else {
                Materialize.toast('Author Updated.', 2000, 'success');
            }
        },

        removeAuthor: function(article, authorIndex) {
            removeAt(article.Authors, authorIndex);
            Materialize.toast('Author Removed.', 2000, 'success');
        },

        initItem: function (item) {
            if (item != null) {
                if (item.Children != null && item.Children.length > 0) {
                    item.Children.forEach(function(child) {
                        if (child.Attributes.length > 0) {
                            child.Attributes.map(function (attr) {
                                child[attr.Name] = attr.Value;
                            });
                        }
                    });
                    var childrenEnum = Enumerable.From(item.Children);
                    item.Caption = childrenEnum.Where(function(i) { return i.IsCaption }).FirstOrDefault();
                }

                if (item.Attributes.length > 0) {
                    item.Attributes.map(function(attr) {
                        item[attr.Name] = attr.Value;
                    });
                }
            }

            return item;
        },


        deleted: function(item) {
            return item.IsDeleted;
        },

        itemAttrHasValues: function (articleItem, attrName) {
            if (articleItem.AttributeValues == null || articleItem.AttributeValues.length === 0) {
                return false;
            }
            return articleItem.AttributeValues[attrName] != null && articleItem.AttributeValues[attrName].length > 0;
        },

        itemHasAttributes: function(articleItem) {
            return articleItem.Attributes != null && articleItem.Attributes.length > 0;
        },

        editItem: function(articleItem) {
            articleItem.Edit = true;
            $scope.UI.showItemEditorModal(articleItem);
        },

        getAttrBoolVal: function (attr) {
            return toBool(attr.Value);
        },

        changeAttrBoolVal: function (attr, enabled) {
            attr.Value = toBoolStr(enabled);
        },


        changeFeedback: function(article, isEnabled) {
            article.ArticleFeedbackEnabled = isEnabled;
        },

        changeFacebookFeedback: function (article, feedbackType) {

            var values = [1, 2];


            if (feedbackType.length === 0) {
                article.FacebookFeedback = null;
            }

            if (feedbackType.length === 1) {

                var setVal = parseInt(feedbackType[0]);
                var unsetVal = Enumerable.From(values).Except(setVal).ToArray()[0];

                article.FacebookFeedback = setFlag(article.FacebookFeedback, setVal);
                article.FacebookFeedback = unsetFlag(article.FacebookFeedback, unsetVal);
            }

            else if (feedbackType.length === 2) {
                article.FacebookFeedback = setFlag(article.FacebookFeedback, parseInt(feedbackType[0]));
                article.FacebookFeedback = setFlag(article.FacebookFeedback, parseInt(feedbackType[1]));
            }
        },


        addHeaderAd(article) {

            var self = this;

            $http.post(
                       '/Article/CreateItem',
                       { type: self.ItemType.Ad, articleId: article.Id }
                   ).then(function successCallback(resp) {
                       if (resp.data != null) {
                           var adItem = resp.data;
                           adItem.InstantArticleId = model.Id;
                           adItem.IsHeader = true;
                           self.initItem(adItem);
                           article.HeaderAds.push(adItem);
                           if (adItem.Attributes.length > 0) {
                               $scope.UI.showItemEditorModal(adItem, true);
                           } else {
                               Materialize.toast('Ad Added.', 2000, 'success');
                           }

                       }
                   });
        },

        removeHeaderAd(article, ad) {
            var self = this;
            self.removeItem(article, article.HeaderAds, ad, true);
        },

        changeHeaderMedia: function(article, type) {

            var self = this;

            var itemEnum = Enumerable.From(article.Items);
            var headerMedia = itemEnum.Where(function (i) { return i.IsHeader && (i.Type === self.ItemType.Video || i.Type === self.ItemType.Image); }).FirstOrDefault();

            if (headerMedia != null) {
                self.removeItem(article, article.Items, headerMedia, false);
            }

            if (type == 0) {
                article.HeaderMedia = null;
            }

            else {
                $http.post(
                    '/Article/CreateItem',
                    { type: type, articleId: article.Id }
                ).then(function successCallback(resp) {
                    if (resp.data != null) {
                        var mediaItem = resp.data;
                        article.HeaderMedia = mediaItem;
                        article.HeaderMedia.IsHeader = true;
                        article.HeaderMedia.InstantArticleId = article.Id;
                        self.initItem(article.HeaderMedia);
                        $scope.UI.showItemEditorModal(article.HeaderMedia, false);
                    }
                });
            }
        },

        addBodyItem: function (article, type) {

            var self = this;

            $http.post(
                '/Article/CreateItem',
                { type: type, articleId: article.Id }
            ).then(function successCallback(resp) {
                if (resp.data != null) {
                    var articleItem = resp.data;
                    articleItem.InstantArticleId = article.Id;
                    self.initItem(articleItem);
                    article.BodyItems.push(articleItem);
                    if (articleItem.Attributes.length > 0) {
                        $scope.UI.showItemEditorModal(articleItem, true);
                    } else {
                        Materialize.toast(articleItem.Name + ' Added.', 2000, 'success');
                    }

                }
            });

        },

        removeBodyItem(article, item) {
            var self = this;
            self.removeItem(article, article.BodyItems, item, true);
        },

        removeItem: function (article, itemCol, item, showMsg) {

            var self = this;

            if (item.Id === 0) { //not yet saved
                var itemName = "" + item.Name;
                //console.log('Index-' + item.Index);
                itemCol.splice(item.Index, 1);
                if (showMsg) {
                    Materialize.toast(itemName + ' Deleted.', 2000, 'success');
                }
          
            } else { //saved
                $http.post(
                    '/Article/DeleteItem',
                    { item: item }
                ).then(function successCallback(resp) {
                    if (resp.data != null) {
                        var result = resp.data;
                        if (result.Success) {
                            if (result.Items.length > 0) {
                                replaceArr(article.Items, result.Items);
                            } else {
                                clearArr(article.Items);
                            }
                            $scope.model = article;
                            self.init($scope.model);
                            if (showMsg) {
                                Materialize.toast(item.Name + ' Deleted.', 2000, 'success');
                            }
                        } else {
                            if (showMsg) {
                                Materialize.toast('An error occured.', 2000, 'error');
                            }
                        }
                    }

                });
            }
        },

        populateTextEditorValues: function(article) {

            var ids = ['NewsFeedDescription', 'Credits', 'Copyright', 'RelatedArticles'];

            ids.forEach(function(id) {
                var editorVal = tinyMCE.get(id).getContent();
                if (!isNullOrEmpty(editorVal)) {
                    article[id] = editorVal;
                }
            });

            $('textarea.editor').each(function(elem) {
                var $elem = $(this);
                var id = $elem.attr('id');
                var index = $elem.attr('data-index');
                var editorVal = tinyMCE.get(id).getContent();

                if (!isNullOrEmpty(editorVal)) {
                    article.BodyItems[index].Value = editorVal;
                }

            });
        },

        save: function (article) {

            var self = this;

            article.Items = article.BodyItems.concat(article.HeaderAds, article.Captions, article.HeaderMedia);
            article.Items = removeNulls(article.Items);

            self.clean(article);

            //self.populateTextEditorValues(article);



            var articleJSON = toJSON(article);

            $http.post(
                '/Article/Save',
                { articleJSON: articleJSON }
            ).then(function successCallback(resp) {
                if (resp.data != null) {
                    var result = resp.data;
                    if (result.Success) {
                        //console.log(result.InstantArticle);
                        $scope.model = result.InstantArticle;
                        self.init($scope.model);
                        Materialize.toast('Article Saved.', 2000, 'success');
                    } else {
                        Materialize.toast('An error occured.', 2000, 'error');
                    }
                }
            });
        }

    }

    $scope.InstantArticle.init($scope.model);

    $scope.$watch('$viewContentLoaded', function () {

    });

    //$scope.$on('$viewContentLoaded', function (event) {
    //    // code that will be executed ... 
    //    // every time this view is loaded
  
    //});

    $timeout(function () {

    });

}
