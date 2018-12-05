


function pageInit($scope, model, $http, $timeout) {

    $scope.model = model;

    $scope.Settings = {

        save: function (settings) {

            var self = this;

            var settingsJSON = toJSON(settings);

            $http.post(
                '/Settings/Save',
                { settingsJSON: settingsJSON }
            ).then(function successCallback(resp) {
                if (resp.data != null) {
                    var result = resp.data;
                    if (result.Success) {
                        //console.log(result.InstantArticle);
                        $scope.model = result.Settings;
                        Materialize.toast('Settings Saved.', 2000, 'success');
                    } else {
                        Materialize.toast('An error occured.', 2000, 'error');
                    }
                }
            });
        }

    }

}
