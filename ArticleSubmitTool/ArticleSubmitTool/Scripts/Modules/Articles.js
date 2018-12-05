function pageInit($scope, model, $http, $timeout) {

    $scope.model = model;

    $scope.UI = {

        closeModal: function (id) {
            $('#' + id).closeModal();
        },

        showPublishModal: function(article) {
            $('#publishModal').openModal({
                dismissible: false, // Modal can be dismissed by clicking outside of the modal
                opacity: .5, // Opacity of modal background
                in_duration: 200, // Transition in duration
                out_duration: 200, // Transition out duration
                //starting_top: '4%', // Starting top style attribute
                //ending_top: '10%', // Ending top style attribute
                ready: function(modal, trigger) { // Callback for Modal open. Modal and trigger parameters available.
        
                },
                complete: function () {


                } // Callback for Modal close
            });
        }
    }

    $scope.InstantArticlesPage = {
        getPublishedClass: function (article) {
            return article.IsPublished ? "color-success fa fa-check" : "color-danger fa fa-times";
        },


        remove: function (article) {
            $http.post(
                   '/Article/Delete',
                   { articleId: article.Id }
               ).then(function successCallback(resp) {
                   if (resp.data != null) {
                       var result = resp.data;
                       if (result.Success) {
                           if (result.Items.length > 0) {
                               replaceArr(model.InstantArticles, result.Items);
                           } else {
                               clearArr(model.InstantArticles);
                           }
                           Materialize.toast('Article Deleted.', 2000, 'success');
                       } else {
                           Materialize.toast('An error occured.', 2000, 'error');
                       }

                   }
               });
        }
    }


 

}
