function fbInit($scope, $window, $http, appId) {

    $window.fbAsyncInit = function() {
        // Executed when the SDK is loaded

        FB.init({

            /*
             The app id of the web app;
             To register a new app visit Facebook App Dashboard
             ( https://developers.facebook.com/apps/ )
            */

            appId: appId,

            /*
             Adding a Channel File improves the performance
             of the javascript SDK, by addressing issues
             with cross-domain communication in certain browsers.
            */

            /*
             Set if you want to check the authentication status
             at the start up of the app
            */

            status: true,

            /*
             Enable cookies to allow the server to access
             the session
            */

            cookie: true,

            /* Parse XFBML */

            xfbml: true
        });
    };

    $scope.Facebook = {

        IsLoggedIn: false,

        LoginInfo: {
            AccessToken: '',
            UserId: ''
        },

        init: function() {

            var self = this;
            self.IsLoggedIn = false;

            //FB.getLoginStatus(function(response) {
     
            //});

            FB.Event.subscribe('auth.statusChange', function (response) {
                if (response.status === 'connected') {

                
                    console.log('connected');

                    var accessToken = FB.getAuthResponse()['accessToken'];
                    var userId = FB.getAuthResponse()['userID'];

                    console.log('Access Token: ' + accessToken + '\n\r' + 'User ID: ' + userId);

                    //FB.api('/me', function(response) {
                    //    console.log('Good to see you, ' + response.name + '.');
                    //});

                    self.IsLoggedIn = true;

                    self.LoginInfo = {
                        AccessToken: accessToken,
                        UserId: userId
                    };

                    $scope.$apply();
                    globalInit();

                    $http.post(
                     '/Account/UpdateFacebookSessionInfo',
                     { accessToken: accessToken, userId: userId }
                 ).then(function successCallback(resp) {
                     if (resp.data != null) {
                     }
                 });

                }
            });
        },

        login: function () {
            FB.login(function(resp) {
                if (resp.authResponse) {
                    console.log('Logged into Facebook.');
                    Materialize.toast('Login Successful.', 2000, 'success');
                }
                else {
                    console.log('User cancelled login or did not fully authorize.');
                    Materialize.toast('An error occured logging into Facebook.', 2000, 'error');
                }
            });
        }
    }

}
